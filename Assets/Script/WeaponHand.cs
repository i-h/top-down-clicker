using UnityEngine;
using System.Collections;

public class WeaponHand : MonoBehaviour {
    public Weapon equipped;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            equipped.Attack();
        }
	}
}
