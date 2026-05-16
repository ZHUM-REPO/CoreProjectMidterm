using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float baseSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float sprintSpeed = 7f;
    float speedBoost = 1f;
    Vector3 velocity;
    PlayerStamina playerStamina;



    void Start()
    {
        playerStamina = GetComponent<PlayerStamina>();
    }

    void Update()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * baseSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && !playerStamina.isExhausted)
        {
            playerStamina.Stamina();
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }



        else
        {
            playerStamina.Regain();
        }
    }
}
