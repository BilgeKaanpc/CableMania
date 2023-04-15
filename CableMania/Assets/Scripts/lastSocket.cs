using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastSocket : MonoBehaviour
{
    [SerializeField] public GameObject activeSocket;
    [SerializeField] public string socketColor;
    [SerializeField] private GameManager _GameManager;

    bool picked;
    bool posChange;
    bool sitToSocket;

    GameObject movePosition;
    GameObject socketosition;

    public void Move(string process, GameObject socket, GameObject moveToObj = null)
    {
        switch (process)
        {
            case "pick":
                movePosition = moveToObj;
                picked = true;
                break;
            case "change":
                movePosition = moveToObj;
                socketosition = socket;
                posChange = true;
                break;
            case "join":
                socketosition = socket;
                sitToSocket = true;
                break;
            default:
                break;
        }
    }
    

    void Update()
    {
        if (picked)
        {
            transform.position = Vector3.Lerp(transform.position, movePosition.transform.position, .02f);
            if (Vector3.Distance(transform.position, movePosition.transform.position) < .05f)
            {
                picked = false;
            }
        }
        if (posChange)
        {
            transform.position = Vector3.Lerp(transform.position, movePosition.transform.position, .02f);
            if (Vector3.Distance(transform.position, movePosition.transform.position) < .05f)
            {
                posChange = false;
                sitToSocket = true;
            }
        }
        if (sitToSocket)
        {
            transform.position = Vector3.Lerp(transform.position, socketosition.transform.position, .02f);
            if (Vector3.Distance(transform.position, socketosition.transform.position) < .05f)
            {
                sitToSocket = false;
                _GameManager.movement = false;
                activeSocket = socketosition;
                _GameManager.SocketControl();
            }
        }
    }
}
