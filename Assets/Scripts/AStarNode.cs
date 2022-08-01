using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode
{
    public float xPos;
    public float yPos;
    public int xIndex;
    public int yIndex;
    
    public float hCost;
    public float gCost;

    public float fCost
    {
        get { return gCost + hCost; }
    }

    public AStarNode parent;
    public bool isWalkable;

    public AStarNode()
    {
        xPos = -1;
        yPos = -1;
        xIndex = -1;
        yIndex = -1;
        hCost = 0;
        gCost = 0;
        parent = null;
    }

    public void Reset()
    {
        hCost = 0;
        gCost = int.MaxValue;
        parent = null;
    }
}
