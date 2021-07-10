using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class PostScore : MonoBehaviour
{
    public Text input;
    public Manager manager;
    public Text placeHolder;

    public void Submit()
    {
        if (placeHolder.text != "" && placeHolder.text != null)
        {
            PostToSheet(input.text, manager.killstreak.ToString());
        }
        else
        {
            placeHolder.text = "Please Enter A Name";
        }
    }

    public async Task PostToSheet(string name, string score)
    {
        Debug.Log("Starting Post");
        ScoreRecord sr = new ScoreRecord(name, score);
        HttpClient client = new HttpClient();
        HttpContent content = new StringContent(JsonConvert.SerializeObject(sr));

        //Debug.Log("Content: " + JsonConvert.SerializeObject(sr));
        var response = await client.PostAsync("https://script.google.com/macros/s/AKfycbzrAt8RADCtUmIRFKWcsZczw7FY9qp58g8Vx_EU2VwmsjW1XFs/exec", content);

        //Debug.Log("Resonse Code: " + response.StatusCode);
        client.Dispose();
        content.Dispose();
    }

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
