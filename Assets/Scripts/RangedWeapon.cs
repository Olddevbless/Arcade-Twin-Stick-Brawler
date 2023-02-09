using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [SerializeField] GameObject playerAim;
    [SerializeField] float projectileForce;
    [SerializeField] int projectileDamage;
    [SerializeField] GameObject[] projectiles;
    //[SerializeField] ParticleSystem[] projectiles;
    //ParticleSystem.EmissionModule emissionModule;

    private void Awake()
    {
        //emissionModule = projectiles[0].GetComponent<ParticleSystem>().emission;
    }
    private void Update()
    {
        transform.transform.forward = (FindObjectOfType<PlayerMovement>().mouseToGroundPoint - playerAim.transform.position);
    }
    public void FireWeapon()
    {
        GameObject projectile =Instantiate(projectiles[0], this.transform.position, Quaternion.identity);
        Vector3 aim = FindObjectOfType<PlayerMovement>().mouseToGroundPoint - playerAim.transform.position;
        aim.y = 0;
        projectile.GetComponent<Rigidbody>().AddForce(aim*projectileForce, ForceMode.Impulse);

        //emissionModule.enabled = true;
    }
    public void StopFiring()
    {
        //emissionModule.enabled = false;
    }
}
