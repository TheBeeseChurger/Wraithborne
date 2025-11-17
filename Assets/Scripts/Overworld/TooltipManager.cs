using System.Collections;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    private static TooltipManager _instance;

    public static TooltipManager Instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private TextMeshProUGUI tooltipBox;
    private RectTransform tooltipRect;
    [SerializeField]
    private CanvasGroup canvasGroup;

    [Header("Positions")]
    [SerializeField]
    private Vector2 hidingPos;
    [SerializeField]
    private Vector2 showingPos;

    [Header("Transition")]
    [SerializeField]
    private float transitionDuration = 5f;
    [SerializeField]
    private AnimationCurve easingCurve;

    private float transitionProgress = 0f;
    private float transitionDirection = 1f;

    private Coroutine _currentTransition;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        tooltipRect = (RectTransform)canvasGroup.gameObject.transform;
    }

    public void ShowTooltip(string displayText)
    {
        tooltipBox.text = displayText;
        transitionDirection = 1f;

        if (_currentTransition != null) StopCoroutine(_currentTransition);

        _currentTransition = StartCoroutine(TransitionTooltip());
    }

    public void HideTooltip()
    {
        transitionDirection = -1f;

        if (_currentTransition != null) StopCoroutine(_currentTransition);

        _currentTransition = StartCoroutine(TransitionTooltip());
    }

    private IEnumerator TransitionTooltip()
    {
        float elapsed = transitionProgress * transitionDuration;
        float duration = transitionDuration;

        while (elapsed >= 0f && elapsed <= duration)
        {
            transitionProgress = Mathf.Clamp01(elapsed / duration);
            float easedT = easingCurve.Evaluate(transitionProgress);

            tooltipRect.anchoredPosition = Vector2.Lerp(hidingPos, showingPos, easedT);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, easedT);

            elapsed += Time.deltaTime * transitionDirection;
            yield return null;
        }

        transitionProgress = Mathf.Clamp01(transitionDirection > 0 ? 1f : 0f);
        float finalT = easingCurve.Evaluate(transitionProgress);

        tooltipRect.anchoredPosition = Vector2.Lerp(hidingPos, showingPos, finalT);
        canvasGroup.alpha = Mathf.Lerp(0f, 1f, finalT);

        _currentTransition = null;
    }
}
