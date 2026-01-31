using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnPoint;
    
    [Header("Fear Gauge")]
    [SerializeField] private float maxFear = 10.0f;
    [SerializeField] private Slider fearSlider;
    private float _currentFear = 0.0f;
    public bool isGainingFear = false;
    
    //Closing/Opening Eyes
    [Header("Closing Eyes Mechanic")]
    [SerializeField] private ParticleSystem sparkleParticles;
    [SerializeField] private Light2D globalLight;   // Global Light 2D
    [SerializeField] private Light2D playerLight;   // Point Light 2D on player
    [SerializeField] private float normalGlobalIntensity = 1.0f;
    [SerializeField] private float darkGlobalIntensity = 0.0f;
    [SerializeField] private float normalPlayerIntensity = 0.0f;
    [SerializeField] private float darkPlayerIntensity = 1.0f;

    [Header("Closing Eyes Timings")]
    [SerializeField] private float fadeToBlackTime = 0.25f;
    [SerializeField] private float fadeCircleInTime = 0.20f;
    [SerializeField] private float fadeCircleOutTime = 0.15f;
    [SerializeField] private float fadeBackToNormalTime = 0.25f;
    [SerializeField] private float circleDelayAfterBlack = 0.10f;
    [SerializeField] private float globalDelayAfterCircleOff = 0.10f;

    private bool _eyesClosed;
    private Coroutine _sequence;
    private bool _isTransitionRunning;
    private bool _isParticleSystemRunning;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFear();
    }

    private void UpdateFear()
    {
        if (!isGainingFear) return;
        _currentFear += Time.deltaTime;
        fearSlider.value = _currentFear;
        CheckFear();
    }

    private void CheckFear()
    {
        if (_currentFear < maxFear) return;
        if (_isTransitionRunning) return;
        
        StartSequence(ReturnToSpawnRoutine());
    }
    
    public void GainMaxFear(int value)
    {
        maxFear += value;
        fearSlider.maxValue = maxFear;
    }

    public void GainFear(int value)
    {
        _currentFear += value;
        fearSlider.value = _currentFear;
        CheckFear();
    }
    
    public void SetIsGainingFear(bool value)
    {
        isGainingFear = value;
    }

    private void ToggleParticleSystem()
    {
        _isParticleSystemRunning = !_isParticleSystemRunning;
        if (_isParticleSystemRunning)
        {
            // Start Particle System
            sparkleParticles.Play();
        }
        else
        {
            // Stop Particle System
            sparkleParticles.Stop();
        }
    }
    
    public void OnToggleEyes(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if (_isTransitionRunning)
        {
            return;
        }

        ToggleParticleSystem();
        _eyesClosed = !_eyesClosed;
        StartSequence(_eyesClosed ? CloseEyesRoutine() : OpenEyesRoutine());
    }

    public bool GetIsEyesClosed()
    {
        return _eyesClosed;
    }

    private void StartSequence(IEnumerator routine)
    {
        if (_sequence != null)
            StopCoroutine(_sequence);

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
        playerController.SetCanMove(false);
        
        // Fade whole screne to black
        yield return FadeLight(globalLight, globalLight.intensity, darkGlobalIntensity, fadeToBlackTime);
        
        // Light Circle
        if (circleDelayAfterBlack > 0f)
            yield return new WaitForSeconds(circleDelayAfterBlack);

        yield return FadeLight(playerLight, playerLight.intensity, darkPlayerIntensity, fadeCircleInTime);
    }

    private IEnumerator OpenEyesRoutine()
    {
        playerController.SetCanMove(true);
        
        // Remove Circle
        yield return FadeLight(playerLight, playerLight.intensity, normalPlayerIntensity, fadeCircleOutTime);
        
        // Then Restore GlobalLight
        if (globalDelayAfterCircleOff > 0f)
            yield return new WaitForSeconds(globalDelayAfterCircleOff);

        yield return FadeLight(globalLight, globalLight.intensity, normalGlobalIntensity, fadeBackToNormalTime);
    }
    
    private IEnumerator ReturnToSpawnRoutine()
    {
        playerController.SetCanMove(false);
        isGainingFear = false;
        _eyesClosed = false;
        
        if (_isParticleSystemRunning)
        {
            ToggleParticleSystem();
        }
        
        // Fade Player Light to black
        yield return FadeLight(playerLight, playerLight.intensity, 0f, fadeCircleOutTime);

        // Fade Global Light to black
        yield return FadeLight(globalLight, globalLight.intensity, 0f, fadeToBlackTime);

        playerLight.intensity = 0f;
        globalLight.intensity = 0f;

        // Teleport while dark
        player.transform.position = spawnPoint.transform.position;

        // Reset fear
        _currentFear = 0f;
        fearSlider.value = _currentFear;
        //TODO hide slider

        yield return new WaitForSeconds(0.05f);

        // Restore normal lighting
        yield return FadeLight(playerLight, playerLight.intensity, normalPlayerIntensity, fadeCircleOutTime);

        if (globalDelayAfterCircleOff > 0f)
            yield return new WaitForSeconds(globalDelayAfterCircleOff);

        yield return FadeLight(globalLight, globalLight.intensity, normalGlobalIntensity, fadeBackToNormalTime);
        
        
        playerController.SetCanMove(true);
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
