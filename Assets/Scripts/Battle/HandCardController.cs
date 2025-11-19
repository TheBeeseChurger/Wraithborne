using System.Collections;
using UnityEngine;

public class HandCardController : MonoBehaviour
{
    [SerializeField] private float adjustDuration = 0.5f;
    private float cumulativeTime = 0.0f;

    [SerializeField] private UICardInstanceRenderer m_UIInstanceRenderer;
    [SerializeField] private CardInstanceRenderer m_InstanceRenderer;
    private Transform _trans;

    private bool m_UIMode;
    private Vector2 _targetPosition;
    private float _targetRotation;

    private bool _dragging;

    private void Awake()
    {
        _trans = GetComponent<Transform>();

        if (m_InstanceRenderer == null) m_UIMode = true;
        else if (m_UIInstanceRenderer == null) m_UIMode = false;
        else Debug.LogError("ERROR! UIInstanceRenderer and InstanceRenderer are both present!");
    }

    public void Initialize(CardInstance cardInstance, Sprite s)
    {
        if (m_UIMode) m_UIInstanceRenderer.Initialize(cardInstance, s);
        else m_InstanceRenderer.Initialize(cardInstance, s);
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
}
