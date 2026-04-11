using System;
using Unity.VisualScripting;
using UnityEngine;

public static class GameEvents 
{
    public static Action<string> PlaySound;
    public static Action<int> DayChanged;
    public static Action ChangeInputAuthorityToPlayer; 
    public static Action ChangeInputAuthorityToNpc;
}
