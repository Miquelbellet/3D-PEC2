using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public bool isHorizontal;
    public float timeToMove;
    public float moveSpeed;
    public bool isActive;

    private float time;

    void Start()
    {
        
    }

    void Update()
    {
        if (isActive)
        {
            time += Time.deltaTime;
            if(time < timeToMove)
            {
                if (isHorizontal)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);
                }
            }
            else if(time > timeToMove)
            {
                if (isHorizontal)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
                }
                if(time > timeToMove * 2) time = 0;
            }
        }
    }
}
