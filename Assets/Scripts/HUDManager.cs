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
                GameObject newArrow = Instantiate(arrowPrefab, spawnPoint.transform);
                int arrowIndex = Random.Range(0, arrows.Length);
                newArrow.GetComponent<Image>().sprite = arrows[arrowIndex];
                newArrow.GetComponent<Arrow>().SetDirection((ArrowDirection)arrowIndex);
            } 
            if (_arrowCounter >= arrowToWin) OnEndGame();
        }

    }

    private void OnEndGame()
    {
        _inGame = false;
        
        if (arrowInputDetector.GetFailedCounter() > 0)
        {
            arrowInputDetector.ResetFailedCounter();
        }
        else
        {
            OnWinGame.Invoke();
        }
        
        OnEndGameEvent.Invoke();    
        QTEPanel.SetActive(false);
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
            
        }
        ResetArrowCounter();
    }

    public void ResetArrowCounter()
    {
        _arrowCounter = 0;
    }
    
    public void CancelGame()
    {
        QTEPanel.SetActive(false);
        _inGame = false;

    }

    public void StartGame()
    {
        if (!_inGame)
        {
            ResetArrowCounter();
        }
        
        QTEPanel.SetActive(true);
        _inGame = true;
    }
    
    
    
    
    
}
