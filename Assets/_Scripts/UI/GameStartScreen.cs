using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScreen : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] RectTransform startScreen;

    private void OnEnable() {
        GameManager.OnRoundStart += HideScreen;
        GameManager.OnGameReset += ShowScreen;
    }

    private void OnDisable() {
        GameManager.OnRoundStart -= HideScreen;
        GameManager.OnGameReset -= ShowScreen;
    }

    private void ShowScreen() {
        startScreen.gameObject.SetActive(true);
    }

    private void HideScreen(PlayerSide playerSide) {
        startScreen.gameObject.SetActive(false);
    }
}
