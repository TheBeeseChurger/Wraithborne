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
    public PlayerRuntimeState Player;
    public PlayerRuntimeState Enemy;

    public static void StartMatch()
    {
        MatchSession.CurrentMatch = new MatchSession();
    }

    public static void EndMatch() { MatchSession.CurrentMatch = null; }
}
