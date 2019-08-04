using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfieldPlayer
{
    public string Name;
    public int Age;
    public string Position;
    public int Passing;
    public int Tackling;
    public int Shooting;
    public int Interception;
    public int Vision;

    public OutfieldPlayer(string Pos)
    {
        Words w = new Words();
        Name = w.GetRandomFirstName() + " " + w.GetRandomLastName();
        Age = Random.Range(15,32);
        Position = Pos;
        if (Position == "Defender")
        {
            Passing = Random.Range(1, 20);
            Tackling = Random.Range(13, 20);
            Shooting = Random.Range(1, 10);
            Interception = Random.Range(11, 20);
        }

        if (Position == "Midfielder")
        {
            Passing = Random.Range(6, 20);
            Tackling = Random.Range(6, 20);
            Shooting = Random.Range(6, 20);
            Interception = Random.Range(6, 20);
        }
        if (Position == "Forward")
        {
            Passing = Random.Range(11, 20);
            Tackling = Random.Range(1, 10);
            Shooting = Random.Range(13, 20);
            Interception = Random.Range(1, 10);
        }
        Vision = Random.Range(1, 20);
    }
  
}
