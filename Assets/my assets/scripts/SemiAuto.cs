using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAuto : MonoBehaviour {
    public PlayerController controller;
    public GameObject bulletSpawner; //attach in inspector. this empty object is placed at the edge of our gun object and spawns the bullet objects
    public GameObject bulletPrefab; //attach in inspector. this prefab object is spawned every time we pull the trigger
    public float bulletForce; //set in inspector. how much force should the prefab be fired with?
    public float semiRate = 0.2f;
    public float reloadTime = 0.8f;
    public float bulletSpread = 0.01f;
    public float minBulletSpread = 0.0f;
    public float maxBulletSpread = 0.1f;
    public float spreadIncrement = 0.05f;
    public float spreadShrink = 0.05f;
    public float meleeTimer = 0f;
    public float meleeActivate = 0.1f;
    public Animator anim;



    private RaycastHit _hit; //the variable which stores data about the object hit by our raycast in hitscan shooting
    private AmmoManager ammo;
    private bool isShooting = false;
    private bool isMeleeing = false;
    public bool canFire = true;
    private float semiTimer = 0f;
    private float timer = 0f; //this timer will increase between shots, so that we can determine when to spawn the next bullet prefab


    // Use this for initialization
    void Start ()
    {
        ammo = GetComponentInChildren<AmmoManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SemiPhysicsShoot() //this function spawns an actual 3d physics bullet prefab and pushes it out of an invisible object called bullet spawner attached to the camera
    {
        isShooting = true;

        if (anim.GetBool("Scoped") == true)
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, transform.rotation); //so first, we must tell unity to create this new prefab where we want it
            Vector3 bulletDirection = bulletSpawner.transform.forward;
            newBullet.transform.up = bulletDirection; //next, we set the bullet object's up direction to match the bullet direction of the spawner, as we set above...
            newBullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletForce); //finally, we reference the bullet's rigidbody, and apply force in the desired direction, which fires the bullet.

        }
        else if (anim.GetBool("Scoped") == false)
        {

            GameObject newBullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, transform.rotation); //so first, we must tell unity to create this new prefab where we want it
            Vector3 bulletDirection = (bulletSpawner.transform.forward + bulletSpawner.transform.up * Random.Range(-bulletSpread, bulletSpread) + bulletSpawner.transform.right * Random.Range(-bulletSpread, bulletSpread)).normalized; //next, we create a vector3 which we will use to match the bullet's direction with that of the bullet spawner object
            newBullet.transform.up = bulletDirection; //next, we set the bullet object's up direction to match the bullet direction of the spawner, as we set above...
            newBullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletForce); //finally, we reference the bullet's rigidbody, and apply force in the desired direction, which fires the bullet.


            if (bulletSpread < maxBulletSpread)
            {
                bulletSpread += spreadIncrement;
                if (bulletSpread > maxBulletSpread)
                {
                    bulletSpread = maxBulletSpread;
                }
            }


        }


    }
}
