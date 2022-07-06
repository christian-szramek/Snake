using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Finish").GetComponent<TextMesh>().text = Mathf.Abs(Food.score).ToString("F2");
        //GameObject.FindGameObjectWithTag("Finish").GetComponent<TextMesh>().text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.FindGameObjectWithTag("Finish").GetComponent<TextMesh>().text = "0";
    }
}
