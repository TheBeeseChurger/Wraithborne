using UnityEngine;

public class TileManager : MonoBehaviour
{
    public TileInstance instance;

    //public UnitManager unit = null;

    public MeshRenderer meshRenderer;
    public Color hoverColor;

    private MaterialPropertyBlock _mpb = null;
    private Color _startingColor;

    public void Initialize(TileInstance tileInstance)
    {
        instance = tileInstance;

        _mpb = new MaterialPropertyBlock();
        _mpb.SetColor("_BaseColor", hoverColor);
    }

    public void OnMouseEnter()
    {
        if (instance == null) return;

        // if (unit != null) unit.OnMouseEnter();
        _startingColor = meshRenderer.material.color;
        meshRenderer.SetPropertyBlock(_mpb);
    }

    public void OnMouseExit()
    {
        if (instance == null) return;

        // if (unit != null) unit.OnMouseExit();

        if (_startingColor != Color.white)
        {
            _mpb.SetColor("_BaseColor", _startingColor);
            meshRenderer.SetPropertyBlock(_mpb);
            _startingColor = Color.white;
            _mpb.SetColor("_BaseColor", hoverColor);
        }
    }
}
