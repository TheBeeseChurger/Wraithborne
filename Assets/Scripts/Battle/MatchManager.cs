using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] MapData mapData;
    [SerializeField] DeckData playerDeck;
    [SerializeField] DeckData enemyDeck;
    [SerializeField] FrameMaker fm;
    [SerializeField] GameObject worldCardPrefab;

    void Start()
    {
        if (MatchSession.CurrentMatch == null) MatchSession.StartMatch(mapData, playerDeck, enemyDeck, fm);
        HandLayoutController.Instance.Initialize();
        MapLayoutController.Instance.Initialize(MatchSession.CurrentMatch.Map);

        //TestSpawnWorldCard();
    }

    public void TestDrawCard()
    {
        MatchSession.CurrentMatch.Player.Draw(1);
    }

    public void TestSpawnWorldCard()
    {
        var card = Instantiate(worldCardPrefab).GetComponent<CardInstanceRenderer>();
        var instance = MatchSession.CurrentMatch.Player.HeartCard;
        card.Initialize(instance, MatchSession.CurrentMatch.GetCardFrame(instance.Data));
    }

    public void TestExitTable()
    {
        OverworldManager.Instance.ReturnToOverworld();
    }
}
