using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnPoint;
    
    [Header("Fear Gauge")]
    [SerializeField] private Slider fearSlider;
    [SerializeField] private float _maxFear = 10.0f;
    private float _currentFear = 0.0f;
    public bool _isGainingFear = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fearSlider.maxValue = _maxFear;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGainingFear && _currentFear <= _maxFear)
        {
            _currentFear += Time.deltaTime;
        }
        else if (_currentFear >= _maxFear)
        {
            //DIE POTATO
            _currentFear = 0.0f;
            player.transform.position = spawnPoint.transform.position;
        }
        fearSlider.value = _currentFear;
    }

    public void AddMaxFear(int value)
    {
        _maxFear += value;
        fearSlider.maxValue = _maxFear;
    }
    
    public void SetIsGainingFear(bool value)
    {
        _isGainingFear = value;
    }
}
