using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject activeObject;
    GameObject activeSocket;
    public bool movement;

    [Header("Level Settings")]
    public GameObject[] Controllers;
    public GameObject[] Sockets;
    public int socketCount;
    public bool[] situations;
    int completeNumber;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hit , 100))
            {
                if(hit.collider != null)
                {
                    if(activeObject == null && !movement)
                    {
                        if (hit.collider.CompareTag("blueSocket") || hit.collider.CompareTag("redSocket") || hit.collider.CompareTag("yellowSocket"))
                        {
                            lastSocket _LastSocket = hit.collider.GetComponent<lastSocket>();
                            hit.collider.GetComponent<lastSocket>().ChoosePosition(_LastSocket.activeSocket.GetComponent<Socket>().movePosition, _LastSocket.activeSocket);
                            activeObject = hit.collider.gameObject;
                            activeSocket = _LastSocket.activeSocket;
                            movement = true;
                        }
                    }

                    if (hit.collider.CompareTag("socket"))
                    {
                        if(activeObject != null && !hit.collider.GetComponent<Socket>().isEmpty && activeObject != hit.collider.gameObject)
                        {
                            Socket _Socket = hit.collider.GetComponent<Socket>();
                            activeSocket.GetComponent<Socket>().isEmpty = false;
                            activeObject.GetComponent<lastSocket>().ChangePosition(_Socket.movePosition,hit.collider.gameObject);
                            _Socket.isEmpty = true;
                            activeObject = null;
                            activeSocket = null;
                            movement = true;
                        }
                        else if (activeSocket == hit.collider.gameObject)
                        {
                            activeObject.GetComponent<lastSocket>().JoinSocket(hit.collider.gameObject);
                            activeSocket = null;
                            activeObject = null;
                            movement = true;
                        }
                    }
                }
            }
        }
    }

    public void SocketControl()
    {
        foreach (var item in Sockets)
        {
            if(item.GetComponent<lastSocket>().activeSocket.name == item.GetComponent<lastSocket>().socketColor)
            {
                completeNumber++;
            }
        }

        if(completeNumber == socketCount)
        {
            foreach (var item in Controllers)
            {
                item.SetActive(true);
            }
        }
        else
        {

        }
        completeNumber = 0;
    }

    public void ColControl(int colIndex,bool durum)
    {
        situations[colIndex] = durum;

        if(situations[0] && situations[1])
        {
            Debug.Log("Win");
        }
        else
        {
            Debug.Log("Deðiyor");
        }
    }
}
