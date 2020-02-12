using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Windows.WebCam;


public class StateManager : MonoSingleton<StateManager>
{
    public State[] states;
    [HideInInspector] public PhotonView pv;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        states = new State[LevelManager.Instance.stateCount];
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


    public void capture(int i, string uid)
    {
        if (LevelManager.Instance.finished)
            return;
        doCapture(i, uid);
        pv.RPC("doCapture", RpcTarget.OthersBuffered, i, uid);
    }

    [PunRPC]
    void doCapture(int i, string uid)
    {
        states[i].capture(LevelManager.Instance.playersColor[uid], uid);
    }
}