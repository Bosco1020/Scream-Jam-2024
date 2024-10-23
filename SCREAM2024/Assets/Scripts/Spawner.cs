using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objects;

    public Transform spawn;

    public void spawnObject(int index)
    {
        Instantiate(objects[index], spawn);
    }
}
