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

    private bool isChasing = false;
    private GameObject player;

    //private Random rnd = new Random();
    private System.Random rnd = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per framec
    void Update()
    {
        // If chasing player, implement alternative pathing
        if (isChasing)
        {
            SmootheMove(player.transform); // Replace with pathing toward player, rather than random, choose node closest to player?
        } else
        {
            SmootheMove(target.transform);
            if (CalcDistance(Creature.transform, target.transform) <= 0.1)
            {
                int index;
                // If only 1 option, take it, otherwise find new route from options
                if (neighbors.Count == 2) // 2 includes original, meaning only 1 possible target
                {
                    if (neighbors.IndexOf(target) == 0) index = 1;
                    else index = 0;
                }
                else {
                do
                    { index = (rnd.Next(1, neighbors.Count +1)) - 1; } // Get random target that doesn't involve staying still or going back
                    while (index == neighbors.IndexOf(target) || index == neighbors.IndexOf(oldTarget));
                }
                    // Could add some logic for chance to pause, or turn around but make it less likely

                oldTarget = target;
                target = (GameObject)neighbors[index];
            }
        }
    }

    private void SmootheMove(Transform Target)
    {
        // Smooth move to target
        float step = speed * Time.deltaTime;
        Creature.transform.position = Vector3.MoveTowards(Creature.transform.position, Target.position, step);
        // Add smooth rotation in direction
    }

    private float CalcDistance(Transform a, Transform b)
    {
        return Vector3.Distance(a.position, b.position);
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
