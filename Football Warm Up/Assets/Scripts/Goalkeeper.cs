using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper
{
    public int ID;
    public string Name;
    public int Age;
    public Club club;
    public int Reflexes
    {
        get { return reflexes; }
        set { reflexes = value; if (value > 20) { reflexes = 20; } }
    }
    int reflexes;

    public int Handling
    {
        get { return handling; }
        set { handling = value; if (value > 20) { handling = 20; } }
    }
    int handling;

    public int OneOnOnes
    {
        get { return oneonones; }
        set { oneonones = value; if (value > 20) { oneonones = 20; } }
    }
    int oneonones;

    public int Passing
    {
        get { return passing; }
        set { passing = value; if (value > 20) { passing = 20; } }
    }
    int passing;

    public int Diving
    {
        get { return diving; }
        set { diving = value; if (value > 20) { diving = 20; } }
    }
    int diving;

    public Goalkeeper(double balance, Club c)
    {
        Words w = new Words();
        ID = Numbers.current.GetPlayerID();
        Name = w.GetRandomFirstName() + " " + w.GetRandomLastName();
        Age = Random.Range(15, 32);
        club = c;
        int min = (int)balance / 75;
        int max = (int)balance / 50;
        Reflexes = Random.Range(min, max);
        Handling = Random.Range(min, max);
        OneOnOnes = Random.Range(min, max);
        Passing = Random.Range(min, max);
        Diving = Random.Range(min, max);
    }
}
