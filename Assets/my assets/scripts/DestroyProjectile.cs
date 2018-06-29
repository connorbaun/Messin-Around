using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour {

    public float timeTillDestruction = 3f; //set in inspector. how much time it takes before the projectile self-destructs, which will help with game optimization
    public float _damage = 10;
    public GameObject sparkParticles;
    public GameObject bloodPrefab;
    public GameObject gorePrefab;
    public GameObject bulletHolePrefab;
    private RaycastHit _hit;
    public GameObject stainPrefab;
    public float decalRange = 100f;
    public LayerMask mask;
    private EnemyDeath enemy;
    private PlayerController player;
    private Health health;


    public void Start()
    {
        enemy = FindObjectOfType<EnemyDeath>();
        player = FindObjectOfType<PlayerController>();
        health = FindObjectOfType<Health>();
    }








    private float timer = 0f; //timer will constantly increase as each bullet is spawned. once it reaches the timeTillDestruction, it self-destructs and deletes itself
    
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime; // as the game runs, time will increase by 1 every single second. this way, we can determine how many seconds we want it to take for the bullet to self-destruct


        if (timer >= timeTillDestruction) //if that time reaches the time till destruction...
        {
            Destroy(gameObject); //destroy the object attached to this script. in this case, it's the bullet object
        }
	}

    public void OnCollisionEnter(Collision collision)
    {

        Destroy(gameObject);
        if (collision.collider.tag == "ground")
        {

            GameObject bulletHole = Instantiate(bulletHolePrefab);
            bulletHole.transform.position = collision.contacts[0].point;
            bulletHole.transform.forward = collision.contacts[0].normal * -1f;

            GameObject sparks = Instantiate(sparkParticles);
            sparks.transform.position = collision.contacts[0].point;
            sparks.transform.forward = collision.contacts[0].normal;
            Destroy(gameObject);

            //gameObject.SetActive(false);

        }

        else
        {

          /*  if (collision.collider.tag == "Player")
            {
                health.Player1TakeDamage();
                Debug.Log("P1 was shot by bullet");
            }

            if (collision.collider.tag == "Player2")
            {
                health.Player2TakeDamage();
                Debug.Log("P2 was shot by bullet");
            }
            if (collision.collider.tag == "hitbox")
            {
                enemy.hp -= 100;
                Debug.Log("headshot");
                //player.Hitmarker();
                
            } 

            if (collision.collider.tag == "enemy")
            {
                //player.TorsoMarker();
            } */
            //Debug.Log("torso hit");

            if (Physics.Raycast(collision.contacts[0].point, collision.contacts[0].normal * -1f, out _hit, decalRange, mask))
            {
                GameObject decal = Instantiate(stainPrefab);
                decal.transform.position = _hit.point;
                decal.transform.forward = _hit.normal * -1f;
                decal.transform.Rotate(0, 0, UnityEngine.Random.Range(75, 140));
                Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.blue, 3f);

            }
            GameObject blood = Instantiate(bloodPrefab);
            blood.transform.position = collision.contacts[0].point;
            blood.transform.forward = collision.contacts[0].normal;

            GameObject gore = Instantiate(gorePrefab, collision.contacts[0].point, Quaternion.identity);
            gore.transform.position = collision.contacts[0].point;
            gore.transform.forward = collision.contacts[0].normal * -1f;
            Destroy(gameObject);
        }


        

        
       
       
    }

}
