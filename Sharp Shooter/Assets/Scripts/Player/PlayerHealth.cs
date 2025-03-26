using Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 5;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    int currentHealth;
    int gameOverVirtualCameraPriority = 20;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(amount + " damage taken");
        if (currentHealth <= 0)
        {
            weaponCamera.parent = null; // weaponCameraが、Playerと一緒に壊されないようにする
            deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
            Destroy(gameObject);
        }
    }
}
