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
    private int NextCardID = 1;

    public MatchPhases CurrentPhase;
    public TurnPhases CurrentTurn;
    public int TurnCount;

    private FrameMaker FrameMaker;

    //Other states
    public PlayerRuntimeState Player;
    public PlayerRuntimeState Enemy;

    public static void StartMatch(DeckData playerDeck, DeckData enemyDeck, FrameMaker fm)
    {
        MatchSession.CurrentMatch = new MatchSession();
        CurrentMatch.CurrentPhase = MatchPhases.Pre;
        CurrentMatch.CurrentTurn = TurnPhases.None;

        CurrentMatch.Player = new PlayerRuntimeState(playerDeck);
        CurrentMatch.Enemy = new PlayerRuntimeState(enemyDeck);
        CurrentMatch.FrameMaker = fm;

        CurrentMatch.TurnCount = 1;
    }

    public static void EndMatch() { MatchSession.CurrentMatch = null; }

    public int GetCardID()
    {
        NextCardID++;
        return NextCardID - 1;
    }

    public Sprite GetCardFrame(CardData cardData)
    {
        return FrameMaker.PickFrame(cardData);
    }
}
