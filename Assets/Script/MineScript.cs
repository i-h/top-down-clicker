using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour
{
    public float money = 5.0f;
    public float stamina = 20.0f;
    public Transform coin;
    Vector3 launchDir = new Vector3();
    int moneyYield = 0;
    bool running = false;


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
            if (coin)
            {
                moneyYield += Mathf.RoundToInt(money * PlayerData.pickaxe_level);
                if (!running)
                {
                    StartCoroutine(giveCoins());
                }
            }
            else
            {
                PlayerData.money = PlayerData.money + money * PlayerData.pickaxe_level;
            }

            PlayerData.stamina = PlayerData.stamina - requiredStamina;
            //Debug.Log("Money: " + PlayerData.money + "\nStamina: " + PlayerData.stamina);
        }
    }

    IEnumerator giveCoins()
    {
        running = true;
        for (int i = 0; i < moneyYield; i++)
        {
            Transform coin_obj = Transform.Instantiate(coin, transform.position + Vector3.up * 2 + Random.insideUnitSphere, Quaternion.identity) as Transform;
            launchDir.x = Random.insideUnitCircle.x;
            launchDir.z = Random.insideUnitCircle.y;
            launchDir.y = 1.0f;
            coin_obj.rigidbody.AddForce(launchDir.normalized*10, ForceMode.Impulse);
            yield return new WaitForSeconds(10/moneyYield);
        }
        running = false;
    }
}
