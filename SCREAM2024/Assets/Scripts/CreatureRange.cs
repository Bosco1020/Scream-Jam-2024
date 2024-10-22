using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreatureRange : MonoBehaviour
{
    public UnityEvent beginChase = new UnityEvent();
    public UnityEvent endChase = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            beginChase.Invoke();
            Debug.Log("RUN");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            endChase.Invoke();
        }
    }
}
