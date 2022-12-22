using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Paddle))]
public class PaddleMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float maxDistanceFromCenter;

    private Paddle paddle;
    private Vector3 startPosition;
    private float verticalPosition;

    private void OnEnable() {
        GameManager.OnGameReset += ResetPosition;
    }

    private void OnDisable() {
        GameManager.OnGameReset -= ResetPosition;
    }

    private void Awake() {
        paddle = GetComponent<Paddle>();
        startPosition = transform.position;
    }

    private void Update() {
        if(!paddle.movementPaused) {
            MovePaddle(paddle.input.GetMoveInput());
        }
    }

    private void ResetPosition() {
        verticalPosition = 0;
        transform.position = startPosition;
    }

    public void SetMoveSpeed(float newValue) {
        moveSpeed = newValue;
    }

    private void MovePaddle(float moveInput) {
        verticalPosition += moveInput * Time.deltaTime * moveSpeed;

        verticalPosition = Mathf.Clamp(verticalPosition, -maxDistanceFromCenter, maxDistanceFromCenter);

        transform.position = startPosition + Vector3.up * verticalPosition;
    }
}
