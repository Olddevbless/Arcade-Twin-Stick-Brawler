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
    Rigidbody playerRB;
    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(movement.x * playerSpeed * Time.deltaTime, 0, movement.y * playerSpeed * Time.deltaTime));
    }
    private void FixedUpdate()
    {
       
    }

    private void OnMove(InputValue value)
    {

        movement = value.Get<Vector2>();

    }
    private void OnJump(InputValue value)
    {
        playerRB.AddForce(Vector3.up * playerJumpPower, ForceMode.Impulse);
    }
    private void OnFire(InputValue value)
    {
        FindObjectOfType<Weapon>().SwingWeapon();
    }
    private void OnDodge(InputValue value)
    {
        playerRB.AddForce(movement * playerDodgePower, ForceMode.Impulse);
    }

}
