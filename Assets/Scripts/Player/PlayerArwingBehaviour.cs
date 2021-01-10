using DG.Tweening;
using System;
using UnityEngine;

public class PlayerArwingBehaviour : MonoBehaviour
{
    [Header("Logic FPS")]
    [Range(0, 60)]
    public int fps;

    [Header("Player Settings")]
    public PlayerData data;

    [Header("Bones")]
    [SerializeField] Transform leftUpWing;
    [SerializeField] Transform leftDownWing;
    [SerializeField] Transform rightUpWing;
    [SerializeField] Transform rightDownWing;

    [Header("Wings")]
    [SerializeField] Transform leftWing;
    [SerializeField] Transform rightWing;

    [Header("Wings")]
    [SerializeField] Transform hyperLaserRig;

    [HideInInspector] public CustomFixedUpdate FU_LogicInstance;

    void Awake()
    {
        FU_LogicInstance = new CustomFixedUpdate(OnLogicFixedUpdate, fps);

        data.onOpenWings += (Vector3 rotation, float time) => OpenWings(rotation, time);
        data.onOpenHyperLaser += (bool state) => OpenHyperLaser(state);
    }

    void Update()
    {
        FU_LogicInstance.Update();
    }

    void OnLogicFixedUpdate(float aDeltaTime)
    {
        if (data.buffState != BuffState.Break)
            LocalRigWings(data.movementInput.x, data.movementInput.y, .25f);
        else if (data.buffState == BuffState.Break)
            BreakRigWings(.25f);
    }

    public void OpenWings(Vector3 rotation, float time)
    {
        leftWing.DOLocalRotate(new Vector3(rotation.x, -rotation.y, rotation.z), time);
        rightWing.DOLocalRotate(new Vector3(rotation.x, rotation.y, -rotation.z), time);

        AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.OpenWings));
    }

    public void WingsLevel1() => OpenWings(data.spaceWings, 5f);

    public void WingsLevel2() => OpenWings(data.atmosphereWings, 5f);

    public void WingsLevel3() => OpenWings(data.allRangeWings, 5f);

    void LocalRigWings(float h, float v, float speed)
    {
        if (h >= -.05f && h <= .05f)
        {
            BoneAngleWing(leftUpWing, v, 60, speed);
            BoneAngleWing(leftDownWing, v, 60, speed);
            BoneAngleWing(rightUpWing, v, 60, speed);
            BoneAngleWing(rightDownWing, v, 60, speed);
        }
        else if (h > .05f)
        {
            BoneAngleWing(rightUpWing, h, 60, speed);
            BoneAngleWing(rightDownWing, -h, 60, speed);
            BoneAngleWing(leftUpWing, v, 60, speed);
            BoneAngleWing(leftDownWing, v, 60, speed);
        }
        else if (h < -.05f)
        {
            BoneAngleWing(leftUpWing, -h, 60, speed);
            BoneAngleWing(leftDownWing, h, 60, speed);
            BoneAngleWing(rightUpWing, v, 60, speed);
            BoneAngleWing(rightDownWing, v, 60, speed);
        }
    }

    void BreakRigWings(float speed)
    {
        BoneAngleWing(rightUpWing, 1, 50, speed);
        BoneAngleWing(rightDownWing, -1, 50, speed);
        BoneAngleWing(leftUpWing, 1, 50, speed);
        BoneAngleWing(leftDownWing, -1, 50, speed);
    }

    //Set transform angle based on axis 
    public void BoneAngleWing(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(Mathf.LerpAngle(targetEulerAngels.x, axis * leanLimit, lerpTime), 0, 0);
    }

    void OpenHyperLaser(bool state)
    {
        hyperLaserRig.DOLocalMoveZ(state ? -1.55f : -0.55f, .4f);
    }
}
