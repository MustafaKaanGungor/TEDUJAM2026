using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Data/NpcData")]
public class NpcData : ScriptableObject
{
    [field: SerializeField] public string NpcName { get; private set; }
    [field: SerializeField] public Sprite NpcSprite { get; private set; }
    [field: SerializeField] public NpcDialogue[] Dialogues { get; private set; } = new NpcDialogue[2];
    [HideInInspector] public NpcState State { get; set; } = NpcState.alive;
    public struct NpcDialogue
    {
        public Direction direction1;
        public Direction direction2;
    }
    public enum NpcState
    {
        alive,
        dead
    }
}
