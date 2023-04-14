using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject activeObject;
    GameObject activeSocket;
    public bool movement;
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
                        }
                    }
                }
            }
        }
    }
}
