using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateGrabRay : MonoBehaviour
{

    public GameObject leftRay;
    public GameObject rightRay;

    public XRDirectInteractor leftDirgrab;
    public XRDirectInteractor rightDirgrab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftRay.SetActive(leftDirgrab.interactablesSelected.Count == 0);
        rightRay.SetActive(rightDirgrab.interactablesSelected.Count == 0);
    }
}
