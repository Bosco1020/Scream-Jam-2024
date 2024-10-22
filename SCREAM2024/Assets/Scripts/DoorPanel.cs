using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorPanel : MonoBehaviour
{
    public UnityEvent enter = new UnityEvent();
    public UnityEvent exit = new UnityEvent();

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            enter.Invoke();
            Debug.Log("Yay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            exit.Invoke();
            Debug.Log("Yoo");
        }
    }
}
