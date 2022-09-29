using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static bool hurt = false,
                action = false,
                movForward,
                movBackward,
                turno=true;

    public float timer = 0.0f,
             timerPunch = 0.0f,
             dmgReceived=0,
             i = 0.0f;

    public Transform target,respawn;

    public GameObject punch;
    public GameObject punchSpawn;

    
    
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        //The knight goes in front of the enemy and attacks him, then he turns back to his initial point
        if (action == true)
        {
            movForward = true;
            GetComponent<Animator>().SetBool("Run", true);
            float step = 5f * Time.deltaTime;

            if (movForward == true)
            {
                if (Vector3.Distance(transform.position, target.position) > 0.9f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                }
                else
                {
                    GetComponent<Animator>().SetBool("Attack", true);
                    timer += Time.deltaTime;

                    if (timer > (1.05 % 60))
                    {
                        if (i == 0)
                        {
                            var temp = Instantiate(punch, punchSpawn.transform.position, Quaternion.identity);
                            temp.transform.right = punchSpawn.transform.right;

                            if (PlayerStats.strength > 0)       //Validation of the strength points
                            {
                                PlayerStats.durStrength--;

                                if (PlayerStats.durStrength == 0)
                                {
                                    PlayerStats.strength = 0;
                                }
                            }
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
                            PlayerStats.dmgCaused = 0;
                        }
                    }

                }
            }

            movForward = false;

            if (movBackward == true)
            {

                GetComponent<SpriteRenderer>().flipX = movBackward;

                if (Vector3.Distance(transform.position, respawn.position) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, respawn.position, step * 1.75f);
                }
                else
                {
                    movBackward = false;
                    GetComponent<SpriteRenderer>().flipX = false;
                    GetComponent<Animator>().SetBool("Run", false);
                    movBackward = false;
                    action = false;
                }

            }

        }


        //The name says all xD
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


        //CounterAttack action
        if (GetComponent<Animator>().GetBool("CounterDamage") == true)
        {
            GetComponent<Animator>().SetBool("Attack", true);
            timer += Time.deltaTime;

            if (timer > (1.05 % 60))
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
                    GetComponent<Animator>().SetBool("CounterDamage", false);
                    timerPunch = 0;
                    timer = 0;
                    i = 0;
                }
            }

        }

        //Finish the turn
        // if (FinishTurn.finish == true)
        // {
        //     if (PlayerStats.durCounter == 0)
        //     {
        //         PlayerStats.counterDmg = 0;
        //     }
        //     PlayerStats.mana = 5;
        //     RandomSpawn.quantity--;            
        //     if (Enemy.turno > 1)
        //     {
        //         Enemy.turno = 0;
        //     }
        //     Enemy.action = true;
            
        //     FinishTurn.finish = false;
        // }

    }


    

    //When the knight collides with the invisible punch
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Punch")
        {
            if ((PlayerStats.armor - (EnemyStats.dmgCaused + EnemyStats.strength)) < 0) //First the armor absorbs the damage
            {
                dmgReceived = (EnemyStats.dmgCaused + EnemyStats.strength)-PlayerStats.armor;
                PlayerStats.health = PlayerStats.health - dmgReceived;
                dmgReceived = 0;
            }
            else
            {
                PlayerStats.armor -= (EnemyStats.dmgCaused + EnemyStats.strength);
            }

            if (PlayerStats.health >= 1) //Validation if the knight is dead or not
            {
                hurt = true;

                if (PlayerStats.durCounter > 0) //Counter attack
                {
                    PlayerStats.durCounter--;
                    GetComponent<Animator>().SetBool("CounterDamage", true);
                    GetComponent<Animator>().SetBool("Hurt", false);                    
                }
            }
            else if (PlayerStats.health <= 0)
            {
                GetComponent<Animator>().SetBool("Hurt", true);
                GetComponent<Animator>().SetBool("Die", true);
            }
        }
    }


}
