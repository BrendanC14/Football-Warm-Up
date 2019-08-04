using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameMenuController : MonoBehaviour
{

    public GameObject LeagueTablePanel;
    public GameObject TeamLeagueTablePrefab;
    public GameObject TeamLeagueParent;
    public GameObject ResultsPanel;
    public GameObject ResultViewPrefab;
    public GameObject ResultViewParent;

    ResultVuewPrefabController resultViewController;
    TeamLeaguePrefabController teamLeageController;

    // Start is called before the first frame update
    void Start()
    {
        
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
        SimulateFakeResults();
    }

    void SimulateFakeResults()
    {
        CreateFakeMatch(0, 10);
        CreateFakeMatch(1, 11);
        CreateFakeMatch(2, 12);
        CreateFakeMatch(3, 13);
        CreateFakeMatch(4, 14);
        CreateFakeMatch(5, 15);
        CreateFakeMatch(6, 16);
        CreateFakeMatch(7, 17);
        CreateFakeMatch(8, 18);
        CreateFakeMatch(9, 19);
    }
    void CreateFakeMatch(int HomeID, int AwayId)
    {
        Match m = new Match(HomeID, AwayId);
        WorldController.current.LeagueResults.Add(m);
        WorldController.current.Clubs[HomeID].UpdateLeagueStats(m);
        WorldController.current.Clubs[AwayId].UpdateLeagueStats(m);
    }

    public void OpenResults()
    {
        ResultsPanel.SetActive(true);
        foreach (Match m in WorldController.current.LeagueResults)
        {
            GameObject resultDisplay = Instantiate(ResultViewPrefab) as GameObject;
            resultDisplay.name = m.HomeID.ToString() + "V" + m.AwayID.ToString();
            resultViewController = resultDisplay.GetComponent<ResultVuewPrefabController>();
            resultViewController.HomeTeam.text = WorldController.current.Clubs[m.HomeID].Name;
            resultViewController.HomeScore.text = m.HomeScore.ToString();
            resultViewController.AwayScore.text = m.AwayScore.ToString();
            resultViewController.AwayTeam.text = WorldController.current.Clubs[m.AwayID].Name;
            resultDisplay.transform.SetParent(ResultViewParent.transform);
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
}
