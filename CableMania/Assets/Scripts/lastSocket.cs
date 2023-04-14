using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lastSocket : MonoBehaviour
{
    [SerializeField] public GameObject activeSocket;
    [SerializeField] private string socketColor;
    [SerializeField] private GameManager _GameManager;

    bool picked;
    bool posChange;
    bool sitToSocket;

    GameObject movePosition;
    GameObject socketosition;

    public void ChoosePosition(GameObject moveToObj, GameObject socket)
    {
        movePosition = moveToObj;
        picked = true;
    }
    public void ChangePosition(GameObject moveToObj, GameObject socket)
    {
        movePosition = moveToObj;
        socketosition = socket;
        posChange = true;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (picked)
        {
            transform.position = Vector3.Lerp(transform.position, movePosition.transform.position, .02f);
            if (Vector3.Distance(transform.position, movePosition.transform.position) < .1f)
            {
                picked = false;
            }
        }
        if (posChange)
        {
            transform.position = Vector3.Lerp(transform.position, movePosition.transform.position, .02f);
            if (Vector3.Distance(transform.position, movePosition.transform.position) < .1f)
            {
                posChange = false;
                sitToSocket = true;
            }
        }
        if (sitToSocket)
        {
            transform.position = Vector3.Lerp(transform.position, socketosition.transform.position, .02f);
            if (Vector3.Distance(transform.position, socketosition.transform.position) < .1f)
            {
                sitToSocket = false;
                _GameManager.movement = false;
                activeSocket = socketosition;
            }
        }
    }
}
