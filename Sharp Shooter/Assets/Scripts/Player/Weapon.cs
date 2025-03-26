using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzuleFlash;
    [SerializeField] LayerMask interactionLayers;

    CinemachineImpulseSource impulseSource;

    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO)
    {
        muzzuleFlash.Play();
        impulseSource.GenerateImpulse();

        RaycastHit hit;
        // 最後の引数は、TriggerEnterのついたオブジェクトには、あたらないということ。その前の引数には、必ずLayerMask型のものが必要で、今回の場合、Everythingにしている
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
        {
            Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

            //同じ意味。null構文
            enemyHealth?.TakeDamage(weaponSO.Damage);
            // if (enemyHealth)
            // {
            //     enemyHealth.TakeDamage(damageAmount);
            // }
        }
    }
}
