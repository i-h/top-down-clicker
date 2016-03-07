using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {
    public static float money = 0;
    public static float stamina = 100;
    public static float max_stamina = 100;
    public static int pickaxe_level = 1;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void LevelUp()
    {
        float nextLevel = GetReqForNextLevel(pickaxe_level);
        Debug.Log("Trying to upgrade pickaxe. Money required: " + nextLevel);
        if (money >= nextLevel)
        {
            pickaxe_level++;
            money -= nextLevel;
        }
        Debug.Log("Level now " + pickaxe_level);
    }
    public static float GetReqForNextLevel(int lvl_cur)
    {
        float nextLevel = 0.0f;
        float init_req = 50;

        nextLevel = init_req * Mathf.Pow(2, lvl_cur-1);

        return nextLevel;
    }
}
