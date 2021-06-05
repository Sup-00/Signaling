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

    private void Start()
    {
        _isPlayerEnter = false;
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
        _playAlarm.Invoke();

        while (_isPlayerEnter)
        {
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

            _audioSource.volume -= ChangeVolume();

            yield return null;
        }

        if (_isPlayerEnter == false)
        {
            _stopAlarm.Invoke();
        }

    }

    private float ChangeVolume()
    {
        return Mathf.MoveTowards(0, _maxVolume, _volumeChangeRate);
    }
}
