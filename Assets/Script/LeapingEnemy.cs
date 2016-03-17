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

    void Think ()
    {
        // Check if we are within leap range
        if(currentBehaviour == BehaviourMode.GetUp)
        {

        } else if (target_dir.magnitude <= leap_distance && !leaping)
        {
            ChangeBehaviour(BehaviourMode.Leap);
        } else if (CheckRotation())
        {
            ChangeBehaviour(BehaviourMode.GetUp);
        }
        else
        {
            ChangeBehaviour(BehaviourMode.Approach);
        }
    }

    bool CheckRotation()
    {
        Vector3 angles = transform.rotation.eulerAngles;
        bool angle = Mathf.Abs(angles.x) > tip_threshold || Mathf.Abs(angles.z) > tip_threshold;


        return angle && rigidbody.velocity.magnitude <= vel_threshold;
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

        switch (currentBehaviour)
        {
            case BehaviourMode.Leap:
                leap_target = (target_dir + Vector3.up * 2) * leap_force;
                rigidbody.AddForce(leap_target, ForceMode.Impulse);
                leaping = true;
                Invoke("ResetLeap", leap_cooldown);
                ChangeBehaviour(BehaviourMode.Idle);
                break;
            case BehaviourMode.Approach:
                // Move towards player
                rigidbody.MovePosition(transform.position + target_dir.normalized * move_speed * Time.deltaTime);
                break;
            case BehaviourMode.GetUp:
                Debug.Log(name + " needs to get up!\nAngularVelocity: " + rigidbody.angularVelocity + "\tVelocity: " + rigidbody.velocity.magnitude);
                Vector3 angles = transform.rotation.eulerAngles;
                angles.x = 0;
                angles.z = 0;
                transform.rotation = Quaternion.Euler(angles);
                ChangeBehaviour(BehaviourMode.Idle);
                break;
            case BehaviourMode.Idle:
            default:

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
}
