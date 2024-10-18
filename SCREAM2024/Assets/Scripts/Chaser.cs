using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chaser : MonoBehaviour
{
    public GameObject Creature;

    private GameObject target;
    private ArrayList neighbors = new ArrayList();

    // Random rnd = new Random();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // when reach target, identify new target
            // if distance to target <0.01, then find new
        foreach(GameObject node in neighbors)
        {
            
        }

        // After select new target, remove old target from neighbors
        neighbors.Remove(target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Node"))
        {
            // add to list of possible targets
            neighbors.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Node") && neighbors.Contains(collision.gameObject))
        {
            // aremove from list
            neighbors.Remove(collision.gameObject);
        }
    }
}
