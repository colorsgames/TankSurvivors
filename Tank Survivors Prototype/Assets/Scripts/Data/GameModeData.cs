using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Game Mode Data", menuName = "Game Mode", order = 62)]
public class GameModeData : ScriptableObject
{
    [HideInInspector] public int maxMunites;
    [HideInInspector] public int profitRatio;
}
