using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Animator animator;
    StarterAssetsInputs starterAssetsInputs;
    [SerializeField] int damageAmount = 1;
    [SerializeField] ParticleSystem muzzuleFlash;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        // 親の中から、StarterAssetsInputsをコンポーネントに持つオブジェクトを一つ取ってきて入れる
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }
    void Update()
    {
        if (!starterAssetsInputs.shoot) return;

        muzzuleFlash.Play();
        animator.Play(SHOOT_STRING, 0, 0f);
        starterAssetsInputs.ShootInput(false);

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            //同じ意味。null構文
            enemyHealth?.TakeDamage(damageAmount);
            // if (enemyHealth)
            // {
            //     enemyHealth.TakeDamage(damageAmount);
            // }
        }
    }
}
