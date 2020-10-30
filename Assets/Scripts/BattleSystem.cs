using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions to complete:
/// - Do Round
/// - Fight Over
/// </summary>
public class BattleSystem : MonoBehaviour
{
    public DanceTeam teamA, teamB; //References to TeamA and TeamB
    public FightManager fightManager; // References to our FightManager.

    public float battlePrepTime = 2;  // the amount of time we need to wait before a battle starts
    public float fightCompletedWaitTime = 2; // the amount of time we need to wait till a fight/round is completed.

    /// <summary>
    /// This occurs every round or every X number of seconds, is the core battle logic/game loop.
    /// </summary>
    /// <returns></returns>
    IEnumerator DoRound()
    {

        // waits for a float number of seconds....
        yield return new WaitForSeconds(battlePrepTime);

        //checking for no dancers on either team
        if (teamA.allDancers.Count == 0 && teamB.allDancers.Count == 0)
        {
            // Debug.LogWarning("DoRound called, but there are no dancers on either team. DanceTeamInit needs to be completed");
            // This will be called if there are 0 dancers on both teams.

            teamA.StartCoroutine(DoRound());
            teamB.StartCoroutine(DoRound());
        }

        else if (teamA.activeDancers.Count > 0 && teamB.activeDancers.Count > 0)
        {
            // Debug.LogWarning("DoRound called, it needs to select a dancer from each team to dance off and put in the FightEventData below");
            //You need to select two random or engineered random characters to fight...so one from team a and one from team b....
            //We then need to pass in the two selected dancers into fightManager.Fight(CharacterA,CharacterB);
            //fightManager.Fight(charA, charB);
            Character charA = teamA.activeDancers[Random.Range(0, teamA.activeDancers.Count)];
            Character charB = teamB.activeDancers[Random.Range(0, teamB.activeDancers.Count)];

            fightManager.Fight(charA, charB);
        }
        else
        {
            // IF we get to here...then we have a team has won...winner winner chicken dinner.
            DanceTeam winner = null; // null is the same as saying nothing...often seen as a null reference in your logs.

            // We need to determine a winner...but how?...maybe look at the previous if statements for clues?
            //Enables the win effects, and logs it out to the console.32

            winner.EnableWinEffects();
            BattleLog.Log(winner.danceTeamName.ToString(), winner.teamColor);

            Debug.Log("DoRound called, but we have a winner so Game Over");

            teamA.StopAllCoroutines();
            teamB.StopAllCoroutines();           

            //calling the coroutine so we can put waits in for anims to play
            StartCoroutine(HandleFightOver());

            teamA.StartCoroutine(DoRound());
            teamB.StartCoroutine(DoRound());

        }
    }    
            /// Used to Request A round.
            // </summary>
            // <returns></returns
        public void RequestRound()
        {
            //calling the coroutine so we can put waits in for anims to play
            StartCoroutine(DoRound());

            teamA.StartCoroutine(DoRound());
            teamB.StartCoroutine(DoRound());
        }
        /// <summary>
        // Handles the end of a fight and waits to start the next round.
        /// </summary>
        // <returns></returns
    IEnumerator HandleFightOver()
    {
        yield return new WaitForSeconds(fightCompletedWaitTime);
        {
            teamA.DisableWinEffects();
            teamB.DisableWinEffects();


            Debug.LogWarning("HandleFightOver called, may need to prepare or clean dancers or teams and checks before doing GameEvents.RequestFighters()");

            RequestRound();

            teamA.DisableWinEffects();
            teamB.DisableWinEffects();

        }
    }

    public void FightOver(Character winner, Character defeated, float outcome)
    {

    }
}
        
 









