using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonInstantiater : MonoBehaviour
{
    public string prefab;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateSceneObject(prefab, transform.position, transform.rotation, 0);
        }
    }


}
