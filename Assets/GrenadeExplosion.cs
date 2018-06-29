using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour {
    public float timeBeforeExplosion = 3;
    public GameObject explosionParticles;
    public GameObject graphics;
    public GameObject hitbox;
    

    private float timer = 0;
    private bool startCounting;
    

   


    // Use this for initialization
    private void Start()
    {
        startCounting = false;
    }

    // Update is called once per frame
    void Update ()
    {
        //timer += Time.deltaTime;
        //Debug.Log("nades" + timer);


        if (startCounting == true)
        {
            timer += Time.deltaTime;
        }

        if (timer >= timeBeforeExplosion)
        {
            StartCoroutine(Explode());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        startCounting = true;
    }

    public IEnumerator Explode()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        hitbox.SetActive(true);
        GameObject boom = Instantiate(explosionParticles);
        boom.transform.position = transform.position;
        hitbox.SetActive(true);

        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);        
    }

}
