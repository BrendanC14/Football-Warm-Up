﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper
{
    public string Name;
    public int Age;
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

    public Goalkeeper(double balance)
    {
        Words w = new Words();
        Name = w.GetRandomFirstName() + " " + w.GetRandomLastName();
        Age = Random.Range(15, 32);
        Reflexes = Random.Range(1, 20) + Random.Range(0, (int)(balance / 100));
        Handling = Random.Range(1, 20) + Random.Range(0, (int)(balance / 100));
        OneOnOnes = Random.Range(1, 20) + Random.Range(0, (int)(balance / 100));
        Passing = Random.Range(1, 20) + Random.Range(0, (int)(balance / 100));
        Diving = Random.Range(1, 20) + Random.Range(0, (int)(balance / 100));

    }
}
