using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource sourceMusic;
    [SerializeField] private AudioSource sourceFx;

    [SerializeField] private AudioClip defaultMusic;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if(defaultMusic!=null){
            PlayMusic(defaultMusic);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        sourceMusic.clip = clip;
        sourceMusic.Play();
    }
    public void PlayEffect(AudioClip clip)
    {
        sourceFx.PlayOneShot(clip);
    }

    public void SetVolumeMusic(float amount)
    {
        sourceMusic.volume = amount;
    }

    public void SetVolumeFx(float amount)
    {
        sourceFx.volume = amount;
    }
}
