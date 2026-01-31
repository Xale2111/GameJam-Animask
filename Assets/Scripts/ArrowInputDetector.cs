using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ArrowInputDetector : MonoBehaviour
{
    [SerializeField] private UnityEvent OnFailedArrow;
    
    [SerializeField] private UnityEvent OnInputPress;

    private ArrowDirection _lastDirectionPressed = ArrowDirection.none;

    private bool _canPress;

    private ArrowDirection _directionToPress;

    private GameObject _arrowGO;

    private bool _successed;

    private void Update()
    {
        if (_canPress)
        {
            if (_lastDirectionPressed == _directionToPress)
            {
                Debug.Log("SUCCESS");
                _canPress = false;
                _lastDirectionPressed = ArrowDirection.none;
                _successed = true;
                OnInputPress.Invoke();
                Destroy(_arrowGO.gameObject);
            }
            else if (_lastDirectionPressed != _directionToPress && _lastDirectionPressed != ArrowDirection.none)
            {
                OnInputPress.Invoke();
                InputFailed();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Arrow arrow))
        {
            _successed = false;
            _canPress = true;
            _lastDirectionPressed = ArrowDirection.none;
            _directionToPress = arrow.GetDirection();
            _arrowGO = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Arrow") && !_successed)
        {
            _canPress = false;
            _lastDirectionPressed = ArrowDirection.none;
            Debug.Log("FAILED");
            OnFailedArrow.Invoke();
            _arrowGO = other.gameObject;
            InputFailed();
        }
    }

    private void InputFailed()
    {
        Destroy(_arrowGO.gameObject);
    }

    public void OnLeftArrowPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lastDirectionPressed = ArrowDirection.Left;   
        }

    }
    
    public void OnUpArrowPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lastDirectionPressed = ArrowDirection.Up;   
        }
    }
    
    public void OnDownArrowPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lastDirectionPressed = ArrowDirection.Down;  
        }
    }
    
    public void OnRightArrowPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lastDirectionPressed = ArrowDirection.Right; 
        }

    }
}
