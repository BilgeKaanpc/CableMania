using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameObject activeObject;
    GameObject activeSocket;
    public bool movement;

    [SerializeField] HingeJoint[] breakPoint;

    [Header("Level Settings")]
    public GameObject[] Controllers;
    public GameObject[] Sockets;
    [SerializeField] public int moveCount;
    public int socketCount;
    public List<bool> situations;
    int completeNumber;
    int controlCount;

    [Header("UI Elements")]
    [SerializeField] TMP_Text controlText;
    [SerializeField] TMP_Text countText;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;


    [Header("Others")]
    [SerializeField] GameObject[] lights;

    lastSocket _LastSocket;
    void Start()
    {
        countText.text = "Move : " +  moveCount;
        for (int i = 0; i < socketCount-1; i++)
        {
            situations.Add(false);
        }
        Invoke("SetPoint",2);
    }
    void SetPoint()
    {
        foreach (var item in breakPoint)
        {
            item.breakForce = 600;
            item.breakTorque = 500;
        }

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RaycastHit hit , 100))
            {
                if(hit.collider != null && moveCount > 0)
                {
                    if(activeObject == null && !movement)
                    {
                        if (hit.collider.CompareTag("blueSocket") || hit.collider.CompareTag("redSocket") || hit.collider.CompareTag("yellowSocket") || hit.collider.CompareTag("greenSocket"))
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
                            moveCount--;
                            countText.text = "Move : " + moveCount;
                            if(moveCount <= 0)
                            {
                                StartCoroutine(FinishControl());
                            }
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
            if(moveCount <= 0)
            {
                Debug.Log("bitti");
            }
        }
        completeNumber = 0;
    }

    public void ColControl(int colIndex,bool durum)
    {
        situations[colIndex] = durum;  
    }

    IEnumerator FinishControl()
    {
        lights[0].SetActive(false);
        lights[1].SetActive(true);
        controlPanel.SetActive(true);
        controlText.text = "Kontrol Ediliyor...";
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
            Win();
            lights[1].SetActive(false);
            lights[2].SetActive(true);
        }
        else if(moveCount <= 0)
        {
            controlText.text = "Hamle Bitti";
            lights[1].SetActive(false);
            lights[0].SetActive(true);
            Lose();
        }
        else
        {
            controlText.text = "Temas Var";
            Invoke("closePanel", 3);
            foreach (var item in Controllers)
            {
                item.SetActive(false);
            }
            if (moveCount <= 0)
            {
                Lose();
            }
            lights[1].SetActive(false);
            lights[0].SetActive(true);
        }

        controlCount = 0;
    }

    public void Lose()
    {
        losePanel.SetActive(true);
    }
    public void Win()
    {
        winPanel.SetActive(true);
        controlPanel.SetActive(false);
    }
    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(0);
    }
    void closePanel()
    {
        controlPanel.SetActive(false);
    }
}
