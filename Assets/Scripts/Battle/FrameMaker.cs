using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrameMaker", menuName = "Scriptable Objects/FrameMaker")]
public class FrameMaker : ScriptableObject
{
    [SerializeField] private List<Sprite> heartFrames;
    [SerializeField] private List<Sprite> normalFrames;

    public Sprite PickFrame(CardData card)
    {
        if (card.CardType != CardTypes.Heart && card.CardType != CardTypes.Ritual)
        {
            return normalFrames[(int)card.CardPulseType];
        }
        return heartFrames[(int)card.CardPulseType];
    }
}
