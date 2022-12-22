using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/SceneLoadEvent")]
public class SceneLoadEventChannelSO : ScriptableObject
{
    public Action<SceneName> OnEventRaised;

    public void RequestSceneLoad(SceneName sceneToLoad) {
        if(OnEventRaised != null) {
            OnEventRaised.Invoke(sceneToLoad);

        } else {
            Debug.LogWarning("EVENT HAS NO LISTENER: SceneLoad event was called but not picked up, check if SceneLoader is active and listening");
        }
    }
}
