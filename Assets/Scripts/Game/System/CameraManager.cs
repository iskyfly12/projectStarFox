using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static Camera mainCamera;
    static CinemachineBrain brainVCam;
    static CinemachineVirtualCamera[] vCams;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("BrainCamera").GetComponent<Camera>();
        brainVCam = mainCamera.GetComponent<CinemachineBrain>();
        vCams = FindObjectsOfType<CinemachineVirtualCamera>();
    }

    public static void SetUpdateMethod(CinemachineBrain.UpdateMethod method)
    {
        brainVCam.m_UpdateMethod = method;
    }

    public static void SetLiveCamera(string tag)
    {
        for (int i = 0; i < vCams.Length; i++)
        {
            if (vCams[i].CompareTag(tag))
                vCams[i].enabled = true;
            else
                vCams[i].enabled = false;
        }
    }

    public static void SetLiveCamera(CinemachineVirtualCamera vcam)
    {
        for (int i = 0; i < vCams.Length; i++)
            vCams[i].enabled = false;

        vcam.enabled = true;
    }

    public static CinemachineVirtualCamera GetLiveCamera()
    {
        return (CinemachineVirtualCamera)brainVCam.ActiveVirtualCamera;
    }

    public static CinemachineBasicMultiChannelPerlin GetCameraChannel()
    {
        return GetLiveCamera().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public static void SetDistanceCameraFramingTransposer(float distance, float time)
    {
        CinemachineFramingTransposer framingTransposer = GetLiveCamera().GetCinemachineComponent<CinemachineFramingTransposer>();
        DOVirtual.Float(framingTransposer.m_CameraDistance, distance, time, x => framingTransposer.m_CameraDistance = x);
    }

    public static float GetDistanceCamera()
    {
        return GetLiveCamera().GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
    }

    public static void SetAmplitudeGain(float to, float time, bool loop)
    {
        if (GetCameraChannel().m_AmplitudeGain == to)
            return;

        int timeLoop = (loop) ? 2 : 0;
        DOVirtual.Float(GetCameraChannel().m_AmplitudeGain, to, time, x => GetCameraChannel().m_AmplitudeGain = x).SetLoops(timeLoop, LoopType.Yoyo);
    }

    public static void SetZoomCamera(float endZoom, float time)
    {
        if (GetLiveCamera().TryGetComponent(out CinemachineFollowZoom followZoom))
        {
            if (followZoom.m_Width == endZoom)
                return;

            DOVirtual.Float(followZoom.m_Width, endZoom, time, x => followZoom.m_Width = x);
        }
    }

    public static float GetZoomCamera()
    {
        if (GetLiveCamera().TryGetComponent(out CinemachineFollowZoom followZoom))
            return followZoom.m_Width;

        return 0;
    }

    public static void SetOffsetTransposer(Vector3 offset)
    {
        CinemachineTransposer transposer = GetLiveCamera().GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = offset;
    }

    public static void SetOffsetTransposer(string tag, Vector3 offset)
    {
        for (int i = 0; i < vCams.Length; i++)
        {
            if (vCams[i].CompareTag(tag))
            {
                CinemachineTransposer transposer = vCams[i].GetCinemachineComponent<CinemachineTransposer>();
                transposer.m_FollowOffset = offset;
                break;
            }
        }
    }

    public static void SetDeadZoneComposer(string tag, float height, float width)
    {
        for (int i = 0; i < vCams.Length; i++)
        {
            if (vCams[i].CompareTag(tag))
            {
                CinemachineComposer composer = vCams[i].GetCinemachineComponent<CinemachineComposer>();
                composer.m_DeadZoneHeight = height;
                composer.m_DeadZoneWidth = width;
                break;
            }
        }
    }

    public static void SetCenterActivate(string tag, bool active)
    {
        for (int i = 0; i < vCams.Length; i++)
        {
            if (vCams[i].CompareTag(tag))
            {
                CinemachineComposer composer = vCams[i].GetCinemachineComponent<CinemachineComposer>();
                composer.m_CenterOnActivate = active;
                break;
            }
        }
    }

    public static void SetUnlimitedSoftZone(bool active)
    {
        CinemachineFramingTransposer framingTransposer = GetLiveCamera().GetCinemachineComponent<CinemachineFramingTransposer>();
        framingTransposer.m_UnlimitedSoftZone = active;
    }
}
