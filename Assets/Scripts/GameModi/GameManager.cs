using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Dictionaries")]
    [SerializeField]
    [GreyOut]
    public Dictionary<GameObject, string> Teams;
    [SerializeField]
    [GreyOut]
    public Dictionary<string, float> Kills;


    [Header("Variablen")]
    [SerializeField]
    [GreyOut]
    bool isRed;
    [SerializeField]
    [GreyOut]
    float PointsRed;
    [SerializeField]
    [GreyOut]
    float PointsBlue;
    [SerializeField]
    LayerMask PLayerLayer;
    [SerializeField]
    [GreyOut]
    float Deletedblocks;
    [SerializeField]
    [GreyOut]
    public static bool isDead = false;
    [SerializeField]
    float Streak5 = 2;
    [SerializeField]
    float Streak10 = 5;
    [SerializeField]
    float Streak15 = 13;
    [SerializeField]
    float Streak30 = 80;
    [SerializeField]
    [GreyOut]
    float Killcount = 0;
    GameObject[] AllPlayers;
    string[] Keys;

    [Header("Referenzen")]
    [SerializeField]
    GameObject DeathScreen;
    [SerializeField]
    Text PunkteAusgabe;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject Blocks;
    [SerializeField]
    Transform BlockRespawn;
    [SerializeField]
    Level level;

    void Start()
    {
        Kills = new Dictionary<string, float>();
        Teams = new Dictionary<GameObject, string>();
        AllPlayers = GameObject.FindGameObjectsWithTag("Player");
        isRed = false;

        foreach(GameObject go in AllPlayers)
        {
            if (isRed)
            {
                Teams.Add(go, "red");
                isRed = !isRed;
            }
            else
            {
                Teams.Add(go, "blue");
                isRed = !isRed;
            }
        }
    }

    public bool isEnemy(GameObject shooter, GameObject hitted)
    {
        bool sos = false;

        if (Teams.ContainsKey(shooter) && Teams.ContainsKey(hitted))
        {
            string TeamShooter = Teams[shooter];
            string TeamHitted = Teams[hitted];

            if(TeamShooter != TeamHitted)
            {
                sos = true;
            }
            else
            {
                sos = false;
            }
        }

        return sos;
    }

    public void OnDeathBot(GameObject killed, float waitSeconds, GameObject killer)
    {
        level.ExpErhöhen(10f);
        killed.SetActive(false);
        killed.GetComponent<EnemyAI>().streak = 0;
        OnKilled(killer);
        StartCoroutine(DeathWait(waitSeconds, killed));

        if(NeedTeam(killed) == "red")
        {
            PointsBlue++;
        }
        else
        {
            PointsRed++;
        }
    }

    public IEnumerator DeathWait(float waitSeconds, GameObject Bot)
    {
        yield return new WaitForSeconds(waitSeconds);
        Bot.SetActive(true);
        BotHealth health = Bot.GetComponent<BotHealth>();
        health.Respawn();
    }

    string NeedTeam(GameObject Bot)
    {
        return Teams[Bot];
    }

    public void OnDeathPlayer()
    {
        isDead = true;
        DeathScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        PointsRed++;
    }

    private void Update()
    {
        PunkteAusgabe.text = ("Rot: " + PointsRed + " / Blau: " + PointsBlue);

        if(PointsBlue >= 100)
        {
            PunkteAusgabe.text = "Blau hat gewonnen";
            Time.timeScale = 0f;
        }
        else if(PointsRed >= 100)
        {
            PunkteAusgabe.text = "Rot hat gewonnen";
            Time.timeScale = 0f;
        }
    }

    public void OnPlayerRespawn()
    {
        isDead = false;
        Debug.Log("Yeet");
        DeathScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Transform Respawn = GameObject.Find("RespawnPoint").GetComponent<Transform>();
        Player.transform.position = Respawn.position;
        Leben leben = Player.GetComponent<Leben>();

        leben.PlayerRespawn();
    }

    public Transform GetTarget(GameObject thisGameobject, GameObject NonFocus)
    {
        float lowestDist = 1000;
        GameObject nearestEnemy = NonFocus;

        Collider[] colls = Physics.OverlapSphere(thisGameobject.transform.position, 100000000, PLayerLayer);
        foreach(Collider col in colls)
        {
            if(col.gameObject.tag == "Player")
            {
                if (isEnemy(col.gameObject, thisGameobject))
                {
                    float dist = Vector3.Distance(thisGameobject.transform.position, col.gameObject.transform.position);

                    if (dist < lowestDist)
                    {
                        lowestDist = dist;
                        nearestEnemy = col.gameObject;
                    }
                }
            }
        }

        return nearestEnemy.transform;
    }

    public void BlockDeleted(float amount)
    {
        Deletedblocks = Deletedblocks + amount;

        if(Deletedblocks >= 50)
        {
            float count = 0;
            Deletedblocks = 0;

            do
            {
                GameObject paum = Instantiate(Blocks, BlockRespawn, true);
                count++;
            } while (count < 50);
        }
    }

    public string GetTeam(GameObject typ)
    {
        string DudeTeam = "keins";

        if (typ != null)
        {

            if (Teams.ContainsKey(typ))
            {
                DudeTeam = NeedTeam(typ);
            }
        }
        return DudeTeam;
    }

    public void OnKilled(GameObject killer)
    {
        string cut = "sos";

        if(!Regex.IsMatch(killer.name, "Player"))
        {
            cut = Regex.Replace(killer.name, ".+(", "");
            cut = Regex.Replace(cut, ")", "");
            Killcount++;
            cut = cut + "_" + Killcount;
            killer.GetComponent<EnemyAI>().Keys.Add(cut);
            EnemyAI ai = killer.GetComponent<EnemyAI>();
            ai.streak++;

            if (ai.streak == 5)
            {
                if (NeedTeam(killer) == "red")
                {
                    PointsRed = PointsRed + Streak5;
                }
                else
                {
                    PointsBlue = PointsBlue + Streak5;
                }
            }
            else if (ai.streak == 10)
            {
                if (NeedTeam(killer) == "red")
                {
                    PointsRed = PointsRed + Streak10;
                }
                else
                {
                    PointsBlue = PointsBlue + Streak10;
                }
            }
            else if (ai.streak == 15)
            {
                if (NeedTeam(killer) == "red")
                {
                    PointsRed = PointsRed + Streak15;
                }
                else
                {
                    PointsBlue = PointsBlue + Streak15;
                }
            }
            else if (ai.streak == 30)
            {
                if (NeedTeam(killer) == "red")
                {
                    PointsRed = PointsRed + Streak30;
                }
                else
                {
                    PointsBlue = PointsBlue + Streak30;
                }
            }
        }
        else
        {
            cut = "Player_" + Killcount;
            Killcount++;
            killer.GetComponent<PlayerMove>().Keys.Add(cut);
            PlayerMove pm = killer.GetComponent<PlayerMove>();
            pm.Streaks++;

            if (pm.Streaks == 5)
            {
                if (NeedTeam(killer) == "red")
                {
                    PointsRed = PointsRed + Streak5;
                }
                else
                {
                    PointsBlue = PointsBlue + Streak5;
                }
            }
            else if (pm.Streaks == 10)
            {
                if (NeedTeam(killer) == "red")
                {
                    PointsRed = PointsRed + Streak10;
                }
                else
                {
                    PointsBlue = PointsBlue + Streak10;
                }
            }
            else if (pm.Streaks == 15)
            {
                if (NeedTeam(killer) == "red")
                {
                    PointsRed = PointsRed + Streak15;
                }
                else
                {
                    PointsBlue = PointsBlue + Streak15;
                }
            }
            else if (pm.Streaks == 30)
            {
                if (NeedTeam(killer) == "red")
                {
                    PointsRed = PointsRed + Streak30;
                }
                else
                {
                    PointsBlue = PointsBlue + Streak30;
                }
            }
        }

        Kills.Add(cut, Time.time);
        CheckMultiKill(killer);
    }

    public void CheckMultiKill(GameObject Killer)
    {
        List<string> vs = null;

        if(Killer.name == "Player")
        {
            vs = Killer.GetComponent<PlayerMove>().Keys;
        }
        else
        {
            vs = Killer.GetComponent<EnemyAI>().Keys;
        }

        int count = 0;
        float time1 = 0;
        float time2 = 1;

        foreach(string i in vs)
        {
            count++;
            if(vs.ToArray().Length - 1 == count)
            {
                time1 = Kills[i];
            }
            else if(vs.ToArray().Length == count)
            {
                time2 = Kills[i];
            }
        }

        float difftime = time2 - time1;
        Debug.Log(difftime);

        if(difftime < 5f)
        {
            if(NeedTeam(Killer) == "red")
            {
                PointsRed = PointsRed + Streak5;
            }
            else if(NeedTeam(Killer) == "blue")
            {
                PointsBlue = PointsBlue + Streak5;
            }
        }
    }
}
