using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponState
    {
        Idle,
        Firing,
    }
    [SerializeField] WeaponSO weaponSO;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    WeaponState currentState;
    float timeSinceLastShot = 0f;

    Weapon currentWeapon;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        // 親の中から、StarterAssetsInputsをコンポーネントに持つオブジェクトを一つ取ってきて入れる
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        currentState = WeaponState.Idle;
    }
    void Update()
    {
        HandleShoot();
    }

    void HandleShoot()
    {
        switch (currentState)
        {
            case WeaponState.Idle:
                if (!starterAssetsInputs.shoot) return;
                currentWeapon.Shoot(weaponSO);
                animator.Play(SHOOT_STRING, 0, 0f);
                currentState = WeaponState.Firing;
                break;
            case WeaponState.Firing:
                timeSinceLastShot += Time.deltaTime;
                if (timeSinceLastShot >= weaponSO.FireRate)
                {
                    timeSinceLastShot = 0;
                    currentState = WeaponState.Idle;
                    if (weaponSO.IsAutomatic) break;
                    starterAssetsInputs.ShootInput(false);
                }
                break;
        }
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        Debug.Log(weaponSO.name);
    }
}
