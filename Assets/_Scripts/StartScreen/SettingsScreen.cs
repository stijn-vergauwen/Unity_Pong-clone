using UnityEngine.UI;
using UnityEngine;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] GameSettingsSO gameSettings;
    
    [Header("UI Elements")]
    [SerializeField] Toggle fullScreenToggle;

    private void Start() {
        fullScreenToggle.isOn = Screen.fullScreen;
    }
    
    public void SetFullScreen(bool isFullScreen) {
        if(Screen.fullScreen == isFullScreen) return;

        Screen.fullScreen = isFullScreen;

        if(isFullScreen) {
            Resolution highestRes = Screen.resolutions[Screen.resolutions.Length - 1];
            Screen.SetResolution(highestRes.width, highestRes.height, FullScreenMode.FullScreenWindow);
        }
    }

    public void SetAudio(bool useAudio) {
        gameSettings.SetUseAudio(useAudio);
    }
}
