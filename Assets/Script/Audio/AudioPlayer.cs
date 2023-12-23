using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioPlayer instance;

    [SerializeField]
    private AudioSource sfxAudioSource;

    [SerializeField]
    private AudioSource bgmAudioSource;


    [SerializeField]
    private List<AudioClip> sfxClip;

    [SerializeField]
    private List<AudioClip> bgmClip;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;

        }


        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }



    public void PlayBGM(int index)
    {
        if (bgmAudioSource.clip != null)
        {
            bgmAudioSource.Stop();
        }
        bgmAudioSource.clip = bgmClip[index];
        bgmAudioSource.Play();
    }


    public void PlaySFX(int index)
    {
        if (sfxAudioSource.clip != null)
        {
            sfxAudioSource.Stop();
        }
        sfxAudioSource.clip = sfxClip[index];
        sfxAudioSource.Play();
    }
}
