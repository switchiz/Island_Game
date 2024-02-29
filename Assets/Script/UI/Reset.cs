using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    ResetInput reset;
    void Awake()
    {
        reset = new();
    }

    private void OnEnable()
    {
        reset.Reset.Enable();
        reset.Reset.Reset.performed += Resetscene;
    }

    private void OnDisable()
    {
        reset.Reset.Reset.performed -= Resetscene;
        reset.Reset.Disable();

    }

    private void Resetscene(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("Title");
    }
}
