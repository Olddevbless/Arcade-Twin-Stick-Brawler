using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float playerJumpPower;
    [SerializeField] float playerDodgePower;
    [SerializeField] Vector2 playerAim;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Vector2 movement;
    [SerializeField] GameObject torso;
    Vector3 mouseToGroundPoint;

    [Header("Jump")]
    [SerializeField] float coyoteTime;
    [SerializeField] float jumpBufferTime;
    [SerializeField] float coyoteTimeCounter;
    [SerializeField] float jumpBufferCounter;
    [SerializeField] bool isGrounded;
    

    Rigidbody playerRB;
    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (playerRB.velocity.y < 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        HandleMovement();
        HandleInput();
        HandleAim();



    }
    private void FixedUpdate()
    {
       
    }

    private void HandleAim()
    {
        Vector2 mousePos = playerInput.actions["Look"].ReadValue<Vector2>();
        Ray mouseRay = Camera.main.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 50));
        RaycastHit mouseRayHit;
        if (Physics.Raycast(mouseRay, out mouseRayHit, Mathf.Infinity, (1 << 3)))
        {
            mouseToGroundPoint = mouseRayHit.point;
            Vector3 dir = torso.transform.position - mouseToGroundPoint;
            dir.y = 0;
            torso.transform.forward = dir;
        }
    }
    private void HandleInput()
    {
        //dodge
        if (playerInput.actions["Dodge"].IsPressed())
        {
            playerRB.AddForce(movement * playerDodgePower, ForceMode.Impulse);
        }
        //Jump
        if (playerInput.actions["Jump"].IsPressed() && jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            playerRB.AddForce(Vector3.up * playerJumpPower, ForceMode.Impulse);
            isGrounded = false;
        }
        if (!playerInput.actions["Jump"].IsPressed() && playerRB.velocity.y > 0)
        {
            coyoteTimeCounter = 0;
        }
        //Fire
        if (playerInput.actions["Fire"].IsPressed())
        {
            FindObjectOfType<Weapon>().SwingWeapon();
        }

    }
    private void HandleMovement()
    {
        movement = playerInput.actions["Move"].ReadValue<Vector2>();
        transform.Translate(new Vector3(movement.x * playerSpeed * Time.deltaTime, 0, movement.y * playerSpeed * Time.deltaTime));
    }
    /*private void OnMove(InputValue value)
    {

        movement = value.Get<Vector2>();

    }*/0
    /*private void OnJump(InputValue value)
    {

        if (value.isPressed == true && jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            playerRB.AddForce(Vector3.up * playerJumpPower, ForceMode.Impulse);
            isGrounded = false;

        }
        if (value.isPressed == false && playerRB.velocity.y > 0)
        {
            coyoteTimeCounter = 0;



        }
    


    }*/
    /*private void OnFire(InputValue value)
    {
        FindObjectOfType<Weapon>().SwingWeapon();
    }*/
    /*private void OnDodge(InputValue value)
    {
        playerRB.AddForce(movement * playerDodgePower, ForceMode.Impulse);
    }*/
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer ==3)
        {
            isGrounded = false;
        }
    }

}
