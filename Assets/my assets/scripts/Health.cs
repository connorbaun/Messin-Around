using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public float hp1 = 100;
    public float hp2 = 100;

    public float respawnTime = 3f;

    public bool p1Dead = false;
    public bool p2Dead = false;
    public GameObject[] spawnpoints = new GameObject[5];

    public PlayerController controller1; 
    public GameObject p1PlayerModel;
    public GameObject p1Ragdoll;
    public GameObject p1CorspeSpawn;
    public GameObject p1WeaponHolder;
    public Rigidbody p1RB;
    public GameObject p1Canvas;
    public GameObject p1GameplayUI;
    public Text p1HealthUI;
    public Text p2HealthUI;
    public Score score;

    public PlayerController2 controller2;
    public GameObject p2PlayerModel;
    public GameObject p2Ragdoll;
    public GameObject p2CorspeSpawn;
    public GameObject p2WeaponHolder;
    public Rigidbody p2RB;
    public GameObject p2Canvas;
    public GameObject p2GameplayUI;

    public GameObject ragdoll;

    private WeaponSwap weapon1;
    private WeaponSwap2 weapon2;
    private AmmoManager ammo1;
    private AmmoManager2 ammo2;
    private GrenadeAmmo grenades;
    




	// Use this for initialization
	void Start ()
    {

        hp1 = 100;
        hp2 = 100;

        p1PlayerModel.SetActive(true);
        p1Ragdoll.SetActive(false);

        p2PlayerModel.SetActive(true);
        p2Ragdoll.SetActive(false);

        weapon1 = FindObjectOfType<WeaponSwap>();
        weapon2 = FindObjectOfType<WeaponSwap2>();
        ammo1 = FindObjectOfType<AmmoManager>();
        ammo2 = FindObjectOfType<AmmoManager2>();
        grenades = FindObjectOfType<GrenadeAmmo>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        p1HealthUI.text = "Health " + hp1;
        p2HealthUI.text = "Health " + hp2;
	}

    public void TakeDamage(int playerNumber )
    {
        if (playerNumber == 1)
        {
             
            hp1 -= weapon2._damage;
            if (hp1 <= 0)
            {
                Death(1);
                SpawnCorspe(1);
            }
        }

        if (playerNumber == 2)
        {
            hp2 -= weapon1._damage;
            if (hp2 <= 0)
            {
                Death(2);
                SpawnCorspe(2);
            }
        }
 
    }

    public void ExplosionDamage(int playerNumber)
    {
        if (playerNumber == 1)
        {
            Death(1);
            SpawnCorspe(1);
        }
        if (playerNumber == 2)
        {
            Death(2);
            SpawnCorspe(2);
        }
    }


    public void Death(int playerNumber)
    {
        if (playerNumber == 1)
        {
            score.AddScore(2);
            p1GameplayUI.SetActive(false);
            p1PlayerModel.SetActive(false);
            //p1Ragdoll.SetActive(true);
            p1WeaponHolder.SetActive(false);
            p1RB.isKinematic = true;
            p1RB.constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(Respawn(1));

        }

        if (playerNumber == 2)
        {
            score.AddScore(1);
            p2GameplayUI.SetActive(false);
            p2PlayerModel.SetActive(false);
            //p2Ragdoll.SetActive(true);
            p2WeaponHolder.SetActive(false);
            p2RB.isKinematic = true;
            p2RB.constraints = RigidbodyConstraints.FreezeAll;
            StartCoroutine(Respawn(2));
        }

    }


    public void SpawnCorspe(int playerNumber)
    {
        if (playerNumber == 1)
        {
            GameObject corpse = Instantiate(ragdoll, p1CorspeSpawn.transform.position, controller1.transform.rotation);
        }

        if (playerNumber == 2)
        {
            GameObject corpse = Instantiate(ragdoll, p2CorspeSpawn.transform.position, controller2.transform.rotation);
        }
    }

    public IEnumerator Respawn(int playerNumber)
    {
        if (playerNumber == 1)
        {
            
            yield return new WaitForSeconds(respawnTime);
            grenades.p1Grenades = grenades.maxGrenades;
            ammo1.AmmoRefill();
            p1GameplayUI.SetActive(true);
            controller1.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;
            hp1 = 100;
            p1PlayerModel.SetActive(true);
            //p1Ragdoll.SetActive(true);
            p1WeaponHolder.SetActive(true);
            p1RB.isKinematic = false;
            p1RB.constraints = RigidbodyConstraints.None;
            p1RB.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (playerNumber == 2)
        {
            yield return new WaitForSeconds(respawnTime);
            grenades.p2Grenades = grenades.maxGrenades;
            ammo2.AmmoRefill();
            p2GameplayUI.SetActive(true);
            controller2.transform.position = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;
            hp2 = 100;
            p2PlayerModel.SetActive(true);
            //p1Ragdoll.SetActive(true);
            p2WeaponHolder.SetActive(true);
            p2RB.isKinematic = false;
            p2RB.constraints = RigidbodyConstraints.None;
            p2RB.constraints = RigidbodyConstraints.FreezeRotation;
        }

    }

}
