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
    [TextArea] public string CardDescription;
    public Sprite Artwork;

    [Header("Type Information")]
    public CardTypes CardType;
    public PulseTypes CardPulseType;

    [TextArea] public string HeartPassive;
    public int HeartHealth;
    public int HeartPulseGen;

    public int EntityPulseCost;
    public int EntityHealth;
    public int EntityDamage;
    public int EntityRange;
    public int EntitySpeed;

    public int StructurePulseCost;
    public int StructureHealth;
    public int StructureDamage;
    public int StructureRange;

    public int RitualPulseCost;
    [TextArea] public string RitualEffect;
}
