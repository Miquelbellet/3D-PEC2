using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Header("AK Config")]
    public float AK_Damage;
    public GameObject AK_decalPrefab;
    public GameObject AK_particleShotPrefab;
    public GameObject AK_gunPoint;
    public AudioClip AK_fireClip;
    public int AK_totalAmmo;
    public float AK_shootingCadency;
    public float AK_distanceArrival;
    public float AK_reloadingTime;
    public Vector3 AK_recoil;
    public float AK_recoilTime;
    public float AK_recoilReturnTime;

    [Header("Pistol Config")]
    public float P_Damage;
    public GameObject P_decalPrefab;
    public GameObject P_particleShotPrefab;
    public GameObject P_gunPoint;
    public AudioClip P_fireClip;
    public int P_totalAmmo;
    public float P_shootingCadency;
    public float P_distanceArrival;
    public float P_reloadingTime;

    [Header("General Config")]
    public AudioClip chooseAKClip;
    public AudioClip choosePistolClip;
    public AudioClip reloadingClip;

    private enum Weapons { AK, Pistol };
    private Weapons currentWeapon;
    private GameObject gameManager;
    private GameObject AK;
    private GameObject Pistol;
    private AudioSource weaponAS;
    private bool hasShot;
    private bool reloadingAK;
    private bool reloadingPistol;
    private float time;

    private int AK_Ammo = 20;
    private int currentAkAmmo = 20;
    private int AK_MaxAmmo;
    private int Pistol_Ammo = 8;
    private int currentPistolAmmo = 8;
    private int P_MaxAmmo;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        weaponAS = GetComponent<AudioSource>();
        AK_MaxAmmo = AK_totalAmmo;
        P_MaxAmmo = P_totalAmmo;
        AK = transform.GetChild(0).gameObject;
        Pistol = transform.GetChild(1).gameObject;
        ChangeWeapon(Weapons.AK);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (currentWeapon == Weapons.AK) AKShoot();
            else if (currentWeapon == Weapons.Pistol) PistolShoot();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentWeapon == Weapons.AK) ChangeWeapon(Weapons.Pistol);
            else if (currentWeapon == Weapons.Pistol) ChangeWeapon(Weapons.AK);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentWeapon == Weapons.AK && currentAkAmmo < AK_Ammo) ReloadingAK();
            else if (currentWeapon == Weapons.Pistol && currentPistolAmmo < Pistol_Ammo) ReloadingPistol();
        }
    }

    private void ChangeWeapon(Weapons newWeapon)
    {
        currentWeapon = newWeapon;
        if (newWeapon == Weapons.AK)
        {
            AK.gameObject.SetActive(true);
            Pistol.gameObject.SetActive(false);
            gameManager.GetComponent<UIScript>().ActivateAKAmmo(currentAkAmmo, AK_totalAmmo);
            GetComponent<AudioSource>().PlayOneShot(chooseAKClip);
        }
        else if (newWeapon == Weapons.Pistol)
        {
            AK.gameObject.SetActive(false);
            Pistol.gameObject.SetActive(true);
            gameManager.GetComponent<UIScript>().ActivatePistolAmmo(currentPistolAmmo, P_totalAmmo);
            GetComponent<AudioSource>().PlayOneShot(choosePistolClip);
        }
    }

    private void AKShoot()
    {
        if (!hasShot && !reloadingAK && AK_totalAmmo > 0 && time <= AK_shootingCadency)
        {
            hasShot = true;
            weaponAS.PlayOneShot(AK_fireClip);
            currentAkAmmo--;
            AK_totalAmmo--;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {
                var distance = Vector3.Distance(hit.point, transform.position);
                if(distance < AK_distanceArrival)
                {
                    if (hit.transform.tag == "Enemy") hit.transform.parent.GetComponent<enemyAI>().Hit(AK_Damage);
                    else
                    {
                        GameObject bullet = Instantiate(AK_decalPrefab,
                            hit.point + hit.normal * 0.01f,
                            Quaternion.FromToRotation(Vector3.forward, -hit.normal)
                        );
                        Destroy(bullet, 8f);
                    }
                }
                GameObject ps1 = Instantiate(AK_particleShotPrefab, hit.point + hit.normal * 0.01f, Quaternion.Euler(0, 0, 0));
                Destroy(ps1, 1f);
                GameObject ps2 = Instantiate(AK_particleShotPrefab, AK_gunPoint.transform.position, Quaternion.Euler(0, 0, 0));
                Destroy(ps2, 1f);
            }
            gameManager.GetComponent<UIScript>().SetAkAmmo(currentAkAmmo, AK_totalAmmo);
            if (currentAkAmmo == 0) ReloadingAK();
        }
        else if (time >= AK_shootingCadency)
        {
            time = 0;
            hasShot = false;
        }
    }

    private void PistolShoot()
    {
        if (!hasShot && !reloadingPistol && P_totalAmmo > 0 && time <= P_shootingCadency)
        {
            hasShot = true;
            weaponAS.PlayOneShot(P_fireClip);
            currentPistolAmmo--;
            P_totalAmmo--;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {
                var distance = Vector3.Distance(hit.point, transform.position);
                if (distance < P_distanceArrival)
                {
                    if (hit.transform.tag == "Enemy") hit.transform.parent.GetComponent<enemyAI>().Hit(P_Damage);
                    else
                    {
                        GameObject bullet = Instantiate(P_decalPrefab,
                            hit.point + hit.normal * 0.01f,
                            Quaternion.FromToRotation(Vector3.forward, -hit.normal)
                        );
                        Destroy(bullet, 8f);
                    }
                }
                GameObject ps1 = Instantiate(P_particleShotPrefab, hit.point + hit.normal * 0.01f, Quaternion.Euler(0, 0, 0));
                Destroy(ps1, 1f);
                GameObject ps2 = Instantiate(P_particleShotPrefab, P_gunPoint.transform.position, Quaternion.Euler(0, 0, 0));
                Destroy(ps2, 1f);
            }
            gameManager.GetComponent<UIScript>().SetPistolAmmo(currentPistolAmmo, P_totalAmmo);
            if (currentPistolAmmo == 0) ReloadingPistol();
        }
        else if (time >= P_shootingCadency)
        {
            time = 0;
            hasShot = false;
        }
    }

    private void ReloadingAK()
    {
        if (!reloadingAK && AK_totalAmmo > currentAkAmmo)
        {
            reloadingAK = true;
            gameManager.GetComponent<UIScript>().ActivateReloading();
            GetComponent<AudioSource>().PlayOneShot(reloadingClip);
            StartCoroutine(waitReloadingAK(AK_reloadingTime));
        }
    }

    private void ReloadingPistol()
    {
        if (!reloadingPistol && P_totalAmmo > currentPistolAmmo)
        {
            reloadingPistol = true;
            gameManager.GetComponent<UIScript>().ActivateReloading();
            GetComponent<AudioSource>().PlayOneShot(reloadingClip);
            StartCoroutine(waitReloadingPistol(P_reloadingTime));
        }
    }

    private IEnumerator waitReloadingAK(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        reloadingAK = false;
        gameManager.GetComponent<UIScript>().DeactivateReloading();
        if (AK_totalAmmo < AK_Ammo) currentAkAmmo = AK_totalAmmo;
        else currentAkAmmo = AK_Ammo;
        gameManager.GetComponent<UIScript>().SetAkAmmo(currentAkAmmo, AK_totalAmmo);
    }

    private IEnumerator waitReloadingPistol(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        reloadingPistol = false;
        gameManager.GetComponent<UIScript>().DeactivateReloading();
        if (P_totalAmmo < Pistol_Ammo) currentPistolAmmo = P_totalAmmo;
        else currentPistolAmmo = Pistol_Ammo;
        gameManager.GetComponent<UIScript>().SetPistolAmmo(currentPistolAmmo, P_totalAmmo);
    }

    public void PlusAmmo(float plusAmmo)
    {
        if (currentWeapon == Weapons.AK)
        {
            AK_totalAmmo += (int)plusAmmo;
            if (AK_totalAmmo > AK_MaxAmmo) AK_totalAmmo = AK_MaxAmmo;
            gameManager.GetComponent<UIScript>().SetAkAmmo(currentAkAmmo, AK_totalAmmo);
        }
        else if (currentWeapon == Weapons.Pistol)
        {
            P_totalAmmo += (int)plusAmmo;
            if (P_totalAmmo > P_MaxAmmo) P_totalAmmo = P_MaxAmmo;
            gameManager.GetComponent<UIScript>().SetPistolAmmo(currentPistolAmmo, P_totalAmmo);
        }
    }
}
