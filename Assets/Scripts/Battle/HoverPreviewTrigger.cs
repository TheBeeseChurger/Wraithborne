using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverPreviewTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public IPreviewable previewSource;
    Coroutine hoverRoutine;

    [SerializeField] float delay = 0.3f;

    private void Awake()
    {
        previewSource = GetComponent<IPreviewable>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverRoutine = StartCoroutine(ShowPreview());
    }

    private void OnMouseEnter()
    {
        hoverRoutine = StartCoroutine(ShowPreview());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverRoutine != null) StopCoroutine(hoverRoutine);

        CardPreviewPanel.Instance.Hide();
    }

    private void OnMouseExit()
    {
        if (hoverRoutine != null) StopCoroutine(hoverRoutine);

        CardPreviewPanel.Instance.Hide();
    }

    IEnumerator ShowPreview()
    {
        yield return new WaitForSeconds(delay);
        CardPreviewPanel.Instance.Show(previewSource);
    }
}
