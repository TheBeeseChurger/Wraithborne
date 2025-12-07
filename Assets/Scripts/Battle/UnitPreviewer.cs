using UnityEngine;

public class UnitPreviewer : MonoBehaviour, IPreviewable
{
    private UnitInstance _instance;

    public CardInstance GetCardInstance()
    {
        if (_instance != null) return _instance.sourceCard;
        return null;
    }

    public void Initialize(UnitInstance unitInstance)
    {
        _instance = unitInstance;
    }
}
