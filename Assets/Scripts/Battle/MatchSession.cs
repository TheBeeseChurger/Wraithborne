using UnityEngine;

public enum MatchPhases
{
    Pre,
    Player,
    Enemy,
    Wandering,
    Post
}

public enum TurnPhases
{
    None,
    Start,
    Gather,
    Summon,
    Command,
    Resolve,
    End
}

public class MatchSession
{
    public static MatchSession CurrentMatch;
    private static int NextCardID = 1;

    public MatchPhases CurrentPhase;
    public TurnPhases CurrentTurn;
    public int TurnCount;

    //Other states
    public PlayerRuntimeState Player;
    public PlayerRuntimeState Enemy;

    public static void StartMatch(DeckData playerDeck, DeckData enemyDeck)
    {
        MatchSession.CurrentMatch = new MatchSession();
        CurrentMatch.CurrentPhase = MatchPhases.Pre;
        CurrentMatch.CurrentTurn = TurnPhases.None;

        CurrentMatch.Player = new PlayerRuntimeState(playerDeck);
        CurrentMatch.Enemy = new PlayerRuntimeState(enemyDeck);

        CurrentMatch.TurnCount = 1;
    }

    public static void EndMatch() { MatchSession.CurrentMatch = null; }

    public static int GetCardID()
    {
        NextCardID++;
        return --NextCardID;
    }
}
