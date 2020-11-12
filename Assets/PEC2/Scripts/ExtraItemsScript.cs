using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraItemsScript : MonoBehaviour
{
    public GameObject lifeBoxPrefab;
    public float lifeBoxPlus;
    public GameObject shieldBoxPrefab;
    public float shieldBoxPlus;
    public GameObject ammoBoxPrefab;
    public float ammoBoxPlus;
    public AudioClip catchItemClip;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DropRandomItem(Vector3 pos)
    {
        var rand = Random.Range(0, 3);
        if (rand == 0) Instantiate(lifeBoxPrefab, pos, Quaternion.Euler(-90, 0, 0));
        else if (rand == 1) Instantiate(shieldBoxPrefab, pos, Quaternion.Euler(0, 0, 0));
        else if (rand == 2) Instantiate(ammoBoxPrefab, pos, Quaternion.Euler(-90, 0, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LifeBox")
        {
            GetComponent<HealthScript>().PlusHealth(lifeBoxPlus);
            GetComponent<AudioSource>().PlayOneShot(catchItemClip);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "ShieldBox")
        {
            GetComponent<HealthScript>().PlusShield(shieldBoxPlus);
            GetComponent<AudioSource>().PlayOneShot(catchItemClip);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "AmmoBox")
        {
            transform.GetChild(0).GetComponent<WeaponScript>().PlusAmmo(ammoBoxPlus);
            GetComponent<AudioSource>().PlayOneShot(catchItemClip);
            Destroy(collision.gameObject);
        }
    }
}
