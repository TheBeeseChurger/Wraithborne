using UnityEngine;

public class TileManager : MonoBehaviour
{
    public TileInstance instance;

    public HoverPreviewTrigger occupantTrigger;

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

        if (occupantTrigger != null) occupantTrigger.OnMouseEnter();

        _startingColor = meshRenderer.material.color;
        meshRenderer.SetPropertyBlock(_mpb);
    }

    public async void OnMouseExit()
    {
        if (instance == null) return;

        if (occupantTrigger != null)
        {
            occupantTrigger.entered = false;
            await Awaitable.NextFrameAsync();
            if (!occupantTrigger.entered) occupantTrigger.ExitHover();
        }

        if (_startingColor != Color.white)
        {
            _mpb.SetColor("_BaseColor", _startingColor);
            meshRenderer.SetPropertyBlock(_mpb);
            _startingColor = Color.white;
            _mpb.SetColor("_BaseColor", hoverColor);
        }
    }
}
