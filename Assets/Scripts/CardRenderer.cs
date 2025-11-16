using UnityEngine;

public class CardRenderer : MonoBehaviour
{
    public Sprite sprite;
    
    void Start()
    {
        SetSprite(sprite);
    }

    public void SetSprite(Sprite newSprite)
    {
        var mpb = new MaterialPropertyBlock();
        var meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.GetPropertyBlock(mpb);

        mpb.SetTexture("_BaseMap", newSprite.texture);

        meshRenderer.SetPropertyBlock(mpb);
        sprite = newSprite;
    }
}
