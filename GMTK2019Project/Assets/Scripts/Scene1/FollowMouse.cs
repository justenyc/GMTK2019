using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed = 1;
    public float scaleSpeed = 0.1f;
    public float maxScale;
    public float minScale;
    private Vector3 v = Quaternion.identity.eulerAngles;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MouseFollow();
        RotateFun();
        ScaleModifier();
    }

    void MouseFollow()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 21));
        transform.position = new Vector3(mousePosition.x, mousePosition.y, mousePosition.z);
    }

    void RotateFun()
    {
        if (v.z > 360)
        {
            v = Vector3.zero;
        }

        v = v + new Vector3(0, 0, 1 * rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(v);
    }

    void ScaleModifier()
    {
       if(Input.GetMouseButtonUp(0))
        {
            transform.localScale = new Vector3(minScale, minScale, 1);
        }

        if (transform.localScale.x <= maxScale)
            transform.localScale += new Vector3(1 * scaleSpeed * Time.deltaTime, 1 * scaleSpeed * Time.deltaTime, 0);
        else
            transform.localScale = new Vector3(maxScale, maxScale, 1);
    }
}
