using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;


    const string PLAYER_STRING = "Player";

    void Awake()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_STRING))
        {
            ActiveWeapon activeWeapon = other.gameObject.GetComponentInChildren<ActiveWeapon>();

            activeWeapon.SwitchWeapon(weaponSO);
            Destroy(gameObject);
        }
    }
}
