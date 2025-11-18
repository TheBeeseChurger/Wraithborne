using UnityEngine;

public class UnitInstance
{
    public CardInstance sourceCard;
    public PlayerRuntimeState owner;

    public UnitInstance(CardInstance sourceCard, PlayerRuntimeState owner)
    {
        this.sourceCard = sourceCard;
        this.owner = owner;
    }
}
