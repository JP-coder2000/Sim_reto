using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    public float speed = 5.0f;

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
}