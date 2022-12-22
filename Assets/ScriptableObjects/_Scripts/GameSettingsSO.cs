using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameSettingsData")]
public class GameSettingsSO : ScriptableObject
{
    public bool useAudio {get; private set;} = true;

    public PlayerSettings leftPlayer {get; private set;} = new PlayerSettings();
    public PlayerSettings rightPlayer {get; private set;} = new PlayerSettings();

    public float ballSpeed {get; private set;}

    public void SetUseAudio(bool useAudio) {
        this.useAudio = useAudio;
    }

    public void SetPlayers(int playerCount) {
        if(playerCount == 1) {
            SetupSinglePlayer();

        } else {
            SetupTwoPlayers();
        }
    }

    public void SetDifficulty(GameDifficultySO gameDifficulty) {
        ballSpeed = gameDifficulty.ballSpeed;
        leftPlayer.SetDifficulty(gameDifficulty);
        rightPlayer.SetDifficulty(gameDifficulty);
    }

    private void SetupSinglePlayer() {
        leftPlayer.SetPlayerMode(true, PaddleInput.UsedKeys.SAndX);
        rightPlayer.SetPlayerMode(false, PaddleInput.UsedKeys.Arrows);
    }

    private void SetupTwoPlayers() {
        leftPlayer.SetPlayerMode(false, PaddleInput.UsedKeys.SAndX);
        rightPlayer.SetPlayerMode(false, PaddleInput.UsedKeys.Arrows);
    }
}

[System.Serializable]
public class PlayerSettings {
    public bool isBot {get; private set;}
    public PaddleInput.UsedKeys keyBindings {get; private set;}

    public float moveSpeed {get; private set;}

    public float botSpeedMultiplier {get; private set;}

    public void SetPlayerMode(bool isBot, PaddleInput.UsedKeys keyBindings) {
        this.isBot = isBot;
        this.keyBindings = keyBindings;
    }

    public void SetDifficulty(GameDifficultySO gameDifficulty) {
        
        if(isBot) {
            moveSpeed = gameDifficulty.botMoveSpeed;
            botSpeedMultiplier = gameDifficulty.botSpeedMultiplier;

        } else {
            moveSpeed = gameDifficulty.playerMoveSpeed;
        }
    }
}