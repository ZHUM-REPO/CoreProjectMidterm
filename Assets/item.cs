using UnityEngine;
using UnityEngine.UI;


public class item : MonoBehaviour
{
    public GameObject itemObject; 
    // public Image itemUI;

    private bool playerNear = false;
    private bool hasItem = false;

    private void Start()
    {
        // itemUI.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            hasItem = !hasItem;
            itemObject.SetActive(!hasItem);

            // itemUI.enabled = hasItem;
        }
    }
}