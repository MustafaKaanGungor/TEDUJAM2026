using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [HideInInspector]public MapNode[,] mapArray = new MapNode[5,5];
    private Dictionary<IslandType, int> islandTypeAmounts = new Dictionary<IslandType, int>
    {
        {IslandType.EMPTY, 8}, {IslandType.ISLAND1, 1}, {IslandType.ISLAND2, 1}, {IslandType.ISLAND3, 1},
        {IslandType.ISLAND4, 1}, {IslandType.ISLAND5, 1}, {IslandType.ISLAND6, 1}, {IslandType.DANGER1, 3}, {IslandType.DANGER2, 2},
        {IslandType.DANGER3, 1}, {IslandType.LANDMARK1, 1}, {IslandType.LANDMARK2, 1}, {IslandType.LANDMARK3, 1}, {IslandType.LANDMARK4, 1} 
    };

    void Awake()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if(i == 2 && j == 2)
                {
                    mapArray[i, j] = new MapNode
                    {
                        type = IslandType.BASE,
                        coordinate = new Vector2(i, j)
                    };

                    continue;
                }
                while(true)
                {
                    int randomVal = Random.Range(0, islandTypeAmounts.Count);
                    if(islandTypeAmounts[(IslandType)randomVal] > 0)
                    {
                        mapArray[i, j] = new MapNode
                        {
                            type = (IslandType)randomVal,
                            coordinate = new Vector2(i, j)
                        };

                        islandTypeAmounts[(IslandType)randomVal]--;
                        break;
                    }
                }
                
            }
        }

        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                Debug.Log(i + ", " + j + ", "+ mapArray[i,j].type);           
            }
        }
        //GameEvents.MapGenerated?.Invoke(mapArray);
    }

    public (bool, IslandType, IslandType) ExploreDirections(Direction direction1, Direction direction2)
    {
        IslandType island1 = mapArray[2 + (int)GetDirectionMovement(direction1).x, 2 + (int)GetDirectionMovement(direction1).y].type;
        IslandType island2 = mapArray[2 + (int)GetDirectionMovement(direction1).x + (int)GetDirectionMovement(direction2).x, 2 + (int)GetDirectionMovement(direction1).y + (int)GetDirectionMovement(direction2).y].type;
        bool isDead = false;
        if(island1 == IslandType.DANGER1 || island1 == IslandType.DANGER2 || island1 == IslandType.DANGER3 || island2 == IslandType.DANGER1 || island2 == IslandType.DANGER2 || island2 == IslandType.DANGER3)
        {
            isDead = true;
        }
        return (isDead, island1, island2); 
    }

    private Vector2 GetDirectionMovement(Direction direction)
    {
        switch (direction)
        {
            case Direction.NORTH:
                return new Vector2(0, -1);
            case Direction.NORTHEAST:
                return new Vector2(1, -1);
            case Direction.EAST:
                return new Vector2(1, 0);
            case Direction.SOUTHEAST:
                return new Vector2(1, 1);
            case Direction.SOUTH:
                return new Vector2(0, 1);
            case Direction.SOUTHWEST:
                return new Vector2(-1, 1);
            case Direction.WEST:
                return new Vector2(-1, 0);
            case Direction.NORTHWEST:
                return new Vector2(-1, -1);
            default:
                return new Vector2(0, 0);
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
    EMPTY = 0,
    ISLAND1 = 1,
    ISLAND2 = 2,
    ISLAND3 = 3,
    ISLAND4 = 4,
    ISLAND5 = 5,
    ISLAND6 = 6,
    DANGER1 = 7,
    DANGER2 = 8,
    DANGER3 = 9,
    LANDMARK1 = 10,
    LANDMARK2 = 11,
    LANDMARK3 = 12,
    LANDMARK4 = 13,
    BASE = 14,
}