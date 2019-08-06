using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{

    public GameObject LeagueTablePanel;
    public GameObject TeamLeagueTablePrefab;
    public GameObject TeamLeagueParent;
    public GameObject ResultsPanel;
    public GameObject ResultViewPrefab;
    public GameObject ResultViewParent;
    public GameObject FixturesPanel;
    public GameObject FixturePrefab;
    public GameObject FixtureParent;
    public GameObject SchedulePanel;
    public GameObject SchedulePrefab;
    public GameObject ScheduleParent;
    public GameObject SquadHeader;
    public GameObject SquadPanel;
    public GameObject SquadPlayerPrefab;
    public GameObject SquadPlayerParent;
    public GameObject PitchPanel;

    ResultVuewPrefabController resultViewController;
    TeamLeaguePrefabController teamLeageController;
    FixturePrefabController fixtureViewController;
    SchedulePrefabController scheduleViewController;
    SquadPlayerPrefab squadPlayerController;

    public GameObject GameWeek;

    Dictionary<string, GameObject> PositionGOMap;
    Dictionary<Goalkeeper, GameObject> GoalieGOMap;
    Dictionary<OutfieldPlayer, GameObject> DefenderGOMap;
    Dictionary<OutfieldPlayer, GameObject> MidfielderGOMap;
    Dictionary<OutfieldPlayer, GameObject> ForwardGOMap;
    Dictionary<GameObject, OutfieldPlayer> PositionPlayerMap;

    public GameObject Tactic442;
    public GameObject Tactic433;
    public GameObject Tactic41212;
    public GameObject Tactic41231;
    public GameObject Tactic32212;
    public GameObject Tactic33211;

    List<GameObject> AllPositions;
    List<GameObject> List442;
    List<GameObject> List433;
    List<GameObject> List41212;
    List<GameObject> List41231;
    List<GameObject> List32212;
    List<GameObject> List33211;

    GameObject positionSelected;
    public GameObject GK;
    public GameObject DL;
    public GameObject CB1;
    public GameObject CB2;
    public GameObject CB3;
    public GameObject CB4;
    public GameObject CB5;
    public GameObject DR;
    public GameObject WBL;
    public GameObject DM1;
    public GameObject DM2;
    public GameObject DM3;
    public GameObject WBR;
    public GameObject ML;
    public GameObject MC1;
    public GameObject MC2;
    public GameObject MC3;
    public GameObject MC4;
    public GameObject MC5;
    public GameObject MR;
    public GameObject AML;
    public GameObject AMC1;
    public GameObject AMC2;
    public GameObject AMC3;
    public GameObject AMR;
    public GameObject FW1;
    public GameObject FW2;
    public GameObject FW3;
    public GameObject FW4;
    public GameObject FW5;


    // Start is called before the first frame update
    void Start()
    {
        CreateTacticLists();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenLeagueTable()
    {
        LeagueTablePanel.SetActive(true);

        List<Club> copyClubs = new List<Club>(WorldController.current.Clubs);

        copyClubs.Sort();
        foreach (Club c in copyClubs)
        {
            GameObject leagueTeamDisplay = Instantiate(TeamLeagueTablePrefab) as GameObject;
            leagueTeamDisplay.name = c.Name;
            teamLeageController = leagueTeamDisplay.GetComponent<TeamLeaguePrefabController>();
            teamLeageController.Name.text = c.Name;
            teamLeageController.GamesPlayed.text = c.GamesPlayed.ToString();
            teamLeageController.GoalsScored.text = c.GoalsScored.ToString();
            teamLeageController.GoalsConceded.text = c.GoalsConceded.ToString();
            teamLeageController.GoalDifference.text = c.GoalDifference.ToString();
            teamLeageController.Wins.text = c.Wins.ToString();
            teamLeageController.Draws.text = c.Draws.ToString();
            teamLeageController.Losses.text = c.Losses.ToString();
            teamLeageController.Points.text = c.Points.ToString();
            leagueTeamDisplay.transform.SetParent(TeamLeagueParent.transform);
            leagueTeamDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void CloseLeagueTable()
    {
        foreach (Transform child in TeamLeagueParent.transform)
        {
            Destroy(child.gameObject);
        }
        LeagueTablePanel.SetActive(false);
    }

    public void ContinueGame()
    {
        WorldController.current.Fixtures.Sort();
        for (int i = 0; i < 10; i++)
        {
            Fixture f = WorldController.current.Fixtures[0];
            Match m = new Match(f.HomeID, f.AwayID, f.GameWeek);
            WorldController.current.LeagueResults.Add(m);
            WorldController.current.Clubs[f.HomeID].UpdateLeagueStats(m);
            WorldController.current.Clubs[f.AwayID].UpdateLeagueStats(m);
            WorldController.current.Fixtures.Remove(f);
        }
        WorldController.current.GameWeek++;
        GameWeek.GetComponent<Text>().text = "Gameweek: " + WorldController.current.GameWeek.ToString();
    }

    public void OpenResults()
    {
        ResultsPanel.SetActive(true);
        foreach (Match m in WorldController.current.LeagueResults)
        {
            GameObject resultDisplay = Instantiate(ResultViewPrefab) as GameObject;
            resultDisplay.name = m.HomeID.ToString() + "V" + m.AwayID.ToString();
            resultViewController = resultDisplay.GetComponent<ResultVuewPrefabController>();
            resultViewController.GameWeek.text = "Gameweek " + m.GameWeek.ToString();
            resultViewController.HomeTeam.text = WorldController.current.Clubs[m.HomeID].Name;
            resultViewController.HomeScore.text = m.HomeScore.ToString();
            resultViewController.AwayScore.text = m.AwayScore.ToString();
            resultViewController.AwayTeam.text = WorldController.current.Clubs[m.AwayID].Name;
            resultDisplay.transform.SetParent(ResultViewParent.transform);
            if (WorldController.current.IsMyClubID(m.HomeID) || WorldController.current.IsMyClubID(m.AwayID))
            {
                resultDisplay.GetComponent<Image>().color = new Color(0.7f, 1f, 1f, 1f);
            }
        }

    }

    public void CloseResults()
    {

        foreach (Transform child in ResultViewParent.transform)
        {
            Destroy(child.gameObject);
        }
        ResultsPanel.SetActive(false);
    }

    public void OpenFixtures()
    {
        FixturesPanel.SetActive(true);
        foreach (Fixture f in WorldController.current.Fixtures)
        {
            GameObject fixtureDisplay = Instantiate(FixturePrefab) as GameObject;
            fixtureDisplay.name = f.HomeID.ToString() + "V" + f.AwayID.ToString();
            fixtureViewController = fixtureDisplay.GetComponent<FixturePrefabController>();
            fixtureViewController.GameWeek.text = "Gameweek: " + f.GameWeek.ToString();
            fixtureViewController.HomeTeam.text = WorldController.current.Clubs[f.HomeID].Name;
            fixtureViewController.AwayTeam.text = WorldController.current.Clubs[f.AwayID].Name;
            fixtureDisplay.transform.SetParent(FixtureParent.transform);
        }
    }
    public void CloseFixtures()
    {

        foreach (Transform child in FixtureParent.transform)
        {
            Destroy(child.gameObject);
        }
        FixturesPanel.SetActive(false);
    }

    public void OpenSchedule()
    {
        SchedulePanel.SetActive(true);
        WorldController.current.ChosenTeam.Fixtures.Sort();
        foreach (Fixture f in WorldController.current.ChosenTeam.Fixtures)
        {
            GameObject fixtureDisplay = Instantiate(SchedulePrefab) as GameObject;
            fixtureDisplay.name = f.HomeID.ToString() + "V" + f.AwayID.ToString();
            scheduleViewController = fixtureDisplay.GetComponent<SchedulePrefabController>();
            scheduleViewController.GameWeek.text = "Gameweek: " + f.GameWeek.ToString();
            scheduleViewController.HomeTeam.text = WorldController.current.Clubs[f.HomeID].Name;
            scheduleViewController.AwayTeam.text = WorldController.current.Clubs[f.AwayID].Name;
            scheduleViewController.transform.SetParent(ScheduleParent.transform);

        }
    }
    public void CLoseSchedule()
    {
        foreach (Transform child in ScheduleParent.transform)
        {
            Destroy(child.gameObject);
        }
        SchedulePanel.SetActive(false);

    }

    public void OpenSquad()
    {
        GoalieGOMap = new Dictionary<Goalkeeper, GameObject>();
        DefenderGOMap = new Dictionary<OutfieldPlayer, GameObject>();
        MidfielderGOMap = new Dictionary<OutfieldPlayer, GameObject>();
        ForwardGOMap = new Dictionary<OutfieldPlayer, GameObject>();
        foreach (Transform child in SquadPlayerParent.transform)
        {
            Destroy(child.gameObject);
        }


        SquadPanel.SetActive(true);
        Club c = WorldController.current.ChosenTeam;
        SquadHeader.GetComponent<Text>().text = c.Name;

        foreach (Goalkeeper goalie in c.Goalies)
        {
            GameObject newPlayerDisplay = Instantiate(SquadPlayerPrefab) as GameObject;
            newPlayerDisplay.name = goalie.Name;
            squadPlayerController = newPlayerDisplay.GetComponent<SquadPlayerPrefab>();
            squadPlayerController.Name.text = goalie.Name;
            squadPlayerController.Age.text = "Age: " + goalie.Age.ToString();
            squadPlayerController.Position.text = "Goalkeeper";
            squadPlayerController.Stats.text = "Reflexes: " + goalie.Reflexes.ToString() +
                "\nHandling: " + goalie.Handling.ToString() +
                "\nOne on ones: " + goalie.OneOnOnes.ToString() +
                "\nPassing: " + goalie.Passing.ToString() +
                "\nDiving: " + goalie.Diving.ToString();
            squadPlayerController.Select.onClick.AddListener(() => SelectPlayer("Goalkeeper", goalie));
            newPlayerDisplay.transform.SetParent(SquadPlayerParent.transform);
            GoalieGOMap.Add(goalie, newPlayerDisplay);
        }
        foreach (OutfieldPlayer player in c.Defenders)
        {
            GameObject newPlayerDisplay = Instantiate(SquadPlayerPrefab) as GameObject;
            newPlayerDisplay.name = player.Name;
            squadPlayerController = newPlayerDisplay.GetComponent<SquadPlayerPrefab>();
            squadPlayerController.Name.text = player.Name;
            squadPlayerController.Age.text = "Age: " + player.Age.ToString();
            squadPlayerController.Position.text = "Defender";
            squadPlayerController.Stats.text = "Passing: " + player.Passing.ToString() +
                "\nTackling: " + player.Tackling.ToString() +
                "\nShooting: " + player.Shooting.ToString() +
                "\nInterceptions: " + player.Interception.ToString() +
                "\nVision: " + player.Vision.ToString();
            squadPlayerController.Select.onClick.AddListener(() => SelectPlayer("Defender", null, player));
            newPlayerDisplay.transform.SetParent(SquadPlayerParent.transform);
            DefenderGOMap.Add(player, newPlayerDisplay);
        }
        foreach (OutfieldPlayer player in c.Midfielders)
        {
            GameObject newPlayerDisplay = Instantiate(SquadPlayerPrefab) as GameObject;
            newPlayerDisplay.name = player.Name;
            squadPlayerController = newPlayerDisplay.GetComponent<SquadPlayerPrefab>();
            squadPlayerController.Name.text = player.Name;
            squadPlayerController.Age.text = "Age: " + player.Age.ToString();
            squadPlayerController.Position.text = "Midfielder";
            squadPlayerController.Stats.text = "Passing: " + player.Passing.ToString() +
                "\nTackling: " + player.Tackling.ToString() +
                "\nShooting: " + player.Shooting.ToString() +
                "\nInterceptions: " + player.Interception.ToString() +
                "\nVision: " + player.Vision.ToString();
            squadPlayerController.Select.onClick.AddListener(() => SelectPlayer("Midfielder", null, player));
            newPlayerDisplay.transform.SetParent(SquadPlayerParent.transform);
            MidfielderGOMap.Add(player, newPlayerDisplay);
        }
        foreach (OutfieldPlayer player in c.Forwards)
        {
            GameObject newPlayerDisplay = Instantiate(SquadPlayerPrefab) as GameObject;
            newPlayerDisplay.name = player.Name;
            squadPlayerController = newPlayerDisplay.GetComponent<SquadPlayerPrefab>();
            squadPlayerController.Name.text = player.Name;
            squadPlayerController.Age.text = "Age: " + player.Age.ToString();
            squadPlayerController.Position.text = "Forward";
            squadPlayerController.Stats.text = "Passing: " + player.Passing.ToString() +
                "\nTackling: " + player.Tackling.ToString() +
                "\nShooting: " + player.Shooting.ToString() +
                "\nInterceptions: " + player.Interception.ToString() +
                "\nVision: " + player.Vision.ToString();
            squadPlayerController.Select.onClick.AddListener(() => SelectPlayer("Forward", null, player));
            newPlayerDisplay.transform.SetParent(SquadPlayerParent.transform);
            ForwardGOMap.Add(player, newPlayerDisplay);
        }
    }
    void CreateTacticLists()
    {
        PositionGOMap = new Dictionary<string, GameObject>();
        PositionGOMap.Add("GK", GK);
        PositionGOMap.Add("DL", DL);
        PositionGOMap.Add("CB1", CB1);
        PositionGOMap.Add("CB2", CB2);
        PositionGOMap.Add("CB3", CB3);
        PositionGOMap.Add("CB4", CB4);
        PositionGOMap.Add("CB5", CB5);
        PositionGOMap.Add("DR", DR);
        PositionGOMap.Add("WBL", WBL);
        PositionGOMap.Add("DM1", DM1);
        PositionGOMap.Add("DM2", DM2);
        PositionGOMap.Add("DM3", DM3);
        PositionGOMap.Add("WBR", WBR);
        PositionGOMap.Add("ML", ML);
        PositionGOMap.Add("MC1", MC1);
        PositionGOMap.Add("MC2", MC2);
        PositionGOMap.Add("MC3", MC3);
        PositionGOMap.Add("MC4", MC4);
        PositionGOMap.Add("MC5", MC5);
        PositionGOMap.Add("MR", MR);
        PositionGOMap.Add("AML", AML);
        PositionGOMap.Add("AMC1", AMC1);
        PositionGOMap.Add("AMC2", AMC2);
        PositionGOMap.Add("AMC3", AMC3);
        PositionGOMap.Add("AMR", AMR);
        PositionGOMap.Add("FW1", FW1);
        PositionGOMap.Add("FW2", FW2);
        PositionGOMap.Add("FW3", FW3);
        PositionGOMap.Add("FW4", FW4);
        PositionGOMap.Add("FW5", FW5);
        AllPositions = new List<GameObject>();
        List442 = new List<GameObject>();
        List433 = new List<GameObject>();
        List41212 = new List<GameObject>();
        List41231 = new List<GameObject>();
        List32212 = new List<GameObject>();
        List33211 = new List<GameObject>();

        foreach (Transform child in PitchPanel.transform)
        {
            AllPositions.Add(child.gameObject);
        }

        List442.Add(GK);
        List442.Add(DL);
        List442.Add(CB2);
        List442.Add(CB4);
        List442.Add(DR);
        List442.Add(ML);
        List442.Add(MC2);
        List442.Add(MC4);
        List442.Add(MR);
        List442.Add(FW2);
        List442.Add(FW4);

        List433.Add(GK);
        List433.Add(DL);
        List433.Add(CB2);
        List433.Add(CB4);
        List433.Add(DR);
        List433.Add(MC1);
        List433.Add(MC3);
        List433.Add(MC5);
        List433.Add(FW1);
        List433.Add(FW3);
        List433.Add(FW5);

        List41212.Add(GK);
        List41212.Add(DL);
        List41212.Add(CB2);
        List41212.Add(CB4);
        List41212.Add(DR);
        List41212.Add(DM2);
        List41212.Add(MC2);
        List41212.Add(MC4);
        List41212.Add(AMC2);
        List41212.Add(FW2);
        List41212.Add(FW4);

        List41231.Add(GK);
        List41231.Add(DL);
        List41231.Add(CB2);
        List41231.Add(CB4);
        List41231.Add(DR);
        List41231.Add(MC2);
        List41231.Add(MC4);
        List41231.Add(AML);
        List41231.Add(AMC2);
        List41231.Add(AMR);
        List41231.Add(FW3);

        List32212.Add(GK);
        List32212.Add(CB1);
        List32212.Add(CB3);
        List32212.Add(CB5);
        List32212.Add(WBL);
        List32212.Add(WBR);
        List32212.Add(MC2);
        List32212.Add(MC4);
        List32212.Add(AMC2);
        List32212.Add(FW2);
        List32212.Add(FW4);

        List33211.Add(GK);
        List33211.Add(CB1);
        List33211.Add(CB3);
        List33211.Add(CB5);
        List33211.Add(WBL);
        List33211.Add(DM2);
        List33211.Add(WBR);
        List33211.Add(MC2);
        List33211.Add(MC4);
        List33211.Add(AMC2);
        List33211.Add(FW3);
    }

    public void SetTactic442()
    {
        ResetSuad();
        foreach (GameObject go in AllPositions)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in List442)
        {
            go.SetActive(true);
        }
    }
    public void SetTactic433()
    {
        ResetSuad();
        foreach (GameObject go in AllPositions)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in List433)
        {
            go.SetActive(true);
        }

    }
    public void SetTactic41212()
    {
        ResetSuad();
        foreach (GameObject go in AllPositions)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in List41212)
        {
            go.SetActive(true);
        }

    }
    public void SetTactic41231()
    {
        ResetSuad();
        foreach (GameObject go in AllPositions)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in List41231)
        {
            go.SetActive(true);
        }

    }
    public void SetTactic32212()
    {
        ResetSuad();
        foreach (GameObject go in AllPositions)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in List32212)
        {
            go.SetActive(true);
        }

    }
    public void SetTactic33211()
    {
        ResetSuad();
        foreach (GameObject go in AllPositions)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in List33211)
        {
            go.SetActive(true);
        }

    }

    void ResetSuad()
    {
        PositionPlayerMap = new Dictionary<GameObject, OutfieldPlayer>();

        foreach (KeyValuePair<Goalkeeper, GameObject> pair in GoalieGOMap)
        {
            pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = true;
            pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";


        }
        foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in DefenderGOMap)
        {
            pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = true;
            pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";


        }
        foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in MidfielderGOMap)
        {
            pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = true;
            pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";


        }
        foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in ForwardGOMap)
        {
            pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = true;
            pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";


        }

        foreach (KeyValuePair<string, GameObject> pair in PositionGOMap) 
        {
            pair.Value.GetComponentInChildren<Text>().text = "<empty>";
        }
    }

    public void SelectPosition(string position)
    {
        string role = GetRole(position);
        if (positionSelected == PositionGOMap[position])
        {
            foreach (KeyValuePair<Goalkeeper, GameObject> goalie in GoalieGOMap)
            {
                goalie.Value.SetActive(true);
                goalie.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.SetActive(false);
            }
            foreach (KeyValuePair<OutfieldPlayer, GameObject> player in DefenderGOMap)
            {
                player.Value.SetActive(true);
                player.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.SetActive(false);
            }
            foreach (KeyValuePair<OutfieldPlayer, GameObject> player in MidfielderGOMap)
            {
                player.Value.SetActive(true);
                player.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.SetActive(false);
            }
            foreach (KeyValuePair<OutfieldPlayer, GameObject> player in ForwardGOMap)
            {
                player.Value.SetActive(true);
                player.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.SetActive(false);
            }
            positionSelected.GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.4f, 0f);
            positionSelected = null;
        }
        else
        {
            if (role == "Goalkeeper")
            {

                foreach (KeyValuePair<Goalkeeper, GameObject> goalie in GoalieGOMap)
                {
                    goalie.Value.SetActive(true);
                    goalie.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.SetActive(true);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in DefenderGOMap)
                {
                    player.Value.SetActive(false);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in MidfielderGOMap)
                {
                    player.Value.SetActive(false);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in ForwardGOMap)
                {
                    player.Value.SetActive(false);
                }
            }
            else if (role == "Defender")
            {
                foreach (KeyValuePair<Goalkeeper, GameObject> goalie in GoalieGOMap)
                {
                    goalie.Value.SetActive(false);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in DefenderGOMap)
                {
                    player.Value.SetActive(true);
                    player.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.SetActive(true);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in MidfielderGOMap)
                {
                    player.Value.SetActive(false);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in ForwardGOMap)
                {
                    player.Value.SetActive(false);
                }

            }
            else if (role == "Midfielder")
            {
                foreach (KeyValuePair<Goalkeeper, GameObject> goalie in GoalieGOMap)
                {
                    goalie.Value.SetActive(false);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in DefenderGOMap)
                {
                    player.Value.SetActive(false);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in MidfielderGOMap)
                {
                    player.Value.SetActive(true);
                    player.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.SetActive(true);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in ForwardGOMap)
                {
                    player.Value.SetActive(false);
                }

            }
            else
            {

                foreach (KeyValuePair<Goalkeeper, GameObject> goalie in GoalieGOMap)
                {
                    goalie.Value.SetActive(false);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in DefenderGOMap)
                {
                    player.Value.SetActive(false);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in MidfielderGOMap)
                {
                    player.Value.SetActive(false);
                }
                foreach (KeyValuePair<OutfieldPlayer, GameObject> player in ForwardGOMap)
                {
                    player.Value.SetActive(true);
                    player.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.SetActive(true);
                }
            }
            if (positionSelected != null) { positionSelected.GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.4f, 0f); }
            positionSelected = PositionGOMap[position];
            positionSelected.GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.4f, 0.5f);
        }
    }
    void SelectPlayer(string pos, Goalkeeper goalie = null, OutfieldPlayer player = null)
    {
        if (player == null)
        {
            GK.GetComponentInChildren<Text>().text = goalie.Name;
            foreach(KeyValuePair<Goalkeeper, GameObject> pair in GoalieGOMap)
            {
                if (pair.Key == goalie)
                {
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = false;
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Selected";
                }
                else
                {
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = true;
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";


                }
            }
        }
        else
        {

            if (PositionPlayerMap.ContainsKey(positionSelected))
            {
                
                PositionPlayerMap.Remove(positionSelected);
            }
            PositionPlayerMap.Add(positionSelected, player);
            positionSelected.GetComponentInChildren<Text>().text = player.Name;

            foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in DefenderGOMap)
            {
                if (PositionPlayerMap.ContainsValue(pair.Key))
                {
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = false;
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Selected";

                }
                else
                {
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = true;
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";

                }
            }
            foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in MidfielderGOMap)
            {
                if (PositionPlayerMap.ContainsValue(pair.Key))
                {
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = false;
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Selected";

                }
                else
                {
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = true;
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";

                }
            }
            foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in ForwardGOMap)
            {
                if (PositionPlayerMap.ContainsValue(pair.Key))
                {
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = false;
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Selected";

                }
                else
                {
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.interactable = true;
                    pair.Value.GetComponent<SquadPlayerPrefab>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";

                }
            }
        }
    }
        string GetRole(string pos)
    {
        if (pos == "GK")
        {
            return "Goalkeeper";
        }
        else if (pos == "DL" || pos == "CB2" || pos == "CB3" || pos == "CB4" || pos == "CB5" || pos == "DR")
        {
            return "Defender";
        }
        else if (pos == "FW1" || pos == "FW2" || pos == "FW3" || pos == "FW4" || pos == "FW5")
        {
            return "Forward";
        }
        return "Midfielder";
    }

    public void CloseSquad()
    {

        SquadPanel.SetActive(false);
    }

    //      FW FW FW
    //AML   AMC AMC AMC AMR
    //ML MC MC MC MC MC MR
    //WBL   DM DM DM    WBR
    //DL CB CB CB CB CB DR
    //          GK

}
