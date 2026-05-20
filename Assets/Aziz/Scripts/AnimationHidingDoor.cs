using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class AnimationHidingDoor : MonoBehaviour
{


    Animator playerAnim;
    public bool isNear;
    public bool isInside;
    public Transform playerTransform;
    public Transform hidingSpot;
    private CharacterController characterController;


    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        characterController = player.GetComponent<CharacterController>();
        playerTransform = player.transform;
        playerAnim = player.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            if (!isInside)
            {
                EnterCloset();
            }
            else
            {
                ExitCloset();
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = false;
            if (isInside)
            {
                ExitCloset();
            }
        }
    }

    void EnterCloset()
    {
        playerAnim.SetBool("IsInside", true);
        isInside = true;

        // Move player inside the closet
        if (hidingSpot != null)
        {
            characterController.enabled = false;
            playerTransform.position = hidingSpot.position;
            playerTransform.rotation = hidingSpot.rotation;
            characterController.enabled = true;
        }
    }

    void ExitCloset()
    {
        playerAnim.SetBool("IsInside", false);
        isInside = false;
    }

    public bool CheckIfInside()
    {
        Collider[] colliders = Physics.OverlapSphere(playerTransform.position, 0.1f);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("HidingSpot"))
                return true;
        }
        return false;
    }
}