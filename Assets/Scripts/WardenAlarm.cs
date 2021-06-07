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
        StopCoroutine(StopAlarm(_audioSource.volume, _stopAlarm, _maxVolume, _volumeChangeRate, _isPlayerEnter));
        StartCoroutine(PlayAlarm(_audioSource.volume, _playAlarm, _maxVolume, _volumeChangeRate, _isPlayerEnter));
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerEnter = false;
        StopCoroutine(PlayAlarm(_audioSource.volume, _playAlarm, _maxVolume, _volumeChangeRate, _isPlayerEnter));
        StartCoroutine(StopAlarm(_audioSource.volume, _stopAlarm, _maxVolume, _volumeChangeRate, _isPlayerEnter));
    }

    private IEnumerator PlayAlarm(float volume, UnityEvent playAlarm, float maxVolume, float volumeChangeRate, bool isPlayerEnter)
    {
        playAlarm.Invoke();

        while (isPlayerEnter)
        {
            if (volume < maxVolume)
                volume += ChangeVolume(maxVolume, volumeChangeRate);

            yield return null;
        }
    }

    private IEnumerator StopAlarm(float volume, UnityEvent stopAlarm, float maxVolume, float volumeChangeRate, bool isPlayerEnter)
    {
        while (volume > 0.0f)
        {
            if (isPlayerEnter)
                break;

            volume -= ChangeVolume(maxVolume, volumeChangeRate);

            yield return null;
        }

        if (isPlayerEnter == false)
        {
            stopAlarm.Invoke();
        }

    }

    private float ChangeVolume(float maxVolume, float volumeChangeRate)
    {
        return Mathf.MoveTowards(0, maxVolume, volumeChangeRate);
    }
}
