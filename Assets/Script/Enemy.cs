using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform itemDrop;
    public int dropAmount = 1;
    public int health = 10;

	// Use this for initialization
    void Start()
    {
        tag = "Enemy";
        name = "Enemy " + GetHashCode();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        StartCoroutine(giveCoins(dropAmount));

    }
    IEnumerator giveCoins(int amount)
    {
        Vector3 launchDir = new Vector3();
        float launchForce = 0.5f;
        Vector3 spawnPos;
        int factor;
        //int totalYield = (int)Mathf.Pow(10, factor - 1);
            spawnPos = transform.position;
        while (amount > 0)
        {
            factor = 1;
            while (amount > Mathf.Pow(10, factor))
            {
                factor += 1;
            }
            Debug.Log("Amount: " + amount + "\nFactor" + factor);

            Transform coin_obj = Transform.Instantiate(itemDrop, spawnPos + Vector3.up * 2 + Random.insideUnitSphere, Random.rotation) as Transform;
            launchDir.x = Random.insideUnitCircle.x;
            launchDir.z = Random.insideUnitCircle.y;
            launchDir.y = 1.0f * factor;
            coin_obj.localScale = Vector3.one * factor / 2.0f;
            coin_obj.rigidbody.mass = factor;
            coin_obj.rigidbody.AddForce((launchDir.normalized * 10 * coin_obj.rigidbody.mass + transform.forward) * launchForce, ForceMode.Impulse);

            coin_obj.name = "t" + factor + " money";

            MoneyCoin coin_script = coin_obj.GetComponent<MoneyCoin>();
            coin_script.moneyYield = Mathf.Pow(10, factor - 1);
            amount -= (int)Mathf.Pow(10, factor - 1);

            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
