using UnityEngine;
using System.Collections;

public class MoneyCoin : MonoBehaviour {
    public float grabDistance = 2.0f;
    public float moneyYield = 1.0f;
    public float returnSpeed = 10.0f;
    public float lifeTime = 5.0f;
    bool returnMode = false;
    float startTime;
    void Start()
    {
        startTime = Time.time;
    }
    void OnTriggerEnter(Collider c)
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
            //PickUp();
        }
    }
    void Update()
    {
        Vector3 tgt = PlayerData.instance.position - transform.position;
        if (!returnMode && (Input.GetKeyDown(KeyCode.Space) || Time.time - startTime > lifeTime || tgt.magnitude < grabDistance))
        {
            returnMode = true;
            rigidbody.useGravity = false;
            collider.enabled = false;
        }
        if (returnMode){
            
            //rigidbody.AddForce((tgt+transform.right*2).normalized * 50 * rigidbody.mass);
            rigidbody.velocity = tgt.normalized*returnSpeed + tgt*returnSpeed/10;
        }
    }

}