using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportRay : MonoBehaviour
{
    public GameObject rightTeleportation;
    public InputActionProperty rightActivate;

    public InputActionProperty rightCancel;

    public XRRayInteractor RightRay;

    // Update is called once per frame
    void Update()
    {
        bool isRayHovering = RightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 normal, out int number, out bool valid);
        rightTeleportation.SetActive(!isRayHovering && rightCancel.action.ReadValue<float>() < 0.1f && rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
