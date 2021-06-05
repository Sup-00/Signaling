using UnityEngine;
using System.Collections;

public class WardenAlarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _alarmBox;
    [SerializeField] private float _volumeChangeRate;
    [SerializeField] [Range(0f, 1f)] private float _maxVolume;

    private bool _isPlayerEnter;
    private Animator _enemyAnimator;

    private void Start()
    {
        _isPlayerEnter = false;
        _enemyAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isPlayerEnter = true;
        StartCoroutine(PlayAlarm());
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerEnter = false;
        StartCoroutine(StopAlarm());
    }

    private IEnumerator PlayAlarm()
    {
        SetAnimation("Alarm", true);

        while (_isPlayerEnter)
        {
            if (_isPlayerEnter == false)
                break;

            CheckAudioForActive(_audioSource.isPlaying);

            if (_audioSource.volume < _maxVolume)
                _audioSource.volume += ChangeVolume();

            yield return null;
        }
    }

    private IEnumerator StopAlarm()
    {
        while (_audioSource.volume > 0.0f)
        {
            if (_isPlayerEnter)
                break;

            CheckAudioForActive(_audioSource.isPlaying);
            _audioSource.volume -= ChangeVolume();

            yield return null;
        }

        if (_isPlayerEnter == false)
        {
            SetAnimation("Idle", false);
            _audioSource.Stop();
        }

    }

    private void SetAnimation(string Trigger, bool IsActive)
    {
        _enemyAnimator.SetTrigger(Trigger);
        _alarmBox.active = IsActive;
    }

    private void CheckAudioForActive(bool isPlay)
    {
        if (isPlay == false)
        {
            _audioSource.Play();
        }
    }

    private float ChangeVolume()
    {
        return Mathf.MoveTowards(0, _maxVolume, _volumeChangeRate);
    }
}
