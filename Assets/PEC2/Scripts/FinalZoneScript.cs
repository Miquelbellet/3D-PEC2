using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalZoneScript : MonoBehaviour
{
    public GameObject CompletedMapText;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void GoToMenu()
    {
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
