using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Chaser : MonoBehaviour
{
    public GameObject Creature;

    public float speed;
    public float chaseSpeed;

    public GameObject target;
    private GameObject oldTarget;
    private ArrayList neighbors = new ArrayList();

    private bool isChasing = false;
    [SerializeField] private GameObject player;

    //private Random rnd = new Random();
    private System.Random rnd = new System.Random();

    public UnityEvent gameOver = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per framec
    void Update()
    {
        // If chasing player, implement alternative pathing
        if (isChasing)
        {
            //SmootheMove(player.transform); // Replace with pathing toward player, rather than random, choose node closest to player?
            chasePathing();
        } else
        {
            randomPathing();
        }
        SmootheMove(target.transform);
    }

    private void randomPathing()
    {
        if (CalcDistance(Creature.transform, target.transform) <= 0.01)
        {
            int index;
            // If only 1 option, take it, otherwise find new route from options
            if (neighbors.Count == 2) // 2 includes original, meaning only 1 possible target
            {
                if (neighbors.IndexOf(target) == 0) index = 1;
                else index = 0;
            }
            else
            {
                do
                { index = (rnd.Next(1, neighbors.Count + 1)) - 1; } // Get random target that doesn't involve staying still or going back
                while (index == neighbors.IndexOf(target) || index == neighbors.IndexOf(oldTarget));
            }
            //Debug.Log(index);
            // Could add some logic for chance to pause, or turn around but make it less likely

            Creature.transform.LookAt(target.transform);

            oldTarget = target;
            target = (GameObject)neighbors[index];
        }
    }

    private void chasePathing()
    {
        if (CalcDistance(Creature.transform, target.transform) <= 0.01)
        {
            int index = 0;
            float distance =9999999999;

            foreach(GameObject node in neighbors)
            {
                float comparison = CalcDistance(player.transform, node.transform);

                if (comparison < distance)
                {
                    distance = comparison;
                    index = neighbors.IndexOf(node);
                }
            }

            Creature.transform.LookAt(target.transform);

            oldTarget = target;
            target = (GameObject)neighbors[index];
        }
    }

    private void SmootheMove(Transform Target)
    {
        // Smooth move to target
        float step;
        if (isChasing) step = chaseSpeed * Time.deltaTime;
        else step = speed * Time.deltaTime;

        Creature.transform.position = Vector3.MoveTowards(Creature.transform.position, Target.position, step);

        // Add smooth rotation in direction
        Vector3 relativePos = Target.position - Creature.transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.Slerp(Creature.transform.rotation, Quaternion.LookRotation(relativePos, Vector3.up), step);
        Creature.transform.rotation = rotation;
    }

    private float CalcDistance(Transform a, Transform b)
    {
        return Vector3.Distance(a.position, b.position);
    }

    public void beginChasing(bool state)
    {
        isChasing = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Node"))
        {
            // add to list of possible targets
            neighbors.Add(other.gameObject);
            //Debug.Log(other.gameObject);
            //Debug.Log("N = " + neighbors.Count);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // send game over if encounter player
            //other.gameObject.SendMessage("gameOver");
            gameOver.Invoke();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Node") && neighbors.Contains(other.gameObject))
        {
            // aremove from list
            neighbors.Remove(other.gameObject);
        }
    }
}
