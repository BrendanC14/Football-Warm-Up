using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Result { HomeWin, Draw, AwayWin }

public class Match : IComparable<Match>
{
    public int HomeID;
    public int AwayID;
    public int HomeScore;
    public int AwayScore;
    public Result result;
    public int GameWeek;

    Club HomeTeam;
    Club AwayTeam;

    string HomePassingStyle;
    string HomeTacklingStyle;
    string HomeShootingStyle;
    int HomePassingScore;
    int HomeShootingScore;
    string AwayPassingStyle;
    string AwayShootingStyle;
    string AwayTacklingStyle;
    int AwayPassingScore;
    int AwayShootingScore;

    int HomeYellowCards;
    int HomeRedCards;
    int AwayYellowCards;
    int AwayRedCards;

    public string MatchReport;

    public List<OutfieldPlayer> HomeScorers;
    public List<OutfieldPlayer> AwayScorers;
    public List<OutfieldPlayer> Injured;

    int HomeSuccessfulPassing;
    int AwaySuccessfulPassing;
    int HomeShotsWide;
    int AwayShotsWide;




    public Match(int home, int away, int gw)
    {
        HomeScorers = new List<OutfieldPlayer>();
        AwayScorers = new List<OutfieldPlayer>();
        HomeID = home;
        AwayID = away;
        HomeTeam = WorldController.current.Clubs[home];
        AwayTeam = WorldController.current.Clubs[away];
        Injured = new List<OutfieldPlayer>();

        int HomePassingCount = 0;
        int HomeShootingCount = 0;

        HomePassingScore += HomeTeam.Goalie.Passing;
        HomePassingCount++;

        foreach ( OutfieldPlayer player in HomeTeam.FirstTeam)
        {
            if (player.Position == "Defender")
            {
                HomePassingScore += player.Passing;
                HomePassingCount++;
            }
            else if (player.Position == "Midfielder")
            {
                HomePassingScore += player.Passing;
                HomePassingCount++;
                HomeShootingScore += player.Shooting;
                HomeShootingCount++;

            }
            else
            {
                HomePassingScore += player.Passing;
                HomePassingCount++;
                HomeShootingScore += player.Shooting;
                HomeShootingCount++;

            }
        }

        HomePassingScore = HomePassingScore / HomePassingCount;
        HomeShootingScore = HomeShootingScore / HomeShootingCount;

        int AwayPassingCount = 0;
        int AwayShootingCount = 0;

        AwayPassingScore += AwayTeam.Goalie.Passing;
        AwayPassingCount++;

        foreach (OutfieldPlayer player in AwayTeam.FirstTeam)
        {
            if (player.Position == "Defender")
            {
                AwayPassingScore += player.Passing;
                AwayPassingCount++;
            }
            else if (player.Position == "Midfielder")
            {
                AwayPassingScore += player.Passing;
                AwayPassingCount++;
                AwayShootingScore += player.Shooting;
                AwayShootingCount++;

            }
            else
            {
                AwayPassingScore += player.Passing;
                AwayPassingCount++;
                AwayShootingScore += player.Shooting;
                AwayShootingCount++;

            }
        }
        AwayPassingScore = AwayPassingScore / AwayPassingCount;
        AwayShootingScore = AwayShootingScore / AwayShootingCount;


        HomePassingStyle = HomeTeam.PassingStyle;
        HomeTacklingStyle = HomeTeam.TacklingStyle;
        HomeShootingStyle = HomeTeam.ShootingStyle;
        AwayPassingStyle = AwayTeam.PassingStyle;
        AwayTacklingStyle = AwayTeam.TacklingStyle;
        AwayShootingStyle = AwayTeam.ShootingStyle;


        for (int i = 0; i < 7; i++)
        {
            if (WonPassingCompetition(true))
            {
                HomeSuccessfulPassing++;
                if (WonScoringCompetitiom(true))
                {
                    HomeScore++;
                    OutfieldPlayer scorer = ChooseGoalscorer(true);
                    HomeScorers.Add(scorer);
                    if (!WorldController.current.Goalscorers.Contains(scorer))
                    {
                        WorldController.current.Goalscorers.Add(scorer);
                    }
                        

                    
                }
            }

            if (WonPassingCompetition(false))
            {
                AwaySuccessfulPassing++;
                if (WonScoringCompetitiom(false))
                {
                    AwayScore++;
                    OutfieldPlayer scorer = ChooseGoalscorer(false);
                    AwayScorers.Add(scorer);
                    if (!WorldController.current.Goalscorers.Contains(scorer))
                    {
                        WorldController.current.Goalscorers.Add(scorer);
                    }
                }
            }
        }

        foreach (OutfieldPlayer player in HomeScorers)
        {
            player.Goals++;
        }
        foreach (OutfieldPlayer player in AwayScorers)
        {
            player.Goals++;
        }
        result = GetResult();
        MatchReport = GenerateMatchReport();
        GameWeek = gw;
    }

