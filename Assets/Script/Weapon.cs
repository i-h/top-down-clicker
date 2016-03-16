using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    //public WeaponAnimation anim = new WeaponAnimation();
    public bool damaging = false;
    public bool attacking = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision c)
    {
        if (damaging)
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                c.gameObject.GetComponent<Enemy>().Die();
            }
        }
    }
    public void Attack()
    {
        animation.Play("swing");
    }
}
[System.Serializable]
public struct WeaponAnimation
{
    public float duration;
    public AnimationCurve rot_x;
    public AnimationCurve rot_y;
    public AnimationCurve rot_z;
    public AnimationCurve pos_x;
    public AnimationCurve pos_y;
    public AnimationCurve pos_z;
    public AnimationCurve collisionOn;
}
