using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour {
    public float money = 5.0f;
    public float stamina = 20.0f;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Mine();
        }
    }

    void Mine()
    {
        float requiredStamina = stamina / PlayerData.pickaxe_level;
        if (PlayerData.stamina - requiredStamina >= 0)
        {
            PlayerData.money = PlayerData.money + money*PlayerData.pickaxe_level;
            PlayerData.stamina = PlayerData.stamina - requiredStamina;
            //Debug.Log("Money: " + PlayerData.money + "\nStamina: " + PlayerData.stamina);
        }
    }
}
