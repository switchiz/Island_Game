//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/CameraInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @CameraInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CameraInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraInput"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""09620f85-9ca8-41ee-b6bf-8a6eec7c96b7"",
            ""actions"": [
                {
                    ""name"": ""PlayerView"",
                    ""type"": ""Button"",
                    ""id"": ""a32cd176-a3c4-4ca7-8607-e59ebc4dba92"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TopView"",
                    ""type"": ""Button"",
                    ""id"": ""00733f89-3d7b-4518-96da-08bcda29cb00"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""20d68fb8-58cf-4bfa-af64-556dc41c75df"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerView"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""699bd3d2-23e3-4c04-a582-31d7322910b5"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TopView"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_PlayerView = m_Camera.FindAction("PlayerView", throwIfNotFound: true);
        m_Camera_TopView = m_Camera.FindAction("TopView", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Camera
    private readonly InputActionMap m_Camera;
    private List<ICameraActions> m_CameraActionsCallbackInterfaces = new List<ICameraActions>();
    private readonly InputAction m_Camera_PlayerView;
    private readonly InputAction m_Camera_TopView;
    public struct CameraActions
    {
        private @CameraInput m_Wrapper;
        public CameraActions(@CameraInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerView => m_Wrapper.m_Camera_PlayerView;
        public InputAction @TopView => m_Wrapper.m_Camera_TopView;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void AddCallbacks(ICameraActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraActionsCallbackInterfaces.Add(instance);
            @PlayerView.started += instance.OnPlayerView;
            @PlayerView.performed += instance.OnPlayerView;
            @PlayerView.canceled += instance.OnPlayerView;
            @TopView.started += instance.OnTopView;
            @TopView.performed += instance.OnTopView;
            @TopView.canceled += instance.OnTopView;
        }

        private void UnregisterCallbacks(ICameraActions instance)
        {
            @PlayerView.started -= instance.OnPlayerView;
            @PlayerView.performed -= instance.OnPlayerView;
            @PlayerView.canceled -= instance.OnPlayerView;
            @TopView.started -= instance.OnTopView;
            @TopView.performed -= instance.OnTopView;
            @TopView.canceled -= instance.OnTopView;
        }

        public void RemoveCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    public interface ICameraActions
    {
        void OnPlayerView(InputAction.CallbackContext context);
        void OnTopView(InputAction.CallbackContext context);
    }
}
