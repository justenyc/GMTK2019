using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    Player_Controller player;
    public Vector3 target;
    private void Start()
    {
        Material mat = this.GetComponent<MeshRenderer>().material;
        Color randomColor = new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), 1);

        mat.EnableKeyword("_Color");
        mat.EnableKeyword("_EmissionColor");

        mat.SetColor("_Color", randomColor);
        mat.SetColor("_EmissionColor", randomColor * Random.Range(0f, 1f));
        player = GameObject.Find("Player").GetComponent<Player_Controller>();

        target = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 14);
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while(true)
        {
            yield return new WaitForSeconds(5f);
            target = new Vector3(Random.Range(-150, 150), Random.Range(-150, 150), 14);
        }
    }

    private void Update()
    {
        try
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) > 500f)
            {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 14);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, target, Random.Range(0.001f,0.005f) * Time.deltaTime);
            }
        }
        catch
        {
            return;
        }
    }
}

