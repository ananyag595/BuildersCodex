using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIddenSwitch : MonoBehaviour
{
    void OnMouseDown()
    {
        if (gameObject.CompareTag("Interactable"))
        {
            Debug.Log("Hidden switch activated!");
            //puzzlePanel.SetActive(true);
        }
    }

}
