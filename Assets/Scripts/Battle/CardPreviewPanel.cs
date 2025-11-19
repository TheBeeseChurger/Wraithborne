using TMPro;
using UnityEngine;

public class CardPreviewPanel : MonoBehaviour
{
    public static CardPreviewPanel Instance;

    public UICardInstanceRenderer previewRender;
    public TextMeshProUGUI idText;

    void Awake()
    {
        Instance = this;
    }

    public void Show(IPreviewable previewTarget)
    {
        var instance = previewTarget.GetCardInstance();
        previewRender.Initialize(instance, MatchSession.CurrentMatch.GetCardFrame(instance.Data));
        idText.text = "Instance ID: " + instance.instanceID;

        previewRender.gameObject.SetActive(true);
    }

    public void Hide()
    {
        previewRender.gameObject.SetActive(false);
    }
}
