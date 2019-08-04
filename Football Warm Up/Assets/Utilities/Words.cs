using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Words : MonoBehaviour
{
    public string GetRandomFirstName()
    {
        List<string> FirstNames = new List<string>();

        FirstNames.Add("Adam");
        FirstNames.Add("Brendan");
        FirstNames.Add("Carl");
        FirstNames.Add("Dave");
        FirstNames.Add("Edward");
        FirstNames.Add("Fred");
        FirstNames.Add("Gary");
        FirstNames.Add("Harry");
        FirstNames.Add("Isaac");
        FirstNames.Add("Jamie");
        FirstNames.Add("Keith");
        FirstNames.Add("Larry");
        FirstNames.Add("Mike");
        FirstNames.Add("Nathan");
        FirstNames.Add("Ollie");
        FirstNames.Add("Pete");
        FirstNames.Add("Phil");
        FirstNames.Add("Rich");
        FirstNames.Add("Sam");
        FirstNames.Add("Tom");
        FirstNames.Add("Tim");
        FirstNames.Add("Saj");
        FirstNames.Add("Will");
        FirstNames.Add("Alex");
        FirstNames.Add("Rob");
        FirstNames.Add("Miles");

        int rand = Random.Range(0, FirstNames.Count - 1);
        return FirstNames[rand];
    }

    public string GetRandomLastName()
    {
        List<string> FirstNames = new List<string>();

        FirstNames.Add("Aleson");
        FirstNames.Add("Barry");
        FirstNames.Add("Cooper");
        FirstNames.Add("Powell");
        FirstNames.Add("Cullen");
        FirstNames.Add("Folly");
        FirstNames.Add("Greene");
        FirstNames.Add("Potter");
        FirstNames.Add("Banks");
        FirstNames.Add("Jimson");
        FirstNames.Add("Fay");
        FirstNames.Add("Lardstone");
        FirstNames.Add("Murphy");
        FirstNames.Add("Nourish");
        FirstNames.Add("Fry");
        FirstNames.Add("Cope");
        FirstNames.Add("Gadsby");
        FirstNames.Add("Tyler");
        FirstNames.Add("Back");
        FirstNames.Add("Jones");
        FirstNames.Add("Wilson");
        FirstNames.Add("Kent");
        FirstNames.Add("Roff");
        FirstNames.Add("Woodroffe");
        FirstNames.Add("Kray");
        FirstNames.Add("Macallan");

        int rand = Random.Range(0, FirstNames.Count - 1);
        return FirstNames[rand];
    }
}
