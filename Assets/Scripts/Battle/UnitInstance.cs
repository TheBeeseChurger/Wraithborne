using UnityEngine;

public class UnitInstance
{
    public CardInstance sourceCard;
    public PlayerRuntimeState owner;

    public TileInstance currentTile;

    public UnitInstance(CardInstance sourceCard, TileInstance summonedTile, PlayerRuntimeState owner = null)
    {
        this.sourceCard = sourceCard;
        this.currentTile = summonedTile;
        this.owner = owner;

        this.currentTile.occupant = this;
    }

    public void MoveTiles(TileInstance newTile)
    {
        if (newTile == currentTile) return;
        currentTile.occupant = null;
        currentTile = newTile;
        currentTile.occupant = this;
    }
}
