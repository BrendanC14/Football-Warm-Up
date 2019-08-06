using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Result { HomeWin, Draw, AwayWin }

public class Match : IComparable<Match>
{
    public int HomeID;
    public int AwayID;
    public int HomeScore;
    public int AwayScore;
    public Result result;
    public int GameWeek;

    public Match(int home, int away, int gw)
    {
        HomeID = home;
        AwayID = away;
        HomeScore = UnityEngine.Random.Range(0, 5);
        AwayScore = UnityEngine.Random.Range(0, 5);
        result = GetResult();
        GameWeek = gw;
    }

    Result GetResult()
    {
        if (HomeScore == AwayScore)
        {
            return Result.Draw;
        }
        else if (HomeScore > AwayScore)
        {
            return Result.HomeWin;
        }
        else
        {
            return Result.AwayWin;
        }
    }

    public int CompareTo(Match other)
    {
        return GameWeek.CompareTo(other.GameWeek);

    }
}
