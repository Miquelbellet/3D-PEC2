using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class HealthScript : MonoBehaviour
{
    public float life;
    public float shield;
    public AudioClip playerHitted;
    public GameObject gameoverPanel;

    [HideInInspector] public float initLife;
    [HideInInspector] public float initShield;

    private GameObject gameController;
    private FirstPersonController FPSController;
    void Start()
    {
        initLife = life;
        initShield = shield;
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
        if (life <= 0) Dead();
    }

    private void Dead()
    {
        gameoverPanel.SetActive(true);
        Invoke("GoToMenu", 3f);
    }

    private void GoToMenu()
    {
        FPSController = FindObjectOfType<FirstPersonController>();
        FPSController.m_MouseLook.lockCursor = false;
        SceneManager.LoadScene("Menu");
    }

    public void Hit(float damage)
    {
        if(shield > 0)
        {
            shield -= damage * 0.8f;
            life -= damage * 0.2f;
        }
        else life -= damage;
        gameController.GetComponent<UIScript>().SetBars(life, shield);
        GetComponent<AudioSource>().PlayOneShot(playerHitted);
    }

    public void PlusHealth(float plusHelath)
    {
        life += plusHelath;
        if (life > initLife) life = initLife;
        gameController.GetComponent<UIScript>().SetBars(life, shield);
    }

    public void PlusShield(float plusShield)
    {
        shield += plusShield;
        if (shield > initShield) shield = initShield;
        gameController.GetComponent<UIScript>().SetBars(life, shield);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "DeadZone") Dead();
    }
}
