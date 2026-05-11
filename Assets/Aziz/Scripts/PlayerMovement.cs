using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // movement
    public CharacterController characterController;
    public float speed = 3f;
    public float runningSpeed = 7f;
    public Vector3 velocity;
    public const float gravity = -9.8f;
    public bool grounded;
    public float jumpHeight = 7f;
    public Transform target;
    public PlayerStamina playerStamina;

    // audio
    public AudioSource walking;
    public AudioSource running;
    public AudioSource jump;
    private bool isMoving;
    private bool isRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


            grounded = characterController.isGrounded;
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");


        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 move = transform.right * Horizontal + transform.forward * Vertical;


        characterController.Move(move * speed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);


        if (Input.GetKey(KeyCode.LeftShift) && playerStamina.currentStamina > 0f && !playerStamina.isExhausted)
        {
            playerStamina.Stamina();
            characterController.Move(move * runningSpeed * Time.deltaTime);
        }

        else
        {
            playerStamina.Regain();
        }

        if (Input.GetKey(KeyCode.W))
        {
            isMoving = true;
            isRunning = false;
        }
        else
        {
            isMoving = false;
            isRunning = false;
        }



        // Debug.Log(grounded);


        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;

        }

        velocity.y += gravity * Time.deltaTime;


        /*if (Input.GetKey(KeyCode.Space) && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jump.Play();

        }*/

        AudioHandler();
    }


    void AudioHandler()
    {
        if (!isMoving)
        {
            if (walking != null && walking.isPlaying) walking.Stop();
            if (running != null && running.isPlaying) running.Stop();
            return;
        }

        if (isRunning)
        {
            if (walking != null && walking.isPlaying) walking.Stop();
            if (running != null && !running.isPlaying) running.Play();
        }
        else
        {
            if (walking != null && !walking.isPlaying) walking.Play();
            if (running != null && running.isPlaying) running.Stop();
        }
    }
}
