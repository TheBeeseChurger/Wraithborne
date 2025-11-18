using UnityEngine;

public class CardPreviewPanel : MonoBehaviour
{
    public static CardPreviewPanel Instance;

    public FrameMaker FrameMaker;
    public UICardInstanceRenderer previewRender;

    void Awake()
    {
        Instance = this;
    }

    public void Show(IPreviewable previewTarget)
    {
        var instance = previewTarget.GetCardInstance();
        previewRender.Initialize(instance, FrameMaker.PickFrame(instance.Data));

        previewRender.gameObject.SetActive(true);
    }

    public void Hide()
    {
        previewRender.gameObject.SetActive(false);
    }
}
