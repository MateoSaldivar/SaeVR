using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public static MapManager instance;
    public GameObject[] straightPassages;
    public GameObject[] diagonalPassages;

    public Queue<Passage> passages;
    public Passage[] initialPassages;

    public Passage.Type nextPassageType = Passage.Type.Straight;

    private Passage lastPassage;
    public int maxActivePassages = 7;
    private int midPointPassages;
    private int currentPassageIndex = 0;
    private void Awake() {
        instance = this;
    }

    void Start() {
        SpawnInitialPassages();
        midPointPassages = maxActivePassages / 2;
    }

    void SpawnInitialPassages() {
        passages = new Queue<Passage>();

        // Add initial passages to the queue in the desired order
        passages.Enqueue(initialPassages[0]);
        passages.Enqueue(initialPassages[1]);
        lastPassage = initialPassages[1];

        // Instantiate additional passages to fill the queue up to the desired size
        while (passages.Count < maxActivePassages) {
            // Determine which type of passage to instantiate next
            GameObject passagePrefab;
            if (nextPassageType == Passage.Type.Straight) {
                passagePrefab = straightPassages[RandomPiece(straightPassages)];
                nextPassageType = Passage.Type.Diagonal;
            } else {
                passagePrefab = diagonalPassages[RandomPiece(diagonalPassages)];
                nextPassageType = Passage.Type.Straight;
            }

            // Instantiate the passage and add it to the queue
            Passage newPassage = Instantiate(passagePrefab, lastPassage.spawnAnchor.position, lastPassage.spawnAnchor.rotation).GetComponent<Passage>();
            passages.Enqueue(newPassage);
            Platform.instance.passages.Enqueue(newPassage);
            lastPassage = newPassage;
        }
    }

    public void AddNewPassage() {
        Debug.Log("Max Active Passages: " + maxActivePassages + " | Mid Point Passages: " + midPointPassages + " | Current Passage Index: " + currentPassageIndex);

        Passage oldestPassage = passages.Peek();
        if (currentPassageIndex >= midPointPassages) {
            passages.Dequeue();
            Destroy(oldestPassage.gameObject);
        } else {
            if(currentPassageIndex < midPointPassages) currentPassageIndex++;
            return;
        }

        GameObject passagePrefab;
        if (nextPassageType == Passage.Type.Straight) {
            passagePrefab = straightPassages[RandomPiece(straightPassages)];
        } else {
            passagePrefab = diagonalPassages[RandomPiece(diagonalPassages)];
        }

        Passage nextPassage = Instantiate(passagePrefab, lastPassage.spawnAnchor.position, lastPassage.spawnAnchor.rotation).GetComponent<Passage>();
        passages.Enqueue(nextPassage);
        Platform.instance.passages.Enqueue(nextPassage);
        lastPassage = nextPassage;

        if (nextPassageType == Passage.Type.Straight) {
            nextPassageType = Passage.Type.Diagonal;
        } else {
            nextPassageType = Passage.Type.Straight;
        }
    }

    int RandomPiece(GameObject[] elements) {
        float randomValue = Random.value;

        // 70% chance to select the first element
        if (randomValue <= 0.7f) {
            return 0;
        } else {
            // 30% chance to select one of the other elements at random
            return Random.Range(0, elements.Length);
        }
    }

    void Update() {

    }
}

