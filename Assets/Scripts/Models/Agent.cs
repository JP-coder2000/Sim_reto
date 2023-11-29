using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    public float speed = 5.0f;
    public int unique_id;
    public bool carrying_food;
    public string role;

    public void MoveTo(Vector3 newPosition)
    {
        StartCoroutine(MoveToPosition(newPosition));
    }

    private IEnumerator MoveToPosition(Vector3 newPosition)
    {
        while (Vector3.Distance(transform.position, newPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void UpdateRole(string newRole)
    {
        role = newRole;
        // Aquí puedes agregar más lógica, como cambiar la apariencia del agente según su rol
    }

    public bool IsCarrying()
    {
        return carrying_food;
    }
}