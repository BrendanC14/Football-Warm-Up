using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutfieldPlayer : IComparable<OutfieldPlayer>
{
    public int ID;
    public string Name;
    public int Age;
    public Club club;
    public string Position;
    public int Passing
    {
        get { return passing; }
        set { passing = value; if (value > 20) { passing = 20; } }
    }
    int passing;

    public int Tackling
    {
        get { return tackling; }
        set { tackling = value; if (value > 20) { tackling = 20; } }
    }
    int tackling;

    public int Shooting
    {
        get { return shooting; }
        set { shooting = value; if (value > 20) { shooting = 20; } }
    }
    int shooting;

    public int Interception
    {
        get { return interception; }
        set { interception = value; if (value > 20) { interception = 20; } }
    }
    int interception;

    public int Vision
    {
        get { return vision; }
        set { vision = value; if (value > 20) { vision = 20; } }
    }
    int vision;

    public int Goals;
    public int WeeksInjured;

    public OutfieldPlayer(string Pos, double balance, Club c, bool youth)
    {
        Words w = new Words();
        ID = Numbers.current.GetPlayerID();
        Name = w.GetRandomFirstName() + " " + w.GetRandomLastName();
        if (youth)
        {
            Age = UnityEngine.Random.Range(15, 18);
        }
        else { Age = UnityEngine.Random.Range(19, 32); }
        club = c;
        Position = Pos;
        int min = (int)balance / 75;
        int max = (int)balance / 50;

        if (Position == "Defender")
        {
            Passing = UnityEngine.Random.Range(min, max);
            Tackling = UnityEngine.Random.Range(min, max);
            Shooting = UnityEngine.Random.Range(1, 10);
            Interception = UnityEngine.Random.Range(min, max);
        }

        if (Position == "Midfielder")
        {
            Passing = UnityEngine.Random.Range(min, max);
            Tackling = UnityEngine.Random.Range(min, max);
            Shooting = UnityEngine.Random.Range(min, max);
            Interception = UnityEngine.Random.Range(min, max);
        }
        if (Position == "Forward")
        {
            Passing = UnityEngine.Random.Range(min, max);
            Tackling = UnityEngine.Random.Range(1, 10);
            Shooting = UnityEngine.Random.Range(min, max);
            Interception = UnityEngine.Random.Range(1, 10);
        }
        Vision = UnityEngine.Random.Range(min, max);
        WorldController.current.AllPlayers.Add(this);
        if (youth)
        {
            Passing /= 2;
            Tackling /= 2 ;
            Shooting /= 2;
            Interception /= 2;
            Vision /= 2;
        }
    }

    public string GetPositionShortForm(string pos)
    {
        if (pos == "Goalkeeper") { return "GK"; }
        if (pos == "Defender") { return "DEF"; }
        if (pos == "Midfielder") { return "MID"; }
        return "FW";

    }

    public int CompareTo(OutfieldPlayer other)
    {
        return other.Goals.CompareTo(Goals);

    }
}
