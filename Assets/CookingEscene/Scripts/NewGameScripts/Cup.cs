using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Cup : MonoBehaviour
{
    public GameObject coffeeLayer;   // cilindro de café dentro de la taza
    public bool hasCoffee;

    Vector3 startPos;
    Quaternion startRot;
    Rigidbody rb;
    XRGrabInteractable grab;

    public bool IsHeld
    {
        get { return grab != null && grab.isSelected; }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
        startPos = transform.position;
        startRot = transform.rotation;
        coffeeLayer.SetActive(false);
    }

    public void FillCoffee()
    {
        hasCoffee = true;
        coffeeLayer.SetActive(true);
    }

    public void ResetCup()
    {
        hasCoffee = false;
        coffeeLayer.SetActive(false);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(startPos, startRot);
    }
}