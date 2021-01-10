using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using UnityEngine.Rendering.HighDefinition;
using Kino.PostProcessing;

public class PostProcessingSystem : MonoBehaviour
{
    private static Volume postProcess;

    void Awake() => postProcess = GetComponent<Volume>();

    //SET CHROMATIC
    public static void SetChromatic(float endValue, float time, bool loop)
    {
        int timeLoop = (loop) ? 2 : 0;
        ChromaticAberration chromaticAberration = postProcess.profile.TryGet<ChromaticAberration>(out chromaticAberration) ? chromaticAberration : null;
        DOVirtual.Float(chromaticAberration.intensity.value, endValue, time, x => chromaticAberration.intensity.value = x).SetLoops(timeLoop, LoopType.Yoyo);
    }

    //SET VIGNETTE
    public static void SetVignette(float endValue, Color startColor, Color endColor, float time, bool loop)
    {
        int timeLoop = (loop) ? 2 : 0;
        Vignette vignette = postProcess.profile.TryGet<Vignette>(out vignette) ? vignette : null;
        DOVirtual.Float(vignette.intensity.value, endValue, time, x => vignette.intensity.value = x).SetLoops(timeLoop, LoopType.Yoyo);
        vignette.color.value = Color.Lerp(vignette.color.value, endColor, time);
    }

    //SET EXPOSURE
    public static void SetExposure(float endValue, float time, bool loop)
    {
        int timeLoop = (loop) ? 2 : 0;
        Exposure exposure = postProcess.profile.TryGet<Exposure>(out exposure) ? exposure : null;
        DOVirtual.Float(exposure.fixedExposure.value, endValue, time, x => exposure.fixedExposure.value = x).SetLoops(timeLoop, LoopType.Yoyo);
    }

    //SET BLOOM
    public static void SetBloom(float endValue, float time, bool loop)
    {
        int timeLoop = (loop) ? 2 : 0;
        Bloom bloom = postProcess.profile.TryGet<Bloom>(out bloom) ? bloom : null;
        DOVirtual.Float(bloom.intensity.value, endValue, time, x => bloom.intensity.value = x).SetLoops(timeLoop, LoopType.Yoyo);
    }

    //SET COLOR EXPOSURE
    public static void SetColorExposure(float endValue, float time, bool loop)
    {
        int timeLoop = (loop) ? 2 : 0;
        ColorAdjustments colorAdjustments = postProcess.profile.TryGet<ColorAdjustments>(out colorAdjustments) ? colorAdjustments : null;
        DOVirtual.Float(colorAdjustments.postExposure.value, endValue, time, x => colorAdjustments.postExposure.value = x).SetLoops(timeLoop, LoopType.Yoyo);
    }

    //SET COLOR ADJUSTMENT
    public static void SetColorAdjustment(Color endColor, float time)
    {
        ColorAdjustments colorAdjustments = postProcess.profile.TryGet<ColorAdjustments>(out colorAdjustments) ? colorAdjustments : null;
        colorAdjustments.colorFilter.value = Color.Lerp(colorAdjustments.colorFilter.value, endColor, time);
    }

    //SET BLOOM
    public static void SetLensDistortion(float endValue, float time, bool loop)
    {
        int timeLoop = (loop) ? 2 : 0;
        LensDistortion lensDistortion = postProcess.profile.TryGet<LensDistortion>(out lensDistortion) ? lensDistortion : null;
        DOVirtual.Float(lensDistortion.intensity.value, endValue, time, x => lensDistortion.intensity.value = x).SetLoops(timeLoop, LoopType.Yoyo);
    }

    public static void SetGlitch(float endValue, float time, bool loop, bool isBlockEffect, bool isDriftEffect, bool isJitterEffect, bool isJumpEffect, bool isShakeEffect)
    {
        int timeLoop = (loop) ? 2 : 0;
        Glitch glitch = postProcess.profile.TryGet<Glitch>(out glitch) ? glitch : null;

        if(isBlockEffect)
            DOVirtual.Float(glitch.block.value, endValue, time, x => glitch.block.value = x).SetLoops(timeLoop, LoopType.Yoyo);
        if (isDriftEffect)
            DOVirtual.Float(glitch.drift.value, endValue, time, x => glitch.drift.value = x).SetLoops(timeLoop, LoopType.Yoyo);
        if (isJitterEffect)
            DOVirtual.Float(glitch.jitter.value, endValue, time, x => glitch.jitter.value = x).SetLoops(timeLoop, LoopType.Yoyo);
        if (isJumpEffect)
            DOVirtual.Float(glitch.jump.value, endValue, time, x => glitch.jump.value = x).SetLoops(timeLoop, LoopType.Yoyo);
        if (isShakeEffect)
            DOVirtual.Float(glitch.shake.value, endValue, time, x => glitch.shake.value = x).SetLoops(timeLoop, LoopType.Yoyo);
    }
}
