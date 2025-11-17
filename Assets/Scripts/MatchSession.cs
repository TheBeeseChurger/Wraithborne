using UnityEngine;

public enum MatchPhases
{
    Gather,
    Summon,
    Command,
    Resolve,
    Wandering,
    Post
}

public class MatchSession
{
    public static MatchSession CurrentMatch;

    public MatchPhases CurrentPhase;
    public int TurnCount;
    
    //Other states

    public void StartMatch()
    {
        MatchSession.CurrentMatch = new MatchSession();
    }

    public void EndMatch() { MatchSession.CurrentMatch = null; }
}
