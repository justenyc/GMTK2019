using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextShadow : MonoBehaviour
{
    public Text textToShadow;

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = textToShadow.text;
    }
}
