using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNearPlayer : MonoBehaviour {
    GameObject playerObject;
    Player player;
    Rigidbody2D rb;
    bool isDropping = false;
    bool isMovingBack = false;
    public float xThreshold = 5f;
    public float yThreshold = 5f;
    public float moveSpeed = 20f;
    public float moveBackSpeed = 5f;
    public enum MoveDirection {
        Up,
        Down,
        Left,
        Right,
    };
    public MoveDirection direction = MoveDirection.Down;
    Vector2 dropDirection = Vector2.down;

    Vector3 originalPosition;

    void Start() {
        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    void Update() {
        float xDist = Mathf.Abs(transform.position.x - player.transform.position.x);
        float yDist = Mathf.Abs(transform.position.y - player.transform.position.y);
        bool isAtOriginalPosition = (transform.position - originalPosition).magnitude < 0.1f;
        if (isAtOriginalPosition) {
            if (isMovingBack) {
                StopMoving();
            }
            else if (xDist < xThreshold && yDist < yThreshold) {
                Drop();
            }
        }
        else {
            if (!isDropping) {
                MoveBack();
            }
        }
    }

    void Drop() {
        isDropping = true;
        if (direction == MoveDirection.Up) {
            dropDirection = Vector2.up;
        }
        else if (direction == MoveDirection.Down) {
            dropDirection = Vector2.down;
        }
        else if (direction == MoveDirection.Left) {
            dropDirection = Vector2.left;
        }
        else if (direction == MoveDirection.Right) {
            dropDirection = Vector2.right;
        }
        rb.velocity = dropDirection * moveSpeed;
    }

    void MoveBack() {
        Vector3 moveBackDirection = dropDirection * -1;
        rb.velocity = moveBackDirection * moveBackSpeed;
    }

    void StopMoving() {
        rb.velocity = Vector3.zero;
        isMovingBack = false;
        isDropping = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        string otherTag = other.gameObject.tag;
        if (isDropping) {
            if (otherTag == "Player") {
                player.Die();
            }
            else if (otherTag == "Ground") {
                isDropping = false;
                isMovingBack = true;
            }
        }
    }
}
