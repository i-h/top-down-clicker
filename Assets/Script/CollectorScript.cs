using UnityEngine;
using System.Collections;

public class CollectorScript : MonoBehaviour {
    SphereCollider col;

    void Start()
    {
        col = GetComponent<SphereCollider>();
    }
	// Update is called once per frame
	void FixedUpdate () {
        col.radius = 1.0f * PlayerData.pickaxe_level;
	}
}
