using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public Text levelText;
    public Text moneyText;
    public Text staminaText;
    public Text healthText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        levelText.text = "Level: " + PlayerData.pickaxe_level;
        moneyText.text = "Money: " + PlayerData.money;
        staminaText.text = "Stamina: " + PlayerData.stamina;
        healthText.text = "HP: " + PlayerData.health;
	}
}
