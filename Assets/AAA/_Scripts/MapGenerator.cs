using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapNode[,] mapArray = new MapNode[5,5];
    private Dictionary<IslandType, int> islandTypeAmounts = new Dictionary<IslandType, int>
    {
        {IslandType.EMPTY, 8}, {IslandType.BASE, 1}, {IslandType.ISLAND1, 1}, {IslandType.ISLAND2, 1}, {IslandType.ISLAND3, 1},
        {IslandType.ISLAND4, 1}, {IslandType.ISLAND5, 1}, {IslandType.ISLAND6, 1}, {IslandType.DANGER1, 3}, {IslandType.DANGER2, 2},
        {IslandType.DANGER3, 1}, {IslandType.LANDMARK1, 1}, {IslandType.LANDMARK2, 1}, {IslandType.LANDMARK3, 1}, {IslandType.LANDMARK4, 1} 
    };

    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                mapArray[i, j] = new MapNode();
            }
        }
    }
}

public struct MapNode
{
    public IslandType type;
    public Vector2 coordinate;
    public Vector3 position;
}

public enum Direction
{
    NORTH,
    SOUTH,
    WEST,
    EAST,
    NORTHWEST,
    NORTHEAST,
    SOUTHWEST,
    SOUTHEAST
}

public enum IslandType
{
    EMPTY = 8,
    BASE = 1,
    ISLAND1 = 1,
    ISLAND2 = 1,
    ISLAND3 = 1,
    ISLAND4 = 1,
    ISLAND5 = 1,
    ISLAND6 = 1,
    DANGER1 = 3,
    DANGER2 = 2,
    DANGER3 = 1,
    LANDMARK1 = 1,
    LANDMARK2 = 1,
    LANDMARK3 = 1,
    LANDMARK4 = 1
}