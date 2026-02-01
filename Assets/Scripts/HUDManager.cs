using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject QTEPanel;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnSpeed = 2f;
    [SerializeField] private int arrowToWin = 10;
    [SerializeField] private ArrowInputDetector arrowInputDetector;
    
    [SerializeField] private Sprite[] arrows;
    
    [SerializeField] private UnityEvent OnWinGame;
    [SerializeField] private UnityEvent OnEndGameEvent;

    
    private int _arrowCounter = 0;

    private float _spawnDelay;

    private bool _inGame = false;
    private bool _hasWon = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QTEPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_inGame)
        {
            _spawnDelay += Time.deltaTime;
            
            if (_spawnDelay >= spawnSpeed && _arrowCounter < arrowToWin)
            {
                Debug.Log("Current arrows :" + _arrowCounter + " out of " + arrowToWin );
                
                _spawnDelay = 0;
                _arrowCounter++;
                arrowInputDetector.AddRemainingArrow();
                GameObject newArrow = Instantiate(arrowPrefab, spawnPoint.transform);
                int arrowIndex = Random.Range(0, arrows.Length);
                newArrow.GetComponent<Image>().sprite = arrows[arrowIndex];
                newArrow.GetComponent<Arrow>().SetDirection((ArrowDirection)arrowIndex);
            } 
            if (arrowInputDetector.GetRemainingArrows()<=0 && _arrowCounter >= arrowToWin) OnEndGame();
        }
        else
        {
            ResetGame();
        }

    }

    private void ResetGame()
    {
        arrowInputDetector.ResetFailedCounter();
        _spawnDelay = 0;
        QTEPanel.SetActive(false);
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
            
        }
        _arrowCounter = 0;
    }

    private void OnEndGame()
    {
        _inGame = false;
        
        if (arrowInputDetector.GetFailedCounter() <= 0)
        {
            OnWinGame.Invoke();
            ResetGame();
            _hasWon = true;
        }
        
        OnEndGameEvent.Invoke();  
    }
    
    public void CancelGame()
    {
        QTEPanel.SetActive(false);
        _inGame = false;

    }

    public void StartGame()
    {
        if (!_hasWon)
        {
            QTEPanel.SetActive(true);
            _inGame = true;
        }
    }
    
    
    
    
    
}
