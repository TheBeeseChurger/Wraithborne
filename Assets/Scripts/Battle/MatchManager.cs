using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    [SerializeField] DeckData playerDeck;
    [SerializeField] DeckData enemyDeck;
    [SerializeField] FrameMaker fm;
    [SerializeField] GameObject worldCardPrefab;

    void Start()
    {
        if (MatchSession.CurrentMatch == null) MatchSession.StartMatch(playerDeck, enemyDeck, fm);
        HandLayoutController.Instance.Initialize();

        TestSpawnWorldCard();
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
        SceneManager.LoadScene("Map1");
    }
}
