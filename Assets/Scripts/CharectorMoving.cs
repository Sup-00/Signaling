using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharectorMoving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isStayOnFeet;

    private void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _spriteRenderer.flipX = true;
            ActivateAnimation("Run");
            Move(-_speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _spriteRenderer.flipX = false;
            ActivateAnimation("Run");
            Move(_speed);
        }
        else
        {
            ActivateAnimation("Idle");
        }

        if (_isStayOnFeet && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isStayOnFeet = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isStayOnFeet = false;
    }

    private void Move(float MoveDirection)
    {
        transform.Translate(MoveDirection * Time.deltaTime, 0, 0);
    }

    private void ActivateAnimation(string AnimationName)
    {
        _animator.SetTrigger(AnimationName);
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }
}
