using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headshot : MonoBehaviour {
    public float headShotDamage = 100;
    public EnemyDeath enemy;
    

	// Use this for initialization
	void Start ()
    {
        enemy = GetComponent<EnemyDeath>();
	}

    public void DealHeadshotDamage()
    {
        enemy.hp -= headShotDamage;
    }


}
