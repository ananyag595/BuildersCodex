using UnityEngine;
using UnityEngine.EventSystems;

public class PagePuzzle : MonoBehaviour
{
    public RectTransform[] pieces; // Assign all torn pieces in Inspector
    public float snapDistance = 50f;
    public GameObject trapDoorX; // Assign the trapdoor GameObject in Inspector

    private int correctPieces = 0;

    // Call this when a piece is moved (e.g., in DraggablePiece's OnEndDrag)
    public void CheckPuzzle()
    {
        correctPieces = 0;

        foreach (RectTransform piece in pieces)
        {
            PieceData data = piece.GetComponent<PieceData>();
            if (data != null &&
                Vector2.Distance(piece.anchoredPosition, data.correctPos) < snapDistance)
            {
                correctPieces++;
                // Optional: Snap piece to exact correct position
                piece.anchoredPosition = data.correctPos;
            }
        }

        if (correctPieces == pieces.Length)
        {
            PuzzleSolved();
        }
    }

    private void PuzzleSolved()
    {
        // Show trapdoor (enable it if previously hidden)
        if (trapDoorX != null)
        {
            trapDoorX.SetActive(true);

            // Optional: Trigger trapdoor animation
            Animator anim = trapDoorX.GetComponent<Animator>();
            if (anim != null) anim.SetTrigger("Open");
        }

        // Hide puzzle UI and lock cursor
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}