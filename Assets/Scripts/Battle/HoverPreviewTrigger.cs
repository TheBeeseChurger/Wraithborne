using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverPreviewTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public IPreviewable previewSource;
    Coroutine hoverRoutine;

    [SerializeField] bool pointerMode;

    [SerializeField] float delay = 0.3f;

    public bool entered = false;

    private void Awake()
    {
        previewSource = GetComponent<IPreviewable>();
        previewSource ??= GetComponentInParent<IPreviewable>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!pointerMode) return;
        if (entered) return;
        entered = true;
        if (hoverRoutine != null) return;
        hoverRoutine = StartCoroutine(ShowPreview());
        entered = true;
    }

    public void OnMouseEnter()
    {
        if (pointerMode) return;
        if (entered) return;
        entered = true;
        if (hoverRoutine != null) return;
        hoverRoutine = StartCoroutine(ShowPreview());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!pointerMode) return;
        if (!entered) return;
        ExitHover();
    }

    public async void OnMouseExit()
    {
        if (pointerMode) return;
        entered = false;
        await Awaitable.NextFrameAsync();
        if (entered) return;
        ExitHover();
    }

    public void ExitHover()
    {
        if (hoverRoutine != null) StopCoroutine(hoverRoutine);
        hoverRoutine = null;
        CardPreviewPanel.Instance.Hide(previewSource.GetCardInstance().instanceID);
        entered = false;
    }

    IEnumerator ShowPreview()
    {
        yield return new WaitForSeconds(delay);
        CardPreviewPanel.Instance.Show(previewSource);
    }
}
