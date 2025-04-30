using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPuzzleTrigger : MonoBehaviour
{
    public GameObject puzzleUI; // Assign a Canvas with the puzzle
    public void OnInteract()
    {
        puzzleUI.SetActive(true); // Show puzzle
        Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI
        Cursor.visible = true;
    }
}