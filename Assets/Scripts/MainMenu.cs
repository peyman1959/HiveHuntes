using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourPunCallbacks
{
    public List<GameObject> panels;
    public Text status;
    public InputField nickName;
    public int playerCount;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void btnPlay()
    {
        PhotonNetwork.JoinRandomRoom();
        status.text = "Joining Room";
        panels[0].SetActive(true);
        panels[1].SetActive(false);
    }

    public void btnExit()
    {
        Application.Quit();
    }
    public override void OnConnectedToMaster()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions=new RoomOptions();
        roomOptions.MaxPlayers = 3;
        roomOptions.PublishUserId = true;
        PhotonNetwork.CreateRoom("",roomOptions);
    }

    public override void OnJoinedRoom()
    {
        status.text = "Waiting For Other Players";
        PhotonNetwork.LocalPlayer.NickName = nickName.text;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
            if (PhotonNetwork.PlayerList.Length==playerCount)
            {
                status.text = "Loading....";
                SceneManager.LoadScene(1);
            }
        }
}
