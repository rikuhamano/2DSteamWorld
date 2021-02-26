using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using static LookUpItem;
using static BuffScript;

public class PlayerScript : MonoBehaviour {

    [HideInInspector] public int playerHP;
    [HideInInspector] public int playerBullet;
    public int bulletDamage = 20;

    // public LookUpItem itemScript;
    // public BuffScript buffScript;

    public PlatformManager platformScript;
    int moveDirection;
    int[] ItemBox;
    int[][] buffBox = { new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 } };
    //4 buff / time -> replace the oldest in box -> incase tronger -> replace weaker
    int maxBuffNumber = 4;
    int[] deBuffBox = new int[] { 0, 0 };
    //1 debuff / time ->  poison/burn -> replace each other, new debuff replace old debuff
    int maxBuffTypeNumber = 7;
    int maxDebuffTypeNumber = 4;
    int maxItemfromBoxType = 5;

    int playerPos = 0;
    int itemNumber = 4; //The maximum number of item In itemBox
    int PlayerWeapon = 0;
    int PlayerArmor = 0;

    private float moveTime = 0.1f;
    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    private Vector2 touchOrigin = -Vector2.one;
    private Vector2 clickOrigin = -Vector2.one;
    public GameObject toInstantiate;
    int[] DoT = { 3, 99 };
    private bool playerSetupDone = false;

    GameObject player;
    void Awake () {
        platformScript = GetComponent<PlatformManager> ();
        ItemBox = new int[itemNumber];
        player = GameObject.FindGameObjectWithTag ("Player");
        inverseMoveTime = 1 / moveTime;
    }

    void PlayerSetup () {
        importPlayerStat ();
        playerSetupDone = true;
    }

    public void importPlayerStat () {
        playerHP = GameManager.instance.playerStat.HPs;
        playerBullet = GameManager.instance.playerStat.bullets;
        ItemBox[0] = GameManager.instance.playerStat.itemStack1;
        ItemBox[1] = GameManager.instance.playerStat.itemStack2;
        ItemBox[2] = GameManager.instance.playerStat.itemStack3;
        ItemBox[3] = GameManager.instance.playerStat.itemStack4;
        PlayerWeapon = GameManager.instance.playerStat.weapon;
        PlayerArmor = GameManager.instance.playerStat.armor;
    }

    public void exportPlayerStat () {
        GameManager.instance.setPlayerStat (playerHP, playerBullet, ItemBox[0], ItemBox[1], ItemBox[2], ItemBox[3], buffBox[0], buffBox[1], buffBox[2], buffBox[3], deBuffBox, PlayerWeapon, PlayerArmor);
    }

    // Update is called once per frame
    void Update () {
        if (GameManager.instance.setupDone && !playerSetupDone) {
            PlayerSetup (); //Let Gamemanager finish the setup first -> [ELSE] = working as normal
        } else {
            if (!GameManager.instance.playersTurn)
                return;
            int horizontal = 0;
            int vertical = 0;
            if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.DownArrow)) {
                horizontal = (int) Input.GetAxisRaw ("Horizontal");
                vertical = (int) Input.GetAxisRaw ("Vertical");
            }
            if (horizontal != 0)
                vertical = 0;

            if (Input.GetMouseButtonDown (0)) {
                clickOrigin = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp (0)) {
                Vector2 ClickEnd = Input.mousePosition;
                float x = ClickEnd.x - clickOrigin.x;
                float y = ClickEnd.y - clickOrigin.y;
                if (Mathf.Abs (x) > Mathf.Abs (y))
                    horizontal = x > 0 ? 1 : -1;
                else
                    vertical = y > 0 ? 1 : -1;
            }
            if (horizontal != 0 || vertical != 0) {
                if (horizontal > 0) {
                    AttemptMove (0);
                }
                if (vertical > 0) {
                    AttemptMove (1);
                }
                if (vertical < 0) {
                    AttemptMove (-1);
                }
            }
            if (Input.GetKeyUp (KeyCode.Alpha1)) {
                useItemAt (0);
            }
            if (Input.GetKeyUp (KeyCode.Alpha2)) {
                useItemAt (1);
            }
            if (Input.GetKeyUp (KeyCode.Alpha3)) {
                useItemAt (2);
            }
            if (Input.GetKeyUp (KeyCode.Alpha4)) {
                useItemAt (3);
            }
            if (Input.GetKeyUp (KeyCode.P)) {
                getBuff (RandomItem (maxBuffTypeNumber)); //buff(7) ATM
            }
            if (Input.GetKeyUp (KeyCode.O)) {
                getDebuff (RandomItem (maxDebuffTypeNumber)); //debuff(4) ATM
            }
            if (Input.GetKeyUp (KeyCode.I)) {
                getItem (RandomItem (maxItemfromBoxType)); //random Item
            }
            exportPlayerStat ();
            importPlayerStat (); //Update PlayerStat from GameManager
            checkIfGameOver ();
        }
    }

    int RandomItem (int MaxNumber) { //Use this fundtion to random Number with MOD(%) input
        int randBuff = (Random.Range (1, 100) % (MaxNumber + 1));
        if (randBuff == 0) randBuff += 1;
        return randBuff;
    }

    void ItemActive (int item) {
        //0:Null 1:HP 2:skill 
        if (item == 1) {
            PlayerHPchange (20);
        } else if (item == 2) {
            playerBullet += 1;
        } else if (item == 3) { } else { }
        // else {

        // }
    }

    public void checkIfGameOver () {
        if (playerHP <= 0) {
            GameManager.instance.gameOver ();
        }
    }

    public void AttemptMove (int moveDirection) {
        Vector3 start = player.transform.position;
        Vector3 end;
        switch (moveDirection) {
            case -1:
                if (playerPos > -1) {
                    GameManager.instance.movePlatform (moveDirection);
                    end = start + new Vector3 (0, -2.85f, 0f);
                    StartCoroutine (SmoothMOveMent (end));
                    playerPos = playerPos - 1;
                    activeDoT ();
                }
                break;
            case 0:
                GameManager.instance.movePlatform (moveDirection);
                activeDoT ();
                break;
            case 1:
                if (playerPos < 1) {
                    GameManager.instance.movePlatform (moveDirection);
                    end = start + new Vector3 (0, 2.85f, 0f);
                    StartCoroutine (SmoothMOveMent (end));
                    playerPos = playerPos + 1;
                    activeDoT ();
                }
                break;
        }
    }

    IEnumerator SmoothMOveMent (Vector3 end) {
        float sqrRemainDistance = (player.transform.position - end).sqrMagnitude;
        while (sqrRemainDistance > float.Epsilon) {
            Vector3 newPosition = Vector3.MoveTowards (player.transform.position, end, inverseMoveTime * Time.deltaTime);
            GameManager.instance.isMoving (true);
            player.transform.position = newPosition;
            sqrRemainDistance = (player.transform.position - end).sqrMagnitude;
            yield return null;
        }
        GameManager.instance.isMoving (false);
    }

    int ItemIndex = 0; //use to rotate the item
    public void getItem (int item) {
        string getItemName = "None";
        getItemName = LookUpItem.LookupItem (item);
        if (getItemName == "Buff") {
            getBuff (RandomItem (maxBuffTypeNumber));
        } else if (getItemName == "Debuff") {
            getDebuff (RandomItem (maxDebuffTypeNumber));
        } else {
            ItemIndex = FindEmptySlot (ItemIndex);
            ItemBox[ItemIndex] = item;
            ItemIndex = (ItemIndex + 1) % itemNumber;
        }
    }

    int FindEmptySlot (int index) {
        for (int x = 0; x < itemNumber; x++) {
            if (ItemBox[x] == 0) {
                return x;
            }
        }
        return index;
    }

    public void useItemAt (int index) {
        if (ItemBox[index] == 0) {
            Debug.Log ("No Item in :" + (index + 1));
        } else {
            Debug.Log ("Use " + LookUpItem.LookupItem (index) + (" at ") + (index + 1));
            int itemTypeNum = ItemBox[index];
            ItemActive (itemTypeNum);
            ItemBox[index] = 0;
        }
    }

    int BuffCycle = 0; //Use to cycle through all buff in buffBox
    public void getBuff (int inBuffNumber) {
        buffBox[BuffCycle][0] = inBuffNumber;
        buffBox[BuffCycle][1] = 5;
        BuffCycle = (BuffCycle + 1) % maxBuffNumber;
    }

    public void getDebuff (int inDebuffNumber) {
        deBuffBox[0] = inDebuffNumber;
        deBuffBox[1] = 5;
    }

    void activeDoT () {
        if (Array.Exists (DoT, element => element == deBuffBox[0])) {
            string DebuffString = LookUpItem.LookupDebuff (deBuffBox[0]);
            if (DebuffString == "Poison") {
                Debug.Log ("Poison");
                PlayerHPchange (-10);
            }
        }
        foreach (int[] buff in buffBox) {
            if (LookUpItem.LookupBuff (buff[0]) == "Regenerate") {
                PlayerHPchange (5);
            }
        }
        for (int x = 0; x < maxBuffNumber; x++) {
            if (buffBox[x][1] <= 0) {
                buffBox[x][1] = 0;
                buffBox[x][0] = 0;
            } else
                buffBox[x][1] = buffBox[x][1] - 1;
        }
        if (deBuffBox[1] <= 0)
            deBuffBox[1] = 0;
        else
            deBuffBox[1] = deBuffBox[1] - 1;
    }

    public void PlayerHPchange (int HP) {
        playerHP += HP;
        if (HP <= 0) {
            Debug.Log ("oww :" + HP);
        } else {
            Debug.Log ("HP gained :" + HP);
        }
    }

}