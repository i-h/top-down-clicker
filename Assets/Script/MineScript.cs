using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour {

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
            Mine();
        }
    }

    void Mine()
    {
        if (PlayerData.stamina - 20 >= 0)
        {
            PlayerData.money = PlayerData.money + 5;
            PlayerData.stamina = PlayerData.stamina - 20;
            Debug.Log("Money: " + PlayerData.money + "\nStamina: " + PlayerData.stamina);
        }
    }
}
