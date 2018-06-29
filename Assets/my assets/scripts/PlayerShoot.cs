using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {

    public Camera cam; //attach in inspector. a reference to the camera attached to our player. bullets will spawn from center of screen if we use Raycast
    public int currentClipAmmo;
    public int currentPocketAmmo;
    public int maxClipAmmo;
    public int maxPocketAmmo;
    public Text clipAmmoUI;
    public Text pocketAmmoUI;
    public LayerMask mask; // set in inspector. the mask which determines what surfaces our raycasts can collide with
    public Animator anim; //a ref to the animator attached to the weapon holder. set in inspector
    public GameObject bulletSpawner; //attach in inspector. this empty object is placed at the edge of our gun object and spawns the bullet objects
    public GameObject bulletTarget;
    public GameObject muzzleFlashParticles;
    public GameObject bulletPrefab; //attach in inspector. this prefab object is spawned every time we pull the trigger
    public GameObject reloadPrompt;
    public GameObject meleeTarget;
    public WeaponSwap weapon;
    public EnemyDeath enemy;
    public PlayerController controller;
    public float bulletForce; //set in inspector. how much force should the prefab be fired with?
    public float semiRate = 0.2f;
    public float autoFireRate = 0.2f; //set in inspector. how long between shots when using fully-automatic gunfire?
    public float burstRate = 0.2f;
    public float timeBetweenBullets = 0.1f; //set in inspector. how long between shots when using burst-fire
    public float burstSize = 3; //set in inspector. how many shots per burst?
    public float reloadTime = 0.8f;
    public float meleeTime = 1.5f; //this number refers to the length of time we must wait before being able to shoot again once beginning the melee animation
    public float bulletSpread = 0.01f;
    public float minBulletSpread = 0.0f;
    public float maxBulletSpread = 0.1f;
    public float spreadIncrement = 0.05f;
    public float spreadShrink = 0.05f;
    public float meleeTimer = 0f;
    public float meleeActivate = 0.1f;


    private RaycastHit _hit; //the variable which stores data about the object hit by our raycast in hitscan shooting
    private AmmoAuthority ammo;
    private bool isShooting = false;
    private bool isMeleeing = false;
    public bool canFire = true;
    private float semiTimer = 0f;
    private float timer = 0f; //this timer will increase between shots, so that we can determine when to spawn the next bullet prefab
    private float burstTimer = 0f;
    private Health health;
    




    public void Start()
    {
        burstTimer = burstRate; //make sure we are able to fire at spawn with our burst weapon.
        semiTimer = semiRate; //make sure we are able to fire at spawn with our semi weapon
        //ammo = FindObjectOfType<AmmoAuthority>();
        bulletSpread = minBulletSpread;
        //meleeTarget.SetActive(false);
        meleeTimer = 0;
        currentClipAmmo = maxClipAmmo;
        currentPocketAmmo = maxPocketAmmo;
        health = FindObjectOfType<Health>();
    }

    public void OnEnable()
    {
        currentClipAmmo = maxClipAmmo;
        currentPocketAmmo = maxPocketAmmo;
    }

    public void Update()
    {
        AmmoHandler(); //constantly call the AmmoManager function
        CheckForReload();
        DrawText();
        MeleeHandler(); //constantly call the MeleeManager function
        RunWeaponTimers();
        isShooting = false;
  

        if (bulletSpread > minBulletSpread)
        {
            ResetBulletSpread();
        }

        if (health.hp1 <= 0)
        {
            currentClipAmmo = maxClipAmmo;
            currentPocketAmmo = maxPocketAmmo;
        }



        
            if (Input.GetButtonDown("Fire2")) //if the player pushes whatever button we labeled as "FIRE1" in the Input Manager...
            {
                if (canFire)
                    //shoot.HitscanShoot(); //call the hitscan shoot function inside the PlayerShoot script

                    if (weapon.currentWeapon == 0)
                    {
                        if (semiTimer >= semiRate)
                        {
                            SemiPhysicsShoot(); //call the physics shoot function inside the PlayerShoot script
                            semiTimer = 0; //immediately reset semitimer so that we cannot fire faster than we want you to.
                        }

                    }
                if (canFire)
                    if (weapon.currentWeapon == 2)
                    {
                        if (burstTimer >= burstRate)
                        {

                            StartCoroutine(BurstPhysicsShoot()); //call the burstfire shoot function inside the player shoot script. coroutine allows us to time our bullet spawns better
                            burstTimer = 0; //immediately reset burstimer so that we cannot fire faster than we want you to. 
                        }

                    }


            }


            if (Input.GetButton("Fire2"))
            {
                if (canFire)
                    if (weapon.currentWeapon == 1)
                    {

                        AutoPhysicsShoot(); //call the automatic fire shoot function inside the PlayerShoot script
                    }
            }


        







        //tell animator when to activate firing animation and when not to
        if (isShooting == true)
        {
            anim.SetBool("Firing", true);
        }
        else if (isShooting == false)
        {
            anim.SetBool("Firing", false);
        }
        
        if (isMeleeing == true)
        {
            meleeTarget.SetActive(true);
        }
        else if (isMeleeing == false)
        {
            meleeTarget.SetActive(false);
        }
    }




    public void HitscanShoot() //this function spawns an invisible raycast "bullet" from the center of the camera in the direction our camera is facing
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, 100f, mask)) 
        {
            Debug.Log(_hit.collider.name); //for now, it just console-prints out the name of the object you hit. later, you would insert any damage script or whatever instead
        }
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
            currentClipAmmo -= 1;
        } else if (anim.GetBool("Scoped") == false)
        {

            GameObject newBullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, transform.rotation); //so first, we must tell unity to create this new prefab where we want it
            Vector3 bulletDirection = (bulletSpawner.transform.forward + bulletSpawner.transform.up * Random.Range(-bulletSpread, bulletSpread) + bulletSpawner.transform.right * Random.Range(-bulletSpread, bulletSpread)).normalized; //next, we create a vector3 which we will use to match the bullet's direction with that of the bullet spawner object
            newBullet.transform.up = bulletDirection; //next, we set the bullet object's up direction to match the bullet direction of the spawner, as we set above...
            newBullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletForce); //finally, we reference the bullet's rigidbody, and apply force in the desired direction, which fires the bullet.
            currentClipAmmo -= 1;

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

    public void AutoPhysicsShoot()
    {
        isShooting = true;

        if (timer >= autoFireRate)
        {

        timer = 0;

          if (anim.GetBool("Scoped") == true)
            {
                GameObject newBullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, transform.rotation); //so first, we must tell unity to create this new prefab where we want it
                Vector3 bulletDirection = bulletSpawner.transform.forward;
                newBullet.transform.up = bulletDirection; //next, we set the bullet object's up direction to match the bullet direction of the spawner, as we set above...
                newBullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletForce); //finally, we reference the bullet's rigidbody, and apply force in the desired direction, which fires the bullet.
                currentClipAmmo -= 1;
            } else if (anim.GetBool("Scoped") == false)
            {

                GameObject newBullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, transform.rotation); //so first, we must tell unity to create this new prefab where we want it
                Vector3 bulletDirection = (bulletSpawner.transform.forward + bulletSpawner.transform.up * Random.Range(-bulletSpread, bulletSpread) + bulletSpawner.transform.right * Random.Range(-bulletSpread, bulletSpread)).normalized; //next, we create a vector3 which we will use to match the bullet's direction with that of the bullet spawner object
                newBullet.transform.up = bulletDirection;
                newBullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletForce); //finally, we reference the bullet's rigidbody, and apply force in the desired direction, which fires the bullet.
                currentClipAmmo -= 1;

                if (bulletSpread < maxBulletSpread)
                {
                    bulletSpread += spreadIncrement;
                    if (bulletSpread > maxBulletSpread)
                    {
                        bulletSpread = maxBulletSpread;
                    }
                }
            }

            
        } else if (timer < autoFireRate)
        {
            timer += Time.deltaTime;
            
        }
        
    }

    public IEnumerator BurstPhysicsShoot()
    {
        isShooting = true;

        if (anim.GetBool("Scoped")== true)
        {
            for (int i = 0; i < burstSize; i++)
            {

                GameObject newBullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation); //so first, we must tell unity to create this new prefab where we want it
                Vector3 bulletDirection = bulletSpawner.transform.forward; //next, we create a vector3 which we will use to match the bullet's direction with that of the bullet spawner object
                newBullet.transform.up = bulletDirection; //next, we set the bullet object's up direction to match the bullet direction of the spawner, as we set above...
                newBullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletForce); //finally, we reference the bullet's rigidbody, and apply force in the desired direction, which fires the bullet.
                yield return new WaitForSeconds(timeBetweenBullets);
                currentClipAmmo -= 1;
            }

        } else if (anim.GetBool("Scoped")== false)
        {
            for (int i = 0; i < burstSize; i++)
            {

                GameObject newBullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation); //so first, we must tell unity to create this new prefab where we want it
                Vector3 bulletDirection = (bulletSpawner.transform.forward + bulletSpawner.transform.up * Random.Range(-bulletSpread, bulletSpread) + bulletSpawner.transform.right * Random.Range(-bulletSpread, bulletSpread)).normalized; //next, we create a vector3 which we will use to match the bullet's direction with that of the bullet spawner object
                newBullet.transform.up = bulletDirection; //next, we set the bullet object's up direction to match the bullet direction of the spawner, as we set above...
                newBullet.GetComponent<Rigidbody>().AddForce(bulletDirection * bulletForce); //finally, we reference the bullet's rigidbody, and apply force in the desired direction, which fires the bullet.
                yield return new WaitForSeconds(timeBetweenBullets);
                currentClipAmmo -= 1;

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

    public void AmmoHandler()
    {
        if (currentClipAmmo < maxClipAmmo && currentPocketAmmo > 0)
        {

            
                if (Input.GetButtonDown("Reload"))
                {

                    StartCoroutine(Reload());
                    anim.Play("Reloading");
                }
            


        }

        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reloading"))
        {
            controller.reticule.SetActive(false);
            canFire = false;
            weapon.canSwitch = false;
            anim.SetBool("Reloading", true);
            

        }
        else
        {
            canFire = true;
            weapon.canSwitch = true;
            anim.SetBool("Reloading", false);
        }
        
        if (currentClipAmmo <= 0)
        {
            currentClipAmmo = 0;
            canFire = false;
        }
    }

    public void MeleeHandler()
    {

        {
            if (Input.GetButtonDown("Melee") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Reloading")) //if at anypoint we push melee
            {
                anim.Play("Meleeing"); //play the melee animation
            }
        }


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Meleeing")) //if we are in the midst of a melee animation
        {
            meleeTimer += Time.deltaTime;
            Debug.Log(meleeTimer);
            controller.reticule.SetActive(false);
            canFire = false; //we cant shoot
            weapon.canSwitch = false; //we cant switch weapons either
        }

    }

    public void RunWeaponTimers()
    {
        burstTimer += Time.deltaTime; //increase the bursttimer constantly so that we are reloading our canShoot timer constantly
        semiTimer += Time.deltaTime; //increase semi timer constantly so that we are reloading our canShoot timer constantly
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);

        int takeAway = maxClipAmmo - currentClipAmmo;
        currentPocketAmmo -= takeAway;
        currentClipAmmo += takeAway;
        if (currentPocketAmmo < 0)
        {
            currentPocketAmmo = 0;
        }
    }

    public void ResetBulletSpread()
    {
        bulletSpread -= spreadShrink * Time.deltaTime;

    }

    public void CheckForReload()
    {
        if (currentClipAmmo < maxClipAmmo / 3)
        {
            reloadPrompt.SetActive(true);
        }else
        {
            reloadPrompt.SetActive(false);
        }
    }

    public void DrawText()
    {
        clipAmmoUI.text = currentClipAmmo.ToString();
        pocketAmmoUI.text = currentPocketAmmo.ToString();
    }


    
}
