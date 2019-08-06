﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : IComparable<Club>
{
    public string Name;
    public int ID;
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

    public List<Fixture> Fixtures;

    public Club(string name, int index)
    {
        Goalies = new List<Goalkeeper>();
        Defenders = new List<OutfieldPlayer>();
        Midfielders = new List<OutfieldPlayer>();
        Forwards = new List<OutfieldPlayer>();
        Fixtures = new List<Fixture>();
        Name = name;
        ID = index;

        for (int i = 0; i < 3; i++)
        {
            Goalies.Add(new Goalkeeper());
        }
        for (int i = 0; i < 8; i++)
        {
            Defenders.Add(new OutfieldPlayer("Defender"));
        }
        for (int i = 0; i < 8; i++)
        {
            Midfielders.Add(new OutfieldPlayer("Midfielder"));
        }
        for (int i = 0; i < 6; i++)
        {
            Forwards.Add(new OutfieldPlayer("Forward"));
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
