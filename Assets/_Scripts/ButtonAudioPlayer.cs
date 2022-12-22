using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioEventChannelSO audioChannel;

    public void PlayMenuAudio() {
        audioChannel.RequestAudio(new AudioClipInfo(AudioClipName.ButtonClick));
    }
}
