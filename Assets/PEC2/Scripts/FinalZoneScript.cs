using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class FinalZoneScript : MonoBehaviour
{
    public GameObject CompletedMapText;

    private FirstPersonController FPSController;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void GoToMenu()
    {
        FPSController = FindObjectOfType<FirstPersonController>();
        FPSController.m_MouseLook.lockCursor = false;
        SceneManager.LoadScene("Menu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            CompletedMapText.SetActive(true);
            Invoke("GoToMenu", 3f);
        }
    }
}
