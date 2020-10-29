using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject decalPrefab;
    public AudioClip AK_fireClip;
    public float AK_shootingCadency;
    public Vector3 AK_recoil;

    private AudioSource weaponAS;
    private bool hasShot;
    private float time;
    void Start()
    {
        weaponAS = GetComponent<AudioSource>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!hasShot && time <= AK_shootingCadency)
        {
            hasShot = true;
            weaponAS.PlayOneShot(AK_fireClip);
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {
                GameObject bullet = Instantiate(decalPrefab,
                        hit.point + hit.normal * 0.01f,
                        Quaternion.FromToRotation(Vector3.forward, -hit.normal)
                    );
                Destroy(bullet, 8f);
            }
        }
        else if (time >= AK_shootingCadency)
        {
            time = 0;
            hasShot = false;
        }
    }
}
