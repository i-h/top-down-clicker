using UnityEngine;
using System.Collections;

public class MoneyCoin : MonoBehaviour {
    public float grabDistance = 2.0f;
    public float moneyYield = 1.0f;
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }
    void PickUp()
    {
            PlayerData.money += moneyYield;
            Destroy(gameObject);
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            PickUp();
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)){
            Vector3 tgt = PlayerData.instance.position - transform.position;
            rigidbody.AddForce(tgt.normalized * 20);
        }
    }

}