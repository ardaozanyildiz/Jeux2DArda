using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementArda : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;
    public CoinManager cm;
    private float horizontalInput;

    public bool canClimb = false;


    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;
    private AudioSource audioSource;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(jumpSound);
        body.velocity = new Vector2(body.velocity.x, speed);
        animator.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
        }

        if (other.CompareTag("Climb"))
        {
            canClimb = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Climb"))
        {
            canClimb = false;
        }
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && grounded;
    }
}
