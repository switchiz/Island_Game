using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;
public class Camera_view : MonoBehaviour
{
    [SerializeField]
    CameraInput camerainputs;
    public CinemachineVirtualCamera[] vcams;


    private void Awake()
    {
        camerainputs = new();
        vcams = FindObjectsByType<CinemachineVirtualCamera>(FindObjectsSortMode.None);


    }


    private void OnEnable()
    {
        camerainputs.Camera.Enable();
        camerainputs.Camera.PlayerView.performed += view_Player;
        camerainputs.Camera.TopView.performed += view_Top;
    }

    private void OnDisable()
    {

        camerainputs.Camera.PlayerView.performed -= view_Player;
        camerainputs.Camera.TopView.performed -= view_Top;
        camerainputs.Camera.Disable();
    }

    private void view_Top(InputAction.CallbackContext context)
    {
        vcams[0].Priority = 5;
        vcams[1].Priority = 10;
    }

    private void view_Player(InputAction.CallbackContext context)
    {
        vcams[0].Priority = 10;
        vcams[1].Priority = 5;
    }
}
