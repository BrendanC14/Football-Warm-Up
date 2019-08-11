﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    public GameObject ClubDisplayParent;
    public GameObject ClubViewPrefab;
    public GameObject PlayerViewPrefab;
    public GameObject PlayerViewParent;
    public GameObject SquadViewPanel;
    public GameObject SquadViewTitle;
    public GameObject AllClubsPanel;
    public GameObject GameMenuPanel;
    public GameObject ContinueButton;

    PlayerViewPrefabController playerViewController;
    ClubViewPrefabController clubViewController;

    public List<Club> Clubs;
    public List<Fixture> Fixtures;
    public List<Match> LeagueResults;
    public List<OutfieldPlayer> AllPlayers;
    public List<Goalkeeper> Goalies;
    public List<OutfieldPlayer> Goalscorers;
    public List<OutfieldPlayer> Assisters;
    public List<OutfieldPlayer> TransferListedPlayers;

    public GameObject BudgetGameObject;
    public static WorldController current;
    public Club ChosenTeam;

    public int GameWeek;
    public int Year;

    public bool WindowOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        Numbers n = new Numbers();
        SquadViewPanel.SetActive(false);
        AllClubsPanel.SetActive(true);
        GameMenuPanel.SetActive(false);
        Clubs = new List<Club>();
        LeagueResults = new List<Match>();
        Fixtures = new List<Fixture>();
        AllPlayers = new List<OutfieldPlayer>();
        Goalies = new List<Goalkeeper>();
        Goalscorers = new List<OutfieldPlayer>();
        Assisters = new List<OutfieldPlayer>();
        TransferListedPlayers = new List<OutfieldPlayer>();

        Clubs.Add(new Club("Man City", 0, 980, 50));
        Clubs.Add(new Club("Liverpool", 1, 980, 45));
        Clubs.Add(new Club("Arsenal", 2, 960, 45));
        Clubs.Add(new Club("Chelsea", 3, 940, 45));
        Clubs.Add(new Club("Man United", 4, 920, 40));
        Clubs.Add(new Club("Everton", 5, 880, 40));
        Clubs.Add(new Club("Bournemouth", 6, 860, 35));
        Clubs.Add(new Club("Leicester", 7, 840, 35));
        Clubs.Add(new Club("Wolves", 8, 840,35));
        Clubs.Add(new Club("Watford", 9, 820, 35));
        Clubs.Add(new Club("Burnley", 10, 800, 30));
        Clubs.Add(new Club("Brighton", 11, 760, 30));
        Clubs.Add(new Club("West Ham", 12, 740, 30));
        Clubs.Add(new Club("Southampton", 13, 720, 30));
        Clubs.Add(new Club("Newcastle", 14, 700, 25));
        Clubs.Add(new Club("Crystal Palace", 15, 680, 25));
        Clubs.Add(new Club("Aston Villa", 16, 660, 25));
        Clubs.Add(new Club("Norwich", 17, 640, 20));
        Clubs.Add(new Club("Sheffield United", 18, 620, 20));
        Clubs.Add(new Club("Tottenham", 19, 600, 20));

        int ClubCount = 0;
        foreach (Club c in Clubs)
        {
            GameObject newClubDisplay = Instantiate(ClubViewPrefab) as GameObject;
            newClubDisplay.name = c.Name + " GO";
            clubViewController = newClubDisplay.GetComponent<ClubViewPrefabController>();
            clubViewController.ClubName.text = c.Name;
            clubViewController.SelectTeamButton.onClick.AddListener(() => { SelectTeam(c.ID); });
            newClubDisplay.transform.SetParent(ClubDisplayParent.transform);
            newClubDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
            ClubCount++;
        }
        GameWeek = 1;
        Year = 2019;
        WindowOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSquad(int index)
    {
        
        foreach (Transform child in PlayerViewParent.transform)
        {
            Destroy(child.gameObject);
        }


        SquadViewPanel.SetActive(true);
        Club c = Clubs[index];
        SquadViewTitle.GetComponent<Text>().text = c.Name;

        foreach (Goalkeeper goalie in c.Goalies)
        {
            GameObject newPlayerDisplay = Instantiate(PlayerViewPrefab) as GameObject;
            newPlayerDisplay.name = goalie.Name;
            playerViewController = newPlayerDisplay.GetComponent<PlayerViewPrefabController>();
            playerViewController.Name.text = goalie.Name;
            playerViewController.Age.text = "Age: " + goalie.Age.ToString();
            playerViewController.Position.text = "Goalkeeper";
            playerViewController.Stats.text = "Reflexes: " + goalie.Reflexes.ToString() +
                "\nHandling: " + goalie.Handling.ToString() +
                "\nOne on ones: " + goalie.OneOnOnes.ToString() +
                "\nPassing: " + goalie.Passing.ToString() +
                "\nDiving: " + goalie.Diving.ToString();
            newPlayerDisplay.transform.SetParent(PlayerViewParent.transform);
        }
        foreach (OutfieldPlayer player in c.Defenders)
        {
            GameObject newPlayerDisplay = Instantiate(PlayerViewPrefab) as GameObject;
            newPlayerDisplay.name = player.Name;
            playerViewController = newPlayerDisplay.GetComponent<PlayerViewPrefabController>();
            playerViewController.Name.text = player.Name;
            playerViewController.Age.text = "Age: " + player.Age.ToString();
            playerViewController.Position.text = "Defender";
            playerViewController.Stats.text = "Passing: " + player.Passing.ToString() +
                "\nTackling: " + player.Tackling.ToString() +
                "\nShooting: " + player.Shooting.ToString() +
                "\nInterceptions: " + player.Interception.ToString() +
                "\nVision: " + player.Vision.ToString();
            newPlayerDisplay.transform.SetParent(PlayerViewParent.transform);
        }
        foreach (OutfieldPlayer player in c.Midfielders)
        {
            GameObject newPlayerDisplay = Instantiate(PlayerViewPrefab) as GameObject;
            newPlayerDisplay.name = player.Name;
            playerViewController = newPlayerDisplay.GetComponent<PlayerViewPrefabController>();
            playerViewController.Name.text = player.Name;
            playerViewController.Age.text = "Age: " + player.Age.ToString();
            playerViewController.Position.text = "Midfielder";
            playerViewController.Stats.text = "Passing: " + player.Passing.ToString() +
                "\nTackling: " + player.Tackling.ToString() +
                "\nShooting: " + player.Shooting.ToString() +
                "\nInterceptions: " + player.Interception.ToString() +
                "\nVision: " + player.Vision.ToString();
            newPlayerDisplay.transform.SetParent(PlayerViewParent.transform);
        }
        foreach (OutfieldPlayer player in c.Forwards)
        {
            GameObject newPlayerDisplay = Instantiate(PlayerViewPrefab) as GameObject;
            newPlayerDisplay.name = player.Name;
            playerViewController = newPlayerDisplay.GetComponent<PlayerViewPrefabController>();
            playerViewController.Name.text = player.Name;
            playerViewController.Age.text = "Age: " + player.Age.ToString();
            playerViewController.Position.text = "Forward";
            playerViewController.Stats.text = "Passing: " + player.Passing.ToString() +
                "\nTackling: " + player.Tackling.ToString() +
                "\nShooting: " + player.Shooting.ToString() +
                "\nInterceptions: " + player.Interception.ToString() +
                "\nVision: " + player.Vision.ToString();
            newPlayerDisplay.transform.SetParent(PlayerViewParent.transform);
        }

    }

    public void CloseSquad()
    {

        SquadViewPanel.SetActive(false);
    }

    public void SelectTeam(int index)
    {
        AllClubsPanel.SetActive(false);
        SquadViewPanel.SetActive(false);
        GameMenuPanel.SetActive(true);
        ChosenTeam = Clubs[index];
        CreateFixtures();
        ChosenTeam.Fixtures.Sort();
        ContinueButton.GetComponentInChildren<Text>().text = "Next Match\n" +
            Clubs[ChosenTeam.Fixtures[0].HomeID].Name + " vs " + Clubs[ChosenTeam.Fixtures[0].AwayID].Name;
        BudgetGameObject.GetComponentInChildren<Text>().text = "Budget: £" + ChosenTeam.Budget.ToString() + "m";
        ChosenTeam.FirstTeam = new List<OutfieldPlayer>();
        ChosenTeam.Goalie = null;
    }

    public void CreateFixtures()
    {
        //This is a list containing all the game weeks in the first half of the season
        //Later, this gets randomised each loop so that the fixtures appear random.
        List<int> AvailableWeeks = new List<int>();
        for (int i = 1; i < 20; i++)
        {
            AvailableWeeks.Add(i);
        }

        int round;
        for (int gameweek = 0; gameweek < 19; gameweek++)
        {
            round = AvailableWeeks[Random.Range(0, AvailableWeeks.Count)];
            AvailableWeeks.Remove(round);
            for (int match = 0; match < 10; match++)
            {
                //This formula uses the round and match number to cycle through the teams in the list and back round
                //and making sure each team plays each team at least once.
                int HomeTeam = (gameweek + match) % 19;
                int AwayTeam = (gameweek - match + 19) % 19;

                if (match == 0) { AwayTeam = 19; }
                Fixture fix = new Fixture(HomeTeam, AwayTeam, round);
                Fixtures.Add(fix);
                Clubs[HomeTeam].Fixtures.Add(fix);
                Clubs[AwayTeam].Fixtures.Add(fix);
            }
        }

        AvailableWeeks = new List<int>();
        for (int i = 20; i < 39; i++)
        {
            AvailableWeeks.Add(i);
        }
        for (int gameweek = 0; gameweek < 19; gameweek++)
        {
            round = AvailableWeeks[Random.Range(0, AvailableWeeks.Count)];
            AvailableWeeks.Remove(round);
            for (int match = 0; match < 10; match++)
            {
                //This now does exactly the same as above, but in reverse, so that I get home and away fixtures
                int HomeTeam = (gameweek - match + 19) % 19;
                int AwayTeam = (gameweek + match) % 19;

                if (match == 0) { HomeTeam = 19; }
                Fixture fix = new Fixture(HomeTeam, AwayTeam, round);
                Fixtures.Add(fix);
                Clubs[HomeTeam].Fixtures.Add(fix);
                Clubs[AwayTeam].Fixtures.Add(fix);
            }
        }

        Fixtures.Sort();

        return;
       
    }

    public bool IsMyClubID(int ID)
    {
        if (Clubs[ID] == ChosenTeam)
        {
            return true;
        }
        return false;
    }






    public void SaveGame()
    {
        WorldSaveFile save = new WorldSaveFile();
        save.SaveWorld(this);
         
        foreach (Club c in Clubs)
        {
            ClubSaveFIle saveFile = new ClubSaveFIle();
            saveFile.SaveFile(c);
        }

        foreach (Fixture f in Fixtures)
        {
            FixtureSaveFile saveFile = new FixtureSaveFile();
            saveFile.SaveFile(f);
        }

        foreach (Match m in LeagueResults)
        {
            MatchSaveFile saveFile = new MatchSaveFile();
            saveFile.SaveFile(m);
        }

        foreach (OutfieldPlayer player in AllPlayers)
        {
            OutfieldPlayerSaveFile saveFile = new OutfieldPlayerSaveFile();
            saveFile.SaveFile(player);
        }

        foreach (Goalkeeper goalie in Goalies)
        {
            GoalkeeperSaveFile saveFile = new GoalkeeperSaveFile();
            saveFile.SaveFile(goalie);
        }
    }





}

