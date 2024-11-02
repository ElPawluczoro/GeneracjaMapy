using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class MoveCamera : MonoBehaviour
    {
        private Camera mainCamera;
        [SerializeField] float cameraSpeed = 5;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            Move();
        }

        private void Move()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                mainCamera.transform.position += new Vector3(0, cameraSpeed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                mainCamera.transform.position += new Vector3(0, -cameraSpeed * Time.deltaTime, 0);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                mainCamera.transform.position += new Vector3(-cameraSpeed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                mainCamera.transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
            }
        }
    }
}