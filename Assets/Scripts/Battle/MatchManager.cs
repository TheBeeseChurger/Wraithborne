using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] DeckData playerDeck;
    [SerializeField] DeckData enemyDeck;
    [SerializeField] FrameMaker fm;

    void Start()
    {
        MatchSession.StartMatch(playerDeck, enemyDeck, fm);
        HandLayoutController.Instance.Initialize();
    }
}
