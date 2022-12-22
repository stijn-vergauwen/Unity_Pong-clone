using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishScreen : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] RectTransform finishScreen;
    [SerializeField] float distanceFromCenter;

    private void OnEnable() {
        GameManager.OnGameFinish += ShowScreen;
        GameManager.OnGameReset += HideScreen;
    }

    private void OnDisable() {
        GameManager.OnGameFinish -= ShowScreen;
        GameManager.OnGameReset -= HideScreen;
    }

    public void NewGame() {
        gameManager.ResetGame();
    }

    public void ExitToStartScreen() {
        gameManager.ExitToStartScreen();
    }

    private void ShowScreen(PlayerSide sideThatWon) {
        finishScreen.gameObject.SetActive(true);

        float horizontalPosition = (
            sideThatWon == PlayerSide.RightPlayer ?
            distanceFromCenter :
            -distanceFromCenter
        );

        Vector2 screenPostion = new Vector2(horizontalPosition, finishScreen.anchoredPosition.y);
        finishScreen.anchoredPosition = screenPostion;
    }

    private void HideScreen() {
        finishScreen.gameObject.SetActive(false);
    }
}
