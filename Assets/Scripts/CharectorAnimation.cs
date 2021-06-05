using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharectorAnimation : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            SetAnimation("Idle");
        }
        else
        {
            SetAnimation("Run");
        }
    }

    private void SetAnimation(string AnimationName)
    {
        _animator.SetTrigger(AnimationName);
    }
}