    bool WonPassingCompetition(bool homeAttacking)
    {
        if (homeAttacking)
        {
            int HomeRandomNumber = UnityEngine.Random.Range(1, 22);
            int AwayRandomNumber = UnityEngine.Random.Range(1, 20);

            HomeRandomNumber += HomePassingScore;
            int awayDefendingScore = 0;
            int awayDefendingCount = 0;
            if (HomePassingStyle == "Short")
            {
                foreach (OutfieldPlayer player in AwayTeam.FirstTeam)
                {
                    if (player.Position == "Defender")
                    {
                        awayDefendingScore += player.Tackling;
                        awayDefendingCount++;
                    }
                }
                awayDefendingScore = awayDefendingScore / awayDefendingCount;
            }
            else if (HomePassingStyle == "Mixed")
            {
                foreach (OutfieldPlayer player in AwayTeam.FirstTeam)
                {
                    if (player.Position == "Defender")
                    {
                        awayDefendingScore += player.Interception;
                        awayDefendingCount++;
                    }
                }
                awayDefendingScore = awayDefendingScore / awayDefendingCount;
            }
            else
            {
                foreach (OutfieldPlayer player in AwayTeam.FirstTeam)
                {
                    if (player.Position == "Defender" || player.Position == "Midfielder")
                    {
                        awayDefendingScore += player.Vision;
                        awayDefendingCount++;
                    }
                }
                awayDefendingScore = awayDefendingScore / awayDefendingCount;

            }
            AwayRandomNumber += awayDefendingScore;

            int yellowdifference = 0;
            int reddifference = 0;
            if (AwayTacklingStyle == "Soft")
            {
                AwayRandomNumber += 4;
                yellowdifference = 15;
                reddifference = 24;
            }
            else if (AwayTacklingStyle == "Mixed")
            {
                AwayRandomNumber += 5;
                yellowdifference = 12;
                reddifference = 21;
            }
            else
            {
                AwayRandomNumber += 6;
                yellowdifference = 9;
                reddifference = 20;
            }

            if (HomeRandomNumber - AwayRandomNumber - (AwayRedCards * 3) >= 0)
            {
                if (HomeRandomNumber - AwayRandomNumber > reddifference)
                {
                    AwayRedCards++;
                    if (UnityEngine.Random.Range(0, 5) > 2)
                    {
                        int randomPlayer = UnityEngine.Random.Range(0, HomeTeam.FirstTeam.Count - 1);
                        int randomWeeks = UnityEngine.Random.Range(2, 9);

                        HomeTeam.FirstTeam[randomPlayer].WeeksInjured += randomWeeks;
                        Injured.Add(HomeTeam.FirstTeam[randomPlayer]);
                    }
                }
                else if (HomeRandomNumber - AwayRandomNumber > yellowdifference)
                {
                    AwayYellowCards++;
                    if (UnityEngine.Random.Range(0, 5) > 3)
                    {
                        int randomPlayer = UnityEngine.Random.Range(0, HomeTeam.FirstTeam.Count - 1);
                        int randomWeeks = UnityEngine.Random.Range(2, 9);

                        HomeTeam.FirstTeam[randomPlayer].WeeksInjured += randomWeeks;
                        Injured.Add(HomeTeam.FirstTeam[randomPlayer]);
                    }
                    if (AwayYellowCards == 2)
                    {
                        AwayRedCards++;
                        AwayYellowCards = 0;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {

            int HomeRandomNumber = UnityEngine.Random.Range(1, 22);
            int AwayRandomNumber = UnityEngine.Random.Range(1, 20);

            AwayRandomNumber += AwayPassingScore;
            int homeDefendingScore = 0;
            int HomeDefendingCount = 0;
            if (AwayPassingStyle == "Short")
            {
                foreach (OutfieldPlayer player in HomeTeam.FirstTeam)
                {
                    if (player.Position == "Defender")
                    {
                        homeDefendingScore += player.Tackling;
                        HomeDefendingCount++;
                    }
                }
                homeDefendingScore = homeDefendingScore / HomeDefendingCount;

            }
            else if (AwayPassingStyle == "Mixed")
            {
                foreach (OutfieldPlayer player in HomeTeam.FirstTeam)
                {
                    if (player.Position == "Defender")
                    {
                        homeDefendingScore += player.Interception;
                        HomeDefendingCount++;
                    }
                }
                homeDefendingScore = homeDefendingScore / HomeDefendingCount;
            }
            else
            {
                foreach (OutfieldPlayer player in HomeTeam.FirstTeam)
                {
                    if (player.Position == "Defender" || player.Position == "Midfielder")
                    {
                        homeDefendingScore += player.Vision;
                        HomeDefendingCount++;
                    }
                }
                homeDefendingScore = homeDefendingScore / HomeDefendingCount;


            }
            HomeRandomNumber += homeDefendingScore;

            int yellowdifference = 0;
            int reddifference = 0;
            if (HomeTacklingStyle == "Soft")
            {
                HomeRandomNumber += 3;
                yellowdifference = 15;
                reddifference = 24;
            }
            else if (HomeTacklingStyle == "Mixed")
            {
                HomeRandomNumber += 4;
                yellowdifference = 12;
                reddifference = 21;
            }
            else
            {
                HomeRandomNumber += 5;
                yellowdifference = 9;
                reddifference = 15;
            }

            if (AwayRandomNumber - HomeRandomNumber - (HomeRedCards * 3) > 0)
            {
                if (AwayRandomNumber - HomeRandomNumber > reddifference)
                {
                    HomeRedCards++;
                    if (UnityEngine.Random.Range(0, 5) > 2)
                    {
                        int randomPlayer = UnityEngine.Random.Range(0, AwayTeam.FirstTeam.Count - 1);
                        int randomWeeks = UnityEngine.Random.Range(2, 9);

                        AwayTeam.FirstTeam[randomPlayer].WeeksInjured += randomWeeks;
                        Injured.Add(AwayTeam.FirstTeam[randomPlayer]);
                    }
                }
                else if (AwayRandomNumber - HomeRandomNumber > yellowdifference)
                {
                    HomeYellowCards++;
                    if (UnityEngine.Random.Range(0, 5) > 3)
                    {
                        int randomPlayer = UnityEngine.Random.Range(0, AwayTeam.FirstTeam.Count - 1);
                        int randomWeeks = UnityEngine.Random.Range(2, 9);

                        AwayTeam.FirstTeam[randomPlayer].WeeksInjured += randomWeeks;
                        Injured.Add(AwayTeam.FirstTeam[randomPlayer]);
                    }
                    if (HomeYellowCards == 2)
                    {
                        HomeRedCards++;
                        HomeYellowCards = 0;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    bool WonScoringCompetitiom(bool homeAttacking)
    {
        if (homeAttacking)
        {
            int HomeRandomNumber = UnityEngine.Random.Range(1, 22);
            int AwayRandomNumber = UnityEngine.Random.Range(1, 20);

            HomeRandomNumber += HomeShootingScore;
            if (HomeRandomNumber < 20)
            {
                HomeShotsWide++;
                return false;
            }
            int goalkeepingNumber = 0;
            if (HomeShootingStyle == "Short")
            {
                
                goalkeepingNumber += (AwayTeam.Goalie.Reflexes + AwayTeam.Goalie.OneOnOnes) / 2;
            }
            else if (HomeShootingStyle == "Mixed")
            {
                goalkeepingNumber += AwayTeam.Goalie.Diving;

            }
            else
            {

                goalkeepingNumber += AwayTeam.Goalie.Handling;
            }

            AwayRandomNumber += goalkeepingNumber;
            if (HomeRandomNumber >= AwayRandomNumber)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {

            int HomeRandomNumber = UnityEngine.Random.Range(1, 22);
            int AwayRandomNumber = UnityEngine.Random.Range(1, 20);

            AwayRandomNumber += AwayShootingScore;
            if (AwayRandomNumber < 20)
            {
                AwayShotsWide++;
                return false; 
            }
            int goalkeepingNumber = 0;
            if (AwayShootingStyle == "Short")
            {
                goalkeepingNumber += (HomeTeam.Goalie.Reflexes + HomeTeam.Goalie.OneOnOnes) / 2;
            }
            else if (AwayShootingStyle == "Mixed")
            {
                goalkeepingNumber += HomeTeam.Goalie.Diving;

            }
            else
            {

                goalkeepingNumber += HomeTeam.Goalie.Handling;
            }

            HomeRandomNumber += goalkeepingNumber;
            if (AwayRandomNumber >= HomeRandomNumber)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    OutfieldPlayer ChooseGoalscorer(bool homeAttacking)
    {
        if (homeAttacking)
        {
            int totalShootingNumber = 0;
            foreach(OutfieldPlayer player in HomeTeam.FirstTeam)
            {
                if (player.Position == "Midfielder")
                {
                    totalShootingNumber += player.Shooting;
                }
                else if (player.Position == "Forward")
                {
                    totalShootingNumber += player.Shooting * 2;

                }
            }

            int random = UnityEngine.Random.Range(1, totalShootingNumber);
            int accumulatedNumber = 0;

            foreach (OutfieldPlayer player in HomeTeam.FirstTeam)
            {
                if (player.Position == "Midfielder")
                {
                    accumulatedNumber += player.Shooting;
                    if (random <= accumulatedNumber)
                    {
                        return player;
                    }
                }
                else if (player.Position == "Forward")
                {

                    accumulatedNumber += player.Shooting * 2;
                    if (random <= accumulatedNumber)
                    {
                        return player;
                    }
                }
            }
        }
        else
        {
            int totalShootingNumber = 0;
            foreach (OutfieldPlayer player in AwayTeam.FirstTeam)
            {
                if (player.Position == "Midfielder")
                {
                    totalShootingNumber += player.Shooting;
                }
                else if (player.Position == "Forward")
                {

                    totalShootingNumber += player.Shooting * 2;
                }
            }

            int random = UnityEngine.Random.Range(1, totalShootingNumber);
            int accumulatedNumber = 0;

            foreach (OutfieldPlayer player in AwayTeam.FirstTeam)
            {
                if (player.Position == "Midfielder")
                {
                    accumulatedNumber += player.Shooting;
                    if (random <= accumulatedNumber)
                    {
                        return player;
                    }
                }
                else if (player.Position == "Forward")
                {
                    accumulatedNumber += player.Shooting * 2;
                    if (random <= accumulatedNumber)
                    {
                        return player;
                    }
                }
            }
        }

        return null;
    }

    string GenerateMatchReport()
    {
        WorldController w = WorldController.current;
        Club HomeTeam = w.Clubs[HomeID];
        Club AwayTeam = w.Clubs[AwayID];
        string report = "";
        if (HomeRedCards > 0 && AwayRedCards == 0)
        {
            if (HomeScore > AwayScore)
            {
                if (HomeRedCards == 1) { report += "Despite getting one red card, "; }
                else { report += "Despite getting " + HomeRedCards.ToString() + " red cards, "; }
                report += HomeTeam.Name + " have somehow managed to beat " +
                    AwayTeam.Name + " at home.";
            }
            else if (HomeScore == AwayScore)
            {
                if (HomeRedCards == 1) { report += "Despite getting one red card, "; }
                else { report += " Despite getting " + HomeRedCards.ToString() + " red cards, "; }
                report += HomeTeam.Name + " have managed to secure a point with a draw at home";
            }
            else if (HomeScore < AwayScore)
            {
                if (HomeRedCards == 1) { report += "A red card resigned "; }
                else { report += HomeRedCards.ToString() + " red cards resigned "; }
                report += HomeTeam.Name + " to a defeat at home";
            }
        }
        else if (HomeRedCards == 0 && AwayRedCards > 0)
        {
            if (AwayScore > HomeScore)
            {
                if (AwayRedCards == 1) { report += "Despite getting one red card, "; }
                else { report += "Despite getting " + AwayRedCards.ToString() + " red cards, "; }
                report += AwayTeam.Name + " have somehow managed to beat " +
                    HomeTeam.Name + " at home.";
            }
            else if (AwayScore == HomeScore)
            {
                if (AwayRedCards == 1) { report += "Despite getting one red card, "; }
                else { report += " Despite getting " + AwayRedCards.ToString() + "red cards, "; }
                report += AwayTeam.Name + " have managed to secure a point with a draw at home";
            }
            else if (AwayScore < HomeScore)
            {
                if (AwayRedCards == 1) { report += "A red card resigned "; }
                else { report += AwayRedCards.ToString() + " red cards resigned "; }
                report += AwayTeam.Name + " to a defeat at home";
            }

        }
        else if (HomeRedCards > 0 && AwayRedCards > 0)
        {
            if (HomeRedCards == 1 && AwayRedCards == 1)
            {
                report += "In a scrappy game where both teams picked up a red card each, ";
            }
            else if (HomeRedCards > 1 && AwayRedCards == 1)
            {
                report += "In a dirty game where " + HomeTeam.Name + " picked up " + HomeRedCards.ToString()
                    + " red cards, and " + AwayTeam.Name + " also got one, ";
            }
            else if (HomeRedCards > 1 && AwayRedCards > 1)
            {
                report += "In an aggresive game where " + HomeTeam.Name + " picked up " + HomeRedCards.ToString() +
                    " red cards, and " + AwayTeam.Name + " also getting " + AwayRedCards.ToString() +
                    " red cards, ";
            }
            else if (HomeRedCards == 1 && AwayRedCards > 1)
            {
                report += "In a dirty game where " + AwayTeam.Name + " picked up " + AwayRedCards.ToString()
                    + " red cards, and " + HomeTeam.Name + " also got one, ";

            }

            if (HomeScore > AwayScore)
            {
                report += HomeTeam.Name + " emerged victiorious.";
            }
            else if (HomeScore == AwayScore)
            {
                report += "neither team could best the other and finished in a draw";
            }
            else
            {
                report += AwayTeam.Name + " emerged victorious.";
            }
        }
        else
        {
            report += "In a clean game with no cards, ";
            if (HomeScore > AwayScore)
            {
                report += HomeTeam.Name + " secured a victory at home.";
            }
            else if (HomeScore == AwayScore)
            {
                report += "each team cancelled each other out to earn a point each.";
            }
            else
            {
                report += AwayTeam.Name + " secured a victory away from home.";
            }
        }





        report += "\n" + HomeTeam.Name + "'s report: ";


        if (HomeSuccessfulPassing >= 4)
        {
            if (HomePassingStyle == "Short")
            {
                report += HomeTeam.Name + "'s short passing caused all sorts of problems and " +
                    AwayTeam.Name + "'s defence couldn't get a tackle in. ";
            }
            else if (HomePassingStyle == "Mixed")
            {
                report += HomeTeam.Name + "'s mixture of passes left " + AwayTeam.Name +
                    "'s defence struggling to predict where the next pass would come from. ";
            }
            else
            {
                report += HomeTeam.Name + "'s long passes were too good for " + AwayTeam.Name +
                    " to intercept. ";
            }


        }
        else if (HomeSuccessfulPassing >= 2)
        {

            if (HomePassingStyle == "Short")
            {
                report += HomeTeam.Name + "'s short passing caused occasional problems, but " +
                    AwayTeam.Name + "'s defence still managed to get a few tackles in. ";
            }
            else if (HomePassingStyle == "Mixed")
            {
                report += HomeTeam.Name + "'s mixture of passes occasionaly caught " + AwayTeam.Name +
                    " off guard, but the defender often saw the ball first. ";
            }
            else
            {
                report += HomeTeam.Name + "'s long passes occasionally beat " + AwayTeam.Name +
                    "'s defence, but they were able to get a few interceptions in. ";
            }
        }
        else
        {

            if (HomePassingStyle == "Short")
            {
                report += AwayTeam.Name + "'s tackling was on form tonight as they managed to stifle " +
                    "the majority of " + HomeTeam.Name + "'s attacks.";
            }
            else if (AwayPassingStyle == "Mixed")
            {
                report += AwayTeam.Name + "'s vision was on display tonight as the variety of " +
                    HomeTeam.Name + "'s passes were handled relatively well";
            }
            else
            {
                report += AwayTeam.Name + " were in form tonight as they managed to intercept the majority of " +
                    HomeTeam.Name + "'s long passes";
            }
        }

        report += "\n";

        int HomeShotsOnTarget = HomeSuccessfulPassing - HomeShotsWide;

        if (HomeShotsOnTarget == 0)
        {
            report += HomeTeam.Name + " left their shooting boots at home as they failed to register a single shot on target. ";
        }
        else
        {
            if (HomeShotsOnTarget >= 4)
            {
                report += HomeTeam.Name + " enjoyed the ball today as it felt like every attack ended with a shot on target. ";
            }
            else if (HomeShotsOnTarget == HomeSuccessfulPassing)
            {
                report += "Whenever " + HomeTeam.Name + " got the ball they looked dangerous and seemed to hit the target often. ";
            }
            else
            {
                report += "An average performance in front of goal for " + HomeTeam.Name + ". ";
            }

            if (HomeScore == HomeShotsOnTarget)
            {
                if (HomeShootingStyle == "Short")
                {
                    report += "Working the ball into the box worked perfectly for " +
                        HomeTeam.Name + " as the " + AwayTeam.Name + " goalkeeper couldn't save any of the one-on-ones he faced. ";
                }
                else if (HomeShootingStyle == "Mixed")
                {
                    report += HomeTeam.Name + "'s mixed shooting often caught the keeper flat-footed. ";
                }
                else
                {
                    report += HomeTeam.Name + "'s long shots often caught the keeper off guard and questioned his handling. ";
                }
            }
            else if (HomeScore == 0)
            {
                if (HomeShootingStyle == "Short")
                {
                    report += "Working the ball in the box wasn't very successful for " + HomeTeam.Name +
                " as " + AwayTeam.Goalie.Name + " was too good one on one.";
                }
                else if (HomeShootingStyle == "Mixed")
                {
                    report += "The mixed shooting wasn't very successful for " + HomeTeam.Name +
                " as " + AwayTeam.Goalie.Name + " was too good in goal for " + AwayTeam.Name;
                }
                else
                {
                    report += "The long shots weren't very successful for " + HomeTeam.Name +
                " as " + AwayTeam.Goalie.Name + " was too good in goal for " + AwayTeam.Name;
                }

            }
            else
            {
                if (HomeShootingStyle == "Short")
                {
                    report += "Working the ball in the box occasionally tested the " +
                        AwayTeam.Name + " goalkeeper";
                }
                else if (HomeShootingStyle == "Mixed")
                {
                    report += "The mixture of shots occasionally tested the " +
                        AwayTeam.Name + " goalkeeper";
                }
                else
                {
                    report += "The long shots occasionally tested the " +
                        AwayTeam.Name + " goalkeeper";
                }
            }
        }




        report += "\n" + AwayTeam.Name + "'s report: ";


        if (AwaySuccessfulPassing >= 4)
        {
            if (AwayPassingStyle == "Short")
            {
                report += AwayTeam.Name + "'s short passing caused all sorts of problems and " +
                    HomeTeam.Name + "'s defence couldn't get a tackle in. ";
            }
            else if (AwayPassingStyle == "Mixed")
            {
                report += AwayTeam.Name + "'s mixture of passes left " + HomeTeam.Name +
                    "'s defence struggling to predict where the next pass would come from. ";
            }
            else
            {
                report += AwayTeam.Name + "'s long passes were too good for " + HomeTeam.Name +
                    " to intercept. ";
            }


        }
        else if (AwaySuccessfulPassing >= 2)
        {

            if (AwayPassingStyle == "Short")
            {
                report += AwayTeam.Name + "'s short passing caused occasional problems, but " +
                    HomeTeam.Name + "'s defence still managed to get a few tackles in. ";
            }
            else if (AwayPassingStyle == "Mixed")
            {
                report += AwayTeam.Name + "'s mixture of passes occasionaly caught " + HomeTeam.Name +
                    " off guard, but the defender often saw the ball first. ";
            }
            else
            {
                report += AwayTeam.Name + "'s long passes occasionally beat " + HomeTeam.Name +
                    "'s defence, but they were able to get a few interceptions in. ";
            }
        }
        else
        {

            if (AwayPassingStyle == "Short")
            {
                report += HomeTeam.Name + "'s tackling was on form tonight as they managed to stifle " +
                    "the majority of " + AwayTeam.Name + "'s attacks.";
            }
            else if (AwayPassingStyle == "Mixed")
            {
                report += HomeTeam.Name + "'s vision was on display tonight as the variety of " +
                    AwayTeam.Name + "'s passes were handled relatively well";
            }
            else
            {
                report += HomeTeam.Name + " were in form tonight as they managed to intercept the majority of " +
                    AwayTeam.Name + "'s long passes";
            }
        }

        report += "\n";

        int AwayShotsOnTarget = AwaySuccessfulPassing - AwayShotsWide;

        if (AwayShotsOnTarget == 0)
        {
            report += AwayTeam.Name + " left their shooting boots at home as they failed to register a single shot on target. ";
        }
        else
        {
            if (AwayShotsOnTarget >= 4)
            {
                report += AwayTeam.Name + " enjoyed the ball today as it felt like every attack ended with a shot on target. ";
            }
            else if (AwayShotsOnTarget == AwaySuccessfulPassing)
            {
                report += "Whenever " + AwayTeam.Name + " got the ball they looked dangerous and seemed to hit the target often. ";
            }
            else
            {
                report += "An average performance in front of goal for " + AwayTeam.Name + ". ";
            }

            if (AwayScore == AwayShotsOnTarget)
            {
                if (AwayShootingStyle == "Short")
                {
                    report += "Working the ball into the box worked perfectly for " +
                        AwayTeam.Name + " as the " + HomeTeam.Name + " goalkeeper couldn't save any of the one-on-ones he faced. ";
                }
                else if (AwayShootingStyle == "Mixed")
                {
                    report += AwayTeam.Name + "'s mixed shooting often caught the keeper flat-footed. ";
                }
                else
                {
                    report += AwayTeam.Name + "'s long shots often caught the keeper off guard and questioned his handling. ";
                }
            }
            else if (AwayScore == 0)
            {
                if (AwayShootingStyle == "Short")
                {
                    report += "Working the ball in the box wasn't very successful for " + AwayTeam.Name +
                " as " + HomeTeam.Goalie.Name + " was too good one on one.";
                }
                else if (AwayShootingStyle == "Mixed")
                {
                    report += "The mixed shooting wasn't very successful for " + AwayTeam.Name +
                " as " + HomeTeam.Goalie.Name + " was too good in goal for " + HomeTeam.Name;
                }
                else
                {
                    report += "The long shots weren't very successful for " + AwayTeam.Name +
                " as " + HomeTeam.Goalie.Name + " was too good in goal for " + HomeTeam.Name;
                }

            }
            else
            {
                if (AwayShootingStyle == "Short")
                {
                    report += "Working the ball in the box occasionally tested the " +
                        HomeTeam.Name + " goalkeeper";
                }
                else if (AwayShootingStyle == "Mixed")
                {
                    report += "The mixture of shots occasionally tested the " +
                        HomeTeam.Name + " goalkeeper";
                }
                else
                {
                    report += "The long shots occasionally tested the " +
                        HomeTeam.Name + " goalkeeper";
                }
            }
        }


        report += "\n";

        if (Injured.Count > 1)
        {
            report += Injured[0].Name;
            for (int i = 0; i < Injured.Count; i++)
            {
                if (i == Injured.Count - 1)
                {
                    report += " and " + Injured[i].Name;
                }
                else
                {
                    report += ", " + Injured[i].Name;
                }
            }
            report += " suffered injuries in today's game";
        }
        else if (Injured.Count == 1)
        {
            report += Injured[0].Name + " suffered an injury in today's game";
        }


        return report;
    }
    Result GetResult()
    {
        if (HomeScore == AwayScore)
        {
            return Result.Draw;
        }
        else if (HomeScore > AwayScore)
        {
            return Result.HomeWin;
        }
        else
        {
            return Result.AwayWin;
        }
    }

    public int CompareTo(Match other)
    {
        return GameWeek.CompareTo(other.GameWeek);

    }
}

//Mixed
//Passing vs Interceptions

//Long
//Passing vs Vision

//Forand 10. The Home team then adds their Passing score to the random number.
//Then, based on the Home team’s passing style, the Away team adds their random number to the appropriate score as described above.
//The defending team then gets a bonus based on their tackling style: //Stay On Feet: + 3  to the number.If difference -5 or less then yellow card.If difference -8 or less then red card.
//Mixed: + 4 to the number.If difference -4 or less then yellow card.If difference - 7 or less than red card.
//Get Stuck In: + 5 to the number.If difference -3 or less then yellow card.If difference -5 or less then red card.

//Every 2 yellow cards is a red card.
//3 red cards is a 3-0 forfeit.
//Each red card is -3 to every random number. (not taken into account for further cards, just passing challenges)

//If Home Team has the higher number, they’re through on goal.Next challenge determine if it’s a goal or not:  //Shooting Battles
//Short
//Shooting vs (One On Ones + Reflexes)

//Mixed
//Shooting vs Diving

//Long
//Shooting vs Handling