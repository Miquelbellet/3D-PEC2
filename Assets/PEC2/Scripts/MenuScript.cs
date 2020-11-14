using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public TextMeshProUGUI mapName;
    private int mapSelected = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (mapSelected == 0) mapName.text = "Moon";
        else if (mapSelected == 1) mapName.text = "SpaceShip";
    }

    public void NextMap()
    {
        mapSelected++;
        if (mapSelected > 1) mapSelected = 0;
    }

    public void PrevMap()
    {
        mapSelected--;
        if (mapSelected < 0) mapSelected = 1;
    }

    public void Play()
    {
        PlayerPrefs.SetInt("Map", mapSelected + 1);
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
