using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fixture : IComparable<Fixture>
{
    public int HomeID;
    public int AwayID;
    public int GameWeek;

    public Fixture(int home, int away, int gw)
    {
        HomeID = home;
        AwayID = away;
        GameWeek = gw;
    }


    public int CompareTo(Fixture other)
    {
        return GameWeek.CompareTo(other.GameWeek);

    }
}
