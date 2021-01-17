// GENERATED AUTOMATICALLY FROM 'Assets/Input/MenuControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MenuControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MenuControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MenuControls"",
    ""maps"": [
        {
            ""name"": ""Inventory"",
            ""id"": ""3d33a3fc-cf76-4372-9304-e98a349549d1"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""47a49595-fde2-4920-861d-b8ee2dc0a93f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""afd599bd-6985-4f81-959d-e462d08064b6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""CloseTextBox"",
                    ""type"": ""Button"",
                    ""id"": ""6df93465-04dd-4cd0-8691-331293e4c37f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""770632e1-6417-4705-9bf2-70ae920d3aa9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b502e5eb-da6a-4b15-b548-4fb5abeaf16f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b198216-5624-47f1-bc16-be9dd7c6da63"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseTextBox"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_Up = m_Inventory.FindAction("Up", throwIfNotFound: true);
        m_Inventory_Down = m_Inventory.FindAction("Down", throwIfNotFound: true);
        m_Inventory_CloseTextBox = m_Inventory.FindAction("CloseTextBox", throwIfNotFound: true);
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

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_Up;
    private readonly InputAction m_Inventory_Down;
    private readonly InputAction m_Inventory_CloseTextBox;
    public struct InventoryActions
    {
        private @MenuControls m_Wrapper;
        public InventoryActions(@MenuControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_Inventory_Up;
        public InputAction @Down => m_Wrapper.m_Inventory_Down;
        public InputAction @CloseTextBox => m_Wrapper.m_Inventory_CloseTextBox;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @Up.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnDown;
                @CloseTextBox.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnCloseTextBox;
                @CloseTextBox.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnCloseTextBox;
                @CloseTextBox.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnCloseTextBox;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @CloseTextBox.started += instance.OnCloseTextBox;
                @CloseTextBox.performed += instance.OnCloseTextBox;
                @CloseTextBox.canceled += instance.OnCloseTextBox;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);
    public interface IInventoryActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnCloseTextBox(InputAction.CallbackContext context);
    }
}
