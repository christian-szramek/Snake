using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments = new List<Transform>();

    public Transform segmentPrefab;

    public int initialSize= 2;

    private void Start() {
        ResetState();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            _direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            _direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            _direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
             _direction = Vector2.right;
        }
    }

    private void FixedUpdate() {

        // make snake segments follow the snake head
        for (int i = _segments.Count - 1; i > 0 ; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        // make the snake head move
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x, 
            Mathf.Round(this.transform.position.y) + _direction.y, 
            0.0f
        );
    }

    // append new snake segment at the end
    private void Grow() {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);

    }

    public void ResetState() {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++) {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    // grow the snake, when the snake collides with the food
    private void OnTriggerEnter2D(Collider2D other ) {
        if (other.tag == "Food")
        {
            Grow();   
        } else if (other.tag == "Obstacle") {
            SceneManager.LoadScene(2);
        }
    }
}

