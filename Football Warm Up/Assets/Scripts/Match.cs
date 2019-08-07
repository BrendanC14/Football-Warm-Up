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



    public Match(int home, int away, int gw)
    {
        HomeID = home;
        AwayID = away;
        HomeTeam = WorldController.current.Clubs[home];
        AwayTeam = WorldController.current.Clubs[away];

        foreach (Goalkeeper goalie in HomeTeam.Goalies)
        {
            HomePassingScore += goalie.Passing;
        }
        foreach (OutfieldPlayer player in HomeTeam.Defenders)
        {
            HomePassingScore += player.Passing;
        }
        foreach (OutfieldPlayer player in HomeTeam.Midfielders)
        {
            HomePassingScore += player.Passing;
            HomeShootingScore += player.Shooting;
        }
        foreach (OutfieldPlayer player in HomeTeam.Forwards)
        {
            HomePassingScore += player.Passing;
            HomeShootingScore += player.Shooting;
        }

        HomePassingScore = HomePassingScore / 25;
        HomeShootingScore = HomeShootingScore / 25;

        foreach (Goalkeeper goalie in AwayTeam.Goalies)
        {
            AwayPassingScore += goalie.Passing;
        }
        foreach (OutfieldPlayer player in AwayTeam.Defenders)
        {
            AwayPassingScore += player.Passing;
        }
        foreach (OutfieldPlayer player in AwayTeam.Midfielders)
        {
            AwayPassingScore += player.Passing;
            AwayShootingScore += player.Shooting;
        }
        foreach (OutfieldPlayer player in AwayTeam.Forwards)
        {
            AwayPassingScore += player.Passing;
            AwayShootingScore += player.Shooting;
        }

        AwayPassingScore = AwayPassingScore / 25;
        AwayShootingScore = AwayShootingScore / 25;

        HomePassingStyle = HomeTeam.PassingStyle;
        HomeTacklingStyle = HomeTeam.TacklingStyle;
        HomeShootingStyle = HomeTeam.ShootingStyle;
        AwayPassingStyle = AwayTeam.PassingStyle;
        AwayTacklingStyle = AwayTeam.TacklingStyle;
        AwayShootingStyle = AwayTeam.ShootingStyle;


        for (int i = 0; i < 6; i++)
        {
            if (WonPassingCompetition(true))
            {
                if (WonScoringCompetitiom(true))
                {
                    HomeScore++;
                }
            }

            if (WonPassingCompetition(false))
            {
                if (WonScoringCompetitiom(false))
                {
                    AwayScore++;
                }
            }
        }


        result = GetResult();
        GameWeek = gw;
    }

    bool WonPassingCompetition(bool homeAttacking)
    {
        if (homeAttacking)
        {
            int HomeRandomNumber = UnityEngine.Random.Range(1, 10);
            int AwayRandomNumber = UnityEngine.Random.Range(1, 10);

            HomeRandomNumber += HomePassingScore;
            int awayDefendingScore = 0;
            if (HomePassingStyle == "Short")
            {
                foreach (OutfieldPlayer player in AwayTeam.Defenders)
                {
                    awayDefendingScore += player.Tackling;
                }
            }
            else if (HomePassingStyle == "Mixed")
            {
                foreach (OutfieldPlayer player in AwayTeam.Defenders)
                {
                    awayDefendingScore += player.Interception;
                }
            }
            else
            {
                foreach (OutfieldPlayer player in AwayTeam.Defenders)
                {
                    awayDefendingScore += player.Vision;
                }
                foreach (OutfieldPlayer player in AwayTeam.Midfielders)
                {
                    awayDefendingScore += player.Vision;
                }
                foreach (OutfieldPlayer player in AwayTeam.Forwards)
                {
                    awayDefendingScore += player.Vision;
                }

            }
            awayDefendingScore = awayDefendingScore / 22;
            AwayRandomNumber += awayDefendingScore;

            int yellowdifference = 0;
            int reddifference = 0;
            if (AwayTacklingStyle == "Soft")
            {
                AwayRandomNumber += 3;
                yellowdifference = 5;
                reddifference = 8;
            }
            else if (AwayTacklingStyle == "Mixed")
            {
                AwayRandomNumber += 4;
                yellowdifference = 4;
                reddifference = 7;
            }
            else
            {
                AwayRandomNumber += 5;
                yellowdifference = 3;
                reddifference = 5;
            }

            if (HomeRandomNumber - AwayRandomNumber - (AwayRedCards * 3) >= 0)
            {
                if (HomeRandomNumber - AwayRandomNumber > reddifference)
                {
                    AwayRedCards++;
                    Debug.Log("Straight red for away team");
                }
                else if (HomeRandomNumber - AwayRandomNumber > yellowdifference)
                {
                    AwayYellowCards++;
                    if (AwayYellowCards == 2)
                    {
                        AwayRedCards++;
                        Debug.Log("2 yellow cards for away team");
                        AwayYellowCards = 0;
                    }
                }
                Debug.Log("Home team through on goal");
                return true;
            }
            else
            {
                Debug.Log("Away team defended");
                return false;
            }
        }
        else
        {

            int HomeRandomNumber = UnityEngine.Random.Range(1, 10);
            int AwayRandomNumber = UnityEngine.Random.Range(1, 10);

            AwayRandomNumber += AwayPassingScore;
            int homeDefendingScore = 0;
            if (AwayPassingStyle == "Short")
            {
                foreach (OutfieldPlayer player in HomeTeam.Defenders)
                {
                    homeDefendingScore += player.Tackling;
                }
                foreach (OutfieldPlayer player in HomeTeam.Midfielders)
                {
                    homeDefendingScore += player.Tackling;
                }
                foreach (OutfieldPlayer player in HomeTeam.Forwards)
                {
                    homeDefendingScore += player.Tackling;
                }
            }
            else if (AwayPassingStyle == "Mixed")
            {
                foreach (OutfieldPlayer player in HomeTeam.Defenders)
                {
                    homeDefendingScore += player.Interception;
                }
                foreach (OutfieldPlayer player in HomeTeam.Midfielders)
                {
                    homeDefendingScore += player.Interception;
                }
                foreach (OutfieldPlayer player in HomeTeam.Forwards)
                {
                    homeDefendingScore += player.Interception;
                }
            }
            else
            {
                foreach (OutfieldPlayer player in HomeTeam.Defenders)
                {
                    homeDefendingScore += player.Vision;
                }
                foreach (OutfieldPlayer player in HomeTeam.Midfielders)
                {
                    homeDefendingScore += player.Vision;
                }
                foreach (OutfieldPlayer player in HomeTeam.Forwards)
                {
                    homeDefendingScore += player.Vision;
                }

            }
            homeDefendingScore = homeDefendingScore / 22;
            HomeRandomNumber += homeDefendingScore;

            int yellowdifference = 0;
            int reddifference = 0;
            if (HomeTacklingStyle == "Soft")
            {
                HomeRandomNumber += 3;
                yellowdifference = 5;
                reddifference = 8;
            }
            else if (HomeTacklingStyle == "Mixed")
            {
                HomeRandomNumber += 4;
                yellowdifference = 4;
                reddifference = 7;
            }
            else
            {
                HomeRandomNumber += 5;
                yellowdifference = 3;
                reddifference = 5;
            }

            if (AwayRandomNumber - HomeRandomNumber - (HomeRedCards * 3) >= 0)
            {
                if (AwayRandomNumber - HomeRandomNumber > reddifference)
                {
                    HomeRedCards++;
                    Debug.Log("Straight red for home team");
                }
                else if (AwayRandomNumber - HomeRandomNumber > yellowdifference)
                {
                    HomeYellowCards++;
                    if (HomeYellowCards == 2)
                    {
                        HomeRedCards++;
                        Debug.Log("2 yellow cards for home team");
                        HomeYellowCards = 0;
                    }
                }
                Debug.Log("Away team through on goal");
                return true;
            }
            else
            {
                Debug.Log("Home team defended");
                return false;
            }
        }
    }

    bool WonScoringCompetitiom(bool homeAttacking)
    {
        if (homeAttacking)
        {
            int HomeRandomNumber = UnityEngine.Random.Range(1, 10);
            int AwayRandomNumber = UnityEngine.Random.Range(1, 10);

            HomeRandomNumber += HomeShootingScore;
            int goalkeepingNumber = 0;
            if (HomeShootingStyle == "Short")
            {
                foreach (Goalkeeper goalie in AwayTeam.Goalies)
                {
                    goalkeepingNumber += goalie.Reflexes + goalie.OneOnOnes;
                }
                goalkeepingNumber = goalkeepingNumber / 6;
            }
            else if (HomeShootingStyle == "Mixed")
            {
                foreach (Goalkeeper goalie in AwayTeam.Goalies)
                {
                    goalkeepingNumber += goalie.Diving;
                }
                goalkeepingNumber = goalkeepingNumber / 3;

            }
            else
            {

                foreach (Goalkeeper goalie in AwayTeam.Goalies)
                {
                    goalkeepingNumber += goalie.Handling;
                }
                goalkeepingNumber = goalkeepingNumber / 3;
            }

            AwayRandomNumber += goalkeepingNumber;
            if (HomeRandomNumber >= AwayRandomNumber)
            {
                Debug.Log("Home team scored!");
                return true;
            }
            else
            {
                Debug.Log("Away team saved!");
                return false;
            }

        }
        else
        {

            int HomeRandomNumber = UnityEngine.Random.Range(1, 10);
            int AwayRandomNumber = UnityEngine.Random.Range(1, 10);

            AwayRandomNumber += AwayShootingScore;
            int goalkeepingNumber = 0;
            if (AwayShootingStyle == "Short")
            {
                foreach (Goalkeeper goalie in HomeTeam.Goalies)
                {
                    goalkeepingNumber += goalie.Reflexes + goalie.OneOnOnes;
                }
            }
            else if (AwayShootingStyle == "Mixed")
            {
                foreach (Goalkeeper goalie in HomeTeam.Goalies)
                {
                    goalkeepingNumber += goalie.Diving;
                }
                goalkeepingNumber = goalkeepingNumber / 3;

            }
            else
            {

                foreach (Goalkeeper goalie in HomeTeam.Goalies)
                {
                    goalkeepingNumber += goalie.Handling;
                }
                goalkeepingNumber = goalkeepingNumber / 3;
            }

            HomeRandomNumber += goalkeepingNumber;
            if (AwayRandomNumber >= HomeRandomNumber)
            {
                Debug.Log("Away team scored!");
                return true;
            }
            else
            {
                Debug.Log("Home team saved!");
                return false;
            }
        }
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