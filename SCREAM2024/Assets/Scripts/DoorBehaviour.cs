using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Animator>().SetBool("open", true); //open door
                                                             //OR
        this.GetComponent<Animator>().SetBool("open", false); //close door
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
