using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum eTypeBGM
    {
        HOME = 0,
        INGAME,

        max
    }

    public enum eTypeEffectSound
    {
        ATTACKPLAYER = 0,
        ATTACKMONSTER,

        max
    }

#pragma warning disable 0649
    [SerializeField]
    AudioClip[] _bgmClips;
    [SerializeField]
    AudioClip[] _effectClips;
#pragma warning restore

    float _volumBGM = 0.1f;
    float _volumEffect = 1;
    bool _muteBGM = false;
    bool _muteEffect = false;

    AudioSource _playerBGM;
    List<AudioSource> _playersEff = new List<AudioSource>();

    static SoundManager _uniqueInstance;
    public static SoundManager _instance { get { return _uniqueInstance; } }

    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        for (int n = 0; n < _playersEff.Count; n++)
        {
            if (!_playersEff[n].isPlaying)
            {
                Destroy(_playersEff[n].gameObject);
                _playersEff.RemoveAt(n);
                break;
            }
        }
    }

    void ListUpBGMSound()
    {

    }

    void ListUpEffectSound()
    {

    }

    public AudioSource PlayBGMSound(eTypeBGM type)
    {
        if (_playerBGM != null)
            Destroy(_playerBGM.gameObject);
        GameObject obj = new GameObject("BGMPlayer");
        obj.transform.parent = Camera.main.transform;
        transform.localPosition = Vector3.zero;

        _playerBGM = obj.AddComponent<AudioSource>();
        _playerBGM.clip = _bgmClips[(int)type];
        _playerBGM.volume = _volumBGM;
        _playerBGM.mute = _muteBGM;
        _playerBGM.loop = true;
        _playerBGM.Play();

        return _playerBGM;
    }

    public AudioSource PlayEffectSound(eTypeEffectSound type, Transform owner)
    {
        GameObject obj = new GameObject("EffectSound");
        obj.transform.parent = owner.transform;
        transform.localPosition = Vector3.zero;
        AudioSource effPlayer = obj.AddComponent<AudioSource>();
        effPlayer.clip = _effectClips[(int)type];
        effPlayer.volume = _volumEffect;
        effPlayer.mute = _muteEffect;
        effPlayer.loop = false;

        effPlayer.Play();
        _playersEff.Add(effPlayer);

        return effPlayer;
    }
}
