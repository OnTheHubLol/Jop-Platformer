using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbtd; //Defines Variables For Engines Within Unity
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f; //Sets The X Direction Variable
    [SerializeField] private float movespeed = 4f;
    [SerializeField] private float jumpforce = 7f;
    [SerializeField] private LayerMask jumpableground;
    private enum MovementState { idle, running, jumping, falling }
    private BoxCollider2D collider;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Joppy!"); //Joppylicious Debug Command That Inputs To Show The Game Has Started
        rbtd = GetComponent<Rigidbody2D>(); //Pulls The Component For The Variables
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rbtd.velocity = new Vector2(dirX * movespeed, rbtd.velocity.y);

        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            rbtd.velocity = new Vector2(rbtd.velocity.x, jumpforce);
        }

        AnimStateUpdate();
    }

    private void AnimStateUpdate()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;

        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rbtd.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rbtd.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsOnGround()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableground);
    }    
}
