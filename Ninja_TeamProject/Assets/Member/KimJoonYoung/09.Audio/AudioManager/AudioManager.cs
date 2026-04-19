using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#BGM")]
    public AudioClip[] sfxclips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { avgAtk0, avgAtk1, avgAtk2, QSkill , Hit , Step1 , Step2 , Step3}

    private void Awake()
    {
        instance = this;
        Init(); ;
    }
    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false; // 시작시 x
        bgmPlayer.loop = true; // 반복
        bgmPlayer.clip = bgmClip;

        // 플레이어 효과음 
        GameObject sfxObject = new GameObject("SfxObject");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake =false;
            sfxPlayers[i].volume = sfxVolume;
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length;i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[0].clip = sfxclips[(int)sfx];
            sfxPlayers[0].Play();
            break;
        }
        sfxPlayers[0].clip = sfxclips[(int)sfx];
        sfxPlayers[0].Play();
    }
}
