using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject lifeBar;
    public GameObject shieldBar;
    public GameObject reloadTxt;
    public GameObject akAmmo;
    public GameObject pistolAmmo;
    public TextMeshProUGUI totalAmmo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetBars(float life, float shield)
    {
        var completLifeWidth = lifeBar.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        var currentLifeWidth = completLifeWidth * life / GetComponent<HealthScript>().initLife;
        if (currentLifeWidth <= 0) currentLifeWidth = 0;
        lifeBar.transform.GetChild(0).GetChild(0).transform.GetComponent<RectTransform>().offsetMin = new Vector2(currentLifeWidth, lifeBar.transform.GetChild(0).GetChild(0).transform.GetComponent<RectTransform>().offsetMin.y);

        var completShieldWidth = shieldBar.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
        var currentShieldWidth = completShieldWidth * shield / GetComponent<HealthScript>().initShield;
        if (currentShieldWidth <= 0) currentShieldWidth = 0;
        shieldBar.transform.GetChild(0).GetChild(0).transform.GetComponent<RectTransform>().offsetMin = new Vector2(currentShieldWidth, shieldBar.transform.GetChild(0).GetChild(0).transform.GetComponent<RectTransform>().offsetMin.y);
    }

    public void ActivateReloadText()
    {
        reloadTxt.SetActive(true);
    }

    public void DeactivateReloadText()
    {
        reloadTxt.SetActive(false);
    }

    public void ActivateAKAmmo(int currentAmmo, int totalAkAmmo)
    {
        akAmmo.SetActive(true);
        pistolAmmo.SetActive(false);
        totalAmmo.text = totalAkAmmo.ToString();
        if (currentAmmo <= 5) ActivateReloadText();
        else DeactivateReloadText();
    }

    public void ActivatePistolAmmo(int currentAmmo, int totalPAmmo)
    {
        akAmmo.SetActive(false);
        pistolAmmo.SetActive(true);
        totalAmmo.text = totalPAmmo.ToString();
        if (currentAmmo <= 2) ActivateReloadText();
        else DeactivateReloadText();
    }

    public void SetAkAmmo(int currentAmmo, int totalAkAmmo)
    {
        totalAmmo.text = totalAkAmmo.ToString();
        for (int i = 0; i < akAmmo.transform.childCount; i++)
        {
            if (i > currentAmmo - 1) akAmmo.transform.GetChild(i).gameObject.SetActive(false);
            else akAmmo.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (currentAmmo <= 5) ActivateReloadText();
        else DeactivateReloadText();
    }

    public void SetPistolAmmo(int currentAmmo, int totalPAmmo)
    {
        totalAmmo.text = totalPAmmo.ToString();
        for (int i = 0; i < pistolAmmo.transform.childCount; i++)
        {
            if (i > currentAmmo - 1) pistolAmmo.transform.GetChild(i).gameObject.SetActive(false);
            else pistolAmmo.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (currentAmmo <= 2) ActivateReloadText();
        else DeactivateReloadText();
    }
}
