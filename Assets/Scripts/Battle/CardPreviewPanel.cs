using TMPro;
using UnityEngine;

public class CardPreviewPanel : MonoBehaviour
{
    public static CardPreviewPanel Instance;

    public UICardInstanceRenderer previewRender;
    public TextMeshProUGUI idText;

    private int instanceID;
    private bool isShowing = false;
    private bool isLocked = false;

    void Awake()
    {
        Instance = this;
    }

    public void Show(IPreviewable previewTarget)
    {
        if (isLocked) return;
        if (isShowing) return;
        var instance = previewTarget.GetCardInstance();
        previewRender.Initialize(instance, MatchSession.CurrentMatch.GetCardFrame(instance.Data));
        idText.text = "Instance ID: " + instance.instanceID;

        this.instanceID = instance.instanceID;
        isShowing = true;
        previewRender.gameObject.SetActive(true);
    }

    public void Hide(int instanceID)
    {
        if (isLocked) return;
        if (!isShowing) return;
        if (instanceID != this.instanceID) return;

        isShowing = false;
        previewRender.gameObject.SetActive(false);
    }

    public void Lock(bool l)
    {
        if (!isShowing) return;
        isLocked = l;
    }
}
