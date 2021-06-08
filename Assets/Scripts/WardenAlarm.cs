using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class WardenAlarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeChangeRate;
    [SerializeField] [Range(0f, 1f)] private float _maxVolume;
    [SerializeField] private UnityEvent _playAlarm;
    [SerializeField] private UnityEvent _stopAlarm;

    private bool _isPlayerEnter;
    private Coroutine _currentCorutine;

    private void Start()
    {
        _isPlayerEnter = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isPlayerEnter = true;
        StopActiveCorutine(_currentCorutine);
        _currentCorutine = StartCoroutine(PlayAlarm(_playAlarm, _isPlayerEnter));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerEnter = false;
        StopActiveCorutine(_currentCorutine);
        _currentCorutine = StartCoroutine(StopAlarm(_stopAlarm, _isPlayerEnter));
    }

    private void StopActiveCorutine(Coroutine corutine)
    {
        if (_currentCorutine != null)
            StopCoroutine(corutine);
    }

    private IEnumerator PlayAlarm(UnityEvent playAlarm, bool isPlayerEnter)
    {
        playAlarm.Invoke();

        while (isPlayerEnter)
        {
            if (_audioSource.volume < _maxVolume)
                _audioSource.volume += ChangeVolume();

            yield return null;
        }
    }

    private IEnumerator StopAlarm(UnityEvent stopAlarm, bool isPlayerEnter)
    {
        while (_audioSource.volume > 0.0f)
        {
            if (isPlayerEnter)
                break;

            _audioSource.volume -= ChangeVolume();

            yield return null;
        }

        if (isPlayerEnter == false)
        {
            stopAlarm.Invoke();
        }

    }

    private float ChangeVolume()
    {
        return Mathf.MoveTowards(0, _maxVolume, _volumeChangeRate);
    }
}
