using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    [SerializeField] Ball ball;

    private void OnCollisionEnter2D(Collision2D collision) {
        ball.HandlePaddleCollision(collision);
    }
}
