using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickup : MonoBehaviour
{
    public GameObject noteUI; // Assign the UI panel that displays the clue

    private bool isReading = false;

    void Update()
    {
        if (isReading && Input.GetKeyDown(KeyCode.Escape))  // Press Escape to close the note
        {
            CloseNote();
        }
    }

    void OnMouseDown()  // Click to read the note
    {
        if (!isReading)
        {
            OpenNote();
        }
    }

    void OpenNote()
    {
        isReading = true;
        noteUI.SetActive(true);
        Time.timeScale = 0; // Pause the game so the player can read
    }

    void CloseNote()
    {
        isReading = false;
        noteUI.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }
}
