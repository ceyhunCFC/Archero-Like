using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public FloatingJoystick joystick;
    private Rigidbody rb;
    private Vector3 input;
    public int Speed = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = joystick.Horizontal;
        input.z = joystick.Vertical;
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(input.x * Speed, rb.velocity.y, input.z * Speed);
        
        if(input.x != 0 || input.z != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

    }
}
