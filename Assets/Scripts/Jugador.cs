using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float jumpForce;
    public GameManager gameManager;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private bool isJumping=false;
    private bool isDead=false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool userAction = Input.GetKeyDown(KeyCode.Space);
        if (userAction && !isJumping && !isDead)
        {
            animator.SetBool("IsJumping", true);
            rigidbody2D.AddForce(new Vector2 (0f, jumpForce));
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Suelo")
        {
            Debug.Log("Jump: " + jumpForce.ToString());
            animator.SetBool("IsJumping", false);
            isJumping = false;
        }

        if (collision.gameObject.tag == "Obstaculo")
        {
            gameManager.gameOver = true;
            isDead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            gameManager.SendMessage("IncreasePoints");
        }
    }
}
