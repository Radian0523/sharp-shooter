using Cinemachine;
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
    [SerializeField] CinemachineVirtualCamera cinemachineCamera;
    [SerializeField] GameObject zoomVignette;
    FirstPersonController firstPersonController;
    Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    WeaponState currentState;
    float timeSinceLastShot = 0f;

    Weapon currentWeapon;

    bool isZoomingIn = false;
    float defaultFOV;
    float defaultRotationSpeed;

    const string SHOOT_STRING = "Shoot";



    void Awake()
    {
        // 親の中から、StarterAssetsInputsをコンポーネントに持つオブジェクトを一つ取ってきて入れる
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
    }

    void Start()
    {
        currentWeapon = GetComponentInChildren<Weapon>();
        currentState = WeaponState.Idle;
        defaultFOV = cinemachineCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }
    void Update()
    {
        HandleShoot();
        HandleZoom();
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
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, this.gameObject.transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.weaponSO = weaponSO; // 地味に勉強になるコード(weaponSOだと二種類の捉え方があってしまうが、thisを書けば特定可能になる)
        starterAssetsInputs.ZoomInput(false);
    }

    void HandleZoom()
    {
        if (!weaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom)
        {
            if (!isZoomingIn)
            {
                Debug.Log("Zooming in");
                isZoomingIn = true;
                starterAssetsInputs.ZoomInput(false);
            }
            else
            {
                Debug.Log("Not zooming in");
                isZoomingIn = false;
                starterAssetsInputs.ZoomInput(false);
            }
        }

        if (isZoomingIn)
        {
            cinemachineCamera.m_Lens.FieldOfView = weaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            firstPersonController.ChangeRotationSpeed(weaponSO.ZoomRotationSpeed);
        }
        else
        {
            cinemachineCamera.m_Lens.FieldOfView = defaultFOV;
            zoomVignette.SetActive(false);
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);

        }


    }
}
