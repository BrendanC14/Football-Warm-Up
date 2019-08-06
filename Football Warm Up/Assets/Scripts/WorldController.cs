using System.Collections;
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

    PlayerViewPrefabController playerViewController;
    ClubViewPrefabController clubViewController;

    public List<Club> Clubs;
    public List<Fixture> Fixtures;
    public List<Match> LeagueResults;

    public static WorldController current;
    public Club ChosenTeam;

    public int GameWeek;

    // Start is called before the first frame update
    void Start()
    {
        SquadViewPanel.SetActive(false);
        AllClubsPanel.SetActive(true);
        GameMenuPanel.SetActive(false);
        Clubs = new List<Club>();
        LeagueResults = new List<Match>();
        Fixtures = new List<Fixture>();

        Clubs.Add(new Club("Arsenal", 0, 500));
        Clubs.Add(new Club("Aston Villa", 1, 100));
        Clubs.Add(new Club("Bournemouth", 2, 250));
        Clubs.Add(new Club("Brighton", 3, 100));
        Clubs.Add(new Club("Burnley", 4, 150));
        Clubs.Add(new Club("Chelsea", 5, 50));
        Clubs.Add(new Club("Crystal Palace", 6, 150));
        Clubs.Add(new Club("Everton", 7, 200));
        Clubs.Add(new Club("Leicester", 8, 200));
        Clubs.Add(new Club("Liverpool", 9, 450));
        Clubs.Add(new Club("Man City", 10, 450));
        Clubs.Add(new Club("Man United", 11, 400));
        Clubs.Add(new Club("Newcastle", 12, 300));
        Clubs.Add(new Club("Norwich", 13, 100));
        Clubs.Add(new Club("Sheffield United", 14, 50));
        Clubs.Add(new Club("Southampton", 15, 100));
        Clubs.Add(new Club("Tottenham", 16, 50));
        Clubs.Add(new Club("Watford", 17, 200));
        Clubs.Add(new Club("West Ham", 18, 300));
        Clubs.Add(new Club("Wolves", 19, 150));

        int ClubCount = 0;
        foreach (Club c in Clubs)
        {
            GameObject newClubDisplay = Instantiate(ClubViewPrefab) as GameObject;
            newClubDisplay.name = c.Name + " GO";
            clubViewController = newClubDisplay.GetComponent<ClubViewPrefabController>();
            clubViewController.ClubName.text = c.Name;
            clubViewController.Passing.text = c.AvePassing.ToString();
            clubViewController.Shooting.text = c.AveShooting.ToString();
            clubViewController.Tackling.text = c.AveTackling.ToString();
            clubViewController.Interception.text = c.AveInterception.ToString();
            clubViewController.Vision.text = c.AveVision.ToString();
            clubViewController.ViewClubBUtton.onClick.AddListener(() => { OpenSquad(c.ID); });
            clubViewController.SelectTeamButton.onClick.AddListener(() => { SelectTeam(c.ID); });
            newClubDisplay.transform.SetParent(ClubDisplayParent.transform);
            ClubCount++;
        }
        GameWeek = 1;
        current = this;
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
    }

    void CreateFixtures()
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
            round = AvailableWeeks[Random.Range(0, AvailableWeeks.Count - 1)];
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
            round = AvailableWeeks[Random.Range(0, AvailableWeeks.Count - 1)];
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

}
