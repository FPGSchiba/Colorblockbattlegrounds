using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BotHealth : MonoBehaviour
{
    [Header("Variablen")]
    public float health;
    public float MaxHealth;
    [GreyOut]
    public float damage;

    [Header("Referenzen")]
    public GameObject Healthbar;
    public Slider slider;
    public GameObject Enemy;
    GameManager manager;
    SchussGeschwindigkeit schuss;
    [SerializeField]
    BlockSpawnDeath death;
    [SerializeField]
    EnemyAI Ai;

    void Start()
    {
        Healthbar.SetActive(false);
        health = MaxHealth;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        slider.value = CalculateHealthforBar();
    }

    public void Respawn()
    {
        Healthbar.SetActive(false);
        health = MaxHealth;
        Ai.RepawnBot();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "cube" && col.gameObject.layer == 9)
        {
            schuss = col.gameObject.GetComponent<SchussGeschwindigkeit>();
            OnSchussEnter(schuss.SchussSpeed(), manager.isEnemy(schuss.shooter, Enemy));  
        }
    }

    void Update()
    {
        slider.value = CalculateHealthforBar();

        if(health < MaxHealth)
        {
            Healthbar.SetActive(true);
        }

        if(health <= 0)
        {
            Dead();
        }

        if(health > MaxHealth)
        {
            health = MaxHealth;
        }
    }

    public float CalculateHealthforBar()
    {
        return health / MaxHealth;
    }

    public void OnSchussEnter(float Speed, bool isEnemy)
    {
        if (isEnemy)
        {
            damage = 0;

            damage = (Speed / 19.62f).TwoDecimals();

            if(damage < 0)
            {
                damage = damage * -1;
            }

            health = health - damage;
        }

        NavMeshAgent agent = Enemy.GetComponent<NavMeshAgent>();
        agent.SetDestination(schuss.shooter.transform.position);
    }

    void Dead()
    {
        manager.OnDeathBot(Enemy, 10);
    }
}
