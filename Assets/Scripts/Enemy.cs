using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static bool hurt = false,
                action = false,
                movForward,
                movBackward;

    public Transform target, respawn;

    public float timer = 0.0f,
                 timerPunch = 0.0f,
                 i = 0.0f,
                j = 0.0f,
                dmgReceived = 0,
                strength = 1;

    public static int turno = 0;

    public GameObject punch;
    public GameObject punchSpawn;


    // Update is called once per frame
    void Update()
    {
        if (turno == 0)
        {
            if (action == true)
            {
                //if (EnemyStats.durHemo >= 1 && j == 0)
                //{
                    //EnemyStats.health -= EnemyStats.hemo;
                    //j++;
                    //if (EnemyStats.health >= 1)
                    //{
                        //hurt = true;
                    //}
                    //else if (EnemyStats.health <= 0)
                    //{
                        //GetComponent<Animator>().SetBool("Hurt", true);
                        //GetComponent<Animator>().SetBool("Die", true);
                        //action = false;
                    //}

                    //EnemyStats.durHemo--;

                    //if (EnemyStats.durHemo <= 0)
                    //{
                        //EnemyStats.hemo = 0;
                    //}
                //}


                //Moving until the Knight reaches the attack point
                movForward = true;
                GetComponent<Animator>().SetBool("Run", true);
                float step = 5f * Time.deltaTime;

                if (movForward == true)
                {
                    if (Vector3.Distance(transform.position, target.position) > 0.01f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                    }
                    else
                    {
                        //Attack
                        GetComponent<Animator>().SetBool("Attack", true);
                        timer += Time.deltaTime;
                        if (timer > (0.7 % 60))
                        {
                            if (i == 0)
                            {
                                var temp = Instantiate(punch, punchSpawn.transform.position, Quaternion.identity);
                                temp.transform.right = punchSpawn.transform.right;
                                i++;
                            }
                            timerPunch += Time.deltaTime;
                            if (timerPunch > (0.7 % 60))
                            {
                                GetComponent<Animator>().SetBool("Attack", false);
                                movBackward = true;
                                timerPunch = 0;
                                timer = 0;
                                i = 0;
                            }
                        }
                    }

                }

                movForward = false;

                if (movBackward == true)
                {

                    GetComponent<SpriteRenderer>().flipX = false;

                    if (Vector3.Distance(transform.position, respawn.position) > 0.1f)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, respawn.position, step * 1.75f);
                    }
                    else
                    {
                        movBackward = false;
                        GetComponent<SpriteRenderer>().flipX = true;
                        GetComponent<Animator>().SetBool("Run", false);
                        movBackward = false;
                        action = false;
                        j = 0;
                        Player.turno = true;                        
                        turno++;
                    }                  
                }              
            }           
        }
        else if (turno == 1)
        {

            if (action == true)
            {
                EnemyStats.strength+=strength;
                strength++;
                turno++;
                action = false;
            }           
            Player.turno = true;
        }
        
        
        if (hurt == true)
        {
            GetComponent<Animator>().SetBool("Hurt", true);
            timer += Time.deltaTime;
            if (timer > (0.9 % 60))
            {
                GetComponent<Animator>().SetBool("Hurt", false);
                timer = 0f;
                hurt = false;
            }
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Punch")
        {
            if (Player.action == true)
            {
                dmgReceived = (PlayerStats.dmgCaused + PlayerStats.strength);
            }
            else
            {
                dmgReceived = (PlayerStats.counterDmg);
            }

            EnemyStats.health -= dmgReceived;
            if(EnemyStats.health>=1)
            {
                hurt = true;
            }
            else if (EnemyStats.health <= 0)
            {
                GetComponent<Animator>().SetBool("Hurt", true);
                GetComponent<Animator>().SetBool("Die", true);
            }
        }

    }




}
