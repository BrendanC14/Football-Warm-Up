using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Words : MonoBehaviour
{
    public string GetRandomFirstName()
    {
        List<string> FirstNames = new List<string>();

        FirstNames.Add("Adam");
        FirstNames.Add("Alex");
        FirstNames.Add("Andy");
        FirstNames.Add("Ben");
        FirstNames.Add("Brendan");
        FirstNames.Add("Carl");
        FirstNames.Add("Chris");
        FirstNames.Add("Dan");
        FirstNames.Add("Dave");
        FirstNames.Add("Dylan");
        FirstNames.Add("Edward");
        FirstNames.Add("Fred");
        FirstNames.Add("Gary");
        FirstNames.Add("Harry");
        FirstNames.Add("Henry");
        FirstNames.Add("Isaac");
        FirstNames.Add("Jake");
        FirstNames.Add("Jamie");
        FirstNames.Add("James");
        FirstNames.Add("Josh");
        FirstNames.Add("Joe");
        FirstNames.Add("Justin");
        FirstNames.Add("Keith");
        FirstNames.Add("Larry");
        FirstNames.Add("Matt");
        FirstNames.Add("Mike");
        FirstNames.Add("Miles");
        FirstNames.Add("Nathan");
        FirstNames.Add("Nick");
        FirstNames.Add("Ollie");
        FirstNames.Add("Pete");
        FirstNames.Add("Phil");
        FirstNames.Add("Rich");
        FirstNames.Add("Ryan");
        FirstNames.Add("Rob");
        FirstNames.Add("Sam");
        FirstNames.Add("Tom");
        FirstNames.Add("Tim");
        FirstNames.Add("Saj");
        FirstNames.Add("Will");
        FirstNames.Add("Zack");

        int rand = Random.Range(0, FirstNames.Count - 1);
        return FirstNames[rand];
    }

    public string GetRandomLastName()
    {
        List<string> FirstNames = new List<string>();

        FirstNames.Add("Aleson");
        FirstNames.Add("Back");
        FirstNames.Add("Banks");
        FirstNames.Add("Barry");
        FirstNames.Add("Brown");
        FirstNames.Add("Clarke");
        FirstNames.Add("Cope");
        FirstNames.Add("Cooper");
        FirstNames.Add("Cullen");
        FirstNames.Add("Fay");
        FirstNames.Add("Fry");
        FirstNames.Add("Gadsby");
        FirstNames.Add("Gerrard");
        FirstNames.Add("Greene");
        FirstNames.Add("Folly");
        FirstNames.Add("Jimson");
        FirstNames.Add("Johnson");
        FirstNames.Add("Jones");
        FirstNames.Add("Kray");
        FirstNames.Add("Kent");
        FirstNames.Add("Lampard");
        FirstNames.Add("Lardstone");
        FirstNames.Add("Macallan");
        FirstNames.Add("Millar");
        FirstNames.Add("Moore");
        FirstNames.Add("Murphy");
        FirstNames.Add("Murray");
        FirstNames.Add("Nourish");
        FirstNames.Add("Potter");
        FirstNames.Add("Powell");
        FirstNames.Add("Rooney");
        FirstNames.Add("Roff");
        FirstNames.Add("Smith");
        FirstNames.Add("Thompson");
        FirstNames.Add("Tyler");
        FirstNames.Add("Wilson");
        FirstNames.Add("Woodroffe");
        FirstNames.Add("Williams");

        int rand = Random.Range(0, FirstNames.Count - 1);
        return FirstNames[rand];
    }
}
