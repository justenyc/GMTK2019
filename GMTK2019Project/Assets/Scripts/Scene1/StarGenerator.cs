using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public int numberOfStars = 1;
    public float xClamp = 36;
    public float yClamp = 0;
    public float zClamp = 0;
    public GameObject starPrefab;
    public Transform stars;

    void Start()
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            if (xClamp != 0 && yClamp != 0)
            {
                starPrefab.transform.localScale = RandomScale();
                Instantiate(starPrefab, RandomVector3(), Quaternion.identity, stars);
            }
        }
    }

    Vector3 RandomVector3()
    {
        float x = Random.Range(xClamp * -1, xClamp);
        float y = Random.Range(yClamp * -1, yClamp);
        float z = Random.Range(zClamp * -1, zClamp);
        Vector3 vector = new Vector3(x, y, z);

        return vector;
    }

    Vector3 RandomScale()
    {
        float temp = Random.Range(0.1f, 1);
        Vector3 v3 = new Vector3(temp, temp, temp);

        return v3;
    }
}