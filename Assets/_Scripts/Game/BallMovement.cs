using UnityEngine;

[RequireComponent(typeof(Ball))]
public class BallMovement : MonoBehaviour
{
    [Header("Ball settings")]
    [SerializeField, Min(0)] float moveSpeed = 4;
    [SerializeField] Vector3 startPosition;
    [SerializeField, Range(0, 70)] int maxStartAngle = 40;
    [SerializeField, Range(0, 1)] float bounceAngleMultiplier = .4f;

    [Header("Movement borders")]
    [SerializeField] Vector2 movementBorders;
    [SerializeField] bool showBorders;

    private Ball ball;

    private Vector2 position;
    private Vector2 moveDirection;

    public void StartBallMovement(Vector2 startDirection) {
        int angleOffset = UnityEngine.Random.Range(-maxStartAngle, maxStartAngle);
        moveDirection = (Quaternion.AngleAxis(angleOffset, Vector3.forward) * startDirection).normalized;
    }

    public void ResetBall() {
        position = startPosition;
        moveDirection = Vector2.zero;
    }
    
    public void OnPaddleHit(Vector2 paddlePosition) {
        moveDirection = CalculateNewMoveDirection(transform.position, paddlePosition);
    }

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }

    private void Awake() {
        ball = GetComponent<Ball>();
    }

    private void Update() {
        if(!ball.movementPaused) {
            UpdatePosition(moveDirection);
            CheckHorizontalBorders();
            UpdateDirection();
        }
    }

    private void UpdatePosition(Vector2 moveDirection) {
        position += moveDirection * Time.deltaTime * moveSpeed;
        transform.position = position;
    }

    private void CheckHorizontalBorders() {
        if(position.x < -movementBorders.x || position.x > movementBorders.x) {
            // print("Horizontal border hit!");
            ball.RaiseOnEdgeHit(position);
        }
    }

    private void UpdateDirection() {
        if(HitVerticalBorder()) {
            moveDirection.y = -moveDirection.y;
        }
    }

    private bool HitVerticalBorder() {
        return (position.y >= movementBorders.y && moveDirection.y > 0) || (position.y <= -movementBorders.y && moveDirection.y < 0);
    }

    private Vector2 CalculateNewMoveDirection(Vector2 ballPosition, Vector2 paddlePosition) {
        Vector2 paddleToBallDir = (ballPosition - paddlePosition).normalized;
        paddleToBallDir.y *= bounceAngleMultiplier;
        return paddleToBallDir.normalized;
    }

    private void OnDrawGizmos() {
        if(showBorders) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, movementBorders * 2);
        }
    }
}
