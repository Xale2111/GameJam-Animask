using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class DarknessController : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light2D globalLight;   // Global Light 2D
    [SerializeField] private Light2D playerLight;   // Point Light 2D on player
    [SerializeField] private float normalGlobalIntensity = 1.0f;
    [SerializeField] private float darkGlobalIntensity = 0.0f;
    [SerializeField] private float normalPlayerIntensity = 0.0f;
    [SerializeField] private float darkPlayerIntensity = 1.0f;

    [Header("Timings")]
    [SerializeField] private float fadeToBlackTime = 0.25f;
    [SerializeField] private float fadeCircleInTime = 0.20f;
    [SerializeField] private float fadeCircleOutTime = 0.15f;
    [SerializeField] private float fadeBackToNormalTime = 0.25f;
    [SerializeField] private float circleDelayAfterBlack = 0.10f;
    [SerializeField] private float globalDelayAfterCircleOff = 0.10f;

    private bool _eyesClosed;
    private Coroutine _sequence;
    private bool _isTransitionRunning;

    public void OnCloseEyes(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if (_isTransitionRunning)
        {
            return;
        }

        _eyesClosed = !_eyesClosed;
        StartSequence(_eyesClosed ? CloseEyesRoutine() : OpenEyesRoutine());
    }

    private void StartSequence(IEnumerator routine)
    {
        _sequence = StartCoroutine(SequenceWrapper(routine));
    }
    
    private IEnumerator SequenceWrapper(IEnumerator routine)
    {
        _isTransitionRunning = true;

        yield return StartCoroutine(routine);

        _isTransitionRunning = false;
    }


    private IEnumerator CloseEyesRoutine()
    {
        // Fade whole screne to black
        yield return FadeLight(globalLight, globalLight.intensity, darkGlobalIntensity, fadeToBlackTime);

        // Light Circle
        if (circleDelayAfterBlack > 0f)
            yield return new WaitForSeconds(circleDelayAfterBlack);

        yield return FadeLight(playerLight, playerLight.intensity, darkPlayerIntensity, fadeCircleInTime);
    }

    private IEnumerator OpenEyesRoutine()
    {
        // Remove Circle
        yield return FadeLight(playerLight, playerLight.intensity, normalPlayerIntensity, fadeCircleOutTime);

        // Then Restore GlobalLight
        if (globalDelayAfterCircleOff > 0f)
            yield return new WaitForSeconds(globalDelayAfterCircleOff);

        yield return FadeLight(globalLight, globalLight.intensity, normalGlobalIntensity, fadeBackToNormalTime);
    }

    private static IEnumerator FadeLight(Light2D light, float currentIntensity, float intensityGoal, float duration)
    {
        if (!light) yield break;

        if (duration <= 0f)
        {
            light.intensity = intensityGoal;
            yield break;
        }

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            light.intensity = Mathf.Lerp(currentIntensity, intensityGoal, timer / duration);
            yield return null;
        }
        light.intensity = intensityGoal;
    }
}
