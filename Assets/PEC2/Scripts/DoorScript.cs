using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool openDoorSensorPlayer;
    public bool openAxisX;
    public bool openDoor;
    public bool closeDoor;
    public float timeToMove;
    public float speedMove;
    public AudioClip doorOpenClip;

    private GameObject doorLeft;
    private GameObject doorRight;
    private GameObject gameManager;
    private bool isOpened;
    private float time;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        doorRight = transform.GetChild(0).gameObject;
        doorLeft = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if (openDoor && !isOpened)
        {
            time += Time.deltaTime;
            if(time < timeToMove)
            {
                if (openAxisX)
                {
                    doorRight.transform.position = new Vector3(doorRight.transform.position.x - speedMove * Time.deltaTime, doorRight.transform.position.y, doorRight.transform.position.z);
                    doorLeft.transform.position = new Vector3(doorLeft.transform.position.x + speedMove * Time.deltaTime, doorLeft.transform.position.y, doorLeft.transform.position.z);
                }
                else
                {
                    doorRight.transform.position = new Vector3(doorRight.transform.position.x, doorRight.transform.position.y, doorRight.transform.position.z + speedMove * Time.deltaTime);
                    doorLeft.transform.position = new Vector3(doorLeft.transform.position.x, doorLeft.transform.position.y, doorLeft.transform.position.z - speedMove * Time.deltaTime);
                }
            }
            else if (time > timeToMove)
            {
                openDoor = false;
                isOpened = true;
                time = 0;
            }
        }
        if (closeDoor && isOpened)
        {
            time += Time.deltaTime;
            if (time < timeToMove)
            {
                if (openAxisX)
                {
                    doorRight.transform.position = new Vector3(doorRight.transform.position.x + speedMove * Time.deltaTime, doorRight.transform.position.y, doorRight.transform.position.z);
                    doorLeft.transform.position = new Vector3(doorLeft.transform.position.x - speedMove * Time.deltaTime, doorLeft.transform.position.y, doorLeft.transform.position.z);
                }
                else
                {
                    doorRight.transform.position = new Vector3(doorRight.transform.position.x, doorRight.transform.position.y, doorRight.transform.position.z - speedMove * Time.deltaTime);
                    doorLeft.transform.position = new Vector3(doorLeft.transform.position.x, doorLeft.transform.position.y, doorLeft.transform.position.z + speedMove * Time.deltaTime);
                }
                
            }
            else if (time > timeToMove)
            {
                closeDoor = false;
                isOpened = false;
                time = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (openDoorSensorPlayer)
        {
            if (other.tag == "Player")
            {
                if(gameManager.GetComponent<UIScript>().numKeys > 0 && !openDoor && !isOpened)
                {
                    openDoor = true;
                    gameManager.GetComponent<UIScript>().SubsKeys();
                    GetComponent<AudioSource>().PlayOneShot(doorOpenClip);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (openDoorSensorPlayer)
        {
            if (other.tag == "Player")
            {
                if (!closeDoor && isOpened)
                {
                    closeDoor = true;
                    GetComponent<AudioSource>().PlayOneShot(doorOpenClip);
                }
            }
        }
    }
}
