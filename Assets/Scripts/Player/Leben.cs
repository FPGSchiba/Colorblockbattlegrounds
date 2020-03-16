using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leben : MonoBehaviour
{
    [Header("Variablen")]
    [SerializeField]
    [GreyOut]
    float health;
    [SerializeField]
    float MaxHealth;
    [SerializeField]
    [GreyOut]
    float damage;
    [SerializeField]
    [GreyOut]
    bool isDead;

    [Header("Referenzen")]
    [SerializeField]
    GameObject Healthbar;
    [SerializeField]
    Slider slider;
    [SerializeField]
    GameObject Player;

    GameManager manager;
    SchussGeschwindigkeit schuss;

    void Start()
    {
        isDead = false;
        health = MaxHealth;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        slider.value = CalculateHealthforBar();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "cube" && col.gameObject.layer == 9)
        {
            schuss = col.gameObject.GetComponent<SchussGeschwindigkeit>();
            OnSchussEnter(schuss.SchussSpeed(), manager.isEnemy(schuss.shooter, Player));
        }
    }

    void Update()
    {
        slider.value = CalculateHealthforBar();

        if (health <= 0)
        {
            Dead();
        }

        if (health > MaxHealth)
        {
            health = MaxHealth;
        }
    }

    public float CalculateHealthforBar()
    {
        return health / MaxHealth;
    }

    void Dead()
    {
        if (!isDead)
        {
            isDead = true;
            manager.OnDeathPlayer();
        }
    }

    public void OnSchussEnter(float Speed, bool isEnemy)
    {
        if (isEnemy)
        {
            damage = 0;

            float z = 498 - Speed;
            Speed = Speed + z;

            damage = (Speed / 19.62f).TwoDecimals();

            if (damage < 0)
            {
                damage = damage * -1;
            }

            health = health - damage;
        }
    }

    public void PlayerRespawn()
    {
        isDead = false;
        health = MaxHealth;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        slider.value = CalculateHealthforBar();
    }
}
