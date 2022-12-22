using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameDifficultyData")]
public class GameDifficultySO : ScriptableObject
{
    public Difficulty difficulty;

    public float ballSpeed;
    
    public float playerMoveSpeed;
    public float botMoveSpeed;

    public float botSpeedMultiplier;
}

public enum Difficulty {Easy, Medium, Hard}
