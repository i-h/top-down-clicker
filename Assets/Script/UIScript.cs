using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public Text levelText;
    public Text moneyText;
    public Text staminaText;

    public Image staminaBar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        levelText.text = "Level: " + PlayerData.pickaxe_level;
        moneyText.text = "Money: " + PlayerData.money;
        staminaText.text = "Stamina: " +  PlayerData.stamina.ToString("f1");
        //staminaText.text = "Stamina: " + Mathf.Round(PlayerData.stamina);
        staminaBar.fillAmount = PlayerData.stamina / PlayerData.max_stamina;
	}
}
