using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public Transform enemyPrefab;
    public float spawnDelay = 4.0f;
	// Use this for initialization
	void Start () {
        Invoke("Spawn", spawnDelay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Spawn()
    {
        if (enemyPrefab)
        {
            Transform enemy_obj = Transform.Instantiate(enemyPrefab, transform.position + Random.insideUnitSphere + Vector3.up, Quaternion.identity) as Transform;
            Enemy enemy_script = enemy_obj.GetComponent<Enemy>();
            enemy_script.health *= PlayerData.pickaxe_level;
            enemy_script.dropAmount = enemy_script.health * 2;
            Invoke("Spawn", spawnDelay * PlayerData.pickaxe_level);
        }
    }
}
