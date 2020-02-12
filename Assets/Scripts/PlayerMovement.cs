using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, turnSpeed;
    public float camRayLength;
    public LayerMask floorMask;
    private Transform tr;
    private float h, v;
    private Vector3 pos, playerToMouse;
    private Ray camRay;
    RaycastHit floorHit;
    public Transform sight;
    public Camera cam;
    private Plane plane;
    PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        plane = new Plane(Vector3.up, Vector3.zero);
        tr = transform;
        pos = new Vector3();
    }

    private void Update()
    {
        if (!_playerHealth.isAlive || LevelManager.Instance.finished)
            return;
        Move();
        Turning();
    }

    void Move()
    {
        h = ControlFreak2.CF2Input.GetAxis("Horizontal");
        v = ControlFreak2.CF2Input.GetAxis("Vertical");
        pos.x = h * speed * Time.deltaTime;
        pos.z = v * speed * Time.deltaTime;
        tr.Translate(pos);
    }

    void Turning()
    {
#if UNITY_ANDROID

        tr.Rotate(0,ControlFreak2.CF2Input.GetAxis("Mouse X")*turnSpeed,0);
#else
        camRay = cam.ScreenPointToRay(ControlFreak2.CF2Input.mousePosition);
        float distance;
        if (plane.Raycast(camRay, out distance))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            playerToMouse = camRay.GetPoint(distance) - transform.position;
            sight.position = camRay.GetPoint(distance)+new Vector3(0,1.13f,0);
            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

            tr.rotation = Quaternion.Lerp(tr.rotation,newRotatation,Time.deltaTime*turnSpeed);

    }
#endif
}

}