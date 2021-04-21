using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioMnager : MonoBehaviour
{
    public GameInformation GameInfo;
    public AudioSource audioToPlay;
    private void Start()
    {
        SetAudio();
    }
    public void SetAudio()
    {
        Slider volumeController = GetComponent<Slider>();
        AudioSource[] audios = FindObjectsOfType<AudioSource>(true);
        volumeController.value = GameInfo.volume;
        foreach (AudioSource audio in audios)
        {
            audio.volume = volumeController.value;
        }
    }
    public void AudioED(Slider volumeController)
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audios)
        {
            if (audio.isPlaying)
            {
                audio.Pause();
                audioToPlay = audio;
                volumeController.value = volumeController.minValue;
            }
            else
            {
                if(audioToPlay != null)
                {
                    if (audio == audioToPlay) audio.Play();
                }
                else
                {
                    audio.Play();
                }
                volumeController.value = volumeController.maxValue;
            }
        }
        GameInfo.volume = volumeController.value;
    }

    public void VolumeControl(Slider volumeController)
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>(true);
        foreach (AudioSource audio in audios)
        {
            audio.volume = volumeController.value;
        }
        GameInfo.volume = volumeController.value;
    }

}
