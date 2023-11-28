using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;


public class MovementAgent : MonoBehaviour
{
    public float speed = 5.0f;

    public void MoveTo(Vector3 newPosition)
    {
        StartCoroutine(MoveToPosition(newPosition));
    }

    IEnumerator MoveToPosition(Vector3 newPosition)
    {
        while (Vector3.Distance(transform.position, newPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
