using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandCardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float adjustDuration = 0.5f;
    private float cumulativeTime = 0.0f;

    [SerializeField] private UICardInstanceRenderer m_UIInstanceRenderer;
    private Transform _trans;

    private Vector2 _targetPosition;
    private float _targetRotation;

    private bool _dragging;

    private void Awake()
    {
        _trans = GetComponent<Transform>();

        if (m_UIInstanceRenderer == null) Debug.LogError("ERROR! UIInstanceRenderer not set manually!");
    }

    public void Initialize(CardInstance cardInstance, Sprite s)
    {
        m_UIInstanceRenderer.Initialize(cardInstance, s);
    }

    public CardInstance GetInstance()
    {
        return m_UIInstanceRenderer.GetCardInstance();
    }

    private void Update()
    {
        if (_dragging) return;

        if (cumulativeTime > adjustDuration)
        {
            _trans.localPosition = _targetPosition;
            _trans.localEulerAngles = new(0, 0, _targetRotation);
            return;
        }

        float t = cumulativeTime / adjustDuration;

        _trans.localPosition = Vector2.Lerp(
            _trans.localPosition,
            _targetPosition,
            t
        );

        float currentZ = _trans.localEulerAngles.z;
        float newZ = Mathf.LerpAngle(currentZ, _targetRotation, t);
        _trans.localEulerAngles = new(0, 0, newZ);

        cumulativeTime += Time.deltaTime;
    }

    public void SetTargetPosition(Vector2 position)
    {
        _targetPosition = position;
        cumulativeTime = 0f;
    }

    public void SetTargetRotation(float rotationAngle)
    {
        _targetRotation = rotationAngle;
        cumulativeTime = 0f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_dragging) HandLayoutController.Instance.SetHovered(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_dragging) HandLayoutController.Instance.SetHovered(null);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = true;
        HandLayoutController.Instance.SetDragging(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _trans.position = eventData.position;
        if (eventData.position.y > 200f)
        {
            HandLayoutController.Instance.SwitchToWorld(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragging = false;
        HandLayoutController.Instance.SetDragging(false);
    }
}
