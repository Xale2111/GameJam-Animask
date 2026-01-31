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

    
    private int _arrowCounter = 0;

    private float _spawnDelay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QTEPanel.SetActive(false);
        _arrowCounter = arrowToWin;
    }

    // Update is called once per frame
    void Update()
    {
        _spawnDelay += Time.deltaTime;

        if (_spawnDelay >= spawnSpeed && _arrowCounter < arrowToWin)
        {
            _spawnDelay = 0;
            _arrowCounter++;
            GameObject newArrow = Instantiate(arrowPrefab, spawnPoint.transform);
            int arrowIndex = Random.Range(0, arrows.Length);
            newArrow.GetComponent<Image>().sprite = arrows[arrowIndex];
            newArrow.GetComponent<Arrow>().SetDirection((ArrowDirection)arrowIndex);
        }
        if (_arrowCounter >= arrowToWin) OnEndGame();
        
    }

    private void OnEndGame()
    {
        if (arrowInputDetector.GetFailedCounter() > 0)
        {
            arrowInputDetector.ResetFailedCounter();
        }
        else
        {
            OnWinGame.Invoke();
        }
        
        ResetArrowCounter();
        QTEPanel.SetActive(false);
    }

    public void ResetArrowCounter()
    {
        _arrowCounter = 0;
        _arrowCounter = arrowToWin;
    }
    
    public void CancelGame()
    {
        QTEPanel.SetActive(false);
    }

    public void StartGame()
    {
        QTEPanel.SetActive(true);
        _arrowCounter = 0;
    }
    
    
    
}
