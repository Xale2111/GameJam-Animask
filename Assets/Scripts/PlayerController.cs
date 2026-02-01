using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FoodManager foodManager;
    
    
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 _mov;
    
    private bool _canMove = true;
    private bool _canPickUp = false;
    private GameObject currentItemOnGround;
    
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

        if (gameManager.GetIsEyesClosed())
        {
            _animator.SetBool("Masking", true);
        }
        else
        {
            _animator.SetBool("Masking", false);
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
    
    public void GiveAnimationEnd()
    {
        _canMove = true;
    }

    public void PickUpAnimationEnd()
    {
        _canMove = true;
        Destroy(currentItemOnGround);
        _canPickUp = false;
    }

    public void OnPickUpItem()
    {
        if (!_canPickUp) return;
        
        SetCanMove(false);
        _animator.SetTrigger("PickUp");
        gameManager.SetHasItem(true);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CenterArea"))
        {
            gameManager.SetCanCloseEyes(false);
            gameManager.SetIsGainingFear(false);
        }
        
        if (other.CompareTag("FoodItem"))
        {
            _canPickUp = true;
            currentItemOnGround = other.transform.root.gameObject;
        }

        if (other.CompareTag("Deer"))
        {
            if (gameManager.GetHeldItemID() == 0 && gameManager.GetHasItem())
            {
                SetCanMove(false);
                _animator.SetTrigger("Give");
                gameManager.SetHasItem(false);
                gameManager.GainMaxFear(15);
                foodManager.ActivateNextSpot();
            }
        }

        if (other.CompareTag("Bear"))
        {
            if (gameManager.GetHeldItemID() == 1 && gameManager.GetHasItem())
            {
                SetCanMove(false);
                _animator.SetTrigger("Give");
                gameManager.SetHasItem(false);
                gameManager.GainMaxFear(40);
                foodManager.ActivateNextSpot();
            }
        }

        if (other.CompareTag("Horse"))
        {
            if (gameManager.GetHeldItemID() == 2 && gameManager.GetHasItem())
            {
                SetCanMove(false);
                _animator.SetTrigger("Give");
                gameManager.SetHasItem(false);
                gameManager.GainMaxFear(10);
                foodManager.ActivateNextSpot();
            }
        }

        if (other.CompareTag("Shark"))
        {
            if (gameManager.GetHeldItemID() == 3 && gameManager.GetHasItem())
            {
                SetCanMove(false);
                _animator.SetTrigger("Give");
                gameManager.SetHasItem(false);
                gameManager.GainMaxFear(50);
                foodManager.ActivateNextSpot();
            }
        }

        if (other.CompareTag("Spino"))
        {
            if (gameManager.GetHeldItemID() == 4 && gameManager.GetHasItem())
            {
                SetCanMove(false);
                _animator.SetTrigger("Give");
                gameManager.SetHasItem(false);
                gameManager.PlayEnding();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CenterArea"))
        {
            gameManager.SetCanCloseEyes(true);
            gameManager.SetIsGainingFear(true);
        }
        
        if (other.CompareTag("FoodItem"))
        {
            _canPickUp = false;
        }
    }

}
