using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public int shotKills = -1;
    public int killstreak = 0;
    public Text score;
    public Text score_shadow;
    public GameObject deathScreen;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        if (deathScreen == null)
            GameObject.Find("DeathScreen");
        shotKills = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (shotKills > 1 || shotKills == 0)
        {
            Destroy(GameObject.Find("Player"));
        }
    }

    public void SetKills(int i)
    {
        shotKills = i;
        killstreak += i;
        score.text = "Score " + killstreak.ToString();
        score_shadow.text = "Score " + killstreak.ToString();
    }

    public void DepleteBoost()
    {
        RectTransform img = GameObject.Find("BoostBar").GetComponent<RectTransform>();
        img.GetComponent<Animator>().SetBool("Boost", false);
    }

    public void IncreaseBoost()
    {
        RectTransform img = GameObject.Find("BoostBar").GetComponent<RectTransform>();
        img.GetComponent<Animator>().SetBool("Boost", true);
    }

    public void GameOver()
    {
        Cursor.visible = true;
        deathScreen.SetActive(true);
        GameObject.Find("CrossHair").SetActive(false);
    }

    public void SubmitScore()
    {

    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void QuitToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitToDeskTop()
    {
        Application.Quit();
    }

    public void SetNewActive(GameObject go)
    {
        go.SetActive(true);
    }

    public void SetNewInactive(GameObject go)
    {
        go.SetActive(false);
    }

    public void LoadScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
