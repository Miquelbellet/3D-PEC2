using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float life;
    public float shield;

    [HideInInspector] public float initLife;
    [HideInInspector] public float initShield;

    private UIScript uiScript;
    void Start()
    {
        initLife = life;
        initShield = shield;
        uiScript = GetComponent<UIScript>();
    }

    void Update()
    {
        if (life <= 0) Dead();
    }

    private void Dead()
    {
        Debug.Log("Player Dead");
    }

    public void Hit(float damage)
    {
        if(shield > 0)
        {
            shield -= damage * 0.8f;
            life -= damage * 0.2f;
        }
        else life -= damage;
        uiScript.SetBars(life, shield);
    }
}
