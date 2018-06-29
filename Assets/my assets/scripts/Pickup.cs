using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    [SerializeField]
    private WeaponSwap swap;
    [SerializeField]
    private WeaponSwap2 swap2;
    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private PlayerController2 controller2;
    public int _index;
    public AmmoManager ammo;
    public AmmoManager2 ammo2;

	// Use this for initialization
	void Start () {
        controller = FindObjectOfType<PlayerController>();
        controller2 = FindObjectOfType<PlayerController2>();

        swap = FindObjectOfType<WeaponSwap>();
        swap2 = FindObjectOfType<WeaponSwap2>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerStay(Collider other)
    {
        switch (tag)
        {
            case "Player":
                //if (other.GetComponent<Collider>().tag == "Player")
                {
                    if (_index != swap.currentWeapon)
                    {
                        controller.pickupPrompt.SetActive(true);
                        if (Input.GetButtonDown("Fire1"))
                        {
                            swap.ReceiveIndex(_index);
                            //Debug.Log("Swap weapons");
                            gameObject.SetActive(false);
                            controller.pickupPrompt.SetActive(false);
                        }
                    }
                    else if (_index == swap.currentWeapon)
                    {
                        ammo.currentPocketAmmo = ammo.maxPocketAmmo;
                        controller.pickupPrompt.SetActive(false);
                        gameObject.SetActive(false);
                        Debug.Log("Pickup Ammo");
                    }

                }

                break;

            case "Player2":
                //if (other.GetComponent<Collider>().tag == "Player2")
                {
                    if (_index != swap2.currentWeapon)
                    {
                        controller2.pickupPrompt.SetActive(true);
                        if (Input.GetButtonDown("2Fire1"))
                        {
                            swap2.ReceiveIndex(_index);
                            //Debug.Log("Swap weapons");
                            gameObject.SetActive(false);
                            controller2.pickupPrompt.SetActive(false);
                        }
                    }
                    else if (_index == swap2.currentWeapon)
                    {
                        ammo2.currentPocketAmmo = ammo.maxPocketAmmo;
                        controller2.pickupPrompt.SetActive(false);
                        gameObject.SetActive(false);
                        Debug.Log("Player 2 has Pickup Ammo");
                    }

                }
                break;
        }
    }
        
       



    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            controller.pickupPrompt.SetActive(false);
        }

        if (other.GetComponent<Collider>().tag == "Player2")
        {
            controller2.pickupPrompt.SetActive(false);
        }

    }
}
