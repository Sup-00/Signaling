using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharectorMoving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded;

    private void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _spriteRenderer.flipX = true;
            Move(-_speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _spriteRenderer.flipX = false;
            Move(_speed);
        }
        else
        {
            _animator.SetTrigger("Idle");
        }

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false;
    }

    private void Move(float MoveDirection)
    {
        _transform.Translate(MoveDirection * Time.deltaTime, 0, 0);
        _animator.SetTrigger("Run");
    }

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }
}
