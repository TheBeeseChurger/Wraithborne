using UnityEngine;

public class CardSpriteRenderer : MonoBehaviour
{
    public Sprite sprite;

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
