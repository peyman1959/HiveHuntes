using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class LevelUiManager : MonoSingleton<LevelUiManager>
    {
        public Text time;
        public int matchtime=120;
        public Text[] scorees;
        public Image[] colors;
        public Image ammoSlider;
        public GameObject[] weapons;
        public GameObject gameUi,endUi, won, lost;
        private void Start()
        {
            InvokeRepeating("timer",2f,1f);
        }

        void timer()
        {
            matchtime--;
            TimeSpan ts = TimeSpan.FromSeconds(matchtime);
            time.text = ts.ToString();
            if (matchtime==0)
            {
                LevelManager.Instance.finish();
            }
        }
        public void UpdateScore()
        {
            scorees[0].text = PhotonNetwork.PlayerList[0].NickName + " : " + LevelManager.Instance.playersPoint[PhotonNetwork.PlayerList[0].UserId];
            scorees[1].text = PhotonNetwork.PlayerList[1].NickName + " : " + LevelManager.Instance.playersPoint[PhotonNetwork.PlayerList[1].UserId];
            scorees[2].text = PhotonNetwork.PlayerList[2].NickName + " : " + LevelManager.Instance.playersPoint[PhotonNetwork.PlayerList[2].UserId];
        }

        public void setWeapons(string wpnName)
        {
            ammoSlider.fillAmount = 1;
            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i].SetActive(false);
            }

            switch (wpnName)
            {
                case "Pistol":
                    weapons[0].SetActive(true);
                    break;
                case "Handgun":
                    weapons[1].SetActive(true);
                    break;
                case "MP5":
                    weapons[2].SetActive(true);
                    break;
                case "AK47":
                    weapons[3].SetActive(true);
                    break;
                
            }
        }

        public void btnBacktoMenu()
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
        }

        public void showFinish(string winner)
        {
            Cursor.visible = true;
            gameUi.SetActive(false);
            endUi.SetActive(true);
            won.SetActive(PhotonNetwork.LocalPlayer.UserId==winner);
            lost.SetActive(PhotonNetwork.LocalPlayer.UserId!=winner);
        }

        public void setColors()
        {
            colors[0].color = LevelManager.Instance.playersColor[PhotonNetwork.PlayerList[0].UserId];
            colors[1].color = LevelManager.Instance.playersColor[PhotonNetwork.PlayerList[1].UserId];
            colors[2].color = LevelManager.Instance.playersColor[PhotonNetwork.PlayerList[2].UserId];
        }
    }
}