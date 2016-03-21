using UnityEngine;
using System.Collections;

public class LeapingEnemy : MonoBehaviour {
    public float move_speed = 1.0f;
    public float leap_distance = 5.0f;
    public float leap_cooldown = 2.0f;
    public float leap_force = 1.0f;
    public int damage_amount = 5;
    public float think_delay = 1.0f;
    public float tip_threshold = 45.0f;
    public float vel_threshold = 0.2f;
    
    enum BehaviourMode { Idle, Approach, Leap, GetUp }

    BehaviourMode currentBehaviour = BehaviourMode.Idle;
    bool leaping = false;
    Vector3 leap_target;
    Transform player;
    Vector3 target_dir;
    const float think_variation = 0.2f;

    GetUpProcedure getup;

    void Think ()
    {
        if (IsFallen())
        {
            ChangeBehaviour(BehaviourMode.GetUp);
        }
        // Check if we are within leap range
        if (currentBehaviour == BehaviourMode.GetUp)
        {
            return;
        }
        else if (target_dir.magnitude <= leap_distance && !leaping)
        {
            ChangeBehaviour(BehaviourMode.Leap);
        }
        else
        {
            ChangeBehaviour(BehaviourMode.Approach);
        }
    }

    bool IsFallen()
    {
        Vector3 angles = transform.rotation.eulerAngles;
        bool angle_x = angles.x > tip_threshold && angles.x < 360 - tip_threshold;
        bool angle_z = angles.z > tip_threshold && angles.z < 360 - tip_threshold;
        bool angle = angle_x || angle_z;

        return angle && rigidbody.angularVelocity.magnitude <= vel_threshold;
    }

    void ChangeBehaviour(BehaviourMode newMode)
    {
        if (currentBehaviour != newMode)
        {
            currentBehaviour = newMode;
        }
    }

	// Use this for initialization
	void Start () {
        player = PlayerData.instance;
        if (!player)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        think_delay = think_delay + think_delay * think_variation * Random.Range(-1.0f, 1.0f);
        InvokeRepeating("Think", think_delay, think_delay);
	}
	
	// Update is called once per frame
	void Update ()
    {
        target_dir = player.position - transform.position;
        if (IsFallen())
        {
            ChangeBehaviour(BehaviourMode.GetUp);
        }
        switch (currentBehaviour)
        {
            case BehaviourMode.Leap:
                renderer.material.color = Color.red;
                leap_target = (target_dir + Vector3.up * 2) * leap_force;
                rigidbody.AddForce(leap_target*rigidbody.mass, ForceMode.Impulse);
                leaping = true;
                Invoke("ResetLeap", leap_cooldown);
                ChangeBehaviour(BehaviourMode.Idle);
                break;
            case BehaviourMode.Approach:
                renderer.material.color = Color.green;
                // Move towards player
                rigidbody.MovePosition(transform.position + target_dir.normalized * move_speed * Time.deltaTime);
                Vector3 look_dir = transform.position;
                look_dir.x += target_dir.x;
                look_dir.z += target_dir.z;
                transform.LookAt(look_dir);
                break;
            case BehaviourMode.GetUp:
                //Debug.Log(name + " needs to get up!\nAngularVelocity: " + rigidbody.angularVelocity + "\tVelocity: " + rigidbody.velocity.magnitude);
                renderer.material.color = Color.yellow;
                if (getup.Equals(null)) {
                    getup = new GetUpProcedure();
                } else if (!getup.started)
                {
                    Vector3 angles = transform.rotation.eulerAngles;
                    angles.x = 0;
                    angles.z = 0;

                    getup.started = true;
                    getup.start_time = Time.time;
                    getup.start_angle = transform.rotation;
                    getup.target_angle = Quaternion.Euler(angles);
                    //rigidbody.AddForce(Vector3.up * 2, ForceMode.Impulse);
                } else if (getup.progress < 1)
                {
                    getup.progress = (Time.time - getup.start_time) / 1.0f;

                    transform.rotation = Quaternion.Slerp(getup.start_angle, getup.target_angle, getup.progress);
                } else
                {
                    transform.rotation = getup.target_angle;
                    transform.rigidbody.angularVelocity = Vector3.zero;
                    ChangeBehaviour(BehaviourMode.Idle);
                    getup = new GetUpProcedure();
                }
                
                break;
            case BehaviourMode.Idle:
            default:
                renderer.material.color = Color.white;

                break;
        }
    }
    void OnCollisionEnter(Collision c)
    {
        if (leaping && c.gameObject.CompareTag("Player"))
        {
            PlayerData plr_script = c.gameObject.GetComponent<PlayerData>();
            plr_script.TakeDamage(damage_amount);
        }
    }
    void ResetLeap()
    {
        leaping = false;
    }
    struct GetUpProcedure
    {
        public bool started;
        public float start_time;
        public float progress;
        public Quaternion start_angle;
        public Quaternion target_angle;
    }
}
