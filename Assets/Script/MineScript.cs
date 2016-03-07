using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour {
    public static float staminaConsumption = 20.0f;
    public static float yield = 5.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            Mine();
        }
    }

    void Mine()
    {
        if (PlayerData.stamina - GetStaminaConsumption() >= 0)
        {
            YieldResources();
            ConsumeStamina();
            Debug.Log("Money: " + PlayerData.money + "\nStamina: " + PlayerData.stamina);
        }
    }

    void ConsumeStamina()
    {
        PlayerData.stamina = PlayerData.stamina - GetStaminaConsumption();
    }
    void YieldResources()
    {
        PlayerData.money = PlayerData.money + GetResourceYield();

    }
    public static float GetStaminaConsumption()
    {
        return staminaConsumption / PlayerData.pickaxe_level;
    }
    public static float GetResourceYield()
    {
        return 5.0f * PlayerData.pickaxe_level;
    }
}
