using UnityEngine;
using System.Collections;

public class StaminaRegeneration : MonoBehaviour {
    public float regen_amount = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerData.stamina < PlayerData.max_stamina)
        {
            PlayerData.stamina += regen_amount * Time.deltaTime;
        }
	}
}
