using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : IComparable<Club>
{
    public string Name;
    public int ID;
    public double Balance;
    public List<Goalkeeper> Goalies;
    public List<OutfieldPlayer> Defenders;
    public List<OutfieldPlayer> Midfielders;
    public List<OutfieldPlayer> Forwards;
    public int AvePassing;
    public int AveShooting;
    public int AveTackling;
    public int AveInterception;
    public int AveVision;

    public int GamesPlayed;
    public int GoalsScored;
    public int GoalsConceded;
    public int GoalDifference;
    public int Wins;
    public int Draws;
    public int Losses;
    public int Points;

    public string Tactic;
    public string PassingStyle;
    public string TacklingStyle;
    public string ShootingStyle;
    public Goalkeeper Goalie;
    public List<OutfieldPlayer> FirstTeam;
    public List<OutfieldPlayer> YouthTeam;
    public Dictionary<string, OutfieldPlayer> FirstTeamGOKey;

    public List<Match> Results;
    public List<Fixture> Fixtures;

    public int Budget;

    public Club(string name, int index, double balance, int budget)
    {
        Goalies = new List<Goalkeeper>();
        Defenders = new List<OutfieldPlayer>();
        Midfielders = new List<OutfieldPlayer>();
        Forwards = new List<OutfieldPlayer>();
        Results = new List<Match>();
        Fixtures = new List<Fixture>();
        FirstTeam = new List<OutfieldPlayer>();
        FirstTeamGOKey = new Dictionary<string, OutfieldPlayer>();
        YouthTeam = new List<OutfieldPlayer>();
        Name = name;
        ID = index;
        Balance = balance;
        Budget = budget;

        int random = UnityEngine.Random.Range(0, 6);
        if (random == 1) { Tactic = "442"; }
        else if (random == 2) { Tactic = "433"; }
        else if (random == 3) { Tactic = "41212"; }
        else if (random == 4) { Tactic = "4231"; }
        else if (random == 5) { Tactic = "32212"; }
        else { Tactic = "33211"; }
        random = UnityEngine.Random.Range(0, 3);
        if (random == 0) { PassingStyle = "Mixed"; }
        else if (random == 1) { PassingStyle = "Short"; }
        else { PassingStyle = "Long"; }

        random = UnityEngine.Random.Range(0, 3);

        if (random == 0) { TacklingStyle = "Mixed"; }
        else if (random == 1) { TacklingStyle = "Soft"; }
        else { TacklingStyle = "Hard"; }

        random = UnityEngine.Random.Range(0, 3);
        if (random == 0) { ShootingStyle = "Mixed"; }
        else if (random == 1) { ShootingStyle = "Short"; }
        else { ShootingStyle = "Long"; }



        for (int i = 0; i < 3; i++)
        {
            Goalies.Add(new Goalkeeper(balance, this));
        }
        for (int i = 0; i < 8; i++)
        {
            Defenders.Add(new OutfieldPlayer("Defender", balance, this, false));
        }
        for (int i = 0; i < 8; i++)
        {
            Midfielders.Add(new OutfieldPlayer("Midfielder", balance, this, false));
        }
        for (int i = 0; i < 6; i++)
        {
            Forwards.Add(new OutfieldPlayer("Forward", balance, this, false));
        }

        SetFirstTeamSquad();

        for (int i = 0; i < 3; i++)
        {
            YouthTeam.Add(new OutfieldPlayer("Defender", balance, this, true));
        }
        for (int i = 0; i < 3; i++)
        {
            YouthTeam.Add(new OutfieldPlayer("Midfielder", balance,this, true));
        }
        for (int i = 0; i < 2; i++)
        {
            YouthTeam.Add(new OutfieldPlayer("Forward", balance, this, true));
        }

        int PassingCount = 0;
        int ShootingCount = 0;
        int TacklingCount = 0;
        int InterceptionCount = 0;
        int VisionCount = 0;
        int PlayerCount = 0;

        foreach (OutfieldPlayer player in Defenders)
        {
            PlayerCount++;
            PassingCount += player.Passing;
            ShootingCount += player.Shooting;
            TacklingCount += player.Tackling;
            InterceptionCount += player.Interception;
            VisionCount += player.Vision;
        }
        foreach (OutfieldPlayer player in Midfielders)
        {
            PlayerCount++;
            PassingCount += player.Passing;
            ShootingCount += player.Shooting;
            TacklingCount += player.Tackling;
            InterceptionCount += player.Interception;
            VisionCount += player.Vision;
        }
        foreach (OutfieldPlayer player in Forwards)
        {
            PlayerCount++;
            PassingCount += player.Passing;
            ShootingCount += player.Shooting;
            TacklingCount += player.Tackling;
            InterceptionCount += player.Interception;
            VisionCount += player.Vision;
        }

        AvePassing = PassingCount / PlayerCount;
        AveShooting = ShootingCount / PlayerCount;
        AveTackling = TacklingCount / PlayerCount;
        AveInterception = InterceptionCount / PlayerCount;
        AveVision = VisionCount / PlayerCount;
    }

    void SetFirstTeamSquad()
    {
        Goalie = Goalies[UnityEngine.Random.Range(0, Goalies.Count - 1)];
        int random;

        if (Tactic == "442" || Tactic == "41212")
        {
            List<OutfieldPlayer> copyDefenders = new List<OutfieldPlayer>(Defenders);
            for (int i = 0; i < 4; i++)
            {
                random = UnityEngine.Random.Range(0, copyDefenders.Count - 1);
                FirstTeam.Add(copyDefenders[random]);
                copyDefenders.RemoveAt(random);
            }
            List<OutfieldPlayer> copyMidfielders = new List<OutfieldPlayer>(Midfielders);
            for (int i = 0; i < 4; i++)
            {
                random = UnityEngine.Random.Range(0, copyMidfielders.Count - 1);
                FirstTeam.Add(copyMidfielders[random]);
                copyMidfielders.RemoveAt(random);
            }
            List<OutfieldPlayer> copyForwards = new List<OutfieldPlayer>(Forwards);
            for (int i = 0; i < 2; i++)
            {
                random = UnityEngine.Random.Range(0, copyForwards.Count - 1);
                FirstTeam.Add(copyForwards[random]);
                copyForwards.RemoveAt(random);
            }
        }
        else if (Tactic == "433")
        {
            List<OutfieldPlayer> copyDefenders = new List<OutfieldPlayer>(Defenders);
            for (int i = 0; i < 4; i++)
            {
                random = UnityEngine.Random.Range(0, copyDefenders.Count - 1);
                FirstTeam.Add(copyDefenders[random]);
                copyDefenders.RemoveAt(random);
            }
            List<OutfieldPlayer> copyMidfielders = new List<OutfieldPlayer>(Midfielders);
            for (int i = 0; i < 3; i++)
            {
                random = UnityEngine.Random.Range(0, copyMidfielders.Count - 1);
                FirstTeam.Add(copyMidfielders[random]);
                copyMidfielders.RemoveAt(random);
            }
            List<OutfieldPlayer> copyForwards = new List<OutfieldPlayer>(Forwards);
            for (int i = 0; i < 3; i++)
            {
                random = UnityEngine.Random.Range(0, copyForwards.Count - 1);
                FirstTeam.Add(copyForwards[random]);
                copyForwards.RemoveAt(random);
            }
        }
        else if (Tactic == "4231")
        {
            List<OutfieldPlayer> copyDefenders = new List<OutfieldPlayer>(Defenders);
            for (int i = 0; i < 4; i++)
            {
                random = UnityEngine.Random.Range(0, copyDefenders.Count - 1);
                FirstTeam.Add(copyDefenders[random]);
                copyDefenders.RemoveAt(random);
            }
            List<OutfieldPlayer> copyMidfielders = new List<OutfieldPlayer>(Midfielders);
            for (int i = 0; i < 5; i++)
            {
                random = UnityEngine.Random.Range(0, copyMidfielders.Count - 1);
                FirstTeam.Add(copyMidfielders[random]);
                copyMidfielders.RemoveAt(random);
            }
            List<OutfieldPlayer> copyForwards = new List<OutfieldPlayer>(Forwards);
            for (int i = 0; i < 1; i++)
            {
                random = UnityEngine.Random.Range(0, copyForwards.Count - 1);
                FirstTeam.Add(copyForwards[random]);
                copyForwards.RemoveAt(random);
            }
        }
        else if (Tactic == "32212")
        {
            List<OutfieldPlayer> copyDefenders = new List<OutfieldPlayer>(Defenders);
            for (int i = 0; i < 5; i++)
            {
                random = UnityEngine.Random.Range(0, copyDefenders.Count - 1);
                FirstTeam.Add(copyDefenders[random]);
                copyDefenders.RemoveAt(random);
            }
            List<OutfieldPlayer> copyMidfielders = new List<OutfieldPlayer>(Midfielders);
            for (int i = 0; i < 3; i++)
            {
                random = UnityEngine.Random.Range(0, copyMidfielders.Count - 1);
                FirstTeam.Add(copyMidfielders[random]);
                copyMidfielders.RemoveAt(random);
            }
            List<OutfieldPlayer> copyForwards = new List<OutfieldPlayer>(Forwards);
            for (int i = 0; i < 2; i++)
            {
                random = UnityEngine.Random.Range(0, copyForwards.Count - 1);
                FirstTeam.Add(copyForwards[random]);
                copyForwards.RemoveAt(random);
            }
        }
        else if (Tactic == "33211")
        {
            List<OutfieldPlayer> copyDefenders = new List<OutfieldPlayer>(Defenders);
            for (int i = 0; i < 5; i++)
            {
                random = UnityEngine.Random.Range(0, copyDefenders.Count - 1);
                FirstTeam.Add(copyDefenders[random]);
                copyDefenders.RemoveAt(random);
            }
            List<OutfieldPlayer> copyMidfielders = new List<OutfieldPlayer>(Midfielders);
            for (int i = 0; i < 4; i++)
            {
                random = UnityEngine.Random.Range(0, copyMidfielders.Count - 1);
                FirstTeam.Add(copyMidfielders[random]);
                copyMidfielders.RemoveAt(random);
            }
            List<OutfieldPlayer> copyForwards = new List<OutfieldPlayer>(Forwards);
            for (int i = 0; i < 1; i++)
            {
                random = UnityEngine.Random.Range(0, copyForwards.Count - 1);
                FirstTeam.Add(copyForwards[random]); 
                copyForwards.RemoveAt(random);
            }
        }



    }
    public void UpdateLeagueStats(Match m)
    { 
        GamesPlayed++;
        if (ID == m.HomeID)
        {
            GoalsScored += m.HomeScore;
            GoalsConceded += m.AwayScore;
            GoalDifference = GoalsScored - GoalsConceded;
            if (m.result == Result.HomeWin)
            {
                Wins++;
            }
            else if (m.result == Result.Draw)
            {
                Draws++;
            }
            else
            {
                Losses++;
            }
        }
        if (ID == m.AwayID)
        {
            GoalsScored += m.AwayScore;
            GoalsConceded += m.HomeScore;
            GoalDifference = GoalsScored - GoalsConceded;
            if (m.result == Result.AwayWin)
            {
                Wins++;
            }
            else if (m.result == Result.Draw)
            {
                Draws++;
            }
            else
            {
                Losses++;
            }
        }

        UpdatePoints();
    }

    public void EndSeason()
    {
        GoalsScored = 0;
        GoalsConceded = 0;
        GoalDifference = 0;
        Wins = 0;
        Draws = 0;
        Losses = 0;
        Points = 0;
        GamesPlayed = 0;
        Results = new List<Match>();
        Fixtures = new List<Fixture>();
    }
    void UpdatePoints()
    {
        Points = (Wins * 3) + Draws;
    }



    public int CompareTo(Club other)
    {
        if (other.Points != Points) { return other.Points.CompareTo(Points); }
        else if (other.GoalDifference != GoalDifference) { return other.GoalDifference.CompareTo(GoalDifference); }
        else if (other.GoalsScored != GoalsScored) { return other.GoalsScored.CompareTo(GoalsScored); }
        else { return other.Wins.CompareTo(Wins); }
            
    }
}
