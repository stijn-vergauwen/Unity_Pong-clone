using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] PaddleInput paddleInput;
    [SerializeField] PaddleMovement paddleMovement;

    public PaddleInput input => paddleInput;

    public bool movementPaused {get; private set;}

    private void OnEnable() {
        GameManager.OnGamePaused += OnGamePaused;
    }

    private void OnDisable() {
        GameManager.OnGamePaused -= OnGamePaused;
    }

    private void OnGamePaused(bool isPaused) {
        movementPaused = isPaused;
    }

    public void Init(PlayerSettings settings, Transform ballTransform) {
        paddleMovement.SetMoveSpeed(settings.moveSpeed);
        paddleInput.Init(settings, ballTransform);
    }
}
