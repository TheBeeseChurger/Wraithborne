using UnityEngine;

public class PlayerInteracter : MonoBehaviour
{
    [SerializeField]
    private float interactDistance = 5f;
    [SerializeField]
    private Color highlightColor;

    private HighlightInfo currentHighlighted;

    private struct HighlightInfo
    {
        public GameObject gameObject;
        public IInteractable interactable;

        public int originalLayer;
        public Color originalTintColor;

        public HighlightInfo(GameObject gameObject, IInteractable interactable, int originalLayer, Color originalTintColor)
        {
            this.gameObject = gameObject;
            this.interactable = interactable;
            this.originalLayer = originalLayer;
            this.originalTintColor = originalTintColor;
        }
    };

    // Update is called once per frame
    void Update()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            GameObject go = hit.collider.gameObject;

            if (currentHighlighted.gameObject != go)
            {
                ResetHighlight();

                if (go.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    // Save info
                    Color color = go.GetComponent<Renderer>().material.GetColor(Shader.PropertyToID("_TintColor"));
                    currentHighlighted = new HighlightInfo(go, interactable, go.layer, color);

                    // Change to highlighted
                    go.layer = 3;
                    go.GetComponent<Renderer>().material.SetColor(Shader.PropertyToID("_TintColor"), highlightColor);

                    // Display Tooltip with prompt text of that interactable
                    TooltipManager.Instance.ShowTooltip(interactable.GetPromptText());
                }
            }
            

            if (currentHighlighted.gameObject == go && InputManager.Instance.InteractedThisFrame()) currentHighlighted.interactable.Interact();
        }
        else
        {
            ResetHighlight();
        }
    }

    private void ResetHighlight()
    {
        GameObject go = currentHighlighted.gameObject;

        if (go != null)
        {
            TooltipManager.Instance.HideTooltip();
            go.layer = currentHighlighted.originalLayer;
            go.GetComponent<Renderer>().material.SetColor(Shader.PropertyToID("_TintColor"), currentHighlighted.originalTintColor);

            currentHighlighted = new HighlightInfo();
        }
    }
}
