using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum Sides
{
    Cold,
    Warm,
}

public class SideSwitcher : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private VolumeProfile coldProfile;
    [SerializeField] private VolumeProfile warmProfile;
    [SerializeField] private float timeToSwitchSide;
    
    private WhiteBalance whiteBalance;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private MotionBlur motionBlur;

    private WhiteBalance coldWhiteBalance;
    private ChromaticAberration coldChromaticAberration;
    private LensDistortion coldLensDistortion;
    private MotionBlur coldMotionBlur;

    private WhiteBalance warmWhiteBalance;
    private ChromaticAberration warmChromaticAberration;
    private LensDistortion warmLensDistortion;
    private MotionBlur warmMotionBlur;

    private bool isSwitchingSides = false;    
    private Sides currentSide = Sides.Warm;

    private EventHandler eventHandler;

    private void Awake()
    {
        this.eventHandler = EventHandler.Instance;

        this.volume.profile.TryGet(out this.whiteBalance);
        this.volume.profile.TryGet(out this.chromaticAberration);
        this.volume.profile.TryGet(out this.lensDistortion);
        this.volume.profile.TryGet(out this.motionBlur);

        this.warmProfile.TryGet(out this.warmWhiteBalance);
        this.warmProfile.TryGet(out this.warmChromaticAberration);
        this.warmProfile.TryGet(out this.warmLensDistortion);
        this.warmProfile.TryGet(out this.warmMotionBlur);

        this.coldProfile.TryGet(out this.coldWhiteBalance);
        this.coldProfile.TryGet(out this.coldChromaticAberration);
        this.coldProfile.TryGet(out this.coldLensDistortion);
        this.coldProfile.TryGet(out this.coldMotionBlur);
    }

    private void OnEnable()
    {
        this.eventHandler.SwitchSideInputEvent += SwitchSide;
    }

    private void OnDisable()
    {
        this.eventHandler.SwitchSideInputEvent -= SwitchSide;
    }

    private void SwitchSide()
    {
        if (this.isSwitchingSides == false)
        {
            if (this.currentSide == Sides.Cold)
            {
                this.StartCoroutine(this.SwitchToWarmRoutine());
            }
            else if (this.currentSide == Sides.Warm)
            {
                this.StartCoroutine(this.SwitchToColdRoutine());
            }
        }
    }

    private IEnumerator SwitchToColdRoutine()
    {
        var timer = this.timeToSwitchSide;
        this.isSwitchingSides = true;

        while (timer >= 0)
        {            
            var t = (this.timeToSwitchSide - timer) / this.timeToSwitchSide;            
            // White Balance
            this.whiteBalance.temperature.Override
                (Mathf.SmoothStep(this.whiteBalance.temperature.value, this.coldWhiteBalance.temperature.value, t));            
            this.whiteBalance.tint.Override
                (Mathf.SmoothStep(this.whiteBalance.tint.value, this.coldWhiteBalance.tint.value, t));
            // Chromatic Aberration
            this.chromaticAberration.intensity.Override
                (Mathf.SmoothStep(this.chromaticAberration.intensity.value, this.coldChromaticAberration.intensity.value, t));
            // Lens Distortion
            this.lensDistortion.intensity.Override
                (Mathf.SmoothStep(this.lensDistortion.intensity.value, this.coldLensDistortion.intensity.value, t));
            // Motion Blur
            this.motionBlur.intensity.Override
                (Mathf.SmoothStep(this.motionBlur.intensity.value, this.coldMotionBlur.intensity.value, t));

            timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        this.isSwitchingSides = false;
        this.currentSide = Sides.Cold;
        this.eventHandler.InvokeSideSwitched(this.currentSide);
    }

    private IEnumerator SwitchToWarmRoutine()
    {
        var timer = this.timeToSwitchSide;
        this.isSwitchingSides = true;
        while (timer > 0)
        {
            var t = (this.timeToSwitchSide - timer) / this.timeToSwitchSide;
            // White Balance
            this.whiteBalance.temperature.Override
                (Mathf.SmoothStep(this.whiteBalance.temperature.value, this.warmWhiteBalance.temperature.value, t));
            this.whiteBalance.tint.Override
                (Mathf.SmoothStep(this.whiteBalance.tint.value, this.warmWhiteBalance.tint.value, t));
            // Chromatic Aberration
            this.chromaticAberration.intensity.Override
                (Mathf.SmoothStep(this.chromaticAberration.intensity.value, this.warmChromaticAberration.intensity.value, t));
            // Lens Distortion
            this.lensDistortion.intensity.Override
                (Mathf.SmoothStep(this.lensDistortion.intensity.value, this.warmLensDistortion.intensity.value, t));
            // Motion Blur
            this.motionBlur.intensity.Override
                (Mathf.SmoothStep(this.motionBlur.intensity.value, this.warmMotionBlur.intensity.value, t));

            timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        this.isSwitchingSides = false;
        this.currentSide = Sides.Warm;
        this.eventHandler.InvokeSideSwitched(this.currentSide);
    }
}
