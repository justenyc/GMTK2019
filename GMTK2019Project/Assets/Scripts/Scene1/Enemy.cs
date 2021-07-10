using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Vector3 target;
    public float moveSpeed = 1;
    public float timer = 1;
    public float timerStart = 2;
    public bool trackPlayer = true;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackPlayer == true)
        {
            if (GameObject.Find("Player") != null)
                target = GameObject.Find("Player").transform.position;
            else
                Destroy(this.gameObject);
        }

        Countdown();
        LookAtTarget();
        transform.position = Vector3.Lerp(transform.position, target, 1 * moveSpeed * Time.deltaTime);
    }

    void Countdown()
    {
        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }
        else
        {
            timer = timerStart;
            trackPlayer = !trackPlayer;
            target = RandomVector3();
        }
    }

    Vector3 RandomVector3()
    {
        Vector3 temp = new Vector3(Random.Range(-35, 35), Random.Range(-19, 19), 1);
        return temp;
    }

    void LookAtTarget()
    {
        Vector3 targetPosition = target;
        Vector3 direction = new Vector3(targetPosition.x, targetPosition.y, 0) - new Vector3(transform.position.x, transform.position.y, 0);
        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);  
        if (GameObject.Find("Player") != null)
        {
            GameObject.Find("Player").GetComponent<Player_Controller>().SetBoost(true);
            GameObject.Find("GameManager").GetComponent<Manager>().IncreaseBoost();
        }
    }
}
