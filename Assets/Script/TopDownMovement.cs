using UnityEngine;
using System.Collections;

public class TopDownMovement : MonoBehaviour {
    public float speed = 10.0f;
    public float jump_force = 100.0f;
    Vector3 move_vector = new Vector3();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        move_vector.x = Input.GetAxis("Horizontal");
        move_vector.z = Input.GetAxis("Vertical");

        rigidbody.MovePosition(transform.position+move_vector*speed*Time.deltaTime);
	}
}
