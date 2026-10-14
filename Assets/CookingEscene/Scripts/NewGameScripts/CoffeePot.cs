using UnityEngine;

public class CoffeePot : MonoBehaviour
{
    public Transform spout;            // punto del pico
    public ParticleSystem stream;      // partículas del café

    [Range(0f, 1f)]
    public float tiltThreshold = 0.35f; // más alto = hay que inclinar más
    public float maxDistance = 0.8f;    // hasta dónde busca la taza
    public float checkRadius = 0.04f;
    public float fillTime = 1.5f;

    float timer;
    bool pouring;

    void Start()
    {
        stream.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Update()
    {
        // ¿El pico apunta hacia abajo?
        bool tilted = Vector3.Dot(spout.forward, Vector3.down) > tiltThreshold;

        if (tilted != pouring)
        {
            pouring = tilted;
            if (pouring) stream.Play();
            else stream.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        if (!pouring) { timer = 0f; return; }

        Cup cup = FindCupBelow();
        if (cup == null || cup.hasCoffee) { timer = 0f; return; }

        timer += Time.deltaTime;
        if (timer >= fillTime)
        {
            cup.FillCoffee();
            timer = 0f;
        }
    }

    Cup FindCupBelow()
    {
        RaycastHit[] hits = Physics.SphereCastAll(
            spout.position, checkRadius, Vector3.down, maxDistance,
            ~0, QueryTriggerInteraction.Ignore);

        foreach (RaycastHit h in hits)
        {
            Cup c = h.collider.GetComponentInParent<Cup>();
            if (c != null) return c;
        }
        return null;
    }

    void OnDrawGizmosSelected()
    {
        if (spout == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(spout.position, spout.position + Vector3.down * maxDistance);
    }
}