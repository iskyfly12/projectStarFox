using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputBehaviour : MonoBehaviour
{
    [Header("Data Output")]
    public PlayerData playerData;

    [Header("Input Component")]
    public PlayerInput playerInput;

    private Vector2 inputValue;

    void Awake()
    {
        //Default settings
        playerData.shipState = ShipState.TrackMode;
        playerData.acrobaticState = AcrobaticState.None;
        playerData.buffState = BuffState.None;
        playerData.chargeWeaponState = ChargeWeaponState.None;
        playerData.enemyDetectionState = EnemyDetectionState.None;
        playerData.leanState = LeanState.None;
        playerData.weaponState = WeaponState.LvOne;

        playerData.buffAmount = 100;
        playerData.lifeAmount = 100;
        playerData.numLifes = 1;
        playerData.numSmartBombs = 3;

        playerData.onInputActive += (bool state) => { if (state) playerInput.ActivateInput(); else playerInput.DeactivateInput(); };
    }

    void Start()
    {
        playerData.OnUpdateSmartBomb(0);
    }

    void Update()
    {
        UpdateData();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        inputValue = value.ReadValue<Vector2>();
    }

    public void OnBoost(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            playerData.buffState = BuffState.Boost;
            playerData.OnBoost(true);
        }
        else if (value.canceled)
        {
            playerData.buffState = BuffState.None;
            playerData.OnBoost(false);
        }
    }

    public void OnBreak(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            playerData.buffState = BuffState.Break;
            playerData.OnBreak(true);
        }
        else if (value.canceled)
        {
            playerData.buffState = BuffState.None;
            playerData.OnBreak(false);
        }
    }

    public void OnLeftLean(InputAction.CallbackContext value)
    {
        if (value.started)
            playerData.leanAxisInput = -1;
        else if (value.canceled)
            playerData.leanAxisInput = 0;
    }

    public void OnRightLean(InputAction.CallbackContext value)
    {
        if (value.started)
            playerData.leanAxisInput = 1;
        else if (value.canceled)
            playerData.leanAxisInput = 0;
    }

    public void OnLeftBarrelRoll(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            playerData.leanAxisInput = -1;
            playerData.OnBarrelRoll(-1);
            playerData.leanState = LeanState.BarrelRoll;
        }
    }

    public void OnRightBarrelRoll(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            playerData.leanAxisInput = 1;
            playerData.OnBarrelRoll(1);
            playerData.leanState = LeanState.BarrelRoll;
        }
    }

    public void OnFireLaser(InputAction.CallbackContext value)
    {
        if (value.performed)
            playerData.OnFireLaser();
    }

    public void OnChargeLaser(InputAction.CallbackContext value)
    {
        if (value.performed)
            playerData.chargeWeaponState = ChargeWeaponState.Charging;
        else if (value.canceled)
            playerData.OnReleaseLaser();
    }

    public void OnFireSmartBomb(InputAction.CallbackContext value)
    {
        if (value.performed)
            playerData.OnFireSmartBomb();
    }

    public void OnSomersult(InputAction.CallbackContext value)
    {
        if (value.performed)
            playerData.OnSomersult();
    }

    public void OnUturn(InputAction.CallbackContext value)
    {
        if (value.performed)
            playerData.OnUTurn();
    }

    void UpdateData() => playerData.UpdateInputData(inputValue);
}
