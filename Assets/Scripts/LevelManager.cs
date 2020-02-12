using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoSingleton<LevelManager>
{
    public int stateCount;
    public List<Transform> instantPoints;
    public Dictionary<string, Color> playersColor;
    public Dictionary<string, int> playersPoint;
    public string playerPrefab;
    public bool finished;
    public State[] states;
    private void Start()
    {
        setPlayersColor();
        Invoke("instancePlayer", 2f);
    }

    int myIndex = 0;

    void instancePlayer()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].UserId == PhotonNetwork.LocalPlayer.UserId)
            {
                myIndex = i;
            }
        }


        PhotonNetwork.Instantiate(playerPrefab, instantPoints[getRandomPos()].position + new Vector3(0, 1.13f, 0),
            Quaternion.identity, 0);
        
    }

    int getRandomPos()
    {
        switch (myIndex)
        {
            case 0:
                StateManager.Instance.capture(states[0].index, PhotonNetwork.LocalPlayer.UserId);
                StateManager.Instance.capture(states[1].index, PhotonNetwork.LocalPlayer.UserId);
                return Random.Range(0, 3);
            case 1:
                StateManager.Instance.capture(states[2].index, PhotonNetwork.LocalPlayer.UserId);
                StateManager.Instance.capture(states[3].index, PhotonNetwork.LocalPlayer.UserId);
                return Random.Range(3, 6);
            case 2:
                StateManager.Instance.capture(states[4].index, PhotonNetwork.LocalPlayer.UserId);
                StateManager.Instance.capture(states[5].index, PhotonNetwork.LocalPlayer.UserId);
                return Random.Range(6, 9);
        }

        return 0;
    }

    void setPlayersColor()
    {
        playersColor = new Dictionary<string, Color>();
        playersColor.Add(PhotonNetwork.PlayerList[0].UserId, Color.blue);
        playersColor.Add(PhotonNetwork.PlayerList[1].UserId, Color.green);
        playersColor.Add(PhotonNetwork.PlayerList[2].UserId,Color.red);
        playersPoint = new Dictionary<string, int>();
        playersPoint.Add(PhotonNetwork.PlayerList[0].UserId, 0);
        playersPoint.Add(PhotonNetwork.PlayerList[1].UserId, 0);
        playersPoint.Add(PhotonNetwork.PlayerList[2].UserId,0);
        LevelUiManager.Instance.UpdateScore();
        LevelUiManager.Instance.setColors();
    }

    public void AddPLayerPoint(string owner, string uid)
    {
        playersPoint[owner]--;
        playersPoint[uid]++;
        LevelUiManager.Instance.UpdateScore();
    }

    public void AddPLayerPoint(string uid)
    {
        playersPoint[uid]++;
        LevelUiManager.Instance.UpdateScore();
    }

    public Vector3 reSpawn()
    {
        return instantPoints[getRandomPos()].position + new Vector3(0, 1.13f, 0);
    }

    public string winner;

    public void finish()
    {
        finished = true;
        winner = (playersPoint[PhotonNetwork.PlayerList[0].UserId] > playersPoint[PhotonNetwork.PlayerList[1].UserId])
            ? PhotonNetwork.PlayerList[0].UserId
            : PhotonNetwork.PlayerList[1].UserId;
        winner=(playersPoint[winner]>playersPoint[PhotonNetwork.PlayerList[2].UserId])?winner:PhotonNetwork.PlayerList[0].UserId;
        LevelUiManager.Instance.showFinish(winner);
    }
}