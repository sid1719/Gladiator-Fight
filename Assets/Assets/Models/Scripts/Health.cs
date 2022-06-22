using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public float health = 100f;
    private float x_Death = -90f;
    public float death_Smooth = 0.9f;
    public float rotate_Time = 0.23f;
    private bool playerDied;
  
    public bool isPlayer;

    [HideInInspector]
    public bool shieldActivated;
    [SerializeField]
    private Image health_UI;

    private CharacterSound cs;

    private void Awake()
    {
        cs=GetComponentInChildren<CharacterSound>();
    }
    public void Update()
    {
        if(playerDied)
        {
            RotateAfterDeath();
        }
    }
    public void RotateAfterDeath()
    {
        transform.eulerAngles = new Vector3(Mathf.Lerp(transform.eulerAngles.x, x_Death, Time.deltaTime * death_Smooth),
            transform.eulerAngles.y,transform.eulerAngles.z);
    }

    IEnumerator AllowRotate()
    {
        playerDied = true;
        yield return new WaitForSeconds(rotate_Time);
        playerDied=false;
    }
    public void ApplyDamage(float damage)
    {
        if(shieldActivated)
        {
            return;
        }
        health -= damage;
       
        if(health_UI!=null)
        {
            health_UI.fillAmount = health/100f;
        }
        if(health<=0)
        {
            cs.Die();
            GetComponent<Animator>().enabled = false;
            StartCoroutine(AllowRotate());

            if(isPlayer)
            {
                GetComponent<PlayerMove>().enabled = false;
                GetComponent<PlayerAttackInput>().enabled = false;

                Camera.main.transform.SetParent(null);//player is not the parent of the camera anymore
                GameObject.FindGameObjectWithTag(Tags.ENEMY_TAG).GetComponent<EnemyController>().enabled=false;
            }
            else
            {
                GetComponent<EnemyController>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
            }
        }

    }


}

