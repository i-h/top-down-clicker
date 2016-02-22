using UnityEngine;
using System.Collections;

public class BedScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerData.stamina = PlayerData.max_stamina;
        }
    }
}
