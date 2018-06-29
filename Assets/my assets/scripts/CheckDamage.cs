using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDamage : MonoBehaviour {
    public int playerNumber;
    private Health health;

	// Use this for initialization
	void Start () {
        health = FindObjectOfType<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
      
        
            if (collision.collider.tag == "explosion")
            {
                if (playerNumber == 1)
                {
                health.ExplosionDamage(1);
                
                
                }

                if (playerNumber == 2)
                 {
                health.ExplosionDamage(2);
                                
            }

        }

            if (collision.collider.tag == "bullet")
        {
            if (playerNumber == 1)
            {
                health.TakeDamage(1);
            }
            if (playerNumber == 2)
            {
                health.TakeDamage(2);
            }
        }
        

    }
}
