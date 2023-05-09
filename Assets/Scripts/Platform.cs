using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static Platform instance;
    public Transform pivot;
    public Passage[] passagesArray;
    public Queue<Passage> passages;
    public float moveSpeed = 5f; 
    private bool isMoving;
    private Vector3 targetPosition;
    private Passage currentPassage;

    public Passage.Type currentPassageType;
    
    private void Awake() {
		instance = this;
        FillQueueWithArray();
    }
	void Start()
    {
        
        StartMoving();
    }

    public void FillQueueWithArray() {
        passages = new Queue<Passage>(passagesArray);

        // Make sure the first element of the array is the first passage to traverse
        if (passages.Count > 0 && passages.Peek() != passagesArray[0]) {
            Passage[] arrayCopy = new Passage[passages.Count];
            passages.CopyTo(arrayCopy, 0);
            int firstIndex = System.Array.IndexOf(arrayCopy, passagesArray[0]);
            Passage[] reorderedArray = new Passage[passages.Count];
            System.Array.Copy(arrayCopy, firstIndex, reorderedArray, 0, arrayCopy.Length - firstIndex);
            System.Array.Copy(arrayCopy, 0, reorderedArray, arrayCopy.Length - firstIndex, firstIndex);
            passages = new Queue<Passage>(reorderedArray);
        }
    }

    public void StartMoving() {
        if (passages.Count > 0) {
            currentPassage = passages.Peek();
            targetPosition = currentPassage.endPoint.position;
            isMoving = true;
        }
    }

    void Update() {
        if (isMoving) {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition) {
                if (passages.Count > 0) {
                    currentPassage = passages.Dequeue();
                    targetPosition = currentPassage.endPoint.position;
                } else {
                    isMoving = false;
                }
            }
        }
    }

    private void OnDrawGizmos() {
        if (pivot != null) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(pivot.position, 0.015f);

        }
    }
}



