using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusicManager : MonoBehaviour
{

    #region Fields

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClipData _audioClipData;

    [SerializeField] private float _fadeTime;

    private bool _isFading;

    #endregion

    #region Methods

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClipData.AudioClips[0];
        _audioSource.Play();
    }

    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            PlayRandomAudioClip();
            StartCoroutine(Fade(_audioSource.volume, 1.0f));
        }

        if (!_isFading && _audioSource.clip.length - _audioSource.time <= _fadeTime)
            StartCoroutine(Fade(_audioSource.volume, 0.0f));
    }

    private void PlayRandomAudioClip()
    {
        _audioSource.Stop();
        int index = Random.Range(0, _audioClipData.AudioClips.Length);
        _audioSource.clip = _audioClipData.AudioClips[index];
        _audioSource.Play();
    }

    private IEnumerator Fade(float startVolume, float endVolume)
    {
        _isFading = true;
        float t = 0.0f;

        while (t < _fadeTime)
        {
            t += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(startVolume, endVolume, t / _fadeTime);
            yield return null;
        }
        _isFading = false;
    }

    #endregion

}
