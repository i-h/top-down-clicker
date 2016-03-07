using UnityEngine;
using System.Collections;

public class MoneyCoin : MonoBehaviour {
    public float moneyYield = 1.0f;
    void OnTriggerEnter(Collider c)
    {
        Debug.Log(c.tag);
        if (c.gameObject.CompareTag("Player"))
        {
            PlayerData.money += moneyYield;
            Destroy(gameObject);
        }
    }
}
