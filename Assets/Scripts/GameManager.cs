using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public class PlayerStat {
        public int HPs;
        public int bullets;
        public int itemStack1;
        public int itemStack2;
        public int itemStack3;
        public int itemStack4;
        public int[] buffStack1;
        public int[] buffStack2;
        public int[] buffStack3;
        public int[] buffStack4;
        public int[] debuff;
        public int weapon;
        public int armor;

        public PlayerStat (int HP, int buls, int item1, int item2, int item3, int item4, int[] buff1, int[] buff2, int[] buff3, int[] buff4, int[] dbuff, int weap, int wear) {
            HPs = HP;
            bullets = buls;
            itemStack1 = item1;
            itemStack2 = item2;
            itemStack3 = item3;
            itemStack4 = item4;
            buffStack1 = buff1;
            buffStack2 = buff2;
            buffStack3 = buff3;
            buffStack4 = buff4;
            debuff = dbuff;
            weapon = weap;
            armor = wear;
        }
    }

    public Text Turn;
    public BattleEnvironmentScript envScript;
    public HUDTextHandler HUDscript;
    [HideInInspector] public bool playersTurn = true;
    [HideInInspector] public bool playerCombatTurn = false;
    public static GameManager instance = null;
    public PlatformManager platformScript;
    [HideInInspector] public int level = 1;
    private List<FloorScript> objectList;
    public int RSteps = 20;
    public PlayerStat playerStat = new PlayerStat (100, 5, 0, 0, 0, 0, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, new int[] { 0, 0 }, 0, 0);
    public bool setupDone = false;
    public bool inBattle = false;

    // Start is called before the first frame update
    void Awake () {

        HUDscript = GetComponent<HUDTextHandler> ();
        Turn.text = "Remaining Steps: " + RSteps;
        platformScript = GetComponent<PlatformManager> ();
        platformScript.setColumns (RSteps);

        platformScript.SceneSetup (level);

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy (gameObject);

        DontDestroyOnLoad (gameObject);
        objectList = new List<FloorScript> ();
        setupDone = true; //To indicate that GameManager is done setting up
    }

    // Update is called once per frame
    void Update () {
        HUDscript.ChangeText ();
        if (playerStat.HPs <= 0)
            gameOver ();
    }

    public void movePlatform (int direction) {
        platformScript.moveBoard ();
        TurnTick ();
        playersTurn = true;
    }

    public void gameOver () {
        playersTurn = false;
        Debug.Log ("Game Over!");
    }

    private void TurnTick () {
        RSteps -= 1;
        Turn.text = "Remaining Steps: " + RSteps;
        //Debug.Log("1 Turn Pass");
    }

    public void isMoving (bool check) {
        playersTurn = !check;
    }

    public void isBattle (bool check) {
        if (check) {
            playersTurn = false;
            playerCombatTurn = true;
            inBattle = true;
        } else {
            playersTurn = true;
            playerCombatTurn = false;
            inBattle = false;
        }
    }
    
    public void setPlayerStat (int HP, int buls, int item1, int item2, int item3, int item4, int[] buff1, int[] buff2, int[] buff3, int[] buff4, int[] dbuff, int weap, int wear) {
        playerStat.HPs = HP;
        playerStat.bullets = buls;
        playerStat.itemStack1 = item1;
        playerStat.itemStack2 = item2;
        playerStat.itemStack3 = item3;
        playerStat.itemStack4 = item4;
        playerStat.buffStack1 = new int[] { buff1[0], buff1[1] };
        playerStat.buffStack2 = new int[] { buff2[0], buff2[1] };
        playerStat.buffStack3 = new int[] { buff3[0], buff3[1] };
        playerStat.buffStack4 = new int[] { buff4[0], buff4[1] };
        playerStat.debuff = new int[] { dbuff[0], dbuff[1] };
        playerStat.weapon = weap;
        playerStat.armor = wear;
    }

    public void getCombatStat (int HP, int buls, int item1, int item2, int item3, int item4) {
        playerStat.HPs = HP;
        playerStat.bullets = buls;
        playerStat.itemStack1 = item1;
        playerStat.itemStack2 = item2;
        playerStat.itemStack3 = item3;
        playerStat.itemStack4 = item4;
    }

    public void StartBattle (GameObject enemyObj) {
        if (!inBattle) {
            GameObject player = GameObject.FindGameObjectWithTag ("Player");
            player.GetComponent<PlayerScript> ().enabled = false;
            envScript = GetComponent<BattleEnvironmentScript> ();
            envScript.iniBattleScene (enemyObj);
            isBattle (true);
        }
    }
    public void EndBattle () {
        if (inBattle) {
            envScript = GetComponent<BattleEnvironmentScript> ();
            envScript.endCombat ();
            GameObject player = GameObject.FindGameObjectWithTag ("Player");
            player.GetComponent<PlayerScript> ().enabled = true;
            player.GetComponent<PlayerScript> ().importPlayerStat ();
            isBattle (false);
        }
    }

    public void Announcement (string message) {
        if (inBattle) {
            HUDscript.CombatAnnouncement (message);
        }
    }
    public void MonsterHPtext (int monsterHP) {
        if (inBattle) {
            HUDscript.MonsterHPText (monsterHP);
        }
    }

}