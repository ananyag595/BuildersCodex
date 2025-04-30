using UnityEngine;
using UnityEngine.EventSystems; // Critical for drag handlers

public class DraggablePiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public PagePuzzle puzzleManager;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        puzzleManager.CheckPuzzle(); // Validate positions
    }
}