using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour

{
    public bool boost;
    public float decay = 1;
    public float baseMoveSpeed = 10;
    public float moveSpeed;
    public float boostMultiplier = 5;
    public GameObject laser;
    public ParticleSystem engine;

    // Start is called before the first frame update
    void Start()
    {
        boost = true;
        moveSpeed = baseMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        FireLaser();
        LookAtMouse();
        Movement();
    }

    public void SetBoost(bool b)
    {
        boost = b;
    }

    void LookAtMouse()
    {
        Vector3 mousePosition = 
            Camera.main.ScreenToWorldPoint(
                new Vector3 (
                    Input.mousePosition.x, 
                    Input.mousePosition.y,
                    Mathf.Abs(Camera.main.transform.position.z) + this.transform.position.z));

        Vector3 direction = new Vector3(mousePosition.x, mousePosition.y, 0) - new Vector3(transform.position.x, transform.position.y, 0);

        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    void Movement()
    {
        if (moveSpeed > baseMoveSpeed)
        {
            moveSpeed -= 1 * decay * Time.deltaTime;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position - (new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime);
        }
       

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position - (new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime);
        }
        

        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position - (new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime);
        }
        

        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position - (new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime);
        }
        

        if (boost == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                boost = false;
                GameObject.Find("GameManager").GetComponent<Manager>().DepleteBoost();
                moveSpeed *= boostMultiplier;
            }
        }

        if (Input.anyKey)
        {
            var emission = engine.emission;
            emission.enabled = true;
            emission.rateOverTime = 100;
        }
        else
        {
            var emission = engine.emission;
            emission.enabled = true;
            emission.rateOverTime = 0;
        }
    }

    void FireLaser()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(laser, transform.position, transform.rotation);
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void OnDestroy()
    {
        Instantiate(GameObject.Find("GameManager").GetComponent<Manager>().explosionPrefab, transform.position, Quaternion.identity);
        GameObject.Find("GameManager").GetComponent<Manager>().GameOver();
    }
}
