using UnityEngine;

public class CardInstance
{
    public CardData Data;
    public int instanceID;

    public int currentCost;
    public int currentHealth;
    public int currentDamage;
    public bool HasAction;
    public string mainText;

    public CardInstance(CardData data)
    {
        this.Data = data;
        this.instanceID = MatchSession.CurrentMatch.GetCardID();

        this.mainText = "<i>" + data.CardDescription + "</i>";
        this.mainText += "<br><br>";

        switch(data.CardType)
        {
            case CardTypes.Entity:
                this.currentCost = data.EntityPulseCost;
                this.currentHealth = data.EntityHealth;
                this.currentDamage = data.EntityDamage;
                this.HasAction = false;
                break;
            case CardTypes.Heart:
                this.currentHealth = data.HeartHealth;
                this.currentCost = data.HeartPulseGen;
                this.currentDamage = -1;
                this.HasAction = true;

                this.mainText += "Passive - " + data.HeartPassive;
                break;
            case CardTypes.Structure:
                this.currentCost = data.StructurePulseCost;
                this.currentHealth = data.StructureHealth;
                this.currentDamage = data.StructureDamage;
                this.HasAction = false;
                break;
            case CardTypes.Ritual:
                this.currentCost = data.RitualPulseCost;
                this.currentDamage = -1;
                this.currentHealth = -1;
                this.HasAction = true;

                this.mainText += data.RitualEffect;
                break;
            default:
                break;
        }
    }

}
