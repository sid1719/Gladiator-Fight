using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : MonoBehaviour
{
    private CharacterAnim playerAnimations;

    public GameObject attackPoint;

    private PlayerShield shield;

    private CharacterSound cs;
    void Awake()
    {
        playerAnimations = GetComponent<CharacterAnim>();
        shield = GetComponent<PlayerShield>();
        cs=GetComponentInChildren<CharacterSound>();    
    }

 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            playerAnimations.Defend(true);
            shield.ActivateShield(true);
        }
       //release defence
        if(Input.GetKeyUp(KeyCode.Q))
        {
            playerAnimations.Defend(false);
            shield.ActivateShield(false);
            playerAnimations.UnFreezeAnimation();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Random.Range(0, 2) > 0)
            {
                playerAnimations.Attack1();
                cs.Attack_1();
            }
            else
            {
                playerAnimations.Attack2();
                cs.Attack_2();
            }
        }
    }

    void Activate_AttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void Deactivate_AttackPoint()
    {
          if(attackPoint.activeInHierarchy)
        attackPoint.SetActive(false);
    }

}//class
