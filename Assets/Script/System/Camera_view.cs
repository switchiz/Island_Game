using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class Camera_view : MonoBehaviour
{
    [SerializeField]
    CameraInput camerainputs;
    public CinemachineVirtualCamera[] vcams;


    private void Awake()
    {
        camerainputs = new();

    }


    private void OnEnable()
    {
        camerainputs.Camera.Enable();
        camerainputs.Camera.PlayerView.performed += view_Player;
        camerainputs.Camera.TopView.performed += view_Top;
        camerainputs.Camera.TopView_2.performed += view_Top_2;
    }



    private void OnDisable()
    {
        camerainputs.Camera.TopView_2.performed -= view_Top_2;
        camerainputs.Camera.PlayerView.performed -= view_Player;
        camerainputs.Camera.TopView.performed -= view_Top;
        camerainputs.Camera.Disable();
    }
    private void view_Player(InputAction.CallbackContext context) // 1
    {
        vcams[0].Priority = 10;
        vcams[1].Priority = 5;
        vcams[2].Priority = 4;
    }

    private void view_Top(InputAction.CallbackContext context) // 2
    {
        vcams[0].Priority = 5;
        vcams[1].Priority = 10;
        vcams[2].Priority = 4;
    }


    private void view_Top_2(InputAction.CallbackContext context) // 3
    {
        vcams[0].Priority = 4;
        vcams[1].Priority = 5;
        vcams[2].Priority = 10;
    }
}
