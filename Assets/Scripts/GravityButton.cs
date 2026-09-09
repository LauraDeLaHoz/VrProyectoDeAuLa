using UnityEngine;

public class GravityButton : MonoBehaviour
{



    public Rigidbody[] objetos;



    public void CambiarGravedad()
    {

        foreach (Rigidbody rb in objetos)
        {
            if (rb != null)
            {

                rb.useGravity = !rb.useGravity;

            }


        }

    }


}
