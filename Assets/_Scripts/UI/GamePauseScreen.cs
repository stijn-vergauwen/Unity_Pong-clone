using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseScreen : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] RectTransform pauseScreen;

    private void OnEnable() {
        GameManager.OnGamePaused += ToggleScreen;
    }

    private void OnDisable() {
        GameManager.OnGamePaused -= ToggleScreen;
    }

    private void Start() {
        ToggleScreen(false);
    }

    public void ExitToStartScreen() {
        gameManager.ExitToStartScreen();
    }

    private void ToggleScreen(bool show) {
        pauseScreen.gameObject.SetActive(show);
    }
}
