using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Player Weapons")]
    [Tooltip("This is the list of all the weapons that your player uses")]
    public List<Weapon> weaponList;
    [Tooltip("This is the current weapon that the player is using")]
    public Weapon weapon;
    private Vector2 lastKnownDirection;
    [Tooltip("The coolDown before you can attack again")]
    public float coolDown = 0.4f;

    private bool canAttack = true;
    public GameObject player;

    private void Start()
    {
        if (weapon == null && weaponList.Count > 0)
        {
            weapon = weaponList[0];
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//Here is where you can hit the "1" key on your keyboard to activate this weapon
        {
            if (weaponList.Count > 0)
            {
                switchWeaponAtIndex(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))//Remove this if you don't have multiple weapons
        {
            if (weaponList.Count > 1)
            {
                switchWeaponAtIndex(1);
            }
        }
    }

    public void Attack(Vector3 scale)
    {
        //This is where the weapon is rotated in the right direction that you are facing
        if (weapon && canAttack)
        {
            if (weapon.weaponType == Weapon.WeaponType.Melee)
            {
                weapon.WeaponStart();
            }

            else
            {
                GameObject projectile = Instantiate(weapon.projectile, weapon.shootPosition.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().SetValues(weapon.duration, weapon.alignmnent, weapon.damageValue);
                projectile.transform.localScale = new Vector3( projectile.transform.localScale.x * (scale.x / Mathf.Abs(scale.x)), projectile.transform.localScale.y, projectile.transform.localScale.z);
                //projectile.transform.localScale = new Vector3(projectile.transform.localScale.x, projectile.transform.localScale.y, projectile.transform.localScale.z);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (player.transform.localScale.x > 0f)
                {
                    rb.AddForce(weapon.direction * weapon.force);
                }
                else
                {
                    rb.AddForce(new Vector2(-1,0) * weapon.force);
                }
                


            }

            StartCoroutine(CoolDown());
        }
    }

    public void StopAttack()
    {
        if (weapon)
        {
            weapon.WeaponFinished();
        }
    }

    public void switchWeaponAtIndex(int index)
    {
        //Makes sure that if the index exists, then a switch will occur
        if (index < weaponList.Count && weaponList[index])
        {
            //Deactivate current weapon
            weapon.gameObject.SetActive(false);

            //Switch weapon to index then activate
            weapon = weaponList[index];
            weapon.gameObject.SetActive(true);
        }

    }

    private IEnumerator CoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
    }
}
