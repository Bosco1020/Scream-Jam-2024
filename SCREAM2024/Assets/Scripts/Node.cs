using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private System.Random rnd = new System.Random();

    [SerializeField] private GameObject lightObj;
    //private Animator _animator;
    [SerializeField] private Light _light;

    private bool lightTrigger;
    private bool _hasAnimator;

    private float mod;

    [SerializeField] private float lightTriggerRange = 20.0f;

    private float norm_distance;


    // Start is called before the first frame update
    void Start()
    {
        lightObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (lightTrigger)
        {
            if (norm_distance <= 0.001) _light.intensity = 0;
            else { _light.intensity = norm_distance * mod; }
        }
        else
        {
            _light.intensity = 1;
        }
    }

    private void FixedUpdate()
    {
        generateModifier();
    }

    private void generateModifier()
    {
        mod = (rnd.Next(1, 12)) + 14;
        mod /= 20;
        // between 0.75 & 1.25
    }

    public void startFlicker(float distance)
    {
        lightTrigger = true;

        // normalise distance between 0 and 1,
        norm_distance = (distance) / lightTriggerRange - 0.1f;
        //norm_distance = 1 - norm_distance;
        if (norm_distance < 0) norm_distance = 0;
        //Debug.Log(distance+ ":" + norm_distance);
    }

    public void disableLight()
    {
        lightObj.SetActive(false);
    }

    public void enableLight()
    {
        lightObj.SetActive(true);
    }

    /*void OnBecameVisible()
    {
        lightObj.SetActive(true);
    }*/

    public void endFlicker()
    {
        lightTrigger = false;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
