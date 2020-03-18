using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Teams")]
    [SerializeField]
    Dictionary<GameObject, string> Teams;

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

    void Start()
    {
        Teams = new Dictionary<GameObject, string>();
        GameObject[] AllgameObjects = GameObject.FindGameObjectsWithTag("Player");
        isRed = false;

        foreach(GameObject go in AllgameObjects)
        {
            if (isRed)
            {
                Teams.Add(go, "blue");
                isRed = !isRed;
            }
            else
            {
                Teams.Add(go, "red");
                isRed = !isRed;
            }
        }
    }

    public bool isEnemy(GameObject shooter, GameObject hitted)
    {
        bool sos = false;

        if (Teams.ContainsKey(shooter))
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

    public void OnDeathBot(GameObject Bot, float waitSeconds)
    {
        Bot.SetActive(false);
        StartCoroutine(DeathWait(waitSeconds, Bot));

        if(NeedTeam(Bot) == "red")
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
        DeathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        PointsRed++;
    }

    private void Update()
    {
        PunkteAusgabe.text = ("Rot: " + PointsRed + " / Blau: " + PointsBlue);
    }

    public void OnPlayerRespawn()
    {
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
}
