using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Vector3 distance;
        public float dumping;
        private Transform tr;
        private void Start()
        {
            tr = transform;
        }

        private void LateUpdate()
        {

                tr.position = Vector3.Lerp(tr.position, tr.position + distance, Time.deltaTime * dumping);
            
        }
    }
}