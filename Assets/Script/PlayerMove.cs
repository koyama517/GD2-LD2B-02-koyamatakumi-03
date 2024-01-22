using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度
    public float jumpForce = 10f; // ジャンプ力
    private bool isGrounded; // 地面に接地しているかどうかのフラグ

    Vector2 move;

    public bool isInvisible;

    public bool isRight;
    bool isMoving;
    Rigidbody2D rb;

    SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isInvisible = true;
        sprite = rb.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        /*if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }*/

        Attack attack = GetComponent<Attack>();
        if (attack != null)
        {
            isMoving = attack.isMoving;
        }

        if (!isMoving)
        {
            //rb.velocity = new Vector2(move.x * moveSpeed, rb.velocity.y);
            transform.Translate(move * moveSpeed * Time.deltaTime);
        }

    }

    public void Move(InputAction.CallbackContext context)
    {

        var v = context.ReadValue<Vector2>();

        if (v.x > 0)
        {
            isRight = true;
            sprite.flipX = true;
        }
        else if (v.x < 0)
        {
            isRight = false;
            sprite.flipX = false;
        }

        move = new Vector2(v.x, 0).normalized;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        bool jumpTrigger = context.performed;
        if (jumpTrigger && isGrounded && !isMoving)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Blood")
        {
            isInvisible = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Blood")
        {
            isInvisible = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

}