using UnityEngine;
using System.Collections;

public class ShopScript : MonoBehaviour {
    public float firstLevelUp = 50.0f;
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
            float requiredMoney;
            //requiredMoney = firstLevelUp * PlayerData.pickaxe_level;
            requiredMoney = firstLevelUp * Mathf.Pow(2, PlayerData.pickaxe_level - 1);
            Debug.Log(requiredMoney);

            // If the player can afford the level up
            if (PlayerData.money >= requiredMoney)
            {
                PlayerData.pickaxe_level++;
                PlayerData.money -= requiredMoney;
            }
        }
    }
}
