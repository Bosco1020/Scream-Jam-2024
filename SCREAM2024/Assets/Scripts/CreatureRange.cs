using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureRange : MonoBehaviour
{
    public GameObject Creature;

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
        messageLights();
    }

    void messageLights()
    {
        foreach (GameObject node in neighbors)
        {
            node.SendMessage("startFlicker", CalcDistance(Creature.transform, node.transform));
        }
    }

    private float CalcDistance(Transform a, Transform b)
    {
        return Vector3.Distance(a.position, b.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Node"))
        {
            neighbors.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Node") && neighbors.Contains(other.gameObject))
        {
            other.gameObject.SendMessage("endFlicker");
            neighbors.Remove(other.gameObject);
        }
    }
}
