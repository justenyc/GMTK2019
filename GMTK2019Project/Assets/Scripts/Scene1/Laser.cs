using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour

{
    public GameObject crosshair;
    public int kills = 0;
    public float fadeSpeed = 1;
    float fade = 0;

    // Start is called before the first frame update
    void Start()
    {
        crosshair = GameObject.Find("CrossHair");
        transform.localScale = new Vector3(crosshair.transform.localScale.x, 100, 1);
        Destroy(this.gameObject, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        fade += 1 * fadeSpeed;
        this.GetComponent<MeshRenderer>().material.SetFloat("ScrollSpeed", fade);
        transform.Find("Center").GetComponent<MeshRenderer>().material.SetFloat("ScrollSpeed", fade);
    }

    public void AddKills()
    {
        kills += 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Physics2D.IgnoreLayerCollision(0, 8);
        if (collision.tag == "Enemy")
        {
            AddKills();
        }
    }

    private void OnDestroy()
    {
        GameObject.Find("GameManager").GetComponent<Manager>().SetKills(kills);
    }
}
