using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour
{
    public float money = 5.0f;
    public float stamina = 20.0f;
    public Transform coin;
    public float unloadSpeed = 0.01f;
    public float launchForce = 0.0f;
    Vector3 launchDir = new Vector3();
    int moneyYield = 0;
    public Transform spawnPosition;


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
            moneyYield = Mathf.RoundToInt(money * Mathf.Pow(2, PlayerData.pickaxe_level));
            if (coin)
            {
                    StartCoroutine(giveCoins(moneyYield));
            }
            else
            {
                PlayerData.money += moneyYield;
            }

            PlayerData.stamina = PlayerData.stamina - requiredStamina;
            //Debug.Log("Money: " + PlayerData.money + "\nStamina: " + PlayerData.stamina);
        }
    }

    IEnumerator giveCoins(int amount)
    {
        Vector3 spawnPos;
        int factor;
        //int totalYield = (int)Mathf.Pow(10, factor - 1);

        if (spawnPosition)
        {
            spawnPos = spawnPosition.position;
        }
        else
        {
            spawnPos = transform.position;
        }
        while (amount > 0)
        {
            factor = 1;
            while (amount > Mathf.Pow(10, factor))
            {
                factor += 1;
            }
            Debug.Log("Amount: " + amount + "\nFactor" + factor);

            Transform coin_obj = Transform.Instantiate(coin, spawnPos + Vector3.up * 2 + Random.insideUnitSphere, Random.rotation) as Transform;
            launchDir.x = Random.insideUnitCircle.x;
            launchDir.z = Random.insideUnitCircle.y;
            launchDir.y = 1.0f*factor;
            coin_obj.localScale = Vector3.one*factor/2.0f;
            coin_obj.GetComponent<Rigidbody>().mass = factor;
            coin_obj.GetComponent<Rigidbody>().AddForce((launchDir.normalized*10*coin_obj.GetComponent<Rigidbody>().mass+transform.forward)*launchForce, ForceMode.Impulse);

            coin_obj.name = "t" + factor + " money";

            MoneyCoin coin_script = coin_obj.GetComponent<MoneyCoin>();
            coin_script.moneyYield = Mathf.Pow(10, factor-1);
            amount -= (int)Mathf.Pow(10, factor-1);

            yield return new WaitForSeconds(unloadSpeed);
        }
    }
}
