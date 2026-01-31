using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class DarknessAbilityController : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light2D globalLight;   // Global Light 2D
    [SerializeField] private Light2D playerLight;   // Point Light 2D on player

    private const float NormalGlobalIntensity = 1.0f;
    private const float DarkGlobalIntensity = 0.0f;
    private const float LitCircleIntensity = 1.0f;

    [Header("Timings")]
    [SerializeField] private float fadeToBlackTime = 0.25f;
    [SerializeField] private float fadeCircleInTime = 0.20f;
    [SerializeField] private float fadeCircleOutTime = 0.15f;
    [SerializeField] private float fadeBackToNormalTime = 0.25f;
    [SerializeField] private float circleDelayAfterBlack = 0.10f;
    [SerializeField] private float globalDelayAfterCircleOff = 0.05f;

    private Coroutine _sequence;

    // void OnCloseEyes()
    // {
    //     holdAction.action.Enable();
    //     holdAction.action.started += OnHoldStarted;
    //     holdAction.action.canceled += OnHoldCanceled;
    // }
    //
    // void OnOpenEyes()
    // {
    //     holdAction.action.started -= OnHoldStarted;
    //     holdAction.action.canceled -= OnHoldCanceled;
    //     holdAction.action.Disable();
    // }

    private void OnHoldStarted(InputAction.CallbackContext _)
    {
        StartSequence(HoldRoutine());
    }

    private void OnHoldCanceled(InputAction.CallbackContext _)
    {
        StartSequence(ReleaseRoutine());
    }

    private void StartSequence(IEnumerator routine)
    {
        if (_sequence != null) StopCoroutine(_sequence);
        _sequence = StartCoroutine(routine);
    }

    private IEnumerator HoldRoutine()
    {
        // playerLight.intensity = 0f;

        // Fade whole screne to black
        yield return FadeLight(globalLight, globalLight.intensity, DarkGlobalIntensity, fadeToBlackTime);

        // Light Circle
        if (circleDelayAfterBlack > 0f)
            yield return new WaitForSeconds(circleDelayAfterBlack);

        yield return FadeLight(playerLight, playerLight.intensity, LitCircleIntensity, fadeCircleInTime);
    }

    private IEnumerator ReleaseRoutine()
    {
        // Remove Circle
        yield return FadeLight(playerLight, playerLight.intensity, 0f, fadeCircleOutTime);

        // Then Restore GlobalLight
        if (globalDelayAfterCircleOff > 0f)
            yield return new WaitForSeconds(globalDelayAfterCircleOff);

        yield return FadeLight(globalLight, globalLight.intensity, NormalGlobalIntensity, fadeBackToNormalTime);
    }

    private static IEnumerator FadeLight(Light2D light, float from, float to, float duration)
    {
        if (!light) yield break;

        if (duration <= 0f)
        {
            light.intensity = to;
            yield break;
        }

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            light.intensity = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        light.intensity = to;
    }
}
