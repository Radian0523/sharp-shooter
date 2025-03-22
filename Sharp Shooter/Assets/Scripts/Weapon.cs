using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzuleFlash;

    public void Shoot(WeaponSO weaponSO)
    {
        muzzuleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
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
