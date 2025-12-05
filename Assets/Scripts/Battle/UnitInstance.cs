using UnityEngine;

public class UnitInstance
{
    public CardInstance sourceCard;
    public PlayerRuntimeState owner;

    public TileInstance currentTile;

    public UnitInstance(CardInstance sourceCard, TileInstance summonedTile, PlayerRuntimeState owner)
    {
        this.sourceCard = sourceCard;
        this.currentTile = summonedTile;
        this.owner = owner;
    }
}
