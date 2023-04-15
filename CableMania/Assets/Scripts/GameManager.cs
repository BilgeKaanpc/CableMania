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
    public List<bool> situations;
    int completeNumber;
    int controlCount;

    lastSocket _LastSocket;
    void Start()
    {
        for (int i = 0; i < socketCount-1; i++)
        {
            situations.Add(false);
        }
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
                            _LastSocket = hit.collider.GetComponent<lastSocket>();
                            hit.collider.GetComponent<lastSocket>().Move("pick",_LastSocket.activeSocket, _LastSocket.activeSocket.GetComponent<Socket>().movePosition);
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
                            _LastSocket.Move("change", hit.collider.gameObject, _Socket.movePosition);
                            _Socket.isEmpty = true;
                            activeObject = null;
                            activeSocket = null;
                            movement = true;
                        }
                        else if (activeSocket == hit.collider.gameObject)
                        {
                            _LastSocket.Move("join",hit.collider.gameObject);
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
            StartCoroutine(FinishControl());       }
        else
        {

        }
        completeNumber = 0;
    }

    public void ColControl(int colIndex,bool durum)
    {
        situations[colIndex] = durum;  
    }

    IEnumerator FinishControl()
    {
        Debug.Log("Control Ediliyor");
        yield return new WaitForSeconds(4);
        foreach (var item in situations)
        {
            if (item)
            {
                controlCount++;
            }
        }
        if(controlCount == situations.Count)
        {
            Debug.Log("win");
        }
        else
        {
            Debug.Log("Temas");
            foreach (var item in Controllers)
            {
                item.SetActive(false);
            }
        }

        controlCount = 0;
    }
}
