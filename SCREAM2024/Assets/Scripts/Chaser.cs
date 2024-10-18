using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chaser : MonoBehaviour
{
    public GameObject Creature;

    public float speed;

    public GameObject target;
    private GameObject oldTarget;
    private ArrayList neighbors = new ArrayList();

    //private Random rnd = new Random();
    private System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per framec
    void Update()
    {
        // when reach target, identify new target
        // if distance to target <0.01, then find new
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);

        // Smooth move to target
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            // Add smooth rotation in direction

        if (distance <= 0.1)
        {
            // After select new target, remove old target from neighbors
            //neighbors.Remove(target);

            int index;
            // If only 1 option, take it, otherwise find new route from options
            if (neighbors.Count == 2) // 2 includes original, meaning only 1 possible target
            {
                if (neighbors.IndexOf(target) == 0) index = 1;
                else index = 0;
            }
            else {
            do
                { index = (rnd.Next(1, neighbors.Count +1)) - 1; } // Get random target that's doesn't ivolve staying still or going back
                while (index == neighbors.IndexOf(target) || index == neighbors.IndexOf(oldTarget));
            }
                // Could add some logic for chance to pause, or turn around but make it less likely

            oldTarget = target;
            target = (GameObject)neighbors[index];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered TR");
        if (other.gameObject.CompareTag("Node"))
        {
            // add to list of possible targets
            neighbors.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit TR");
        if (other.gameObject.CompareTag("Node") && neighbors.Contains(other.gameObject))
        {
            // aremove from list
            neighbors.Remove(other.gameObject);
        }
    }
}
