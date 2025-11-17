using UnityEngine;

public class CardInstance
{
    public CardData Data;
    public int instanceID;

    public int currentCost;
    public int currentHealth;
    public bool HasAction;

    public CardInstance(CardData data)
    {
        this.Data = data;
        this.instanceID = 1;

        switch(data.CardType)
        {
            case CardTypes.Entity:
                this.currentCost = data.EntityPulseCost;
                this.currentHealth = data.EntityHealth;
                this.HasAction = false;
                break;
            case CardTypes.Heart:
                this.currentHealth = data.HeartHealth;
                this.currentCost = data.HeartPulseGen;
                this.HasAction = true;
                break;
            default:
                break;
        }
    }

}
