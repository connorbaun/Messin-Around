using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour {
    public Health health;


	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            health.Death(1);
        }

        if (collision.collider.tag == "Player2")
        {
            health.Death(2);
        }
    }
}
