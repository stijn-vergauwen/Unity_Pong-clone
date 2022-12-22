using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameSettingsSO gameSettings;
    [SerializeField] AudioSource audioSource;

    [Header("Audio clips")]
    [SerializeField] AudioClip buttonClickAudio;
    [SerializeField] AudioClip BallHitAudio;
    [SerializeField] AudioClip BallScoreAudio;

    [Header("Channels")]
    [SerializeField] AudioEventChannelSO audioChannel;

    private void OnEnable() {
        audioChannel.OnEventRaised += HandleAudioRequest;
    }

    private void OnDisable() {
        audioChannel.OnEventRaised -= HandleAudioRequest;
    }

    private void HandleAudioRequest(AudioClipInfo clipInfo) {
        AudioClip audioClip;
        float volume = 1;

        if(clipInfo.clipName == AudioClipName.ButtonClick) {
            audioClip = buttonClickAudio;
            volume = .4f;

        } else if(clipInfo.clipName == AudioClipName.BallHit) {
            audioClip = BallHitAudio;

        } else {
            audioClip = BallScoreAudio;
        } 

        PlayAudio(audioClip, volume);
    }

    private void PlayAudio(AudioClip audioClip, float volume) {
        if(gameSettings.useAudio) {
            audioSource.PlayOneShot(audioClip, volume);
        }
    }
}
public enum AudioClipName {ButtonClick, BallHit, BallScore};

public struct AudioClipInfo {
    public AudioClipName clipName;

    public AudioClipInfo(AudioClipName clipName) {
        this.clipName = clipName;
    }
}
