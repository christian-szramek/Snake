using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class Food : MonoBehaviour
{ 
    public BoxCollider2D gridArea;
    private char letter;
    private int framesAway;
    private int index;
    private string resultWord;
    private string playerWord;
    private int wordLength = 10;
    
    private string[] resultWords = new string[]{"Strawberry", "Friendship", "Everything", "Appreciate", "Motivation"};
    private int chosenWordIndex;

    private KeywordRecognizer recognizer;

    private float time;
    public static float score;

    private void Start() {
        // initialize everything for the word 
        index = 0;
        chosenWordIndex = GetRandomWordIndex(resultWords.Length);
        resultWord = resultWords[chosenWordIndex];
        playerWord = "----------";
        if (GameObject.FindGameObjectWithTag("Word") != null)
        {
            GameObject.FindGameObjectWithTag("Word").GetComponent<TextMesh>().text = playerWord;
        }

        // initialize the food
        RandomizePosition();
        ChangeLetter(resultWord[index]);
        
        // start and initiate speech keyword recognizer
        recognizer = new KeywordRecognizer(resultWords);
        recognizer.OnPhraseRecognized += RecognizedLetter;
        recognizer.Start();

        time = Time.time;
        if (GameObject.FindGameObjectWithTag("Time") != null)
        {
            GameObject.FindGameObjectWithTag("Time").GetComponent<TextMesh>().text = "0";    
        }
        
    }

    private void Update() {
        if (GameObject.FindGameObjectWithTag("Time") != null)
        {
            GameObject.FindGameObjectWithTag("Time").GetComponent<TextMesh>().text = Mathf.Abs(time - Time.time).ToString("F2");    
        }
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
            UpdateWord();
            UpdateLetterFood();
        }
    }

    private void UpdateLetterFood() {
        if (index < wordLength)
        {
            char newLetter = resultWord[index];
            ChangeLetter(newLetter);
            RandomizePosition();    
        }
    }

    private void UpdateWord() {
        char newChar = resultWord[index];
        StringBuilder sb = new StringBuilder(playerWord);
        sb[index] = newChar;
        playerWord = sb.ToString(); 
        GameObject.FindGameObjectWithTag("Word").GetComponent<TextMesh>().text = playerWord;
        index++;
        if (index == resultWord.Length)
        {
            score = Mathf.Abs(time - Time.time);
            SceneManager.LoadScene(3);
        }
    }

    // function to change the letter in the food
    private void ChangeLetter(char letter) {
       GameObject.FindGameObjectWithTag("Food").GetComponent<TextMesh>().text = "" + letter; 
    }

    // function to get a new randomized letter in lower case
    private int GetRandomWordIndex(int max) {
        return Random.Range(0, max);
    }

    // function to react if user says resultWord
    private void RecognizedLetter(PhraseRecognizedEventArgs args) {
        if (args.text == resultWords[chosenWordIndex])
        {
            score = Mathf.Abs(time - Time.time);
            SceneManager.LoadScene(3);
        }
    }
    
}
