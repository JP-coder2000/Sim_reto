using UnityEngine;

public class FoodDestruction : MonoBehaviour
{
    // Este método se llama cuando otro collider entra en contacto con el collider de este objeto
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que colisionó es el que debe hacer invisible la comida
        if (other.gameObject.CompareTag("Food"))
        {
            other.gameObject.SetActive(false); // Hace invisible la comida desactivándola
        }
    }
}
