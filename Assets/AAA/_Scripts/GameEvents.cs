using System;
using Unity.VisualScripting;
using UnityEngine;

public static class GameEvents 
{
    public static Action<string> PlaySound;
    public static Action<int> DayChanged;
    public static Action ChangeInputAuthorityToPlayer; 
    public static Action ChangeInputAuthorityToNpc;
    public static Action<MapNode[,]> MapGenerated;
    public static Action<Direction,Direction> PlayerMadeASelection;
    public static Action NpcDied;
    public static Action NpcSuccessful;
    public static Action<int, int, MapNode[,]> GameEnd;
    public static Action ShowTutorial_Game;
    public static Action TutorialFinished_TutorialUI;
}
