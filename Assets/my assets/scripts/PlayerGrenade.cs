using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour {

    public GameObject grenadeSpawner; //attach in inspector. invisible empty game object which will spawn the grenades
    public GameObject grenadePrefab;
    public float throwForce = 500f; //set in inspector. how hard are we going to throw the grenade?


    public void ThrowGrenade()
    {
        GameObject newgrenade = Instantiate(grenadePrefab, grenadeSpawner.transform.position, grenadeSpawner.transform.rotation); //create an instance of the grenade prefab at the location of the grenade spawner object
        Vector3 grenadedirection = grenadeSpawner.transform.forward; //match up the grenade's forward direction with the spawner's forward direction
        newgrenade.transform.up = grenadedirection; //set the grenade prefab's up direction to match our new vector3
        newgrenade.GetComponent<Rigidbody>().AddForce(grenadedirection * throwForce); //access the grenade prefab's rigidbody and apply force in desired direction
    }

}
