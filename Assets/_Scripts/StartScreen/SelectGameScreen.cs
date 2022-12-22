using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameScreen : MonoBehaviour
{
    [SerializeField] GameObject playerSelectScreen;
    [SerializeField] GameObject difficultySelectScreen;
    [SerializeField] GameSettingsSO gameSettings;
    [SerializeField] GameDifficultySO[] gameDifficulties;

    [Header("Channels")]
    [SerializeField] SceneLoadEventChannelSO sceneLoadChannel;

    public void HandlePlayerSelect(int playerCount) {
        gameSettings.SetPlayers(playerCount);
        ShowDifficultySelectScreen();
    }

    public void HandleDifficultySelect(int difficulty) {
        gameSettings.SetDifficulty(
            GetGameDifficultyByNumber(difficulty)
        );
        StartGame();
    }

    private void Start() {
        ShowPlayerSelectScreen();
    }

    private void StartGame() {
        sceneLoadChannel.RequestSceneLoad(SceneName.Game);
    }

    public void ShowPlayerSelectScreen() {
        HideAllScreens();
        playerSelectScreen.SetActive(true);
    }

    private void ShowDifficultySelectScreen() {
        HideAllScreens();
        difficultySelectScreen.SetActive(true);
    }

    private void HideAllScreens() {
        playerSelectScreen.SetActive(false);
        difficultySelectScreen.SetActive(false);
    }

    private GameDifficultySO GetGameDifficultyByNumber(int difficultyNumber) {
        return gameDifficulties[difficultyNumber - 1];
    }
}
