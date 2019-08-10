using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numbers
{
    public float SquadGKX = 0f;
    public float SquadGKY = -201.25f;
    int PlayerID;

    public static Numbers current;

    public Numbers()
    {
        current = this;
    }

    public int GetPlayerID()
    {
        PlayerID++;
        return PlayerID;
    }

}
