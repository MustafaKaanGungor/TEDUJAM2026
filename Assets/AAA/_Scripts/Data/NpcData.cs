using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Data/NpcData")]
public class NpcData : ScriptableObject
{
    [field: SerializeField] public string NpcName { get; private set; }
    [field: SerializeField] public Sprite NpcSprite { get; private set; }
    [field: SerializeField] public NpcDialogue Dialogues { get; private set; } = new NpcDialogue();
    [field: SerializeField] public IslandType DesiredIsland { get; set; }
    //[HideInInspector] public NpcState State { get; set; } = NpcState.alive;
    
    public enum NpcState
    {
        alive,
        dead
    }
}

[Serializable]
public class NpcDialogue
{   
    public Direction direction1;
    public IslandType islandOnDirection1;        
    public Direction direction2;
    public IslandType islandOnDirection2;
}