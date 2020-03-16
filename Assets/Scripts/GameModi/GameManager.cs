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

    [Header("Referenzen")]
    [SerializeField]
    GameObject DeathScreen;
    [SerializeField]
    Text PunkteAusgabe;
    [SerializeField]
    GameObject Player;

    void Start()
    {
        DeathScreen.SetActive(false);
        Teams = new Dictionary<GameObject, string>();
        GameObject[] AllgameObjects = GameObject.FindGameObjectsWithTag("Bot");
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

        Teams.Add(GameObject.Find("Player"), "blue");
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
}
