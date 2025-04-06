using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngineManager : MonoBehaviour
{   
    public GameObject CurrentPart;
    public GameObject NextPart;
    public GameObject CurrentSocket;
    public GameObject NextSocket;

    void Start()
    {

    }

    void Update()
    {

    }

    public void StepComplete()
    {
        CurrentSocket.GetComponent<MeshRenderer>().enabled = false;
        CurrentPart.GetComponent<BoxCollider>().enabled = false;
        CurrentSocket.GetComponent<BoxCollider>().enabled = false;
        NextPart.SetActive(true);
        NextSocket.SetActive(true);     
        
    }

    
}
