using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/AudioEvent")]
public class AudioEventChannelSO : ScriptableObject
{
    public Action<AudioClipInfo> OnEventRaised;

    public void RequestAudio(AudioClipInfo audioClipInfo) {
        if(OnEventRaised != null) {
            OnEventRaised.Invoke(audioClipInfo);

        } else {
            Debug.LogWarning("EVENT HAS NO LISTENER: Audio event was called but not picked up, check if AudioManager is active and listening");
        }
    }
}
