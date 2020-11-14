using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManagerScript : MonoBehaviour
{
    public GameObject map1;
    public GameObject map2;

    private GameObject player;
    private FirstPersonController FPSController;
    private int numOfMap;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FPSController = FindObjectOfType<FirstPersonController>();
        FPSController.m_MouseLook.lockCursor = true;

        numOfMap = PlayerPrefs.GetInt("Map", 1);
        if(numOfMap == 1)
        {
            map1.SetActive(true);
            map2.SetActive(false);
            player.transform.position = new Vector3(315f, 4.5f, 344f);
        }
        else if(numOfMap == 2)
        {
            map1.SetActive(false);
            map2.SetActive(true);
            player.transform.position = new Vector3(439f, 25f, 54f);
        }
    }

    void Update()
    {
        
    }
}
