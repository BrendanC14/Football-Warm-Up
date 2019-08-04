using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper
{
    public string Name;
    public int Age;
    public int Reflexes;
    public int Handling;
    public int OneOnOnes;
    public int Passing;
    public int Diving;

    public Goalkeeper()
    {
        Words w = new Words();
        Name = w.GetRandomFirstName() + " " + w.GetRandomLastName();
        Age = Random.Range(15, 32);
        Reflexes = Random.Range(1, 20);
        Handling = Random.Range(1, 20);
        OneOnOnes = Random.Range(1, 20);
        Passing = Random.Range(1, 20);
        Diving = Random.Range(1, 20);

    }
}
