using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator _animator;
    private int _animIDActive;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        AssignAnimationIDs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void open()
    {
        _animator.SetBool(_animIDActive, true);
    }

    private void AssignAnimationIDs()
    {
        _animIDActive = Animator.StringToHash("Active");
    }
}
