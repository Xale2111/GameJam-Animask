using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject pauseMenu;
    
    [SerializeField] private float navigationDelay = 0.3f;
    
    private int selectedButton = 0;
    private float lastNavigationTime;

    
    public void OnMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    
    public void OnQuitClick()
    {
        Application.Quit();
    }
    
    public void OnPlayClick()
    {
        SceneManager.LoadScene("FinalScene");
        Time.timeScale = 1;
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene("Credits");
        Time.timeScale = 1;
    }

    private void Update()
    {
        lastNavigationTime += Time.unscaledDeltaTime;
    }

    
    public void OnNavigateMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (lastNavigationTime > navigationDelay)
        {
            lastNavigationTime = 0;
            Vector2 navigation = context.ReadValue<Vector2>();

            if (navigation.y > 0.5f)
            {
                selectedButton--;
            }
            else if (navigation.y < -0.5f)
            {
                selectedButton++;
            }

            if (selectedButton < 0) selectedButton = buttons.Length - 1;
            if (selectedButton >= buttons.Length) selectedButton = 0;
            buttons[selectedButton].Select();
        }
    }
    
    public void TogglePause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}



