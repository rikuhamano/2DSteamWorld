using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformManager : MonoBehaviour {

    public FloorScript floorScript;
    private Transform boardHolder;
    public GameObject[] item;
    public GameObject[] enemy;
    public GameObject[] Platform;
    int minEnemyCount = 5; //5                   Define minimum enemy in stage
    int minItemCount = 4; //4                    Define minimum item in stage
    int minObjectIndex = 2; //2                   define the first platform object can appear
    private int enemPos;
    private int itemPos;
    int columns; //Column shoule be control by gameManager in SceneSetup method
    int rows = 3;
    public GameObject toInstantiate;

    private float moveTime = 0.1f;
    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    GameObject Board;

    private List<Vector3> gridPositions = new List<Vector3> ();

    void Start () {
        //GameManager.instance.RSteps;
        floorScript = GetComponent<FloorScript> ();
        boxCollider = GetComponent<BoxCollider2D> ();
        rb2D = GetComponent<Rigidbody2D> ();
        inverseMoveTime = 1f / moveTime;
    }
    public void setColumns (int Turn) {
        columns = Turn;
    }

    void InitialiseList () {
        gridPositions.Clear ();
        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                gridPositions.Add (new Vector3 (x, y, 0f));
            }
        }

    }

    public void SceneSetup (int level) {
        boardHolder = new GameObject ("Board").transform;
        boardHolder.gameObject.tag = "Board";

        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                toInstantiate = Platform[0];
                GameObject instance = Instantiate (toInstantiate, new Vector3 ((x * 5) - 5, (y * 2.8f) - 1, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent (boardHolder);
                //instance.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            }
        }

        int addedObject = objectOffsetValue (level);
        int enemyCount = addedObject + minEnemyCount;
        int itemCount = addedObject + minItemCount;
        int allObjectNumber = itemCount + enemyCount;
        int[] ObjectPosition = new int[allObjectNumber];
        ObjectPosition = randomObjectColumn (level);
        int ObjectPlacedCount = 0;

        foreach (int ObjectPos in ObjectPosition) {
            int randomRowIndex = (int) Random.Range (0, rows);
            float posx = (ObjectPos * 5) - 5;
            float posy = (randomRowIndex * 2.8f) - 1;
            if (ObjectPlacedCount < enemyCount) {
                toInstantiate = enemy[0];
            } else {
                toInstantiate = item[0];
            }
            GameObject instance = Instantiate (toInstantiate, new Vector3 (posx, posy, 0f), Quaternion.identity) as GameObject;
            instance.transform.SetParent (boardHolder);
            ObjectPlacedCount += 1;

        }
    }

    int objectOffsetValue (int level) { //Calcualte Number of additional item/enemy added to stage
        int addNumber = (int) Mathf.Floor (level / 3f);
        return addNumber;
    }

    int[] randomObjectColumn (int level) {
        int platformNumber = columns;
        int enemyCount = minEnemyCount + objectOffsetValue (level); //Numbeer of enemy
        int itemCount = minItemCount + objectOffsetValue (level); //Number of Item
        int ObjectCount = enemyCount + itemCount;
        int[] UsedPos = new int[itemCount + enemyCount]; //Number of used position to check if it has been placed at the other object or not
        int randomIndex; //Use to indicate the position

        for (int x = 0; x < ObjectCount; x++) {
            randomIndex = Random.Range (minObjectIndex, columns);
            while (Array.Exists (UsedPos, element => element == randomIndex)) {
                randomIndex = Random.Range (minObjectIndex, columns);
            }
            UsedPos[x] = randomIndex;
        }
        return UsedPos;
    }

    public void moveBoard () {
        Board = GameObject.FindGameObjectWithTag ("Board");
        Vector3 start = Board.transform.position;
        Vector3 end = start + new Vector3 (-5, 0, 0f);
        StartCoroutine (SmoothMOveMent (end));
    }

    IEnumerator SmoothMOveMent (Vector3 end) {
        float sqrRemainDistance = (Board.transform.position - end).sqrMagnitude;
        while (sqrRemainDistance > float.Epsilon) {
            GameManager.instance.isMoving (true);
            Vector3 newPosition = Vector3.MoveTowards (Board.transform.position, end, inverseMoveTime * Time.deltaTime);
            Board.transform.position = newPosition;
            sqrRemainDistance = (Board.transform.position - end).sqrMagnitude;
            yield return null;
        }
        GameManager.instance.isMoving (false);
    }
}