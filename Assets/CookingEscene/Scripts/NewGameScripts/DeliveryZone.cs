using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    readonly List<Cup> inside = new List<Cup>();

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        Cup cup = other.GetComponentInParent<Cup>();
        if (cup != null && !inside.Contains(cup)) inside.Add(cup);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.isTrigger) return;
        Cup cup = other.GetComponentInParent<Cup>();
        if (cup != null) inside.Remove(cup);
    }

    void Update()
    {
        if (GameManager.I == null) return;

        for (int i = inside.Count - 1; i >= 0; i--)
        {
            Cup cup = inside[i];
            if (cup == null) { inside.RemoveAt(i); continue; }

            // Entrega: tiene café, está suelta y hay un cliente esperando
            if (cup.hasCoffee && !cup.IsHeld && GameManager.I.CanDeliver)
            {
                inside.RemoveAt(i);
                GameManager.I.DeliverCoffee();
                cup.ResetCup();
            }
        }
    }
}