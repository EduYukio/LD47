using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNearPlayer : MonoBehaviour {
    GameObject playerObject;
    Player player;
    public Rigidbody2D rb;
    bool isDropping = false;
    bool isMovingBack = false;
    public bool shouldMoveBack = true;
    public bool shouldKillPlayer = true;
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
    public GameObject pushableBoxPrefab;
    Vector2 dropDirection = Vector2.down;
    Vector3 originalPosition;

    void Start() {
        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    void FixedUpdate() {
        float xDist = Mathf.Abs(transform.position.x - player.transform.position.x);
        float yDist = Mathf.Abs(transform.position.y - player.transform.position.y);
        bool isAtOriginalPosition = (transform.position - originalPosition).magnitude < 0.2f;
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
        if (rb.bodyType == RigidbodyType2D.Static) return;
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

        if (!shouldKillPlayer && otherTag == "Player") {
            if (Mathf.Abs(player.transform.position.y - transform.position.y) < 0.5f) return;
            player.isOnTopOfMovingPlatform = true;
            player.movingPlatform = GetComponent<MoveNearPlayer>();
        }

        if (isDropping) {
            if (otherTag == "Player" && shouldKillPlayer) {
                player.Die();
            }
            else if (otherTag == "Breakable") {
                Destroy(other.gameObject);
                MoveBackOrSelfDestroy();
            }
            else if (otherTag == "Ground" || otherTag == "Box") {
                MoveBackOrSelfDestroy();
            }
            else if (otherTag == "Estaca") {
                Vector3 newPos = transform.position + new Vector3(0, 0.2f, 0);
                GameObject newBox = Instantiate(pushableBoxPrefab, newPos, Quaternion.identity, other.transform);
                newBox.GetComponent<PushBox>().isOnTopOfMovingPlatform = true;

                Destroy(transform.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        string otherTag = other.gameObject.tag;
        if (otherTag == "Player" && !shouldKillPlayer) {
            player.isOnTopOfMovingPlatform = false;
            player.movingPlatform = null;
        }
    }

    void MoveBackOrSelfDestroy() {
        if (shouldMoveBack) {
            isDropping = false;
            isMovingBack = true;
        }
        else {
            rb.bodyType = RigidbodyType2D.Static;
            Destroy(transform.gameObject, 0.2f);
        }
    }
}
