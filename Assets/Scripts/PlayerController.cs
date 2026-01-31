using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HUDManager hudManager;
    
    
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 _mov;
    
    private bool _canMove = true;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_canMove)
        {
            _rb.linearVelocity = _mov.normalized * speed;
            if (_mov.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if(_mov.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
        
            _animator.SetFloat("AbsVelocity", Mathf.Abs(_mov.magnitude));
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
            _animator.SetFloat("AbsVelocity", 0);
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _mov = context.ReadValue<Vector2>();
    }
    
    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            if (gameManager.GetIsEyesClosed())
            {
                hudManager.StartGame();
            }
            else
            {
                hudManager.CancelGame();
            }
        }
    }

}
