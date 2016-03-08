using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour
{
    public float money = 5.0f;
    public float stamina = 20.0f;
    public Transform coin;
    public float unloadSpeed = 0.01f;
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
                    StartCoroutine(giveCoins(moneyYield));
            }
            else
            {
                PlayerData.money = PlayerData.money + money * PlayerData.pickaxe_level;
            }

            PlayerData.stamina = PlayerData.stamina - requiredStamina;
            //Debug.Log("Money: " + PlayerData.money + "\nStamina: " + PlayerData.stamina);
        }
    }

    IEnumerator giveCoins(int amount)
    {
        running = true;
        int depth = 0;
        ArrayList amounts = new ArrayList();
        int factor = 1;
        int totalYield = (int)Mathf.Pow(10, factor - 1);

        while (amount / Mathf.Pow(10, depth) < 1)
        {
            amounts[depth] = amount % Mathf.Pow(10, depth);
            depth++;
        }
        Debug.Log("Depth: " + depth + "\n" + amounts.Count);

        while (amount > 0)
        {

            Transform coin_obj = Transform.Instantiate(coin, transform.position + Vector3.up * 2 + Random.insideUnitSphere, Random.rotation) as Transform;
            launchDir.x = Random.insideUnitCircle.x;
            launchDir.z = Random.insideUnitCircle.y;
            launchDir.y = 1.0f*factor;
            coin_obj.localScale *= factor;
            coin_obj.rigidbody.AddForce(launchDir.normalized*10, ForceMode.Impulse);

            MoneyCoin coin_script = coin_obj.GetComponent<MoneyCoin>();
            coin_script.moneyYield = totalYield*100;
            amount -= totalYield;

            yield return new WaitForSeconds(unloadSpeed*factor);
        }
        running = false;
    }
}
