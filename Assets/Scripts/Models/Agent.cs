using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    public float speed = 5.0f;
    public int unique_id;
    public bool carrying_food;
    public string role;
    public Material[] colors;

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

    public void onTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            Destroy(other.gameObject);
        }
    }

    public void Update()
    {
        if (carrying_food)
        {
            GetComponent<Renderer>().material = colors[0];
        }
        else
        {
            GetComponent<Renderer>().material = colors[1];
        }
    }
}