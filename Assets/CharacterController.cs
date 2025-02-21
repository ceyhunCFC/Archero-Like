using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public FloatingJoystick joystick;
    private Rigidbody rb;
    private Vector3 input;
    public int Speed = 3;
    private Animator animator;
    [SerializeField] public int state = 0;

    EnemyFinder enemyFinder;
  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        enemyFinder = GetComponent<EnemyFinder>();
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
            enemyFinder.targetEnemy = null;
            state = 1;
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
        else if (enemyFinder.targetEnemy == null)
        state = 0;

        animator.SetInteger("State",state);
    }
}
