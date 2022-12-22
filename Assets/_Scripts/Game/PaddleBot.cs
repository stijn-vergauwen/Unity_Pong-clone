using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBot : MonoBehaviour
{
    [SerializeField] Transform ball;

    [Header("Bot settings")]
    [SerializeField, Range(0, 20)] float distToBallThreshold = 10;
    [SerializeField, Range(0, .8f)] float moveThreshold;
    [SerializeField, Range(0, .5f)] float moveToBallPrecision;
    [SerializeField] float speedMultiplier = 1;

    private bool isMoving = false;

    public void SetSpeedMultiplier(float speedMultiplier) {
        this.speedMultiplier = speedMultiplier;
    }

    public void SetBallTransform(Transform ballTransform) {
        ball = ballTransform;
    }

    private void Start() {
        if(moveToBallPrecision > moveThreshold) {
            moveToBallPrecision = moveThreshold;
        }
    }

    public float CalculateInput() {
        Vector2 positionToBall = (ball.position - transform.position);
        Vector2 dirToBall = positionToBall.normalized;
        float calculatedInput = 0;

        if(Mathf.Abs(positionToBall.x) < distToBallThreshold) {
            float verticalInput = dirToBall.y;

            if(IsAboveMoveThreshold(verticalInput)) {
                calculatedInput = Mathf.Clamp(verticalInput * speedMultiplier, -1, 1);
                isMoving = true;

            } else {
                isMoving = false;
            }
        }

        return Mathf.RoundToInt(calculatedInput);
    }

    private bool IsAboveMoveThreshold(float input) {
        return Mathf.Abs(input) > (isMoving ? moveToBallPrecision : moveThreshold);
    }
}
