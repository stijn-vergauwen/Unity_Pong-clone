using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameSetup gameSetup;

    [SerializeField] PlayerSide sideToStart = PlayerSide.RightPlayer;

    [Header("Channels")]
    [SerializeField] SceneLoadEventChannelSO sceneLoadChannel;
    [SerializeField] AudioEventChannelSO audioChannel;

    private bool gameActive = false;
    private bool gamePaused = false;
    private bool gameFinished = false;
    
    public static Action<PlayerSide> OnScored;

    public static Action OnRoundStop;
    public static Action<PlayerSide> OnRoundStart;

    public static Action<bool> OnGamePaused;
    public static Action OnGameReset;
    public static Action<PlayerSide> OnGameFinish;

    private void OnEnable() {
        Ball.OnEdgeHit += HandleBallOnEdge;
        ScoreCounter.OnFullScore += HandlePlayerWon;
    }

    private void OnDisable() {
        Ball.OnEdgeHit -= HandleBallOnEdge;
        ScoreCounter.OnFullScore -= HandlePlayerWon;
    }

    private void Start() {
        gameSetup.SetupGame();
        ResetGame();
    }

    private void Update() {
        if(!gameActive && Input.GetKeyDown(KeyCode.Space)) {
            if(gameFinished) {
                ResetGame();

            } else {
                StartGame();
            }

        } else if(Input.GetKeyDown(KeyCode.P)) {
            ToggleGamePaused();
        }
    }

    private void HandleBallOnEdge(Vector2 point) {
        EndRound(
            point.x < 0 ?
            PlayerSide.RightPlayer :
            PlayerSide.LeftPlayer
        );
    }

    private void HandlePlayerWon(PlayerSide playerSide) {
        // TODO: show UI stuff for who won
        OnGameFinish?.Invoke(playerSide);
        StopGame();
    }

    private void StartGame() {
        if(!gameActive && !gameFinished) {
            gameActive = true;
            StartRound();
        }
    }

    private void StopGame() {
        gameActive = false;
        gameFinished = true;
        OnRoundStop?.Invoke();
    }

    public void ResetGame() {
        gameFinished = false;
        OnGameReset?.Invoke();
    }

    private void ToggleGamePaused() {
        if(gameActive) {
            gamePaused = !gamePaused;
            // TODO: show pause screen
            OnGamePaused?.Invoke(gamePaused);
        }
    }

    private void StartRound() {
        if(gameActive) {
            OnRoundStart?.Invoke(sideToStart);
            sideToStart = (
                sideToStart == PlayerSide.RightPlayer ?
                PlayerSide.LeftPlayer :
                PlayerSide.RightPlayer
            );
        }
    }

    private void EndRound(PlayerSide playerThatScored) {
        OnScored?.Invoke(playerThatScored);
        OnRoundStop?.Invoke();

        audioChannel.RequestAudio(new AudioClipInfo(AudioClipName.BallScore));

        Invoke("StartRound", 1);
    }

    public void ExitToStartScreen() {
        sceneLoadChannel.RequestSceneLoad(SceneName.StartScreen);
    }

}

public enum PlayerSide {LeftPlayer, RightPlayer}
