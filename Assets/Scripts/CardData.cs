using UnityEngine;
public enum CardTypes
{
    Entity,
    Structure,
    Ritual,
    Heart
}

public enum PulseTypes
{
    Flesh,
    Ash,
    Glass,
    Iron,
    Echo
}

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    [Header("Basic Information")]
    public string CardName;
    public string CardDescription;
    public Sprite Artwork;

    [Header("Type Information")]
    public CardTypes CardType;
    public PulseTypes CardPulseType;

    public string HeartPassive;
    public int HeartHealth;
    public int HeartPulseGen;

    public int EntityPulseCost;
    public int EntityHealth;
    public int EntityDamage;
}
