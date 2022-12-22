using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] GameSettingsSO gameSettings;
    [SerializeField] Transform dynamicSpace;

    [Header("Prefabs")]
    [SerializeField] Paddle playerPrefab;
    [SerializeField] Paddle botPrefab;
    [SerializeField] Ball ballPrefab;

    public void SetupGame() {
        Ball ball = InitBall(gameSettings.ballSpeed);
        InitPaddle(gameSettings.leftPlayer, PlayerSide.LeftPlayer, ball.transform);
        InitPaddle(gameSettings.rightPlayer, PlayerSide.RightPlayer, ball.transform);
    }

    private Paddle InitPaddle(PlayerSettings settings, PlayerSide side, Transform ballTransform) {
        float horizontalPosition = (side == PlayerSide.LeftPlayer ? -8 : 8);
        Vector3 startPosition = Vector3.right * horizontalPosition;

        Paddle prefab = settings.isBot ? botPrefab : playerPrefab;

        Paddle newPaddle = Instantiate(prefab, startPosition, Quaternion.identity, dynamicSpace);
        newPaddle.Init(settings, ballTransform);

        return newPaddle;
    }

    private Ball InitBall(float ballSpeed) {
        Vector3 startPosition = Vector3.zero;
        Ball newBall = Instantiate(ballPrefab, startPosition, Quaternion.identity, dynamicSpace);
        newBall.Init(ballSpeed);

        return newBall;
    }
}
