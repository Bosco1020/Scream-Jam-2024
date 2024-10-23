using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CodeGenerator : MonoBehaviour
{
    public Passcode passcode;

    [SerializeField] private GameObject[] spawners;
    private System.Random rnd = new System.Random();

   

    private int[] chosenSpawners = new int[4];
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(false);
        }

        passcode.clearCode();
        generateCode();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void generateCode()
    {
        chooseSpawners();
        for (int i = 0; i <= 3; i++)
        {
            int digit = (rnd.Next(1, 11)) -1; // 0-9

            passcode.appendCode(digit); //add to code
            // spawn with code
            spawnDigit(digit, i);
        }
    }

    private void chooseSpawners()
    {
        List<int> intList = new List<int>();
        for (int i = 0; i < spawners.Length; i++)
        {
            intList.Add(i);
        }

        intList = intList.OrderBy(tyz => System.Guid.NewGuid()).ToList();

        for (int i = 0; i < 4; i++)
        {
            chosenSpawners[i] = intList.ElementAt(i);
        }


        Debug.Log(chosenSpawners[0]);
        Debug.Log(chosenSpawners[1]);
        Debug.Log(chosenSpawners[2]);
        Debug.Log(chosenSpawners[3]);
    }

    private void spawnDigit(int digit, int index)
    {
        // Choose spawner, spawn digit, is position index in string
        //Debug.Log(spawners[spawner]);

        spawners[chosenSpawners[index]].SetActive(true);
        spawners[chosenSpawners[index]].GetComponent<Spawner>().spawnObject(digit, index);
    }
}
