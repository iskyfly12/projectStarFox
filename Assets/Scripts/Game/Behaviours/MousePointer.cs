using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MousePointer : MonoBehaviour
{
    public enum DeviceOn { Keyboard, Gamepad }
    public DeviceOn deviceOn;

    public ControlDeviceType currentControlDevice;
    public enum ControlDeviceType { KeyboardAndMouse, Gamepad, }

    public GameObject cursorImage;
    public float smoothMovement;
    public float speed;

    private PlayerInput playerInput;
    private Vector2 inputValue;
    private Vector3 velocity = Vector3.zero;
    private Vector3 lastMousePosition;

    private Camera cam;
    private Button buttonSelected;
    private bool isButtonSelected;

    private void Awake()
    {
        cam = CameraManager.mainCamera;
    }

    void Start()
    {
        Cursor.visible = false;
        inputValue = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    void LateUpdate()
    {
        //if (deviceOn == DeviceOn.Keyboard)
            cursorImage.transform.position = Vector3.SmoothDamp(cursorImage.transform.position, Input.mousePosition, ref velocity, smoothMovement);
        //if (deviceOn == DeviceOn.Gamepad)
            //cursorImage.transform.position = Vector3.SmoothDamp(cursorImage.transform.position, inputValue, ref velocity, smoothMovement);

        ClampPosition(transform);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        inputValue += value.ReadValue<Vector2>() * speed;
    }

    public void OnPress(InputAction.CallbackContext value)
    {
        if (value.performed && isButtonSelected)
            buttonSelected.onClick.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        buttonSelected = other.GetComponent<Button>();
        buttonSelected.OnSelect(null);
        isButtonSelected = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //buttonSelected = other.GetComponent<Button>();
        buttonSelected.OnDeselect(null);
        isButtonSelected = false;
    }

    void ClampPosition(Transform transform)
    {
        //Get the transform point
        Vector2 screenPosition = cam.ScreenToViewportPoint(transform.position);

        //Clamp the position
        screenPosition.x = Mathf.Clamp01(screenPosition.x);
        screenPosition.y = Mathf.Clamp01(screenPosition.y);

        //Set the transform point
        transform.position = cam.ViewportToScreenPoint(screenPosition);
    }
}
