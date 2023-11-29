using UnityEngine;

public class FoodDestruction : MonoBehaviour
{
    // Este método se llama cuando otro collider entra en contacto con el collider de este objeto
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que colisionó es el que debe destruir la comida
        if (other.gameObject.CompareTag("Food"))
        {
            Destroy(gameObject); // Destruye la comida
        }
    }
}
