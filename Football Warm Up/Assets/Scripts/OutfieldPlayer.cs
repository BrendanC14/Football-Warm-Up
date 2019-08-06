using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfieldPlayer
{
    public string Name;
    public int Age;
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

    public OutfieldPlayer(string Pos, double balance)
    {
        Words w = new Words();
        Name = w.GetRandomFirstName() + " " + w.GetRandomLastName();
        Age = Random.Range(15,32);
        Position = Pos;
        if (Position == "Defender")
        {
            Passing = Random.Range(1, 20) + Random.Range(0, (int)(balance / 100));
            Tackling = Random.Range(13, 20) + Random.Range(0, (int)(balance / 100));
            Shooting = Random.Range(1, 10) + Random.Range(0, (int)(balance / 100));
            Interception = Random.Range(11, 20) + Random.Range(0, (int)(balance / 100));
        }

        if (Position == "Midfielder")
        {
            Passing = Random.Range(6, 20) + Random.Range(0, (int)(balance / 100));
            Tackling = Random.Range(6, 20) + Random.Range(0, (int)(balance / 100));
            Shooting = Random.Range(6, 20) + Random.Range(0, (int)(balance / 100));
            Interception = Random.Range(6, 20) + Random.Range(0, (int)(balance / 100));
        }
        if (Position == "Forward")
        {
            Passing = Random.Range(11, 20) + Random.Range(0, (int)(balance / 100));
            Tackling = Random.Range(1, 10) + Random.Range(0, (int)(balance / 100));
            Shooting = Random.Range(13, 20) + Random.Range(0, (int)(balance / 100));
            Interception = Random.Range(1, 10) + Random.Range(0, (int)(balance / 100));
        }
        Vision = Random.Range(1, 20);
    }
  
}
