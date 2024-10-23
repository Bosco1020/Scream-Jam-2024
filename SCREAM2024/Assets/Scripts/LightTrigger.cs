using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject Creature;

    [SerializeField] private bool isPlayer;

    private ArrayList allLights = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per framec
    void Update()
    {
        if(!isPlayer) messageLights();
    }

    void messageLights()
    {
        foreach (GameObject node in allLights)
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
            if (isPlayer) {
                other.gameObject.SendMessage("enableLight");
            }
            else
            {
                allLights.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Node"))
        {
            if (isPlayer) {
                other.gameObject.SendMessage("disableLight");
            }
            else {
                if(allLights.Contains(other.gameObject))
                {
                    other.gameObject.SendMessage("endFlicker");
                    allLights.Remove(other.gameObject);
                }
            }
        }
    }
}
