using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class PlayerVisualHandler : MonoBehaviour
{
    [Header("Player Settings")]
    public PlayerData data;

    [Header("Logic FPS")]
    [Range(0, 60)]
    public int fps;

    [Header("VFX")]
    [SerializeField] VisualEffect fireVFX;
    [SerializeField] VisualEffect spinVFX;
    [SerializeField] VisualEffect damageVFX;
    [SerializeField] VisualEffect dustVFX;
    [SerializeField] VisualEffect[] boostVFX;
    [SerializeField] VisualEffect[] colorVFX;

    [Header("Trails")]
    [SerializeField] TrailRenderer fireTrail;
    [SerializeField] TrailRenderer[] trails;

    private CustomFixedUpdate FU_LogicInstance;

    readonly string string_orientation = "Orientation";
    readonly string string_materialColor = "Vector1_253D52CD";

   void Awake()
    {
        FU_LogicInstance = new CustomFixedUpdate(OnFixedUpdate, fps);

        data.onBoost += (bool state) => BoostEffect(state);
        data.onBreak += (bool state) => BreakEffect(state);
    }

    void Update()
    {
        FU_LogicInstance.Update();
    }

    void OnFixedUpdate(float aDeltaTime)
    {
        UpdateVFXColor();

        if (data.buffAmount <= 0)
            CleanEffects();

        if (data.leanState == LeanState.BarrelRoll)
            SpinEffect(data.leanAxisInput);

        if (data.isDamageEffect)
            DamageEffect();

    }


    #region Effects 
    public void BoostEffect(bool state)
    {
        if (state)
        {
            PostProcessingSystem.SetLensDistortion(-.5f, .5f, false);
            SetAmountSizeVFX(fireVFX, 2);
            dustVFX.Play();
        }
        else
        {
            PostProcessingSystem.SetLensDistortion(0, .7f, false);
            SetAmountSizeVFX(fireVFX, 1);
            dustVFX.Stop();
        }

        
        Trail(state);
        VFXPlay(state, boostVFX);
    }

    public void BreakEffect(bool state)
    {
        if (state)
        {
            PostProcessingSystem.SetLensDistortion(.25f, .5f, false);
            SetAmountSizeVFX(fireVFX, 0);
        }
        else
        {
            PostProcessingSystem.SetLensDistortion(0, .7f, false);
            SetAmountSizeVFX(fireVFX, 1);
        }
    }

    public void SpinEffect(int orient)
    {
        spinVFX.Play();
        spinVFX.SetInt(string_orientation, -orient);
    }

    public void DamageEffect()
    {
        damageVFX.Play();
        PostProcessingSystem.SetGlitch(.25f, .15f, true, true, true, true, false, false);
        data.isDamageEffect = false;
    }

    void VFXPlay(bool state, VisualEffect[] vfx)
    {
        for (int i = 0; i < vfx.Length; i++)
        {
            if (state)
                vfx[i].Play();
            else
                vfx[i].Stop();
        }
    }

    public void Trail(bool state)
    {
        for (int i = 0; i < trails.Length; i++)
            trails[i].emitting = state;
    }

    public void UpdateVFXColor()
    {
        if (data.buffAmount <= 0 || data.buffAmount >= 100)
            return;

        for (int i = 0; i < colorVFX.Length; i++)
            colorVFX[i].SetFloat("Color Fire", data.buffAmount * .01f);

        fireTrail.material.SetFloat(string_materialColor, data.buffAmount * .01f);
    }

    public void CleanEffects()
    {
        PostProcessingSystem.SetLensDistortion(0, .5f, false);
        SetAmountSizeVFX(fireVFX, 1);

        Trail(false);
        VFXPlay(false, boostVFX);

        dustVFX.Stop();
    }

    public IEnumerator AcrobaticVFX()
    {
        Trail(true);
        VFXPlay(true, boostVFX);
        SetAmountSizeVFX(fireVFX, 2);

        yield return new WaitForSeconds(1.25f);

        Trail(false);
        VFXPlay(false, boostVFX);
        SetAmountSizeVFX(fireVFX, 1);
    }

    void SetAmountSizeVFX(VisualEffect vfx, float size)
    {
        DOVirtual.Float(vfx.GetFloat("Multiply Size"), size, .5f, x => vfx.SetFloat("Multiply Size", x));
    }
    #endregion
}
