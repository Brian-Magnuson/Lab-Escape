
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Alignment
    {
        Player,
        Enemy,
        Environment
    }

    public enum WeaponType
    {
        Melee,
        Ranged
    }

    public Alignment alignmnent = Alignment.Player;
    public WeaponType weaponType = WeaponType.Melee;

    public int damageValue;

    [Header("For Both Melee & Range")]
    public Collider2D collider;

    [Header("Only for Ranged Weapon")]
    public GameObject projectile;
    [HideInInspector]
    public Vector2 direction = new Vector2(1,0);
    public float force = 100f;
    public float duration = 10f;
    public Transform shootPosition;



    public bool flipWeapon = false;

    public void WeaponStart()
    {
        collider.enabled = true;
    }

    public void WeaponFinished()
    {
        collider.enabled = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    //private void OnValidate()
    //{
    //    direction = direction.normalized;
    //}










}
