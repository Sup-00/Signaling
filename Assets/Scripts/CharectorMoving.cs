using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharectorMoving : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private bool _isStayOnFeet;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") < 0 )
        {
            _spriteRenderer.flipX = true;
            Move(-_speed);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            _spriteRenderer.flipX = false;
            
            Move(_speed);
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

    private void Jump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }
}
