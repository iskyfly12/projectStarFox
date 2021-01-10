using Cinemachine;
using DG.Tweening;
using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBehaviour : MonoBehaviour, IReflect, IDamagable, IStageLimit
{
    [Header("Player Settings")]
    public PlayerData data;

    [Header("Physics")]
    public Rigidbody _rigidbody;

    [Header("Paths")]
    public CinemachineDollyCart _stageDollyCart;
    public PathCreator _somersultPath;
    public PathCreator _uTurnPath;

    [Header("Layers")]
    public LayerMask _collisionCorretionLayer;

    [Header("References")]
    public Transform _playerModel;
    public Transform _armatureModel;
    public Transform _aimTarget;
    public Transform _playerTransform;
    public Transform _arwingTransform;
    public Transform _pointCollisionDetection;
    public Transform _arrow;

    [Header("All-Range Camera Animation")]
    public Animation _allRangeAnimation;

    private float forwardSpeed;
    private float distanceAcrobaticPath;
    private float smoothAcrobatic;
    private float delayDamage;

    Quaternion deltaRotation;
    private Vector2 smoothAimSpeed;
    private Vector2 smoothLocalSpeed;

    void Awake()
    {
        if (data.physicMovement)
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        else
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        _stageDollyCart.m_Speed = data.trackSpeed;
        forwardSpeed = data.allRangeSpeed;

        data.onBarrelRoll += (int axis) => StartCoroutine(BarrelRoll(axis));
        data.onBoost += (bool state) => Boost(state);
        data.onBreak += (bool state) => Break(state);
        data.onSomersult += () => DOAcrobatic(AcrobaticState.Somersult, null, 50, false);
        data.onUTurn += () => DOAcrobatic(AcrobaticState.UTurn, _playerTransform, 50, false);
    }

    void FixedUpdate()
    {
        CollisionCorrection();

        if (data.shipState == ShipState.AllRangeMode)
            AllRangeMove(_playerTransform, _arwingTransform, data.physicMovement);
        else
            LocalMove();

        if (data.leanAxisInput == 0)
            HorizontalLean(_armatureModel);
        else
            LeanRotation(_armatureModel);

        if (data.acrobaticState == AcrobaticState.None)
            ClampPosition(_arwingTransform, CameraManager.mainCamera);
        else
            PathUpdate(data.acrobaticState);

        if (data.buffAmount <= 0)
            ClearBuffs();

        if (data.shipState == ShipState.TrackMode || (data.shipState == ShipState.AllRangeMode && !data.physicMovement))
            LookRotation(_arwingTransform, _aimTarget);
    }

    #region Movement Calculations
    //Local movement X and Y by transform
    void LocalMove()
    {
        //Smooth value increment and decrement 
        smoothLocalSpeed.x = Mathf.SmoothStep(smoothLocalSpeed.x, data.movementInput.x, data.smoothLocalSpeed);
        smoothLocalSpeed.y = Mathf.SmoothStep(smoothLocalSpeed.y, data.movementInput.y, data.smoothLocalSpeed);

        //Change rigidbody velocity X and Y (increase value when lean)
        if ((data.movementInput.x > 0 && data.leanAxisInput == 1) || (data.movementInput.x < 0 && data.leanAxisInput == -1))
            _rigidbody.velocity = new Vector3(smoothLocalSpeed.x * 1.5f, smoothLocalSpeed.y, 0) * data.localSpeed;
        else
            _rigidbody.velocity = new Vector3(smoothLocalSpeed.x, smoothLocalSpeed.y, 0) * data.localSpeed;
    }

    void AllRangeMove(Transform parentTransform, Transform arwingTransform, bool isPhysic)
    {
        //Smooth value increment and decrement 
        smoothLocalSpeed.x = Mathf.SmoothStep(smoothLocalSpeed.x, data.movementInput.x, data.smoothAllRangeMovement);
        smoothLocalSpeed.y = Mathf.SmoothStep(smoothLocalSpeed.y, data.movementInput.y, data.smoothAllRangeMovement);

        if (isPhysic)
        {
            //Avoid Z rotation
            _rigidbody.rotation = Quaternion.Euler(arwingTransform.eulerAngles.x, arwingTransform.eulerAngles.y, 0);

            //Increase rotation value when lean
            if ((data.movementInput.x > 0 && data.leanAxisInput == 1) || (data.movementInput.x < 0 && data.leanAxisInput == -1))
                deltaRotation = Quaternion.Euler(new Vector3(-data.movementInput.y, data.movementInput.x * 1.5f, 0));
            else
                deltaRotation = Quaternion.Euler(new Vector3(-data.movementInput.y, data.movementInput.x, 0));

            //Rotate the ship
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

            //Constant forward movement
            _rigidbody.velocity = arwingTransform.forward * forwardSpeed;
        }
        else
        {
            //arwingTransform.localPosition = new Vector3(0, arwingTransform.localPosition.y, 0);
            //Rotate the parent Transform of the ship (increase value when lean)
            if ((data.movementInput.x > 0 && data.leanAxisInput == 1) || (data.movementInput.x < 0 && data.leanAxisInput == -1))
                parentTransform.Rotate(0, (smoothLocalSpeed.x * 1.5f), 0);
            else
                parentTransform.Rotate(0, (smoothLocalSpeed.x), 0);

            //Change rigidbody velocity Y
            _rigidbody.velocity = new Vector3(0, smoothLocalSpeed.y, 0) * (data.localSpeed / 2);

            //Move forward the parent based on the direction of the ship
            parentTransform.position += arwingTransform.forward * forwardSpeed * Time.fixedDeltaTime;
        }
        /*_rigidbody.rotation = Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y, 0);
        
        //Rotate the parent Transform of the ship (increase value when lean)
        if ((data.movementInput.x > 0 && data.leanAxisInput == 1) || (data.movementInput.x < 0 && data.leanAxisInput == -1))
            deltaRotation = Quaternion.Euler(new Vector3(-smoothLocalSpeed.y, smoothLocalSpeed.x * 1.5f, 0));//transform.Rotate(0, (smoothLocalSpeed.x * 1.5f), 0);
        else
            deltaRotation = Quaternion.Euler(new Vector3(-smoothLocalSpeed.y, smoothLocalSpeed.x, 0)); //transform.Rotate(0, (smoothLocalSpeed.x), 0);

        //Quaternion deltaRotation = Quaternion.Euler(new Vector3(-smoothLocalSpeed.y, smoothLocalSpeed.x, 0));
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);


        //Change rigidbody velocity Y
        //_rigidbody.velocity = new Vector3(0, smoothLocalSpeed.y, 0) * (data.localSpeed / 2);

        //Move forward the parent based on the direction of the ship
        //transform.position += direction.forward * forwardSpeed * Time.fixedDeltaTime;
        _rigidbody.velocity = direction.forward * forwardSpeed;*/
    }

    void LookRotation(Transform t, Transform aimTarget)
    {
        //Smooth value increment and decrement
        smoothAimSpeed.x = Mathf.SmoothStep(smoothAimSpeed.x, data.movementInput.x, data.smoothAimSpeed);
        smoothAimSpeed.y = Mathf.SmoothStep(smoothAimSpeed.y, data.movementInput.y, data.smoothAimSpeed);

        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(smoothAimSpeed.x, smoothAimSpeed.y, aimTarget.localPosition.z);
        t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * data.aimSpeed * Time.fixedDeltaTime);
    }

    void ClampPosition(Transform transform, Camera camera)
    {
        //Get the transform point
        Vector3 worldPosition = camera.WorldToViewportPoint(transform.position);

        //Clamp the position
        worldPosition.x = Mathf.Clamp01(worldPosition.x);
        worldPosition.y = Mathf.Clamp01(worldPosition.y);

        //Set the transform point
        transform.position = camera.ViewportToWorldPoint(worldPosition);
    }

    //Calculates the lean with an axis
    void LeanRotation(Transform model)
    {
        Vector3 targetEulerAngle = model.localEulerAngles;
        model.localEulerAngles = new Vector3(targetEulerAngle.x, targetEulerAngle.y, Mathf.LerpAngle(targetEulerAngle.z, -data.leanAxisInput * data.leanAngle, data.leanSpeed));
    }

    //Calculates lean when moves in X
    void HorizontalLean(Transform model)
    {
        Vector3 targetEulerAngle = model.localEulerAngles;
        model.localEulerAngles = new Vector3(targetEulerAngle.x, targetEulerAngle.y, Mathf.LerpAngle(targetEulerAngle.z, -smoothLocalSpeed.x * data.horizontalLeanLimit, data.leanSpeed));
    }
    #endregion

    #region Acrobatic Movement
    IEnumerator BarrelRoll(int axis)
    {
        if (data.leanState != LeanState.None)
            yield break;

        _armatureModel.DOLocalRotate(new Vector3(0, 0, 350 * -axis), data.barrelRollSpeed, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);

        AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.BarrelRoll));

        data.leanState = LeanState.BarrelRoll;
        yield return new WaitWhile(() => DOTween.IsTweening(_armatureModel));
        data.leanAxisInput = 0;
        data.leanState = LeanState.None;
    }

    IEnumerator Somersult()
    {
        ClearBuffs();
        _arrow.gameObject.SetActive(false);

        data.OnInputActive(false);
        data.acrobaticState = AcrobaticState.Somersult;

        CameraManager.SetLiveCamera("Somersult VCam");
        AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.Boost));

        _arwingTransform.parent = _playerTransform.parent;
        _somersultPath.transform.position = _arwingTransform.position;

        if (data.shipState == ShipState.AllRangeMode)
        {
            forwardSpeed = 0;
            _somersultPath.transform.rotation = _arwingTransform.rotation;

            _playerTransform.DOLocalRotate(_arwingTransform.eulerAngles, .25f);
            _playerTransform.DOMove(_somersultPath.path.GetPointAtDistance(_somersultPath.path.length), .25f);
        }
        else if (data.shipState == ShipState.TrackMode)
        {
            _stageDollyCart.m_Speed = 0;
            _somersultPath.transform.rotation = _playerTransform.rotation;
        }

        DOVirtual.Float(0, 2000, .5f, x => smoothAcrobatic = x);

        yield return new WaitWhile(() => distanceAcrobaticPath < _somersultPath.path.length);

        _arwingTransform.parent = _playerTransform;
        distanceAcrobaticPath = 0;

        if (data.shipState == ShipState.TrackMode)
        {
            _stageDollyCart.m_Speed = data.trackSpeed;
            CameraManager.SetLiveCamera("Track VCam");
        }

        if (data.shipState == ShipState.AllRangeMode)
        {
            _playerTransform.DOLocalRotate(new Vector3(0, _playerTransform.localEulerAngles.y, 0), .25f);
            forwardSpeed = data.allRangeSpeed;
            CameraManager.SetLiveCamera("All Range VCam");
        }

        _armatureModel.localEulerAngles = Vector3.zero;
        _arrow.gameObject.SetActive(true);

        data.OnInputActive(true);
        data.acrobaticState = AcrobaticState.None;
    }

    IEnumerator Uturn(Transform direction)
    {
        ClearBuffs();
        _arrow.gameObject.SetActive(false);

        data.OnInputActive(false);
        data.acrobaticState = AcrobaticState.UTurn;

        CameraManager.SetLiveCamera("UTurn VCam");
        AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.Boost));

        _arwingTransform.parent = _playerTransform.parent;

        _uTurnPath.transform.position = _arwingTransform.position;
        _uTurnPath.transform.rotation = direction.localRotation;

        Quaternion rotateTransform = Quaternion.Euler(_playerTransform.eulerAngles.x, _uTurnPath.transform.eulerAngles.y + 180, _playerTransform.eulerAngles.z);
        _playerTransform.localRotation = rotateTransform;

        DOVirtual.Float(0, 2000, 1f, x => smoothAcrobatic = x).SetLoops(2, LoopType.Yoyo);

        _armatureModel.DOLocalRotate(new Vector3(_armatureModel.localEulerAngles.x, _armatureModel.localEulerAngles.y, 0), 1.5f).SetDelay(1f);

        yield return new WaitWhile(() => distanceAcrobaticPath < _uTurnPath.path.length);

        _playerTransform.localPosition = _arwingTransform.localPosition;
        _armatureModel.localEulerAngles = Vector3.zero;
        _arwingTransform.parent = _playerTransform;

        distanceAcrobaticPath = 0;
        forwardSpeed = data.allRangeSpeed;

        CameraManager.SetLiveCamera("All Range VCam");
        _arrow.gameObject.SetActive(true);

        data.OnInputActive(true);
        data.acrobaticState = AcrobaticState.None;
    }

    void PathUpdate(AcrobaticState state)
    {
        if (state == AcrobaticState.Somersult)
        {
            distanceAcrobaticPath += Time.deltaTime * data.acrobaticSomersultSpeed;

            _arwingTransform.position = _somersultPath.path.GetPointAtDistance(distanceAcrobaticPath, EndOfPathInstruction.Stop);
            _armatureModel.rotation = Quaternion.RotateTowards(_armatureModel.rotation, _somersultPath.path.GetRotationAtDistance(distanceAcrobaticPath), smoothAcrobatic * Time.deltaTime);
        }
        else if (state == AcrobaticState.UTurn)
        {
            distanceAcrobaticPath += Time.deltaTime * data.acrobaticUTurnSpeed;

            _arwingTransform.position = _uTurnPath.path.GetPointAtDistance(distanceAcrobaticPath, EndOfPathInstruction.Stop);
            _armatureModel.rotation = Quaternion.RotateTowards(_armatureModel.rotation, _uTurnPath.path.GetRotationAtDistance(distanceAcrobaticPath), smoothAcrobatic * Time.deltaTime);
        }
    }

    void DOAcrobatic(AcrobaticState acrobaticState, Transform direction, int amoutCost, bool forced)
    {
        if (!forced && amoutCost > data.buffAmount)
            return;

        if (acrobaticState == AcrobaticState.Somersult)
            StartCoroutine(Somersult());
        else if (acrobaticState == AcrobaticState.UTurn && data.shipState == ShipState.AllRangeMode)
            StartCoroutine(Uturn(direction));
        else
            return;

        data.buffAmount -= amoutCost;

        if (forced)
            data.buffAmount = 0;

        //StartCoroutine(_playerVisualHandler.AcrobaticVFX());
    }
    #endregion

    #region Buffs
    public void Boost(bool state)
    {
        if (state)
        {
            if (_stageDollyCart.isActiveAndEnabled)
                _stageDollyCart.m_Speed = data.boostSpeed;
            else
                forwardSpeed = data.boostSpeed;

            AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.Boost));
        }
        else
        {
            if (_stageDollyCart.isActiveAndEnabled)
                _stageDollyCart.m_Speed = data.trackSpeed;
            else
                forwardSpeed = data.allRangeSpeed;
        }
    }

    public void Break(bool state)
    {
        if (state)
        {
            if (_stageDollyCart.isActiveAndEnabled)
                _stageDollyCart.m_Speed = data.breakSpeed;
            else
                forwardSpeed = data.breakSpeed;

            AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.Break));
        }
        else
        {
            if (_stageDollyCart.isActiveAndEnabled)
                _stageDollyCart.m_Speed = data.trackSpeed;
            else
                forwardSpeed = data.allRangeSpeed;
        }
    }

    public void ClearBuffs()
    {
        Boost(false);
        Break(false);
    }

    #endregion

    #region Collision Corrections
    public void CollisionCorrection()
    {
        if (data.movementInput.x == 0 && data.movementInput.y == 0)
            return;

        int distanceX = 4;
        int distanceY = 3;

        if ((CollisionDetection(_pointCollisionDetection, Vector3.down, distanceY, _collisionCorretionLayer) && data.movementInput.y < 0) ||
            (CollisionDetection(_pointCollisionDetection, Vector3.up, distanceY, _collisionCorretionLayer) && data.movementInput.y > 0))
            data.movementInput = new Vector2(data.movementInput.x, 0);

        if ((CollisionDetection(_pointCollisionDetection, Vector3.left, distanceX, _collisionCorretionLayer) && data.movementInput.x < 0) ||
            (CollisionDetection(_pointCollisionDetection, Vector3.right, distanceX, _collisionCorretionLayer) && data.movementInput.x > 0))
            data.movementInput = new Vector2(0, data.movementInput.y);
    }

    bool CollisionDetection(Transform point, Vector3 direction, float distance, LayerMask layer)
    {
        if (Physics.Raycast(point.position, point.TransformDirection(direction), distance, layer))
            return true;
        else
            return false;
    }
    #endregion

    public bool IsReflective()
    {
        return data.leanState == LeanState.BarrelRoll;
    }

    public void SetDamage(int damage)
    {
        if (delayDamage > 0)
            return;

        delayDamage = 1;
        float delayTime = 1;
        DOVirtual.Float(delayDamage, 0, delayTime, x => delayDamage = x);

        data.isDamageEffect = true;
        data.lifeAmount -= damage;
        data.OnUpdateHP();

        _playerModel.DOShakeRotation(.5f, 40);

        float backwardForce = 50f;
        if (data.shipState == ShipState.AllRangeMode)
            _playerTransform.DOMove(_playerTransform.position - (_playerTransform.forward * backwardForce), .25f);

        if (data.lifeAmount <= 0)
            Debug.Log("Destroyed");

        Debug.Log("Arwing Life (" + data.lifeAmount + ")");
        AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.DamageObstacle));
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public void DOUTurn(Transform direction)
    {
        if(data.acrobaticState == AcrobaticState.None)
            DOAcrobatic(AcrobaticState.UTurn, direction, 50, true);
    }

    public void SetAllRangeMode() => StartCoroutine(SetAllRangeModeCorountine());

    IEnumerator SetAllRangeModeCorountine()
    {
        data.OnInputActive(false);
        data.shipState = ShipState.AllRangeMode;

        _playerTransform.DOLocalRotate(Vector3.zero, .5f);
        _arwingTransform.DOLocalRotate(Vector3.zero, .5f);
        _arwingTransform.DOLocalMoveX(0, 3f).SetUpdate(UpdateType.Fixed);
        _arwingTransform.DOLocalMoveY(0, 3f).SetUpdate(UpdateType.Fixed);
        _rigidbody.velocity = Vector3.zero;

        _stageDollyCart.enabled = false;

        _allRangeAnimation.Play();
        _arrow.gameObject.SetActive(false);

        yield return new WaitForSeconds(3);

        data.OnOpenWings(data.allRangeWings, 5f);

        yield return new WaitUntil(() => !_allRangeAnimation.isPlaying);

        CameraManager.SetUpdateMethod(CinemachineBrain.UpdateMethod.FixedUpdate);
        CameraManager.SetLiveCamera("All Range VCam");
        CameraManager.SetDeadZoneComposer("Acrobatic VCam", 0, 0);

        yield return new WaitForSeconds(.5f);

        _arrow.gameObject.SetActive(true);

        data.OnInputActive(true);

        yield break;
    }

}
