using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("Snap Settings")]
    public RectTransform correctSnapPoint;
    public float snapDistance = 80f;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    // Original tray state
    private Vector3 startWorldPosition;
    private Transform startParent;
    private int startSiblingIndex;

    private bool isPlaced = false;

    private SnapPointHolder snapHolder;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Save original tray position and hierarchy data
        startParent = transform.parent;
        startWorldPosition = rectTransform.position;
        startSiblingIndex = transform.GetSiblingIndex();

        if (correctSnapPoint != null)
        {
            snapHolder = correctSnapPoint.GetComponent<SnapPointHolder>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // If item was already placed, free the snap point first
        if (isPlaced)
        {
            isPlaced = false;

            if (snapHolder != null && snapHolder.currentItem == this)
            {
                snapHolder.currentItem = null;
            }
        }

        // Bring dragged item to front
        rectTransform.SetAsLastSibling();

        // Allow UI raycasts to pass through while dragging
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (correctSnapPoint == null)
        {
            ReturnToTray();
            return;
        }

        float distance = Vector2.Distance(rectTransform.position, correctSnapPoint.position);

        if (distance <= snapDistance)
        {
            // If another item is already using this snap point, send it back
            if (snapHolder != null && snapHolder.currentItem != null && snapHolder.currentItem != this)
            {
                snapHolder.currentItem.ReturnToTray();
            }

            // Snap this item into place
            rectTransform.position = correctSnapPoint.position;
            isPlaced = true;

            if (snapHolder != null)
            {
                snapHolder.currentItem = this;
            }
        }
        else
        {
            ReturnToTray();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Clicking a placed item returns it to the tray
        if (isPlaced)
        {
            ReturnToTray();
        }
    }

    public void ReturnToTray()
    {
        isPlaced = false;

        if (snapHolder != null && snapHolder.currentItem == this)
        {
            snapHolder.currentItem = null;
        }

        // Return to original tray parent
        rectTransform.SetParent(startParent);

        // Restore original order in tray
        rectTransform.SetSiblingIndex(startSiblingIndex);

        // Restore original position
        rectTransform.position = startWorldPosition;

        // Optional reset
        rectTransform.localScale = Vector3.one;
        rectTransform.rotation = Quaternion.identity;
    }
}