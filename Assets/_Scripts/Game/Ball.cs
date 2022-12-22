using System;
using UnityEngine;

[RequireComponent(typeof(BallMovement))]
public class Ball : MonoBehaviour
{
    [SerializeField] GameObject ballObject;

    [Header("Channels")]
    [SerializeField] AudioEventChannelSO audioChannel;

    BallMovement ballMovement;

    public bool movementPaused {get; private set;}

    public static Action<Vector2> OnEdgeHit;
    public static Action OnPaddleHit;

    public void Init(float moveSpeed) {
        ballMovement.SetMoveSpeed(moveSpeed);
    }

    private void Awake() {
        ballMovement = GetComponent<BallMovement>();
        SetBallActive(false);
    }

    private void OnEnable() {
        GameManager.OnGamePaused += OnGamePaused;
        GameManager.OnRoundStop += OnRoundStop;
        GameManager.OnRoundStart += OnRoundStart;
    }

    private void OnDisable() {
        GameManager.OnGamePaused -= OnGamePaused;
        GameManager.OnRoundStop -= OnRoundStop;
        GameManager.OnRoundStart -= OnRoundStart;
    }

    private void OnGamePaused(bool isPaused) {
        movementPaused = isPaused;
    }

    private void OnRoundStop() {
        ballMovement.ResetBall();
        SetBallActive(false);
    }

    private void OnRoundStart(PlayerSide startSide) {
        Vector2 startDirection = (
            startSide == PlayerSide.LeftPlayer ?
            Vector2.left :
            Vector2.right
        );

        ballMovement.StartBallMovement(startDirection);
        SetBallActive(true);
    }

    private void SetBallActive(bool value) {
        ballObject.SetActive(value);
    }

    public void RaiseOnEdgeHit(Vector2 ballPosition) {
        OnEdgeHit?.Invoke(ballPosition);
    }

    public void HandlePaddleCollision(Collision2D collision) {
        Vector3 paddlePosition = collision.transform.position;
        ballMovement.OnPaddleHit(paddlePosition);

        // request audio
        audioChannel.RequestAudio(new AudioClipInfo(AudioClipName.BallHit));

        Paddle paddle;
        if(collision.transform.parent.TryGetComponent<Paddle>(out paddle)) {
            OnPaddleHit?.Invoke();
        }
    }
}
