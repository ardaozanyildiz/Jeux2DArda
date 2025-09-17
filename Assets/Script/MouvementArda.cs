using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementArda : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;

    private void Awake()
    {

        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(4, 4, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-4, 4, 1);

        if (Input.GetKey(KeyCode.UpArrow) && grounded)
            Jump();


        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        animator.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}

