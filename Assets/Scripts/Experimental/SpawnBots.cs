using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpawnBots : MonoBehaviour
{
    [GreyOut]
    public string text;
    float count;

    void Update()
    {
        if (GameObject.Find("Enemy") && count == 2)
        {
            GameObject.Find("Enemy").name = GenerateID();
        }
        else
        {
            count = 1 * Time.deltaTime + count;
        }
    }

    public string GenerateID()
    {
        int length;
        int length2;
        length = 3;
        length2 = 3;
        System.Random random = new System.Random();
        const string chars = "123456789";
        List<string> randStr = new List<string>();
        for (int i = 0; i <= 2000; i++)
        {
            string AlphaRandom = new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

            string NumberRandom = new string(Enumerable.Repeat(chars, length2)
            .Select(s => s[random.Next(s.Length)]).ToArray());

            if (randStr.Contains(AlphaRandom + NumberRandom))
            {
                i--;
            }
            else
            {
                randStr.Add(AlphaRandom + NumberRandom);
                text = randStr[i];
            }
        }

        return text;
    }
}
