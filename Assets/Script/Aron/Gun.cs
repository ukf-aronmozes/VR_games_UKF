using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject projectiles;
    [SerializeField] private int power;
    [SerializeField] private GameObject shoot_point;
    [SerializeField] private GameObject grab_point;

    public void Shoot()
    {
        GameObject new_projectile = Instantiate(projectiles, shoot_point.transform.position, Quaternion.identity) as GameObject;
        new_projectile.GetComponent<Rigidbody>().AddForce(grab_point.transform.forward * power, ForceMode.VelocityChange);
        Destroy(new_projectile, 3);
    }
}
