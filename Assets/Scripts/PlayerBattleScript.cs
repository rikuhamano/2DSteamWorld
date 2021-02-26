using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LookUpItem;
using static BuffScript;

public class PlayerBattleScript : MonoBehaviour {

    public int playerHP;
    public int playerBullet;
    int[] ItemBox = new int[] { 0, 0, 0, 0 }; // import from travel stage
    int[] travelBuff = new int[] { 0, 0, 0, 0 }; // import from travel stage
    int[] combatBuff = new int[] { 0, 0 }; //only gain by using item
    int deBuffBox;

    int weapon = 0;
    public int bulletDamage = 20;
    int armor = 0;

    bool isUseBullet = false;
    bool playerTurn = false;
    bool inCountDown = false;
    // bool isfrenzy = false;

    GameObject playerObject;
    GameObject enemyObject;
    CombatScript combatScript;

    public void getPlayerStat () {
        playerHP = GameManager.instance.playerStat.HPs;
        playerBullet = GameManager.instance.playerStat.bullets;
        ItemBox[0] = GameManager.instance.playerStat.itemStack1;
        ItemBox[1] = GameManager.instance.playerStat.itemStack2;
        ItemBox[2] = GameManager.instance.playerStat.itemStack3;
        ItemBox[3] = GameManager.instance.playerStat.itemStack4;
        int[] Buffer;
        Buffer = GameManager.instance.playerStat.buffStack1;
        travelBuff[0] = Buffer[0];
        Buffer = GameManager.instance.playerStat.buffStack2;
        travelBuff[1] = Buffer[0];
        Buffer = GameManager.instance.playerStat.buffStack3;
        travelBuff[2] = Buffer[0];
        Buffer = GameManager.instance.playerStat.buffStack4;
        travelBuff[3] = Buffer[0];
        Buffer = GameManager.instance.playerStat.debuff;
        deBuffBox = Buffer[0];
        weapon = GameManager.instance.playerStat.weapon;
        armor = GameManager.instance.playerStat.armor;
    }

    public void givePlayerStat () {
        GameManager.instance.getCombatStat (playerHP, playerBullet, ItemBox[0], ItemBox[1], ItemBox[2], ItemBox[3]);
    }

    public void battleEnd () {
        givePlayerStat ();
        Destroy (this);
    }

    public int getReloadSpeed () {
        int CumulativeReloadSpeed;
        CumulativeReloadSpeed = BuffScript.reloadBuff () + LookUpItem.WeaponSpeed (weapon) + LookUpItem.ArmorSpeed (armor);
        return CumulativeReloadSpeed;
    }

    public int getPlayerDmg () {
        int playerAllDmg;
        playerAllDmg = BuffScript.DmgBuff () + LookUpItem.WeaponDmg (weapon);
        return playerAllDmg;
    }

    public int getPlayerDef () {
        int playerAllDef;
        playerAllDef = BuffScript.DefBuff () + LookUpItem.ArmorDef (armor);
        return playerAllDef;
    }

    public void UseBullet () {
        if (!isUseBullet && (playerBullet > 0)) {
            isUseBullet = true;
            GameManager.instance.Announcement ("Using Special Bullet");
            playerBullet -= 1;
        } else {
            GameManager.instance.Announcement ("Special Bullet is already in barrel");
        }
    }

    public int Attack () {
        int inflictDamage = getPlayerDmg ();
        if (isUseBullet && playerBullet > 0) {
            inflictDamage = getPlayerDmg () + bulletDamage;
            isUseBullet = false;
        }
        return (-inflictDamage);
    }

    public void PlayerGetHit (int damage) {
        int receivedDamage = getPlayerDef () - damage;
        if (receivedDamage >= 0)
            receivedDamage = 10;
        playerHP += receivedDamage;
    }

    public void startCombatScript (GameObject enemy) {
        enemyObject = enemy;
        getPlayerStat ();
    }

    IEnumerator Countdown (int second) {
        inCountDown = true;
        int counter = second;
        while (counter > 0) {
            yield return new WaitForSeconds (1);
            counter--;
        }
        GameManager.instance.Announcement ("PlayerTurn");
        playerTurn = true;
        inCountDown = false;
    }

    void useItemAt (int itemIndex) {
        if (ItemBox[itemIndex] == 0) {
            GameManager.instance.Announcement ("No Item to use");
        } else {
            int itemTypeNum = ItemBox[itemIndex];
            GameManager.instance.Announcement ("Use " + LookUpItem.LookupItem (itemTypeNum));
            ItemActive (itemTypeNum);
            ItemBox[itemIndex] = 0;
        }
    }
    void ItemActive (int item) {
        //0:Null 1:HP 2:skill 
        if (item == 1) {
            playerHP += 20;
        } else if (item == 2) {
            playerBullet += 1;
        } else if (item == 3) { } else { }
        // else {

        // }
    }

    // Update is called once per frame
    void Update () {
        if (playerTurn) {
            if (Input.GetKeyUp (KeyCode.K)) {
                enemyObject.GetComponent<CombatScript> ().modifyMonsterHP (-1000);
                Debug.Log ("Kill!");
                playerTurn = false;
            }
            if (Input.GetKeyUp (KeyCode.A)) {
                enemyObject.GetComponent<CombatScript> ().modifyMonsterHP (Attack ());
                Debug.Log ("Attack!");
                playerTurn = false;
            }
            if (Input.GetKeyUp (KeyCode.B)) {
                UseBullet ();
            }
            if (Input.GetKeyUp (KeyCode.Alpha1)) {
                useItemAt (0);
                playerTurn = false;
            }
            if (Input.GetKeyUp (KeyCode.Alpha2)) {
                useItemAt (1);
                playerTurn = false;
            }
            if (Input.GetKeyUp (KeyCode.Alpha3)) {
                useItemAt (2);
                playerTurn = false;
            }
            if (Input.GetKeyUp (KeyCode.Alpha4)) {
                useItemAt (3);
                playerTurn = false;
            }
        } else if (!playerTurn) {
            if (!inCountDown)
                StartCoroutine (Countdown (2));
        }
        givePlayerStat ();
    }

}