using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {

    public float hp = 100;
    public float weaponDamage = 25;
    public float meleeDamage = 50;
    public int decalRange = 10;
    public LayerMask mask;
    public GameObject enemyModel;
    public GameObject enemyRagdoll;
    public GameObject bulletPrefab;
    public GameObject stainPrefab;
    private WeaponSwap weapon;
    private Rigidbody rb;







    private RaycastHit _hit;




    // Use this for initialization
    void Start ()
    {
        enemyModel.SetActive(true);
        enemyRagdoll.SetActive(false);
        weapon = FindObjectOfType<WeaponSwap>();
        rb = GetComponent<Rigidbody>();
        
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        weaponDamage = weapon._damage;
		if (hp <= 0)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            enemyModel.SetActive(false);
            enemyRagdoll.SetActive(true);
        }
	}

    

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "bullet")
        {
            DealDamage(weaponDamage);
            
        }
        
        if (collision.collider.tag == "melee")
        {
            DealDamage(meleeDamage);           
            Debug.Log("colliding with melee");
        }

    }

    public void DealDamage(float weaponDamage)
    {
        hp -= weaponDamage;
    }





}
