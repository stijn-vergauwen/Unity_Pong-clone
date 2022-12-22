using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Paddle))]
public class PaddleInput : MonoBehaviour
{
    public enum InputSource {Player, Bot}
    public enum UsedKeys {SAndX, Arrows}

    [SerializeField] InputSource inputSource;
    [SerializeField] UsedKeys usedKeys;

    private PaddleBot paddleBot;

    private float moveInput;

    public void Init(PlayerSettings settings, Transform ballTransform) {
        inputSource = settings.isBot ? InputSource.Bot : InputSource.Player;
        usedKeys = settings.keyBindings;

        if(settings.isBot) {
            paddleBot.SetSpeedMultiplier(settings.botSpeedMultiplier);
            paddleBot.SetBallTransform(ballTransform);
        }
    }

    public float GetMoveInput() {
        return moveInput;
    }

    private void Awake() {
        if(inputSource == InputSource.Bot) {
            paddleBot = GetComponent<PaddleBot>();
        }
    }

    private void Update() {
        UpdateMoveInput();
    }

    private void UpdateMoveInput() {
        float newMoveInput = 
            inputSource == InputSource.Player ?
            GetPlayerInput() :
            GetBotInput()
        ;

        moveInput = newMoveInput;
    }

    private int GetPlayerInput() {
        bool inputUp;
        bool inputDown;

        if(usedKeys == UsedKeys.Arrows) {
            inputUp = Input.GetKey(KeyCode.UpArrow);
            inputDown = Input.GetKey(KeyCode.DownArrow);

        } else {
            inputUp = Input.GetKey(KeyCode.S);
            inputDown = Input.GetKey(KeyCode.X);
        }

        int playerInput = 0;

        if(inputUp) playerInput++;
        if(inputDown) playerInput--;
        
        return playerInput;
    }

    private float GetBotInput() {
        return paddleBot.CalculateInput();
    }
}
