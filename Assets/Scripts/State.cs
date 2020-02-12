using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    public Renderer renderer;
    private List<PlayerManager> playersInArea;
    private PlayerManager currentPlayer;
    public float timeToCapture = 100;

    private float timeTemp;
    public int index;
    public Text text;
    private string _owner;

    private void Start()
    {
        timeTemp = timeToCapture;
        playersInArea = new List<PlayerManager>();
        Invoke("setIndex", 1f);
    }

    void setIndex()
    {
        StateManager.Instance.states[index] = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInArea.Add(other.GetComponent<PlayerManager>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInArea.Remove(other.GetComponent<PlayerManager>());
            if (other.GetComponent<PlayerManager>() == currentPlayer)
            {
                timeTemp = timeToCapture;
                text.text = timeTemp.ToString();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playersInArea.Count == 1)
            {
                if (playersInArea[0] == currentPlayer)
                {
                    if (_owner==null||_owner=="" || _owner != other.GetComponent<PhotonView>().Owner.UserId)
                    {
                        if (timeTemp <= 0)
                        {
                            timeTemp = 0;
                            text.text = timeTemp.ToString();
                            if (!PhotonNetwork.IsMasterClient)
                                return;

                            StateManager.Instance.capture(index, other.GetComponent<PhotonView>().Owner.UserId);
                        }
                        else
                        {
                            timeTemp -= 0.1f;
                            text.text = timeTemp.ToString();
                        }
                    }
                }
                else
                {
                    timeTemp = timeToCapture;
                    text.text = timeTemp.ToString();
                    currentPlayer = playersInArea[0];
                }
            }
        }
    }

    public void capture(Color c, string uid)
    {

            if (_owner!=null&&_owner!="")
            {
                LevelManager.Instance.AddPLayerPoint(_owner, uid);
            }
            else
            {
                LevelManager.Instance.AddPLayerPoint(uid);
            }
        

        renderer.material.color = c;
        _owner = uid;
    }
}