using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/ShipData/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Movement Data")]
    public bool physicMovement;
    [Range(0, 100)]
    public float trackSpeed;
    [Range(0, 100)]
    public float allRangeSpeed;
    [Range(0, 1)]
    public float smoothAllRangeMovement;
    [Range(0, 100)]
    public float localSpeed;
    [Range(0, 1)]
    public float smoothLocalSpeed;
    [Range(0, 100)]
    public float boostSpeed;
    [Range(0, 100)]
    public float breakSpeed;
    [Range(0, 10000)]
    public float aimSpeed;
    [Range(0, 1)]
    public float smoothAimSpeed;
    [Range(0, 10)]
    public float smoothRotateSpeed;
    [Range(0, 90)]
    public float leanAngle;
    [Range(0, 90)]
    public float horizontalLeanLimit;
    [Range(0, 1)]
    public float leanSpeed;
    [Range(0, 1)]
    public float barrelRollSpeed;
    [Range(0, 100)]
    public float acrobaticSomersultSpeed;
    [Range(0, 100)]
    public float acrobaticUTurnSpeed;

    [Header("Shoot Data")]
    [Range(0, 10)]
    public float timeCharged;

    [Header("Buff Data")]
    [Range(0, 10)]
    public float amountBoostIncrease;
    [Range(0, 10)]
    public float amountBoostDecrease;
    [Range(1, 10)]
    public int maxSmartBomb;

    [Header("Wings Data")]
    public Vector3 spaceWings;
    public Vector3 atmosphereWings;
    public Vector3 allRangeWings;

    [Header("Inputs")]
    [ReadOnly] public Vector2 movementInput;
    [ReadOnly] public int leanAxisInput;
    [ReadOnly] public int lifeAmount;
    [ReadOnly] public float buffAmount;
    [ReadOnly] public int numLifes;
    [ReadOnly] public int numSmartBombs;
    [ReadOnly] public int numGoldRings;
    [ReadOnly] public bool isDamageEffect;

    [Header("State")]
    [ReadOnly] public ShipState shipState;
    [ReadOnly] public LeanState leanState;
    [ReadOnly] public BuffState buffState;
    [ReadOnly] public AcrobaticState acrobaticState;
    [ReadOnly] public WeaponState weaponState;
    [ReadOnly] public ChargeWeaponState chargeWeaponState;
    [ReadOnly] public EnemyDetectionState enemyDetectionState;

    #region Movement Actions
    public event Action<int> onBarrelRoll;
    public void OnBarrelRoll(int axis) { onBarrelRoll?.Invoke(axis); }

    public event Action<bool> onBoost;
    public void OnBoost(bool state) { onBoost?.Invoke(state); }

    public event Action<bool> onBreak;
    public void OnBreak(bool state) { onBreak?.Invoke(state); }

    public event Action onSomersult;
    public void OnSomersult() { onSomersult?.Invoke(); }

    public event Action onUTurn;
    public void OnUTurn() { onUTurn?.Invoke(); }
    #endregion

    #region Rig Actions
    public event Action<Vector3, float> onOpenWings;
    public void OnOpenWings(Vector3 rotation, float time) { onOpenWings?.Invoke(rotation, time); }

    public event Action<bool> onOpenHyperLaser;
    public void OnOpenHyperLaser(bool state) { onOpenHyperLaser?.Invoke(state); }
    #endregion

    #region Weapon Actions
    public event Action onFireLaser;
    public void OnFireLaser() { onFireLaser?.Invoke(); }

    public event Action onFireSmartBomb;
    public void OnFireSmartBomb() { onFireSmartBomb?.Invoke(); }

    public event Action onChargeLaser;
    public void OnChargeLaser() { onChargeLaser?.Invoke(); }

    public event Action onReleaseLaser;
    public void OnReleaseLaser() { onReleaseLaser?.Invoke(); }
    #endregion

    #region UI
    public event Action onUpdateLifes;
    public void OnUpdateLifes() { onUpdateLifes?.Invoke(); }

    public event Action onUpdateHP;
    public void OnUpdateHP() { onUpdateHP?.Invoke(); }

    public event Action<int> onUpdateSmartBomb;
    public void OnUpdateSmartBomb(int numAdd) { onUpdateSmartBomb?.Invoke(numAdd); }
    #endregion

    #region Input
    public event Action<bool> onInputActive;
    public void OnInputActive(bool state) { onInputActive?.Invoke(state); }
    #endregion

    #region Visual
    public event Action onDamageEffect;
    public void OnDamageEffect() { onDamageEffect?.Invoke(); }
    #endregion

    public void UpdateInputData(Vector2 newMovement)
    {
        movementInput = newMovement;
    }

    public void UpdateStateInput(ShipState newShipState, LeanState newLeanState, BuffState newBuffState, AcrobaticState newAcrobaticState)
    {
        shipState = newShipState;
        leanState = newLeanState;
        buffState = newBuffState;
        acrobaticState = newAcrobaticState;
    }
}

public enum ShipState { AllRangeMode, TrackMode }
public enum BuffState { None, Boost, Break }
public enum AcrobaticState { None, Somersult, UTurn }
public enum LeanState { None, LeftLean, RightLean, BarrelRoll }
public enum WeaponState { LvOne, LvTwo, LvThree }
public enum ChargeWeaponState { None, Charging, Charged, Released }
public enum EnemyDetectionState { None, CanDetect, Detected }
public enum BuffAmountState { None, CanFill, Filling, }

