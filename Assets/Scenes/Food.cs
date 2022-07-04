using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{ 
    public BoxCollider2D gridArea;
    private char letter;
    private int framesAway;

    private void Start() {
        RandomizePosition();
        letter = GetRandomLetter();
        ChangeLetter(letter);
    }

    private void Update() {
        if (Input.GetKeyDown("" + letter))
        {
            framesAway = 0;
        }
        framesAway++;
    }

    private void RandomizePosition() {
        
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

    }

    private void OnTriggerEnter2D(Collider2D other ) {
        if (other.tag == "Player")
        {
            if (framesAway <= 50)
            {
                RandomizePosition();
                letter = GetRandomLetter();
                ChangeLetter(letter);       
            } 
            Debug.Log(framesAway);
        }
    }

    // function to change the letter
    private void ChangeLetter(char letter) {
        GameObject letterFood = GameObject.FindGameObjectWithTag ("Food");
        TextMesh t = letterFood.GetComponent<TextMesh>();
        t.text = "" + letter;
    }

    // function to get a new randomized letter in lower case
    private char GetRandomLetter() {
        return System.Convert.ToChar(Random.Range(97,123));
    }
}
