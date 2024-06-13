using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    bool canShoot = true;
    [SerializeField] float timeBetweenShots = 0.2f;

    private void OnEnable()
    {
        if(ammoSlot.GetCurrentAmmo(ammoType) <= 0)
        {
            canShoot = false;
        }
        else
        {
            canShoot = true;
        }
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0) {
            canShoot = false;
            muzzleFlash.Play();
            ProcessRayCast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            yield return new WaitForSeconds(timeBetweenShots);
            canShoot = true;
        }
    }

    void ProcessRayCast()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target == null) return;
            target.TakeDamage(damage);
        }
    }
}