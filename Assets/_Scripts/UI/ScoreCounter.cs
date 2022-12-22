using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] PlayerSide sideToTrack;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int scoreToWin;

    private int currentScore = 0;

    public static System.Action<PlayerSide> OnFullScore;

    private void OnEnable() {
        GameManager.OnScored += HandleOnScored;
        GameManager.OnGameReset += ResetScore;
    }

    private void OnDisable() {
        GameManager.OnScored -= HandleOnScored;
        GameManager.OnGameReset -= ResetScore;
    }

    private void ResetScore() {
        currentScore = 0;
        UpdateText();
    }

    private void HandleOnScored(PlayerSide playerThatScored) {
        if(playerThatScored == sideToTrack) {
            IncreaseScore();
        }
    }

    private void IncreaseScore() {
        currentScore++;
        UpdateText();
        CheckIfWon(currentScore);
    }

    private void CheckIfWon(int score) {
        if(score == scoreToWin) {
            OnFullScore?.Invoke(sideToTrack);
        }
    }

    private void UpdateText() {
        scoreText.text = currentScore.ToString("#0");
    }

}
