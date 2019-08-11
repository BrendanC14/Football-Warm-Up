using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{

    public GameObject ContinueButton;

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
    public GameObject TacticsHeader;
    public GameObject TacticsPanel;
    public GameObject TacticPlayerPrefabController;
    public GameObject TacticsPlayerParent;
    public GameObject PitchPanel;
    public GameObject MatchReviewPanel;
    public GameObject MatchReviewPrefab;
    public GameObject MatchReviewParent;
    public GameObject MatchReviewResultsParent1;
    public GameObject MatchReviewResultsParent2;
    public GameObject MatchReviewResultsParent3;
    public GameObject YouthSquadPanel;
    public GameObject YouthSquadParent;
    public GameObject YouthSquadPrefab;
    public GameObject TransferMarketPanel;
    public GameObject TransferMarketParent;
    public GameObject TransferViewPrefab;

    public GameObject SquadPanel;
    public GameObject SquadViewPrefab;
    public GameObject SquadParent;
    public GameObject LeagueStatsPanel;
    public GameObject LeagueStatsViewPrefab;
    public GameObject LeagueStatsParent;

    ResultVuewPrefabController resultViewController;
    TeamLeaguePrefabController teamLeageController;
    FixturePrefabController fixtureViewController;
    SchedulePrefabController scheduleViewController;
    TacticPlayerPrefabController tacticPlayerController;
    MatchReviewPrefabController matchReviewController;
    SquadViewPrefabController squadPrefabController;
    LeagueStatsPrefabController leaguePrefabController;
    YouthViewPrefabController youthPrefabController;
    TransferViewPrefabController transferPrefabController;

    public GameObject GameWeek;

    Goalkeeper goalieSelected;
    Dictionary<string, GameObject> PositionGOMap;
    Dictionary<Goalkeeper, GameObject> GoalieGOMap;
    Dictionary<OutfieldPlayer, GameObject> DefenderGOMap;
    Dictionary<OutfieldPlayer, GameObject> MidfielderGOMap;
    Dictionary<OutfieldPlayer, GameObject> ForwardGOMap;
    Dictionary<GameObject, OutfieldPlayer> PositionPlayerMap;

    public GameObject PassingShort;
    public GameObject PassingMixed;
    public GameObject PassingDirect;
    public GameObject TacklingSoft;
    public GameObject TacklingMixed;
    public GameObject TacklingHard;
    public GameObject ShootingShort;
    public GameObject ShootingMixed;
    public GameObject ShootingLong;

    public GameObject Tactic442;
    public GameObject Tactic433;
    public GameObject Tactic41212;
    public GameObject Tactic4231;
    public GameObject Tactic32212;
    public GameObject Tactic33211;

    List<GameObject> AllPositions;
    List<GameObject> List442;
    List<GameObject> List433;
    List<GameObject> List41212;
    List<GameObject> List4231;
    List<GameObject> List32212;
    List<GameObject> List33211;

    string passingStyleSelected = "Mixed";
    string tacklingStyleSelected = "Mixed";
    string shootingStyleSelected = "Mixed";
    string tacticSelected;
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

    public GameObject PreviewPanel;
    public GameObject PreviewHomeTeam;
    public GameObject PreviewAwayTeam;
    public GameObject PreviewHomeLineUpParent;
    public GameObject PreviewAwayLineUpParent;
    public GameObject PreviewHomeTactic;
    public GameObject PreviewHomePassing;
    public GameObject PreviewHomeTackling;
    public GameObject PreviewHomeShooting;
    public GameObject PreviewAwayTactic;
    public GameObject PreviewAwayPassing;
    public GameObject PreviewAwayTackling;
    public GameObject PreviewAwayShooting;

    public GameObject TransferMarketButton;

    public GameObject BudgetGameObject;
    public GameObject ConfirmBox;
    public Text ConfirmBoxText;
    public GameObject ConfirmBoxButton;
    public GameObject ConfirmBoxCancelButton;
    public GameObject OKBox;
    public GameObject OKBoxText;


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
            if (c == WorldController.current.ChosenTeam)
            {
                leagueTeamDisplay.GetComponent<Image>().color = new Color(0.7f, 1f, 1f, 1f);
            }
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
        WorldController w = WorldController.current;
        if (w.ChosenTeam.FirstTeam.Count < 10 || WorldController.current.ChosenTeam.Goalie == null)
        {
            OpenOKBox("You haven't selected your first 11 yet!");
            return;
        }
        foreach (OutfieldPlayer player in w.ChosenTeam.FirstTeam)
        {
            if (player.WeeksInjured > 0)
            {
                OpenOKBox("You have an injured player in your first 11!");
                return;

            }
        }



        if (w.GameWeek == 39)
        {
            int currBudget = WorldController.current.ChosenTeam.Budget;
            UpdateBalance(true);
            int budgetChange = WorldController.current.ChosenTeam.Budget - currBudget;
            OpenOKBox("New Season has started and the Transfer Window is open.\nBased on last year's position £" +
                budgetChange.ToString() + "m has been added to your transfer budget");
            BudgetGameObject.GetComponentInChildren<Text>().text = "Budget £" + WorldController.current.ChosenTeam.Budget.ToString() + "m";

            w.LeagueResults = new List<Match>();
            w.Fixtures = new List<Fixture>();
            foreach (Club c in WorldController.current.Clubs)
            {
                c.EndSeason();
            }
            foreach (OutfieldPlayer player in w.Goalscorers)
            {
                player.Goals = 0;
            }
            w.CreateFixtures();
            w.ChosenTeam.Fixtures.Sort();
            ContinueButton.GetComponentInChildren<Text>().text = "Next Match\n" +
             w.Clubs[w.ChosenTeam.Fixtures[0].HomeID].Name + " vs " + w.Clubs[w.ChosenTeam.Fixtures[0].AwayID].Name;

            w.GameWeek = 1;

            w.WindowOpen = true;
            TransferMarketButton.GetComponent<Button>().interactable = true;
            return;
        }
        WorldController.current.Fixtures.Sort();
        Match playersMatch = null;
        for (int i = 0; i < 10; i++)
        {
            Fixture f = WorldController.current.Fixtures[0];
            Match m = new Match(f.HomeID, f.AwayID, f.GameWeek);
            WorldController.current.LeagueResults.Add(m);
            WorldController.current.Clubs[f.HomeID].UpdateLeagueStats(m);
            WorldController.current.Clubs[f.HomeID].Results.Add(m);
            WorldController.current.Clubs[f.AwayID].UpdateLeagueStats(m);
            WorldController.current.Clubs[f.AwayID].Results.Add(m);
            WorldController.current.Fixtures.Remove(f);
            WorldController.current.Clubs[f.HomeID].Fixtures.Remove(f);
            WorldController.current.Clubs[f.AwayID].Fixtures.Remove(f);
            if (m.Injured.Count > 0)
            {
                foreach (OutfieldPlayer player in m.Injured)
                {
                    Club c = player.club;
                    if (c != WorldController.current.ChosenTeam)
                    {
                        if (c.FirstTeam.Contains(player))
                        {
                            c.FirstTeam.Remove(player);
                            if (player.Position == "Defender")
                            {
                                c.FirstTeam.Add(c.Defenders[Random.Range(0, c.Defenders.Count - 1)]);
                            }
                            if (player.Position == "Midfielder")
                            {
                                c.FirstTeam.Add(c.Midfielders[Random.Range(0, c.Midfielders.Count - 1)]);
                            }
                            if (player.Position == "Forward")
                            {
                                c.FirstTeam.Add(c.Forwards[Random.Range(0, c.Forwards.Count - 1)]);
                            }
                        }
                    }
                }
            }
            if (WorldController.current.ChosenTeam.ID == f.HomeID || WorldController.current.ChosenTeam.ID == f.AwayID)
            {
                playersMatch = m;
            }
        }
        WorldController.current.GameWeek++;
        foreach (Club c in WorldController.current.Clubs)
        {
            foreach (OutfieldPlayer player in c.Defenders)
            {
                if (player.WeeksInjured > 0)
                {
                    player.WeeksInjured--;
                    if (player.WeeksInjured == 0 && player.club == WorldController.current.ChosenTeam)
                    {
                        OpenOKBox(player.Name + " has returned from injury");
                    }
                }
            }
            foreach (OutfieldPlayer player in c.Midfielders)
            {
                if (player.WeeksInjured > 0)
                {
                    player.WeeksInjured--;
                    if (player.WeeksInjured == 0 && player.club == WorldController.current.ChosenTeam)
                    {
                        OpenOKBox(player.Name + " has returned from injury");
                    }
                }
            }
            foreach (OutfieldPlayer player in c.Forwards)
            {
                if (player.WeeksInjured > 0)
                {

                    player.WeeksInjured--;
                    if (player.WeeksInjured == 0 && player.club == WorldController.current.ChosenTeam)
                    {
                        OpenOKBox(player.Name + " has returned from injury");
                    }
                }
            }
        }
        
        GameWeek.GetComponent<Text>().text = "Gameweek: " + WorldController.current.GameWeek.ToString() + " " + w.Year.ToString();
        OpenMatchReview(playersMatch);
    }

    void UpdateBalance(bool endOfYear)
    {
        int bal;

        List<Club> leagueTable = new List<Club>(WorldController.current.Clubs);
        leagueTable.Sort();
        int i = 1;
        foreach (Club c in leagueTable)
        {

            if (i == 1)
            {
                bal = 50;
            }
            else if (i >= 2 && i <= 4)
            {
                bal = 45;
            }
            else if (i == 5 || i == 6)
            {
                bal = 40;
            }
            else if (i >= 7 && i <= 10)
            {
                bal = 35;
            }
            else if (i >= 11 && i <= 14)
            {
                bal = 30;
            }
            else if (i >= 15 && i <= 17)
            {
                bal = 25;
            }
            else
            {
                bal = 20;
            }

            if (!endOfYear)
            {
                bal /= 2;
            }

            c.Budget += bal;
            i++;
        }



    }

    public void OpenMatchReview(Match m)
    {
        foreach (Transform child in MatchReviewParent.transform)
        {
            Destroy(child.gameObject);
        }
        MatchReviewPanel.SetActive(true);
        GameObject matchReviewDisplay = Instantiate(MatchReviewPrefab) as GameObject;
        matchReviewDisplay.name = m.HomeID.ToString() + "V" + m.AwayID.ToString();
        matchReviewController = matchReviewDisplay.GetComponent<MatchReviewPrefabController>();
        matchReviewController.HomeTeam.text = WorldController.current.Clubs[m.HomeID].Name;
        matchReviewController.HomeScore.text = m.HomeScore.ToString();
        matchReviewController.AwayScore.text = m.AwayScore.ToString();
        matchReviewController.AwayTeam.text = WorldController.current.Clubs[m.AwayID].Name;
        string homeScorers = "";
        string awayScorers = "";
        foreach (OutfieldPlayer scorer in m.HomeScorers)
        {
            homeScorers += scorer.Name + "\n";
        }
        foreach (OutfieldPlayer scorer in m.AwayScorers)
        {
            awayScorers += scorer.Name + "\n";
        }
        Club chosenTeam = WorldController.current.ChosenTeam;
        if (m.HomeID == chosenTeam.ID || m.AwayID == chosenTeam.ID)
        {
            if (m.HomeScore == m.AwayScore)
            {
                matchReviewDisplay.GetComponent<Image>().color = new Color(1f, 0.5f, 0.3f, 1f);
            }
            else
            {
                if (m.HomeScore > m.AwayScore)
                {
                    if (m.HomeID == chosenTeam.ID)
                    {
                        matchReviewDisplay.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);

                    }
                    else
                    {
                        matchReviewDisplay.GetComponent<Image>().color = new Color(1f, 0f, 0.2f, 1f);

                    }
                }
                else
                {
                    if (m.AwayID == chosenTeam.ID)
                    {
                        matchReviewDisplay.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);

                    }
                    else
                    {
                        matchReviewDisplay.GetComponent<Image>().color = new Color(1f, 0f, 0.2f, 1f);

                    }

                }
            }
        }
        matchReviewController.HomeGoalscorers.text = homeScorers;
        matchReviewController.AwayGoalscorers.text = awayScorers;
        matchReviewController.MatchReport.text = m.MatchReport;
        matchReviewDisplay.transform.SetParent(MatchReviewParent.transform);
        matchReviewDisplay.transform.localPosition = new Vector3(0, 181.125f, 0);
        WorldController w = WorldController.current;
        foreach (Transform child in MatchReviewResultsParent1.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in MatchReviewResultsParent2.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in MatchReviewResultsParent3.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = w.LeagueResults.Count - 1; i > w.LeagueResults.Count - 4; i--)
        {

            Match ma = WorldController.current.LeagueResults[i];
            GameObject resultDisplay = Instantiate(ResultViewPrefab) as GameObject;
            resultDisplay.name = ma.HomeID.ToString() + "V" + m.AwayID.ToString();
            resultViewController = resultDisplay.GetComponent<ResultVuewPrefabController>();
            resultViewController.GameWeek.text = "Gameweek " + ma.GameWeek.ToString();
            resultViewController.HomeTeam.text = WorldController.current.Clubs[ma.HomeID].Name;
            resultViewController.HomeScore.text = ma.HomeScore.ToString();
            resultViewController.AwayScore.text = ma.AwayScore.ToString();
            resultViewController.AwayTeam.text = WorldController.current.Clubs[ma.AwayID].Name;
            resultDisplay.GetComponent<Button>().onClick.AddListener(() => OpenMatchReview(ma));
            resultDisplay.transform.SetParent(MatchReviewResultsParent1.transform);
        }
        for (int i = w.LeagueResults.Count - 4; i > w.LeagueResults.Count - 7; i--)
        {

            Match ma = WorldController.current.LeagueResults[i];
            GameObject resultDisplay = Instantiate(ResultViewPrefab) as GameObject;
            resultDisplay.name = ma.HomeID.ToString() + "V" + m.AwayID.ToString();
            resultViewController = resultDisplay.GetComponent<ResultVuewPrefabController>();
            resultViewController.GameWeek.text = "Gameweek " + ma.GameWeek.ToString();
            resultViewController.HomeTeam.text = WorldController.current.Clubs[ma.HomeID].Name;
            resultViewController.HomeScore.text = ma.HomeScore.ToString();
            resultViewController.AwayScore.text = ma.AwayScore.ToString();
            resultViewController.AwayTeam.text = WorldController.current.Clubs[ma.AwayID].Name;
            resultDisplay.GetComponent<Button>().onClick.AddListener(() => OpenMatchReview(ma));
            resultDisplay.transform.SetParent(MatchReviewResultsParent2.transform);
        }
        for (int i = w.LeagueResults.Count - 7; i > w.LeagueResults.Count - 10; i--)
        {

            Match ma = WorldController.current.LeagueResults[i];
            GameObject resultDisplay = Instantiate(ResultViewPrefab) as GameObject;
            resultDisplay.name = ma.HomeID.ToString() + "V" + m.AwayID.ToString();
            resultViewController = resultDisplay.GetComponent<ResultVuewPrefabController>();
            resultViewController.GameWeek.text = "Gameweek " + ma.GameWeek.ToString();
            resultViewController.HomeTeam.text = WorldController.current.Clubs[ma.HomeID].Name;
            resultViewController.HomeScore.text = ma.HomeScore.ToString();
            resultViewController.AwayScore.text = ma.AwayScore.ToString();
            resultViewController.AwayTeam.text = WorldController.current.Clubs[ma.AwayID].Name;
            resultDisplay.GetComponent<Button>().onClick.AddListener(() => OpenMatchReview(ma));
            resultDisplay.transform.SetParent(MatchReviewResultsParent3.transform);
        }
    }

    public void CloseMatchReview()
    {
        WorldController w = WorldController.current;
        MatchReviewPanel.SetActive(false);
        if (WorldController.current.GameWeek == 39)
        {
            ContinueButton.GetComponentInChildren<Text>().text = "End Season";
        }
        else
        {
            if (w.Fixtures.Count > 0)
            {
                ContinueButton.GetComponentInChildren<Text>().text = "Next Match\n" +
                    w.Clubs[w.ChosenTeam.Fixtures[0].HomeID].Name + " vs " + w.Clubs[w.ChosenTeam.Fixtures[0].AwayID].Name;
            }
        }
        Club chosenCLub = w.ChosenTeam;
        int randomInjury = Random.Range(1, 38);
        if (randomInjury >= 35)
        {
            OutfieldPlayer player;
            if (randomInjury == 35)
            {
                player = chosenCLub.Defenders[Random.Range(0, chosenCLub.Defenders.Count - 1)];
            }
            else if (randomInjury == 36)
            {
                player = chosenCLub.Forwards[Random.Range(0, chosenCLub.Midfielders.Count - 1)];
            }
            else
            {
                player = chosenCLub.Midfielders[Random.Range(0, chosenCLub.Forwards.Count - 1)];
            }


            player.WeeksInjured = Random.Range(2, 9);
            OpenOKBox(player.Name + " has picked up an injury training and is out for " + player.WeeksInjured.ToString() + " weeks");
        }


        if (w.GameWeek == 5)
        {
            OpenOKBox("The Transfer Window closes after your next game");

        }
        else if (w.GameWeek == 6)
        {
            OpenOKBox("The Transfer Window has closed");
            w.WindowOpen = false;
            TransferMarketButton.GetComponent<Button>().interactable = false;
        }
        else if (w.GameWeek == 21)
        {
            w.Year++;
            int currBudget = WorldController.current.ChosenTeam.Budget;
            UpdateBalance(false);
            int budgetChange = WorldController.current.ChosenTeam.Budget - currBudget;
            OpenOKBox("The Transfer Window has opened.\nBased on your current position £" +
                budgetChange.ToString() + "m has been added to your transfer budget");
            w.WindowOpen = true;
            TransferMarketButton.GetComponent<Button>().interactable = true;
            BudgetGameObject.GetComponentInChildren<Text>().text = "£" + WorldController.current.ChosenTeam.Budget.ToString() + "m";
        }
        else if (w.GameWeek == 24)
        {
            OpenOKBox("The Transfer Window closes after your next game");
        }
        else if (w.GameWeek == 24)
        {
            OpenOKBox("The Transfer Window has closed");
            w.WindowOpen = false;
            TransferMarketButton.GetComponent<Button>().interactable = false;
        }
    }

    public void OpenResults()
    {
        ResultsPanel.SetActive(true);
        for (int i = WorldController.current.LeagueResults.Count - 1; i >= 0; i--)
        {
            Match m = WorldController.current.LeagueResults[i];
            GameObject resultDisplay = Instantiate(ResultViewPrefab) as GameObject;
            resultDisplay.name = m.HomeID.ToString() + "V" + m.AwayID.ToString();
            resultViewController = resultDisplay.GetComponent<ResultVuewPrefabController>();
            resultViewController.GameWeek.text = "Gameweek " + m.GameWeek.ToString();
            resultViewController.HomeTeam.text = WorldController.current.Clubs[m.HomeID].Name;
            resultViewController.HomeScore.text = m.HomeScore.ToString();
            resultViewController.AwayScore.text = m.AwayScore.ToString();
            resultViewController.AwayTeam.text = WorldController.current.Clubs[m.AwayID].Name;
            resultDisplay.GetComponent<Button>().onClick.AddListener(() => OpenMatchReview(m));
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
            fixtureDisplay.GetComponent<Button>().onClick.AddListener(() => OpenMatchPreview(f));
            fixtureDisplay.transform.SetParent(FixtureParent.transform);
            if (WorldController.current.IsMyClubID(f.HomeID) || WorldController.current.IsMyClubID(f.AwayID))
            {
                fixtureDisplay.GetComponent<Image>().color = new Color(0.7f, 1f, 1f, 1f);
            }
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
        WorldController.current.ChosenTeam.Results.Sort();
        Club chosenTeam = WorldController.current.ChosenTeam;
        foreach (Match m in chosenTeam.Results)
        {
            GameObject resultDisplay = Instantiate(ResultViewPrefab) as GameObject;
            resultViewController = resultDisplay.GetComponent<ResultVuewPrefabController>();
            resultViewController.GameWeek.text = "Gameweek " + m.GameWeek.ToString();
            resultViewController.HomeTeam.text = WorldController.current.Clubs[m.HomeID].Name;
            resultViewController.HomeScore.text = m.HomeScore.ToString();
            resultViewController.AwayScore.text = m.AwayScore.ToString();
            resultViewController.AwayTeam.text = WorldController.current.Clubs[m.AwayID].Name;
            resultDisplay.GetComponent<Button>().onClick.AddListener(() => OpenMatchReview(m));
            resultDisplay.transform.SetParent(ScheduleParent.transform);

            if (m.HomeScore == m.AwayScore)
            {
                resultDisplay.GetComponent<Image>().color = new Color(1f, 0.5f, 0.3f, 1f);
            }
            else
            {
                if (m.HomeScore > m.AwayScore)
                {
                    if (m.HomeID == chosenTeam.ID)
                    {
                        resultDisplay.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);

                    }
                    else
                    {
                        resultDisplay.GetComponent<Image>().color = new Color(1f, 0f, 0.2f, 1f);

                    }
                }
                else
                {
                    if (m.AwayID == chosenTeam.ID)
                    {
                        resultDisplay.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);

                    }
                    else
                    {
                        resultDisplay.GetComponent<Image>().color = new Color(1f, 0f, 0.2f, 1f);

                    }

                }
            }
        }
        chosenTeam.Fixtures.Sort();
        foreach (Fixture f in chosenTeam.Fixtures)
        {
            GameObject fixtureDisplay = Instantiate(SchedulePrefab) as GameObject;
            fixtureDisplay.name = f.HomeID.ToString() + "V" + f.AwayID.ToString();
            scheduleViewController = fixtureDisplay.GetComponent<SchedulePrefabController>();
            scheduleViewController.GameWeek.text = "Gameweek: " + f.GameWeek.ToString();
            scheduleViewController.HomeTeam.text = WorldController.current.Clubs[f.HomeID].Name;
            scheduleViewController.AwayTeam.text = WorldController.current.Clubs[f.AwayID].Name;
            fixtureDisplay.GetComponent<Button>().onClick.AddListener(() => OpenMatchPreview(f));
            fixtureDisplay.transform.SetParent(ScheduleParent.transform);

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

    public void OpenTactics()
    {
        GoalieGOMap = new Dictionary<Goalkeeper, GameObject>();
        DefenderGOMap = new Dictionary<OutfieldPlayer, GameObject>();
        MidfielderGOMap = new Dictionary<OutfieldPlayer, GameObject>();
        ForwardGOMap = new Dictionary<OutfieldPlayer, GameObject>();
        foreach (Transform child in TacticsPlayerParent.transform)
        {
            Destroy(child.gameObject);
        }


        TacticsPanel.SetActive(true);
        Club c = WorldController.current.ChosenTeam;
        TacticsHeader.GetComponent<Text>().text = c.Name;

        foreach (Goalkeeper goalie in c.Goalies)
        {
            GameObject newPlayerDisplay = Instantiate(TacticPlayerPrefabController) as GameObject;
            newPlayerDisplay.name = goalie.Name + goalie.ID;
            tacticPlayerController = newPlayerDisplay.GetComponent<TacticPlayerPrefabController>();
            tacticPlayerController.Name.text = goalie.Name;
            tacticPlayerController.Age.text = "Age: " + goalie.Age.ToString();
            tacticPlayerController.Position.text = "Goalkeeper";
            tacticPlayerController.Stats.text = "Reflexes: " + goalie.Reflexes.ToString() +
                "\nHandling: " + goalie.Handling.ToString() +
                "\nOne on ones: " + goalie.OneOnOnes.ToString() +
                "\nPassing: " + goalie.Passing.ToString() +
                "\nDiving: " + goalie.Diving.ToString();
            tacticPlayerController.Select.onClick.AddListener(() => SelectPlayer(goalie));
            newPlayerDisplay.transform.SetParent(TacticsPlayerParent.transform);
            GoalieGOMap.Add(goalie, newPlayerDisplay);
        }
        foreach (OutfieldPlayer player in c.Defenders)
        {
            GameObject newPlayerDisplay = Instantiate(TacticPlayerPrefabController) as GameObject;
            newPlayerDisplay.name = player.Name + player.ID;
            tacticPlayerController = newPlayerDisplay.GetComponent<TacticPlayerPrefabController>();
            tacticPlayerController.Name.text = player.Name;
            tacticPlayerController.Age.text = "Age: " + player.Age.ToString();
            tacticPlayerController.Position.text = "Defender";
            tacticPlayerController.Stats.text = "Passing: " + player.Passing.ToString() +
                "\nTackling: " + player.Tackling.ToString() +
                "\nShooting: " + player.Shooting.ToString() +
                "\nInterceptions: " + player.Interception.ToString() +
                "\nVision: " + player.Vision.ToString();
            tacticPlayerController.Select.onClick.AddListener(() => SelectPlayer(null, player));
            if (player.WeeksInjured > 0)
            {
                if (player.WeeksInjured == 1)
                {
                    tacticPlayerController.Injured.text = "*injured for 1 week";
                    tacticPlayerController.Injured.color = new Color(1f, 0f, 0.2f, 1f);
                }
                else
                {
                    tacticPlayerController.Injured.text = "injured for " + player.WeeksInjured.ToString() + " weeks";
                }
            }
            else
            {
                tacticPlayerController.Injured.text = "";
            }
            newPlayerDisplay.transform.SetParent(TacticsPlayerParent.transform);
            DefenderGOMap.Add(player, newPlayerDisplay);
        }
        foreach (OutfieldPlayer player in c.Midfielders)
        {
            GameObject newPlayerDisplay = Instantiate(TacticPlayerPrefabController) as GameObject;
            newPlayerDisplay.name = player.Name + player.ID;
            tacticPlayerController = newPlayerDisplay.GetComponent<TacticPlayerPrefabController>();
            tacticPlayerController.Name.text = player.Name;
            tacticPlayerController.Age.text = "Age: " + player.Age.ToString();
            tacticPlayerController.Position.text = "Midfielder";
            tacticPlayerController.Stats.text = "Passing: " + player.Passing.ToString() +
                "\nTackling: " + player.Tackling.ToString() +
                "\nShooting: " + player.Shooting.ToString() +
                "\nInterceptions: " + player.Interception.ToString() +
                "\nVision: " + player.Vision.ToString();
            tacticPlayerController.Select.onClick.AddListener(() => SelectPlayer(null, player));
            if (player.WeeksInjured > 0)
            {
                if (player.WeeksInjured == 1)
                {
                    tacticPlayerController.Injured.text = "*injured for 1 week";
                }
                else
                {
                    tacticPlayerController.Injured.text = "injured for " + player.WeeksInjured.ToString() + " weeks";
                }
            }
            else
            {
                tacticPlayerController.Injured.text = "";
            }
            newPlayerDisplay.transform.SetParent(TacticsPlayerParent.transform);
            MidfielderGOMap.Add(player, newPlayerDisplay);
        }
        foreach (OutfieldPlayer player in c.Forwards)
        {
            GameObject newPlayerDisplay = Instantiate(TacticPlayerPrefabController) as GameObject;
            newPlayerDisplay.name = player.Name + player.ID;
            tacticPlayerController = newPlayerDisplay.GetComponent<TacticPlayerPrefabController>();
            tacticPlayerController.Name.text = player.Name;
            tacticPlayerController.Age.text = "Age: " + player.Age.ToString();
            tacticPlayerController.Position.text = "Forward";
            tacticPlayerController.Stats.text = "Passing: " + player.Passing.ToString() +
                "\nTackling: " + player.Tackling.ToString() +
                "\nShooting: " + player.Shooting.ToString() +
                "\nInterceptions: " + player.Interception.ToString() +
                "\nVision: " + player.Vision.ToString();
            tacticPlayerController.Select.onClick.AddListener(() => SelectPlayer(null, player));
            if (player.WeeksInjured > 0)
            {
                if (player.WeeksInjured == 1)
                {
                    tacticPlayerController.Injured.text = "*injured for 1 week";
                }
                else
                {
                    tacticPlayerController.Injured.text = "injured for " + player.WeeksInjured.ToString() + " weeks";
                }
            }
            else
            {
                tacticPlayerController.Injured.text = "";
            }
            newPlayerDisplay.transform.SetParent(TacticsPlayerParent.transform);
            ForwardGOMap.Add(player, newPlayerDisplay);
        }

        string clubTactic = c.Tactic;
        if (clubTactic == "442") { SetTactic442(); }
        else if (clubTactic == "433") { SetTactic433(); }
        else if (clubTactic == "41212") { SetTactic41212(); }
        else if (clubTactic == "4231") { SetTactic4231(); }
        else if (clubTactic == "32212") { SetTactic4231(); }
        else if (clubTactic == "32212") { SetTactic33211(); }

        if (c.Goalie != null)
        {
            positionSelected = PositionGOMap["GK"];
            SelectPlayer(c.Goalie);
            positionSelected.GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.4f, 0f);
            positionSelected = null;
        }

        if (c.FirstTeamGOKey.Count > 0)
        {
            foreach (KeyValuePair<string, OutfieldPlayer> pair in c.FirstTeamGOKey)
            {
                positionSelected = PositionGOMap[pair.Key];
                SelectPlayer(null, pair.Value);
                positionSelected.GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.4f, 0f);
                positionSelected = null;
            }
        }

        if (positionSelected != null)
        {
            positionSelected.GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.4f, 0f);
            positionSelected = null;
        }

        SetPassingStyle(c.PassingStyle);
        SetTacklingStyle(c.TacklingStyle);
        SetShootingStyle(c.ShootingStyle);
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
        List4231 = new List<GameObject>();
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

        List4231.Add(GK);
        List4231.Add(DL);
        List4231.Add(CB2);
        List4231.Add(CB4);
        List4231.Add(DR);
        List4231.Add(MC2);
        List4231.Add(MC4);
        List4231.Add(AML);
        List4231.Add(AMC2);
        List4231.Add(AMR);
        List4231.Add(FW3);

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
        Tactic442.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        Tactic433.GetComponent<Image>().color = Color.white;
        Tactic41212.GetComponent<Image>().color = Color.white;
        Tactic4231.GetComponent<Image>().color = Color.white;
        Tactic32212.GetComponent<Image>().color = Color.white;
        Tactic33211.GetComponent<Image>().color = Color.white;
        tacticSelected = "442";
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
        tacticSelected = "433";
        Tactic433.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        Tactic442.GetComponent<Image>().color = Color.white;
        Tactic41212.GetComponent<Image>().color = Color.white;
        Tactic4231.GetComponent<Image>().color = Color.white;
        Tactic32212.GetComponent<Image>().color = Color.white;
        Tactic33211.GetComponent<Image>().color = Color.white;
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
        tacticSelected = "41212";
        Tactic41212.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        Tactic433.GetComponent<Image>().color = Color.white;
        Tactic442.GetComponent<Image>().color = Color.white;
        Tactic4231.GetComponent<Image>().color = Color.white;
        Tactic32212.GetComponent<Image>().color = Color.white;
        Tactic33211.GetComponent<Image>().color = Color.white;
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
    public void SetTactic4231()
    {
        tacticSelected = "4231";
        Tactic4231.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        Tactic433.GetComponent<Image>().color = Color.white;
        Tactic41212.GetComponent<Image>().color = Color.white;
        Tactic442.GetComponent<Image>().color = Color.white;
        Tactic32212.GetComponent<Image>().color = Color.white;
        Tactic33211.GetComponent<Image>().color = Color.white;
        ResetSuad();
        foreach (GameObject go in AllPositions)
        {
            go.SetActive(false);
        }

        foreach (GameObject go in List4231)
        {
            go.SetActive(true);
        }

    }
    public void SetTactic32212()
    {
        tacticSelected = "32212";
        Tactic32212.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        Tactic433.GetComponent<Image>().color = Color.white;
        Tactic41212.GetComponent<Image>().color = Color.white;
        Tactic4231.GetComponent<Image>().color = Color.white;
        Tactic442.GetComponent<Image>().color = Color.white;
        Tactic33211.GetComponent<Image>().color = Color.white;
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
        tacticSelected = "33211";
        Tactic33211.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        Tactic433.GetComponent<Image>().color = Color.white;
        Tactic41212.GetComponent<Image>().color = Color.white;
        Tactic4231.GetComponent<Image>().color = Color.white;
        Tactic32212.GetComponent<Image>().color = Color.white;
        Tactic442.GetComponent<Image>().color = Color.white;
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

    public void SetPassingStyle(string style)
    {
        if (style == "Mixed")
        {
            PassingDirect.GetComponent<Image>().color = Color.white;
            PassingShort.GetComponent<Image>().color = Color.white;
            PassingMixed.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        }
        else if (style == "Short")
        {
            PassingDirect.GetComponent<Image>().color = Color.white;
            PassingMixed.GetComponent<Image>().color = Color.white;
            PassingShort.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);

        }
        else
        {
            PassingMixed.GetComponent<Image>().color = Color.white;
            PassingShort.GetComponent<Image>().color = Color.white;
            PassingDirect.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        }
        passingStyleSelected = style;
    }
    public void SetTacklingStyle(string style)
    {
        if (style == "Mixed")
        {
            TacklingHard.GetComponent<Image>().color = Color.white;
            TacklingSoft.GetComponent<Image>().color = Color.white;
            TacklingMixed.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        }
        else if (style == "Hard")
        {
            TacklingMixed.GetComponent<Image>().color = Color.white;
            TacklingSoft.GetComponent<Image>().color = Color.white;
            TacklingHard.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);

        }
        else
        {
            TacklingHard.GetComponent<Image>().color = Color.white;
            TacklingMixed.GetComponent<Image>().color = Color.white;
            TacklingSoft.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        }
        tacklingStyleSelected = style;
    }
    public void SetShootingStyle(string style)
    {

        if (style == "Mixed")
        {
            ShootingLong.GetComponent<Image>().color = Color.white;
            ShootingShort.GetComponent<Image>().color = Color.white;
            ShootingMixed.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        }
        else if (style == "Long")
        {
            ShootingShort.GetComponent<Image>().color = Color.white;
            ShootingMixed.GetComponent<Image>().color = Color.white;
            ShootingLong.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);

        }
        else
        {
            ShootingLong.GetComponent<Image>().color = Color.white;
            ShootingMixed.GetComponent<Image>().color = Color.white;
            ShootingShort.GetComponent<Image>().color = new Color(0.1f, 0.7f, 0.4f, 1f);
        }
        shootingStyleSelected = style;
    }

    void ResetSuad()
    {
        PositionPlayerMap = new Dictionary<GameObject, OutfieldPlayer>();

        foreach (KeyValuePair<Goalkeeper, GameObject> pair in GoalieGOMap)
        {
            pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = true;
            pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";


        }
        foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in DefenderGOMap)
        {
            pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = true;
            pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";


        }
        foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in MidfielderGOMap)
        {
            pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = true;
            pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";


        }
        foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in ForwardGOMap)
        {
            pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = true;
            pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";


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
                goalie.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.SetActive(false);
            }
            foreach (KeyValuePair<OutfieldPlayer, GameObject> player in DefenderGOMap)
            {
                player.Value.SetActive(true);
                player.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.SetActive(false);
            }
            foreach (KeyValuePair<OutfieldPlayer, GameObject> player in MidfielderGOMap)
            {
                player.Value.SetActive(true);
                player.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.SetActive(false);
            }
            foreach (KeyValuePair<OutfieldPlayer, GameObject> player in ForwardGOMap)
            {
                player.Value.SetActive(true);
                player.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.SetActive(false);
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
                    goalie.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.SetActive(true);
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
                    player.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.SetActive(true);
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
                    player.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.SetActive(true);
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
                    player.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.SetActive(true);
                }
            }
            if (positionSelected != null) { positionSelected.GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.4f, 0f); }
            positionSelected = PositionGOMap[position];
            positionSelected.GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.4f, 0.5f);
        }
    }
    void SelectPlayer(Goalkeeper goalie = null, OutfieldPlayer player = null)
    {
        if (player == null)
        {
            goalieSelected = goalie;
            GK.GetComponentInChildren<Text>().text = goalie.Name;
            foreach (KeyValuePair<Goalkeeper, GameObject> pair in GoalieGOMap)
            {
                if (pair.Key == goalie)
                {
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = false;
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Selected";
                }
                else
                {
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = true;
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";


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
            if (player.WeeksInjured > 0) { positionSelected.GetComponentInChildren<Text>().text = player.Name + "+"; }

            foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in DefenderGOMap)
            {
                if (PositionPlayerMap.ContainsValue(pair.Key))
                {
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = false;
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Selected";

                }
                else
                {
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = true;
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";

                }
            }
            foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in MidfielderGOMap)
            {
                if (PositionPlayerMap.ContainsValue(pair.Key))
                {
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = false;
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Selected";

                }
                else
                {
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = true;
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";

                }
            }
            foreach (KeyValuePair<OutfieldPlayer, GameObject> pair in ForwardGOMap)
            {
                if (PositionPlayerMap.ContainsValue(pair.Key))
                {
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = false;
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";

                }
                else
                {
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.interactable = true;
                    pair.Value.GetComponent<TacticPlayerPrefabController>().Select.gameObject.GetComponentInChildren<Text>().text = "Select";

                }
            }
        }
    }
    public void ConfirmTactics()
    {
        Club c = WorldController.current.ChosenTeam;
        c.Tactic = tacticSelected;
        c.Goalie = goalieSelected;
        c.PassingStyle = passingStyleSelected;
        c.TacklingStyle = tacklingStyleSelected;
        c.ShootingStyle = shootingStyleSelected;
        c.FirstTeam = new List<OutfieldPlayer>();
        c.FirstTeamGOKey = new Dictionary<string, OutfieldPlayer>();
        foreach (KeyValuePair<GameObject, OutfieldPlayer> pair in PositionPlayerMap)
        {
            c.FirstTeamGOKey.Add(pair.Key.name, pair.Value);
            c.FirstTeam.Add(pair.Value);
        }
        CloseTactics();
    }


    string GetRole(string pos)
    {
        if (pos == "GK")
        {
            return "Goalkeeper";
        }
        else if (pos == "DL" || pos == "CB1" || pos == "CB2" || pos == "CB3" || pos == "CB4" || pos == "CB5" || pos == "DR" || pos == "WBL" || pos == "WBR")
        {
            return "Defender";
        }
        else if (pos == "FW1" || pos == "FW2" || pos == "FW3" || pos == "FW4" || pos == "FW5")
        {
            return "Forward";
        }
        return "Midfielder";
    }

    public void CheckCloseTactics()
    {

        ConfirmBoxButton.GetComponent<Button>().onClick.AddListener(() => CloseTactics());
        OpenConfirmBox("Any changes will not be saved. Are you sure you want to leave?");
    }

    public void CloseTactics()
    {
        ConfirmBox.SetActive(false);
        PositionPlayerMap = new Dictionary<GameObject, OutfieldPlayer>();
        TacticsPanel.SetActive(false);
        foreach (KeyValuePair<string, GameObject> go in PositionGOMap)
        {
            go.Value.GetComponentInChildren<Text>().text = "<empty>";
            go.Value.GetComponent<Image>().color = new Color(0.3f, 0.4f, 0.4f, 0f);
        }


    }

    public void OpenSquad()
    {
        SquadPanel.SetActive(true);
        foreach (Transform child in SquadParent.transform)
        {
            Destroy(child.gameObject);
        }
        Club c = WorldController.current.ChosenTeam;
        foreach (Goalkeeper goalie in c.Goalies)
        {
            GameObject goalieDisplay = Instantiate(SquadViewPrefab) as GameObject;
            goalieDisplay.name = goalie.Name;
            squadPrefabController = goalieDisplay.GetComponent<SquadViewPrefabController>();
            squadPrefabController.Name.text = goalie.Name;
            squadPrefabController.Age.text = goalie.Age.ToString();
            squadPrefabController.Position.text = "GK";
            squadPrefabController.Goals.text = "N/A";
            squadPrefabController.Assists.text = "N/A";
            squadPrefabController.Value.text = "£" + goalie.value.ToString() + "m";
            squadPrefabController.Sell.gameObject.SetActive(false);
            goalieDisplay.transform.SetParent(SquadParent.transform);
            goalieDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        foreach (OutfieldPlayer player in c.Defenders)
        {
            GameObject defenderDisplay = Instantiate(SquadViewPrefab) as GameObject;
            defenderDisplay.name = player.Name;
            squadPrefabController = defenderDisplay.GetComponent<SquadViewPrefabController>();
            squadPrefabController.Name.text = player.Name;
            squadPrefabController.Age.text = player.Age.ToString();
            squadPrefabController.Position.text = "DEF";
            squadPrefabController.Value.text = "£" + player.Cost.ToString() + "m";
            squadPrefabController.Goals.text = player.Goals.ToString();
            squadPrefabController.Assists.text = player.Assists.ToString();
            if (WorldController.current.WindowOpen && c.Defenders.Count > 5)
            {
                squadPrefabController.Sell.gameObject.SetActive(true);
                squadPrefabController.Sell.onClick.AddListener(() => SellPlayer(player));
            }
            else
            {
                squadPrefabController.Sell.gameObject.SetActive(false);

            }
            defenderDisplay.transform.SetParent(SquadParent.transform);
            defenderDisplay.transform.localScale = new Vector3(1f, 1f, 1f);

        }
        foreach (OutfieldPlayer player in c.Midfielders)
        {
            GameObject midfielderDisplay = Instantiate(SquadViewPrefab) as GameObject;
            midfielderDisplay.name = player.Name;
            squadPrefabController = midfielderDisplay.GetComponent<SquadViewPrefabController>();
            squadPrefabController.Name.text = player.Name;
            squadPrefabController.Age.text = player.Age.ToString();
            squadPrefabController.Position.text = "MID";
            squadPrefabController.Goals.text = player.Goals.ToString();
            squadPrefabController.Assists.text = player.Assists.ToString();
            squadPrefabController.Value.text = "£" + player.Cost.ToString() + "m";
            if (WorldController.current.WindowOpen && c.Midfielders.Count > 5)
            {
                squadPrefabController.Sell.gameObject.SetActive(true);
                squadPrefabController.Sell.onClick.AddListener(() => SellPlayer(player));
            }
            else
            {
                squadPrefabController.Sell.gameObject.SetActive(false);

            }
            midfielderDisplay.transform.SetParent(SquadParent.transform);
            midfielderDisplay.transform.localScale = new Vector3(1f, 1f, 1f);

        }
        foreach (OutfieldPlayer player in c.Forwards)
        {
            GameObject forwardDisplay = Instantiate(SquadViewPrefab) as GameObject;
            forwardDisplay.name = player.Name;
            squadPrefabController = forwardDisplay.GetComponent<SquadViewPrefabController>();
            squadPrefabController.Name.text = player.Name;
            squadPrefabController.Age.text = player.Age.ToString();
            squadPrefabController.Position.text = "FW";
            squadPrefabController.Goals.text = player.Goals.ToString();
            squadPrefabController.Assists.text = player.Assists.ToString();
            squadPrefabController.Value.text = "£" + player.Cost.ToString() + "m";
            if (WorldController.current.WindowOpen && c.Forwards.Count > 3)
            {
                squadPrefabController.Sell.gameObject.SetActive(true);
                squadPrefabController.Sell.onClick.AddListener(() => SellPlayer(player));
            }
            else
            {
                squadPrefabController.Sell.gameObject.SetActive(false);

            }
            forwardDisplay.transform.SetParent(SquadParent.transform);
            forwardDisplay.transform.localScale = new Vector3(1f, 1f, 1f);

        }
    }

    public void CloseSquad()
    {
        SquadPanel.SetActive(false);


    }

    public void OpenLeagueStats()
    {
        foreach (Transform child in LeagueStatsParent.transform)
        {
            Destroy(child.gameObject);
        }
        LeagueStatsPanel.SetActive(true);
        WorldController.current.Goalscorers.Sort();
        foreach (OutfieldPlayer player in WorldController.current.Goalscorers)
        {
            GameObject playerDisplay = Instantiate(LeagueStatsViewPrefab) as GameObject;
            playerDisplay.name = player.Name;
            leaguePrefabController = playerDisplay.GetComponent<LeagueStatsPrefabController>();
            leaguePrefabController.Name.text = player.Name;
            leaguePrefabController.TeamName.text = player.club.Name;
            leaguePrefabController.Position.text = player.GetPositionShortForm(player.Position);
            leaguePrefabController.Goals.text = player.Goals.ToString();
            playerDisplay.transform.SetParent(LeagueStatsParent.transform);
            playerDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    public void CloseLeagueStats()
    {

        LeagueStatsPanel.SetActive(false);

    }

    public void OpenNextMatchPreview()
    {
        OpenMatchPreview();
    }

    public void OpenMatchPreview(Fixture f = null)
    {
        foreach (Transform child in PreviewAwayLineUpParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in PreviewHomeLineUpParent.transform)
        {
            Destroy(child.gameObject);
        }
        if (WorldController.current.ChosenTeam.Fixtures.Count > 0)
        {
            Club c = WorldController.current.ChosenTeam;
            PreviewPanel.SetActive(true);
            Club HomeTeam;
            Club AwayTeam;
            if (f == null)
            {
                 HomeTeam = WorldController.current.Clubs[c.Fixtures[0].HomeID];
                 AwayTeam = WorldController.current.Clubs[c.Fixtures[0].AwayID];
            }
            else
            {
                HomeTeam = WorldController.current.Clubs[f.HomeID];
                AwayTeam = WorldController.current.Clubs[f.AwayID];
            }

            PreviewHomeTeam.GetComponent<Text>().text = HomeTeam.Name;
            PreviewAwayTeam.GetComponent<Text>().text = AwayTeam.Name;

            PreviewHomeTactic.GetComponentInChildren<Text>().text = HomeTeam.Tactic;
            PreviewHomePassing.GetComponentInChildren<Text>().text = HomeTeam.PassingStyle;
            PreviewHomeTackling.GetComponentInChildren<Text>().text = HomeTeam.TacklingStyle;
            PreviewHomeShooting.GetComponentInChildren<Text>().text = HomeTeam.ShootingStyle;

            PreviewAwayTactic.GetComponentInChildren<Text>().text = AwayTeam.Tactic;
            PreviewAwayPassing.GetComponentInChildren<Text>().text = AwayTeam.PassingStyle;
            PreviewAwayTackling.GetComponentInChildren<Text>().text = AwayTeam.TacklingStyle;
            PreviewAwayShooting.GetComponentInChildren<Text>().text = AwayTeam.ShootingStyle;

            Goalkeeper goalie = HomeTeam.Goalie;
            GameObject newPlayerDisplay = Instantiate(TacticPlayerPrefabController) as GameObject;
            tacticPlayerController = newPlayerDisplay.GetComponent<TacticPlayerPrefabController>();
            if (goalie != null)
            {
                newPlayerDisplay.name = goalie.Name + goalie.ID;
                tacticPlayerController.Name.text = goalie.Name;
                tacticPlayerController.Age.text = "Age: " + goalie.Age.ToString();
                tacticPlayerController.Position.text = "Goalkeeper";
                tacticPlayerController.Stats.text = "Reflexes: " + goalie.Reflexes.ToString() +
                    "\nHandling: " + goalie.Handling.ToString() +
                    "\nOne on ones: " + goalie.OneOnOnes.ToString() +
                    "\nPassing: " + goalie.Passing.ToString() +
                    "\nDiving: " + goalie.Diving.ToString();
            }
            else
            {
                newPlayerDisplay.name = "GK";
                tacticPlayerController.Name.text = "<empty>";

            }
            newPlayerDisplay.transform.SetParent(PreviewHomeLineUpParent.transform);

            goalie = AwayTeam.Goalie;
            newPlayerDisplay = Instantiate(TacticPlayerPrefabController) as GameObject;
            tacticPlayerController = newPlayerDisplay.GetComponent<TacticPlayerPrefabController>();
            if (goalie != null)
            {
                newPlayerDisplay.name = goalie.Name + goalie.ID;
                tacticPlayerController.Name.text = goalie.Name;
                tacticPlayerController.Age.text = "Age: " + goalie.Age.ToString();
                tacticPlayerController.Position.text = "Goalkeeper";
                tacticPlayerController.Stats.text = "Reflexes: " + goalie.Reflexes.ToString() +
                    "\nHandling: " + goalie.Handling.ToString() +
                    "\nOne on ones: " + goalie.OneOnOnes.ToString() +
                    "\nPassing: " + goalie.Passing.ToString() +
                    "\nDiving: " + goalie.Diving.ToString();
            }
            else
            {
                newPlayerDisplay.name = "GK";
                tacticPlayerController.Name.text = "<empty>";

            }
            newPlayerDisplay.transform.SetParent(PreviewAwayLineUpParent.transform);




            foreach (OutfieldPlayer player in HomeTeam.FirstTeam)
            {
                newPlayerDisplay = Instantiate(TacticPlayerPrefabController) as GameObject;
                tacticPlayerController = newPlayerDisplay.GetComponent<TacticPlayerPrefabController>();
                newPlayerDisplay.name = player.Name + player.ID;
                tacticPlayerController.Name.text = player.Name;
                tacticPlayerController.Age.text = "Age: " + player.Age.ToString();
                tacticPlayerController.Position.text = player.Position;
                tacticPlayerController.Stats.text = "Passing: " + player.Passing.ToString() +
                    "\nTackling: " + player.Tackling.ToString() +
                    "\nShooting: " + player.Shooting.ToString() +
                    "\nInterceptions: " + player.Interception.ToString() +
                    "\nVision: " + player.Vision.ToString();


                newPlayerDisplay.transform.SetParent(PreviewHomeLineUpParent.transform);

            }
            foreach (OutfieldPlayer player in AwayTeam.FirstTeam)
            {
                newPlayerDisplay = Instantiate(TacticPlayerPrefabController) as GameObject;
                tacticPlayerController = newPlayerDisplay.GetComponent<TacticPlayerPrefabController>();
                newPlayerDisplay.name = player.Name + player.ID;
                tacticPlayerController.Name.text = player.Name;
                tacticPlayerController.Age.text = "Age: " + player.Age.ToString();
                tacticPlayerController.Position.text = player.Position;
                tacticPlayerController.Stats.text = "Passing: " + player.Passing.ToString() +
                    "\nTackling: " + player.Tackling.ToString() +
                    "\nShooting: " + player.Shooting.ToString() +
                    "\nInterceptions: " + player.Interception.ToString() +
                    "\nVision: " + player.Vision.ToString();


                newPlayerDisplay.transform.SetParent(PreviewAwayLineUpParent.transform);

            }
        }
    }
    public void CloseMatchPreview()
    {
        PreviewPanel.SetActive(false);
    }

    public void OpenYouthSquad()
    {

        YouthSquadPanel.SetActive(true);
        foreach (Transform child in YouthSquadParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (OutfieldPlayer player in WorldController.current.ChosenTeam.YouthTeam)
        {
            GameObject youthDisplay = Instantiate(YouthSquadPrefab) as GameObject;
            youthDisplay.name = player.Name;
            youthPrefabController = youthDisplay.GetComponent<YouthViewPrefabController>();
            youthPrefabController.Name.text = player.Name;
            youthPrefabController.Position.text = player.GetPositionShortForm(player.Position);
            youthPrefabController.Passing.text = player.Passing.ToString();
            youthPrefabController.Tackling.text = player.Tackling.ToString();
            youthPrefabController.Shooting.text = player.Shooting.ToString();
            youthPrefabController.Interceptions.text = player.Interception.ToString();
            youthPrefabController.Vision.text = player.Vision.ToString();
            youthPrefabController.Promote.onClick.AddListener(() => PromotePlayer(player));
            youthDisplay.transform.SetParent(YouthSquadParent.transform);
			youthDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
		}
    }
    public void CloseYouthSquad()
    {
        YouthSquadPanel.SetActive(false);
    }
    public void PromotePlayer(OutfieldPlayer player)
    {
        if (player.Position == "Defender")
        {
            player.club.Defenders.Add(player);
        }
        else if (player.Position == "Midfielder")
        {
            player.club.Midfielders.Add(player);

        }
        else { player.club.Forwards.Add(player); }

        player.club.YouthTeam.Remove(player);
        OpenYouthSquad();

    }

    public void OpenTransferMarket()
    {
        TransferMarketPanel.SetActive(true);
        foreach (Transform child in TransferMarketParent.transform)
        {
            Destroy(child.gameObject);
        }
        List<OutfieldPlayer> players = new List<OutfieldPlayer>();

        if (WorldController.current.TransferListedPlayers.Count > 0)
        {
            players = WorldController.current.TransferListedPlayers;
        }
        else
        {
            players = CreateTransferListedPlayers();
            WorldController.current.TransferListedPlayers = players;
        }


        foreach (OutfieldPlayer player in players)
        {
            GameObject playerDisplay = Instantiate(TransferViewPrefab) as GameObject;
            playerDisplay.name = player.Name;
            transferPrefabController = playerDisplay.GetComponent<TransferViewPrefabController>();
            transferPrefabController.Name.text = player.Name;
            transferPrefabController.Age.text = player.Age.ToString();
            transferPrefabController.Position.text = player.GetPositionShortForm(player.Position);
            transferPrefabController.Passing.text = player.Passing.ToString();
            transferPrefabController.Tackling.text = player.Tackling.ToString();
            transferPrefabController.Shooting.text = player.Shooting.ToString();
            transferPrefabController.Interceptions.text = player.Shooting.ToString();
            transferPrefabController.Vision.text = player.Vision.ToString();
            transferPrefabController.Value.text = "£" + player.Cost.ToString() + "m";
            transferPrefabController.Buy.onClick.AddListener(() => BuyPlayer(player));
            playerDisplay.transform.SetParent(TransferMarketParent.transform);
			playerDisplay.transform.localScale = new Vector3(1f, 1f, 1f);

			if (player.Cost > WorldController.current.ChosenTeam.Budget)
            {
                transferPrefabController.Buy.interactable = false;
            }
        }

    }

    public List<OutfieldPlayer> CreateTransferListedPlayers()
    {
        List<OutfieldPlayer> players = new List<OutfieldPlayer>();

        for (int i = 0; i < 20; i++)
        {
            players.Add(new OutfieldPlayer(GetRandomPosition(), UnityEngine.Random.Range(500, 1000), null, false));
        }

        return players;
    }

    string GetRandomPosition()
    {
        int rand = UnityEngine.Random.Range(1, 4);

        if (rand == 1) { return "Defender"; }
        else if (rand == 2) { return "Midfielder"; }
        else { return "Forward"; }
    }

    public void BuyPlayer(OutfieldPlayer player)
    {
        WorldController.current.TransferListedPlayers.Remove(player);
        player.club = WorldController.current.ChosenTeam;
        if (player.Position == "Defender") { WorldController.current.ChosenTeam.Defenders.Add(player); }
        else if (player.Position == "Midfielder") { WorldController.current.ChosenTeam.Midfielders.Add(player); }
        else { WorldController.current.ChosenTeam.Forwards.Add(player); }

        WorldController.current.ChosenTeam.Budget -= player.Cost;
        BudgetGameObject.GetComponentInChildren<Text>().text = "Budget: £" + WorldController.current.ChosenTeam.Budget +
            "m";

        OpenTransferMarket();
    }

    void SellPlayer(OutfieldPlayer player)
    {
        player.club = WorldController.current.ChosenTeam;
        if (player.Position == "Defender") { WorldController.current.ChosenTeam.Defenders.Remove(player); }
        else if (player.Position == "Midfielder") { WorldController.current.ChosenTeam.Midfielders.Remove(player); }
        else { WorldController.current.ChosenTeam.Forwards.Remove(player); }

        WorldController.current.ChosenTeam.Budget += player.Cost;
        BudgetGameObject.GetComponentInChildren<Text>().text = "Budget: £" + WorldController.current.ChosenTeam.Budget +
            "m";

        OpenSquad();

    }

    public void CloseTransferMarket()
    {
        TransferMarketPanel.SetActive(false);
    }

    void OpenConfirmBox(string text)
    {
        ConfirmBoxText.text = text;
        ConfirmBox.SetActive(true);
    }

    public void CancelConfirmBox()
    {
        ConfirmBox.SetActive(false);

    }

    public void OpenOKBox(string text)
    {
        OKBox.SetActive(true);
        OKBoxText.GetComponent<Text>().text = text;
    }
    public void CloseOKBox()
    {
        OKBox.SetActive(false);
    }

    //      FW FW FW
    //AML   AMC AMC AMC AMR
    //ML MC MC MC MC MC MR
    //WBL   DM DM DM    WBR
    //DL CB CB CB CB CB DR
    //          GK

}
