using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objects;

    public GameObject[] objIndex;

    public Transform spawn;

    public void spawnObject(int digit, int index)
    {
        Instantiate(objects[digit], spawn);

        Instantiate(objIndex[index], transform);
    }
}
