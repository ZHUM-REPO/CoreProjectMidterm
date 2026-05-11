using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina;

    private float drainRate = 20f;
    private float regenRate = 25f;

    public bool isExhausted = false;

    public bool canRun = false;

    void Start()
    {
        currentStamina = maxStamina;
    }

    public void Stamina()
    {
        
            canRun = true;  

            currentStamina -= drainRate * Time.deltaTime;


            if (currentStamina <= 0f)
            {
                currentStamina = 0f;
            isExhausted = true;
            canRun = false;
        }
    }

    public void Regain()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += regenRate * Time.deltaTime;

            if (currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }

            if (isExhausted && currentStamina >= maxStamina)
            {
                isExhausted = false;
            }
        }
    }
}