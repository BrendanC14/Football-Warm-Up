using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class WorldSaveFile
{

    int chosenTeamID;

    public void SaveWorld(WorldController w)
    {

        chosenTeamID = w.ChosenTeam.ID;

        string JSON = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("World", JSON);

    }


}