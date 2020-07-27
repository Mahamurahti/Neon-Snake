using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [Header("Time for object destruction")]
    public float time;

    void Start()
    {
        Destroy(gameObject, time);
    }

    public void SetTime(float desTime)
    {
        this.time = desTime;
    }
}
