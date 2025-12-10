using UnityEngine;

public class WorldCardController : MonoBehaviour
{
    private Vector3 lastPos;

    [SerializeField] private CardInstanceRenderer m_cardInstanceRenderer;
    [SerializeField] private LineRenderer lineRenderer;

    private bool dragging = false;

    private const float hoverHeight = 2.5f;

    private TileManager hitTile;
    
    void Awake()
    {
        if (m_cardInstanceRenderer != null) return;
        m_cardInstanceRenderer = GetComponent<CardInstanceRenderer>();
        if (m_cardInstanceRenderer == null) Debug.LogError("ERROR! CardInstanceRenderer not present and not manually set.");
        lastPos = transform.position;
    }

    public void Initialize(CardInstance cI, Sprite s)
    {
        m_cardInstanceRenderer.Initialize(cI, s);
        dragging = true;
    }

    public CardInstance GetInstance()
    {
        return m_cardInstanceRenderer.GetCardInstance();
    }

    void Update()
    {
        if (!dragging) return;

        FollowMouse();
        ApplyTilt();
        ScanTiles();

        if (InputManager.Instance.LeftReleaseThisFrame())
        {
            dragging = false;
            lineRenderer.positionCount = 0;

            if (hitTile != null)
            {
                if (HandLayoutController.Instance.SpawnOnTile(GetInstance(), hitTile))
                {
                    GetComponentInChildren<WorldCardRecall>().Play(() => HandLayoutController.Instance.DeleteWorldCard(this));
                    return;
                }
            }

            GetComponentInChildren<WorldCardRecall>().Play(() => HandLayoutController.Instance.SwitchToUI(this));
        }
    }

    private void FollowMouse()
    {
        var input = InputManager.Instance.GetMousePosition();
        Ray ray = Camera.main.ScreenPointToRay(input);

        Plane boardPlane = new(Vector3.up, 0f);

        if (boardPlane.Raycast(ray, out float distance))
        {
            Vector3 fieldPoint = ray.GetPoint(distance);

            Vector3 target = fieldPoint + Vector3.up * hoverHeight;
            transform.position = Vector3.Lerp(transform.position, target, 0.4f);
        }
    }

    private void ApplyTilt()
    {
        Vector3 velocity = transform.position - lastPos;

        float tiltX = -velocity.z * 150f;
        float tiltZ = velocity.x * 150f;

        float maxTilt = 30f;
        tiltX = Mathf.Clamp(tiltX, maxTilt * -1, maxTilt);
        tiltZ = Mathf.Clamp(tiltZ, maxTilt * -1, maxTilt);

        transform.rotation = Quaternion.Euler(tiltX, 0f, tiltZ);

        lastPos = transform.position;
    }

    private void ScanTiles()
    {
        Ray ray = new(transform.position, Vector3.down);
        LayerMask lm = LayerMask.GetMask("Tile");

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2.5f, lm))
        {
            lineRenderer.positionCount = 2;

            Vector3[] positions = new Vector3[2];
            positions[0] = transform.position + new Vector3(0f, -0.1f);
            positions[1] = hitInfo.collider.transform.position;
            lineRenderer.SetPositions(positions);

            hitTile = hitInfo.collider.GetComponent<TileManager>();
        }
        else
        {
            lineRenderer.positionCount = 0;
            hitTile = null;
        }
    }
}
