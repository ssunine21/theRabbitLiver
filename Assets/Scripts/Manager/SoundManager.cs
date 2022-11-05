using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public int SFXVolume {
        set {
            _sfxAudioSource.volume = value;
            _monsterAudioSource.volume = value;
            _playerAudioSource.volume = value;
        }
    }
    public int BGMVolume {
        set {
            _bgmAudioSource.volume = value;
        }
    }

    [SerializeField] SerializableDictionary<Definition.SoundType, AudioClip> clips;

    private AudioSource _bgmAudioSource;
    private AudioSource _sfxAudioSource;
    private AudioSource _monsterAudioSource;
    private AudioSource _playerAudioSource;

    private bool isBGMOff;

    private void Awake() {
        Singleton();
        _bgmAudioSource = transform.Find("BGMPlayer").GetComponent<AudioSource>();
        _sfxAudioSource = transform.Find("SFXPlayer").GetComponent<AudioSource>();
        _monsterAudioSource = transform.Find("MonsterPlayer").GetComponent<AudioSource>();
        _playerAudioSource = transform.Find("PlayerPlayer").GetComponent<AudioSource>();
    }

    private void Start() {
        _bgmAudioSource.clip = clips[Definition.SoundType.Intro];
        _bgmAudioSource.loop = true;
        _bgmAudioSource.Play();

        _bgmAudioSource.volume = PlayerPrefs.GetInt("BGMVolum", 1);
        _sfxAudioSource.volume = PlayerPrefs.GetInt("SFXVolum", 1);
        _monsterAudioSource.volume = PlayerPrefs.GetInt("SFXVolum", 1);
        _playerAudioSource.volume = PlayerPrefs.GetInt("SFXVolum", 1);
    }

    public AudioClip GetClips(Definition.SoundType key) {
        return clips[key];
    }

    public void ChangeBGM(Definition.SoundType soundType) {
        if (_bgmAudioSource.volume <= 0) return;
        StartCoroutine(CoBGMChange(soundType));
    }

    public void PlaySFXSound(Definition.SoundType soundType) {

        _sfxAudioSource.clip = clips[soundType];

        _sfxAudioSource.Play();
    }
    public void PlayMonsterSound(Definition.SoundType soundType) {

        _monsterAudioSource.clip = clips[soundType];
        _monsterAudioSource.Play();
    }
    public void PlayPlayerSound(Definition.SoundType soundType) {

        _playerAudioSource.clip = clips[soundType];
        _playerAudioSource.Play();
    }

    public void StopPlayerSound() {
        _playerAudioSource.Stop();
    }

    public AudioClip GetAudioClip(Definition.SoundType soundType) {
        return clips[soundType];
    }

    private IEnumerator CoBGMChange(Definition.SoundType soundType) {
        StartCoroutine(CoSoundFadeOut(_bgmAudioSource));
        yield return new WaitForSeconds(2f);
        _bgmAudioSource.clip = clips[soundType];
        _bgmAudioSource.Play();
        StartCoroutine(CoSoundFadeIn(_bgmAudioSource));
    }

    private IEnumerator CoSoundFadeOut(AudioSource audioSource) {

        while(audioSource.volume > 0) {
            audioSource.volume -= Time.deltaTime * 0.5f;
            yield return null;
        }
        audioSource.volume = 0;
    }

    private IEnumerator CoSoundFadeIn(AudioSource audioSource) {

        while (audioSource.volume < 1) {
            audioSource.volume += Time.deltaTime * 0.5f;
            yield return null;
        }
        audioSource.volume = 1;
    }

    public static SoundManager init;
    private void Singleton() {
        if (init == null) {
            init = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}