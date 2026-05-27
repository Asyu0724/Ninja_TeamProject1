using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    public static AudioManager instance;
    private float _bgmVolume;
    private float _sfxVolume;
    

    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayers;

    [Header("#BGM")]
    public AudioClip[] sfxclips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx
    {
        avgAtk0, avgAtk1, avgAtk2, QSkill , Hit , Step1 , Step2 , Step3 , Crack , Tick , Majestic , Attacked ,
        BossAtk1 , BossAtk2 , BossAtk3 , BossCharge , BossDash , BossDeath , BossJump , BossLand, BossWalk ,
        MinBossAtk1 , MinBossAtk2 , MinBossAtk3 , SharpSweep , MinBossDeath ,
        Spin , Cloud1 , Cloud2 , Quake , CloudCharge , GolemDamage ,
        PlrFootStep
    }
    public enum Bgm { Main , SpiderBoss , MinBoss , GolemBoss , TutoMap}

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(instance.gameObject);
        instance = this;
        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;

        // 플레이어 효과음 
        GameObject sfxObject = new GameObject("SfxObject");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }
        
        bgmPlayers = bgmObject.AddComponent<AudioSource>();
        bgmObject.GetComponent<AudioSource>().outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        bgmPlayers.playOnAwake = false;
        bgmPlayers.loop = true;
        bgmPlayers.volume = bgmVolume;
    }

    public void PlayBgm(Bgm bgm , bool loop)
    {
        StartCoroutine(FadeIn(bgm, loop));
    }

    public void StopBgm()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn(Bgm bgm, bool loop, float time = 2f)
    {
        bgmPlayers.clip = bgmClips[(int)bgm];
        bgmPlayers.loop = loop;
        bgmPlayers.volume = 0f;
        bgmPlayers.Play();
        while (bgmPlayers.volume < bgmVolume)
        {
            bgmPlayers.volume += Time.deltaTime * bgmVolume/time;
            yield return null;
        }
    }

    private IEnumerator FadeOut(float time = 2f)
    {
        while (bgmPlayers.volume > 0f)
        {
            bgmPlayers.volume -= Time.deltaTime * bgmVolume/time;
            yield return null;
        }
    }
    
    public void PlaySfx(Sfx sfx , int ch)
    {
        for (int i = 0; i < sfxPlayers.Length;i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[ch].clip = sfxclips[(int)sfx];
            sfxPlayers[ch].Play();
            break;
        }
    }

    public void SfxSoundVolume(float value)
    {
        sfxVolume = Mathf.Log10(value) * 20;
        mixer.SetFloat("SFXVolume", sfxVolume);
    }
    public void BgmSoundVolume(float value)
    {
        _bgmVolume = Mathf.Log10(value) * 20;
        mixer.SetFloat("BGMVolume", _bgmVolume);
    }
}
