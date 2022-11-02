using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvents : MonoBehaviour
{
    [SerializeField]
    UnityEvent<Collider> TriggerEnter = new UnityEvent<Collider>();
    [SerializeField]
    UnityEvent<Collider> TriggerExit = new UnityEvent<Collider>();
    [SerializeField]
    UnityEvent<Collision> CollisionEnter = new UnityEvent<Collision>();
    [SerializeField]
    UnityEvent<Collision> CollisionExit = new UnityEvent<Collision>();

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit.Invoke(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        CollisionExit.Invoke(collision);
    }
}
