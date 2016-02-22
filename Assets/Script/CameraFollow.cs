using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public Transform camera_position;
    public Transform look_target;
    public bool look_at_target = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = camera_position.position;
        if (look_at_target)
        {
            transform.LookAt(look_target);
        }
	}
}
