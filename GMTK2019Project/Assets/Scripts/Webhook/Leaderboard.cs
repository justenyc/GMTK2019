using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

[Serializable]
public class Leaderboard : MonoBehaviour
{
    public Transform Name_Holder;
    public Transform NameHolder_Shadow;
    public Transform Score_Holder;
    public Transform ScoreHolder_Shadow;
    public Text responseCodeText;

    ScoreRecord[] scoreBoard_local;

    // Start is called before the first frame update
    void Start()
    {
        GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async Task GetScore()
    {
        Debug.Log("Getting Score");
        HttpClient client = new HttpClient();
        var response = await client.GetAsync("https://script.google.com/macros/s/AKfycbzrAt8RADCtUmIRFKWcsZczw7FY9qp58g8Vx_EU2VwmsjW1XFs/exec");//GetStringAsync("https://script.google.com/macros/s/AKfycbzrAt8RADCtUmIRFKWcsZczw7FY9qp58g8Vx_EU2VwmsjW1XFs/exec");
        var statusCode = response.StatusCode;
        Debug.Log(statusCode.ToString());

        var resultJSON = await response.Content.ReadAsStringAsync();
        responseCodeText.text = statusCode.ToString();
        scoreBoard_local = JsonHelper.getJsonArray<ScoreRecord>(resultJSON);//JsonConvert.DeserializeObject<List<ScoreRecord>>(resultJSON.ToString());
        Debug.Log(resultJSON);

        //Debug.Log("Response: " + response);
        //Debug.Log(scoreBoard_local[1].name + " " + scoreBoard_local[1].score);

        UpdateLocalScoreboard(scoreBoard_local);
        client.Dispose();
    }

    public async Task PostScore(string name, string score)
    {
        Debug.Log("Starting Post");
        ScoreRecord sr = new ScoreRecord(name, score);
        HttpClient client = new HttpClient();
        HttpContent content = new StringContent(JsonConvert.SerializeObject(sr));

        //Debug.Log("Content: " + JsonConvert.SerializeObject(sr));
        var response = await client.PostAsync("https://script.google.com/macros/s/AKfycbzrAt8RADCtUmIRFKWcsZczw7FY9qp58g8Vx_EU2VwmsjW1XFs/exec", content);

        Debug.Log(response.StatusCode);
        client.Dispose();
        content.Dispose();
    }

    void UpdateLocalScoreboard(ScoreRecord[] srL)
    {
        responseCodeText.text = "updating scoreboard";
        Text[] namesArray = Name_Holder.GetComponentsInChildren<Text>();
        Text[] namesArray_shadow = NameHolder_Shadow.GetComponentsInChildren<Text>();
        Text[] scoresArray = Score_Holder.GetComponentsInChildren<Text>();
        Text[] scoresArray_shadow = ScoreHolder_Shadow.GetComponentsInChildren<Text>();

        for (int i = 0; i < srL.Length; i++)
        {
            namesArray[i].text = srL[i + 1].name;
            namesArray_shadow[i].text = srL[i + 1].name;
            scoresArray[i].text = srL[i + 1].score;
            scoresArray_shadow[i].text = srL[i + 1].score;
        }
    }
    
    [Serializable]
    public class ScoreRecord
    {
        public string name;
        public string score;

        public ScoreRecord(string toName, string toScore)
        {
            name = toName;
            score = toScore;
        }
    }
}
