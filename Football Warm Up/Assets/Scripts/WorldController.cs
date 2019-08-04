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
    public List<Match> LeagueResults;

    public static WorldController current;

    // Start is called before the first frame update
    void Start()
    {
        SquadViewPanel.SetActive(false);
        AllClubsPanel.SetActive(true);
        GameMenuPanel.SetActive(false);
        Clubs = new List<Club>();
        LeagueResults = new List<Match>();

        Clubs.Add(new Club("Arsenal", 0));
        Clubs.Add(new Club("Aston Villa", 1));
        Clubs.Add(new Club("Bournemouth", 2));
        Clubs.Add(new Club("Brighton", 3));
        Clubs.Add(new Club("Burnley", 4));
        Clubs.Add(new Club("Chelsea", 5));
        Clubs.Add(new Club("Crystal Palace", 6));
        Clubs.Add(new Club("Everton", 7));
        Clubs.Add(new Club("Leicester", 8));
        Clubs.Add(new Club("Liverpool", 9));
        Clubs.Add(new Club("Man City", 10));
        Clubs.Add(new Club("Man United", 11));
        Clubs.Add(new Club("Newcastle", 12));
        Clubs.Add(new Club("Norwich", 13));
        Clubs.Add(new Club("Sheffield United", 14));
        Clubs.Add(new Club("Southampton", 15));
        Clubs.Add(new Club("Tottenham", 16));
        Clubs.Add(new Club("Watford", 17));
        Clubs.Add(new Club("West Ham", 18));
        Clubs.Add(new Club("Wolves", 19));

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
    }
}
