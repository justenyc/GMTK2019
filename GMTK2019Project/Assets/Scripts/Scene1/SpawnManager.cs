using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public float timer;
    public float timerStart;
    public int SpawnNumber;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Countdown();
    }

    Vector3 RandomSpawn()
    {
        Vector3 temp = new Vector3(Random.Range(-35, 35), Random.Range(-19, 19), 1);

        if (Vector3.Distance(temp, GameObject.Find("Player").transform.position) < 5)
        {
            RandomSpawn();
        }
        else
        {
            return temp;
        }

        return new Vector3(-35, -19, 1);
    }

    void Countdown()
    {
        if (timer > 0)
        {
            timer -= 1 * Time.deltaTime;
        }
        else
        {
            if (GameObject.Find("Player") != null)
            {
                timer = timerStart;
                Spawn(SpawnNumber);
            }
            else
                this.enabled = false;
        }
    }

    void Spawn(int i)
    {
        int temp = 0;
        
        while(temp < i)
        {
            temp++;
            Instantiate(enemy, RandomSpawn(), Quaternion.identity);
        }

        SpawnNumber++;
    }
}
