using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNote : MonoBehaviour
{
 public Transform tail;  // Reference to the tail transform
public float duration;  // Duration the note should be held
public float speed;

private float originalTailLength;
private float holdStartTime;

Rigidbody2D rb;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
void Start() {
    rb.velocity = new Vector2(0, -speed);
    if (tail != null) {
        originalTailLength = tail.localScale.y;
        holdStartTime = Time.time;
    }
}

void Update() {
    // Shrink the tail based on hold duration
    if (tail != null) {
        float timeHeld = Time.time - holdStartTime;
        float remainingFraction = Mathf.Max(0, 1 - timeHeld / duration);
        tail.localScale = new Vector3(tail.localScale.x, originalTailLength * remainingFraction, tail.localScale.z);
    }
}
}
