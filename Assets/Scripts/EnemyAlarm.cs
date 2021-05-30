using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] [Range(0f, 1f)] private float _maxVolume;
    [SerializeField] [Range(0f, 0.1f)] private float _runTime;

    private bool _isPlayerEnter;
    private Animator _enemyAnimator;
    private GameObject _alarmBox;

    void Start()
    {
        _isPlayerEnter = false;
        _enemyAnimator = gameObject.GetComponent<Animator>();
        _alarmBox = GameObject.FindGameObjectWithTag("AlarmBox");
    }

    void Update()
    {
        if (_isPlayerEnter)
        {
            PlayAlarm();
        }
        else
        {
            StopAlarm();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isPlayerEnter = true;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerEnter = false;
    }

    private void PlayAlarm()
    {
        IsPlaying(_audioSource.isPlaying);

        if (_audioSource.volume < 0.5f)
        {
            _audioSource.volume += ChangeVolume();
        }

        _enemyAnimator.SetTrigger("Alarm");
        _alarmBox.active = true;
    }

    private void StopAlarm()
    {
        if (_audioSource.volume > 0f)
        {
            IsPlaying(_audioSource.isPlaying);
            _audioSource.volume -= ChangeVolume();
        }
        else
        {
            _audioSource.Stop();
            _enemyAnimator.SetTrigger("Idle");
            _alarmBox.active = false;
        }
    }

    private void IsPlaying(bool isPlay)
    {
        if (isPlay == false)
        {
            _audioSource.Play();
        }
    }

    private float ChangeVolume()
    {
        return Mathf.MoveTowards(0, _maxVolume, _runTime * Time.deltaTime);
    }
}
