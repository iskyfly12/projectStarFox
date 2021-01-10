// GENERATED AUTOMATICALLY FROM 'Assets/Resources/InputActions_StarFox.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions_StarFox : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions_StarFox()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions_StarFox"",
    ""maps"": [
        {
            ""name"": ""PlayerController"",
            ""id"": ""f780f470-cd57-4b81-a943-f8d1caca999a"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""2519572c-1217-4f30-a0f5-2d769a8cf0a9"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Boost"",
                    ""type"": ""Button"",
                    ""id"": ""532edfbb-08bb-4ad1-96d7-89b3963f4d36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Break"",
                    ""type"": ""Button"",
                    ""id"": ""0c4d248c-3e65-4314-8670-b8f4c31884f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""5b87fa14-ed21-46f7-963f-dd0b687acbc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left Lean"",
                    ""type"": ""Button"",
                    ""id"": ""d2f41c29-4bae-4562-bf44-e520801f45d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right Lean"",
                    ""type"": ""Button"",
                    ""id"": ""5fc39a35-593b-4d95-a909-069503e6f588"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Left BarrelRoll"",
                    ""type"": ""Button"",
                    ""id"": ""dbc244a8-2583-4b68-a42a-8d8b4eb6de02"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right BarrelRoll"",
                    ""type"": ""Button"",
                    ""id"": ""c87b944c-dac5-4afc-8362-a80deb68e252"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""MultiTap(pressPoint=0.01)""
                },
                {
                    ""name"": ""Charge Laser"",
                    ""type"": ""Button"",
                    ""id"": ""d934a197-ee2c-4053-91ad-72d87b4d6421"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.4)""
                },
                {
                    ""name"": ""Bomb"",
                    ""type"": ""Button"",
                    ""id"": ""28ddda26-3674-42e7-b0ab-ae103183bd4e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Somersult"",
                    ""type"": ""Button"",
                    ""id"": ""6c5b1930-a486-45e3-a84d-c77c5e74ea4c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""U-Turn"",
                    ""type"": ""Button"",
                    ""id"": ""71e9e658-b7e9-400d-929c-37f3a206d24c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b618a5ce-2122-473c-b4f2-6a988fc89132"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a8fb38a2-3a06-4d27-a695-8e44ee1513b4"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e6ce71f9-56c9-414d-b5a8-2d5fd396d81f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c598bff7-e1ba-42e7-9237-21c98d7baa8f"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""21ce901c-597d-4035-a107-60a6fc83aca0"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""65585291-b594-4faa-836d-f08a7100041c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f01a1b37-e9b9-4d2d-871e-a214edb83189"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f9059f02-e8c7-4b50-99be-d594a15b231d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""935242c3-5361-4034-879e-3df80b5bb525"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a3521f94-a6c9-4a62-b22c-650f21ead4f8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e5aa2ca1-35e4-4dfd-863d-d4d35d1af5e6"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6cf0d828-07ff-40c0-8d66-bc4c93bfdb45"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Boost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35ae8e03-9e4e-4de7-b154-a62076051ba4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Break"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a5555c9-b06e-4f31-8410-2948c4e7898d"",
                    ""path"": ""<Keyboard>/alt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Break"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36f13c42-1e00-47a4-a95b-116d7fbee8f9"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67afdd2b-46c1-4dad-9a55-cbb3789fa0e8"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ea1c8db-f422-4309-8c37-ead727d6b20e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Left Lean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""06276a22-a10b-4eeb-b7c9-eb35dbd97753"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Left Lean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31347481-a97c-4243-98ee-3cecb42ef406"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Right Lean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de117e6b-0164-4e5a-b911-37ea5edd36ea"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Right Lean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e37612c9-cc3b-49ec-a48d-73ad7866e3cd"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""MultiTap(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Left BarrelRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb3a8e30-73e6-48ba-9760-9e103f28bd2e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""MultiTap(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Left BarrelRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e41ce32c-1e51-4a16-be19-4341e8a64818"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Right BarrelRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f9e1420-e689-4d7f-b11c-ce113e784de3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Right BarrelRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b992253-0c2c-42e1-b9e8-43c681f8e6f6"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Charge Laser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3bf3981-4998-4215-9e52-0011829c898a"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Charge Laser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe580708-6e29-4877-a467-35df427862d9"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Bomb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b8585f9-28f9-4d6d-bbd1-16ab55158002"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Bomb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4513269-8c9d-4fa0-902d-7535cde4b422"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""Somersult"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b074eea-d109-4f80-9e75-d229654ef831"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""Somersult"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1f200cb-28e6-45da-88e8-6cd444093bc1"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamepadInput"",
                    ""action"": ""U-Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6fcab75a-f9aa-4bd8-b24d-fd37cf40d387"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput"",
                    ""action"": ""U-Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GamepadInput"",
            ""bindingGroup"": ""GamepadInput"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyboardInput"",
            ""bindingGroup"": ""KeyboardInput"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerController
        m_PlayerController = asset.FindActionMap("PlayerController", throwIfNotFound: true);
        m_PlayerController_Movement = m_PlayerController.FindAction("Movement", throwIfNotFound: true);
        m_PlayerController_Boost = m_PlayerController.FindAction("Boost", throwIfNotFound: true);
        m_PlayerController_Break = m_PlayerController.FindAction("Break", throwIfNotFound: true);
        m_PlayerController_Fire = m_PlayerController.FindAction("Fire", throwIfNotFound: true);
        m_PlayerController_LeftLean = m_PlayerController.FindAction("Left Lean", throwIfNotFound: true);
        m_PlayerController_RightLean = m_PlayerController.FindAction("Right Lean", throwIfNotFound: true);
        m_PlayerController_LeftBarrelRoll = m_PlayerController.FindAction("Left BarrelRoll", throwIfNotFound: true);
        m_PlayerController_RightBarrelRoll = m_PlayerController.FindAction("Right BarrelRoll", throwIfNotFound: true);
        m_PlayerController_ChargeLaser = m_PlayerController.FindAction("Charge Laser", throwIfNotFound: true);
        m_PlayerController_Bomb = m_PlayerController.FindAction("Bomb", throwIfNotFound: true);
        m_PlayerController_Somersult = m_PlayerController.FindAction("Somersult", throwIfNotFound: true);
        m_PlayerController_UTurn = m_PlayerController.FindAction("U-Turn", throwIfNotFound: true);
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

    // PlayerController
    private readonly InputActionMap m_PlayerController;
    private IPlayerControllerActions m_PlayerControllerActionsCallbackInterface;
    private readonly InputAction m_PlayerController_Movement;
    private readonly InputAction m_PlayerController_Boost;
    private readonly InputAction m_PlayerController_Break;
    private readonly InputAction m_PlayerController_Fire;
    private readonly InputAction m_PlayerController_LeftLean;
    private readonly InputAction m_PlayerController_RightLean;
    private readonly InputAction m_PlayerController_LeftBarrelRoll;
    private readonly InputAction m_PlayerController_RightBarrelRoll;
    private readonly InputAction m_PlayerController_ChargeLaser;
    private readonly InputAction m_PlayerController_Bomb;
    private readonly InputAction m_PlayerController_Somersult;
    private readonly InputAction m_PlayerController_UTurn;
    public struct PlayerControllerActions
    {
        private @InputActions_StarFox m_Wrapper;
        public PlayerControllerActions(@InputActions_StarFox wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerController_Movement;
        public InputAction @Boost => m_Wrapper.m_PlayerController_Boost;
        public InputAction @Break => m_Wrapper.m_PlayerController_Break;
        public InputAction @Fire => m_Wrapper.m_PlayerController_Fire;
        public InputAction @LeftLean => m_Wrapper.m_PlayerController_LeftLean;
        public InputAction @RightLean => m_Wrapper.m_PlayerController_RightLean;
        public InputAction @LeftBarrelRoll => m_Wrapper.m_PlayerController_LeftBarrelRoll;
        public InputAction @RightBarrelRoll => m_Wrapper.m_PlayerController_RightBarrelRoll;
        public InputAction @ChargeLaser => m_Wrapper.m_PlayerController_ChargeLaser;
        public InputAction @Bomb => m_Wrapper.m_PlayerController_Bomb;
        public InputAction @Somersult => m_Wrapper.m_PlayerController_Somersult;
        public InputAction @UTurn => m_Wrapper.m_PlayerController_UTurn;
        public InputActionMap Get() { return m_Wrapper.m_PlayerController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControllerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControllerActions instance)
        {
            if (m_Wrapper.m_PlayerControllerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnMovement;
                @Boost.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnBoost;
                @Boost.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnBoost;
                @Boost.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnBoost;
                @Break.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnBreak;
                @Break.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnBreak;
                @Break.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnBreak;
                @Fire.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnFire;
                @LeftLean.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnLeftLean;
                @LeftLean.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnLeftLean;
                @LeftLean.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnLeftLean;
                @RightLean.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnRightLean;
                @RightLean.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnRightLean;
                @RightLean.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnRightLean;
                @LeftBarrelRoll.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnLeftBarrelRoll;
                @LeftBarrelRoll.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnLeftBarrelRoll;
                @LeftBarrelRoll.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnLeftBarrelRoll;
                @RightBarrelRoll.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnRightBarrelRoll;
                @RightBarrelRoll.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnRightBarrelRoll;
                @RightBarrelRoll.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnRightBarrelRoll;
                @ChargeLaser.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnChargeLaser;
                @ChargeLaser.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnChargeLaser;
                @ChargeLaser.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnChargeLaser;
                @Bomb.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnBomb;
                @Bomb.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnBomb;
                @Bomb.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnBomb;
                @Somersult.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnSomersult;
                @Somersult.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnSomersult;
                @Somersult.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnSomersult;
                @UTurn.started -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnUTurn;
                @UTurn.performed -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnUTurn;
                @UTurn.canceled -= m_Wrapper.m_PlayerControllerActionsCallbackInterface.OnUTurn;
            }
            m_Wrapper.m_PlayerControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Boost.started += instance.OnBoost;
                @Boost.performed += instance.OnBoost;
                @Boost.canceled += instance.OnBoost;
                @Break.started += instance.OnBreak;
                @Break.performed += instance.OnBreak;
                @Break.canceled += instance.OnBreak;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @LeftLean.started += instance.OnLeftLean;
                @LeftLean.performed += instance.OnLeftLean;
                @LeftLean.canceled += instance.OnLeftLean;
                @RightLean.started += instance.OnRightLean;
                @RightLean.performed += instance.OnRightLean;
                @RightLean.canceled += instance.OnRightLean;
                @LeftBarrelRoll.started += instance.OnLeftBarrelRoll;
                @LeftBarrelRoll.performed += instance.OnLeftBarrelRoll;
                @LeftBarrelRoll.canceled += instance.OnLeftBarrelRoll;
                @RightBarrelRoll.started += instance.OnRightBarrelRoll;
                @RightBarrelRoll.performed += instance.OnRightBarrelRoll;
                @RightBarrelRoll.canceled += instance.OnRightBarrelRoll;
                @ChargeLaser.started += instance.OnChargeLaser;
                @ChargeLaser.performed += instance.OnChargeLaser;
                @ChargeLaser.canceled += instance.OnChargeLaser;
                @Bomb.started += instance.OnBomb;
                @Bomb.performed += instance.OnBomb;
                @Bomb.canceled += instance.OnBomb;
                @Somersult.started += instance.OnSomersult;
                @Somersult.performed += instance.OnSomersult;
                @Somersult.canceled += instance.OnSomersult;
                @UTurn.started += instance.OnUTurn;
                @UTurn.performed += instance.OnUTurn;
                @UTurn.canceled += instance.OnUTurn;
            }
        }
    }
    public PlayerControllerActions @PlayerController => new PlayerControllerActions(this);
    private int m_GamepadInputSchemeIndex = -1;
    public InputControlScheme GamepadInputScheme
    {
        get
        {
            if (m_GamepadInputSchemeIndex == -1) m_GamepadInputSchemeIndex = asset.FindControlSchemeIndex("GamepadInput");
            return asset.controlSchemes[m_GamepadInputSchemeIndex];
        }
    }
    private int m_KeyboardInputSchemeIndex = -1;
    public InputControlScheme KeyboardInputScheme
    {
        get
        {
            if (m_KeyboardInputSchemeIndex == -1) m_KeyboardInputSchemeIndex = asset.FindControlSchemeIndex("KeyboardInput");
            return asset.controlSchemes[m_KeyboardInputSchemeIndex];
        }
    }
    public interface IPlayerControllerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnBoost(InputAction.CallbackContext context);
        void OnBreak(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnLeftLean(InputAction.CallbackContext context);
        void OnRightLean(InputAction.CallbackContext context);
        void OnLeftBarrelRoll(InputAction.CallbackContext context);
        void OnRightBarrelRoll(InputAction.CallbackContext context);
        void OnChargeLaser(InputAction.CallbackContext context);
        void OnBomb(InputAction.CallbackContext context);
        void OnSomersult(InputAction.CallbackContext context);
        void OnUTurn(InputAction.CallbackContext context);
    }
}
