using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverPreviewTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public IPreviewable previewSource;
    Coroutine hoverRoutine;

    [SerializeField] bool pointerMode;

    [SerializeField] float delay = 0.3f;

    private void Awake()
    {
        previewSource = GetComponent<IPreviewable>();
        previewSource ??= GetComponentInParent<IPreviewable>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!pointerMode) return;
        if (hoverRoutine != null) return;
        hoverRoutine = StartCoroutine(ShowPreview());
    }

    public void OnMouseEnter()
    {
        if (pointerMode) return;
        if (hoverRoutine != null) return;
        hoverRoutine = StartCoroutine(ShowPreview());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!pointerMode) return;
        if (hoverRoutine != null) StopCoroutine(hoverRoutine);
        hoverRoutine = null;
        CardPreviewPanel.Instance.Hide(previewSource.GetCardInstance().instanceID);
    }

    public void OnMouseExit()
    {
        if (pointerMode) return;
        if (hoverRoutine != null) StopCoroutine(hoverRoutine);
        hoverRoutine = null;
        CardPreviewPanel.Instance.Hide(previewSource.GetCardInstance().instanceID);
    }

    IEnumerator ShowPreview()
    {
        yield return new WaitForSeconds(delay);
        CardPreviewPanel.Instance.Show(previewSource);
    }
}
