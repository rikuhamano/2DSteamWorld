using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LookUpItem;
using static BuffScript;

public class HUDTextHandler : MonoBehaviour {

    // LookUpItem itemScript;
    // BuffScript buffScript;
    public Text HPText;
    public Text AtkText;
    public Text DefText;
    public Text BulletText;
    public Text BuffStack1;
    public Text BuffStack2;
    public Text BuffStack3;
    public Text BuffStack4;
    public Text DebuffStack;
    public Text itemSlot1;
    public Text itemSlot2;
    public Text itemSlot3;
    public Text itemSlot4;
    public Text Armor;
    public Text Weapon;
    public Text ReloadSpeed;
    public Text BattleHPText;
    public Text BattleAtkText;
    public Text BattleDefText;
    public Text BattleBulletText;
    public Text BattleitemSlot1;
    public Text BattleitemSlot2;
    public Text BattleitemSlot3;
    public Text BattleitemSlot4;
    public Text MonsterHP;

    int playerHP;
    int PlayerArmor;
    int PlayerWeapon;
    int[][] buffBox;
    int[] deBuffBox;
    int playerBullet;
    int[] ItemBox;
    // private void Awake () {
    //     itemScript = GetComponent<LookUpItem> ();
    //     buffScript = GetComponent<BuffScript> ();
    // }
    public void ChangeText () {
        getPlayerStat ();
        if (GameManager.instance.inBattle) {
            BattleCanvasText ();
        } else {
            TravelCanvasText ();
        }
    }
    void getPlayerStat () {
        playerHP = GameManager.instance.playerStat.HPs;
        PlayerWeapon = GameManager.instance.playerStat.weapon;
        PlayerArmor = GameManager.instance.playerStat.armor;
        playerBullet = GameManager.instance.playerStat.bullets;

        int[] buff1 = GameManager.instance.playerStat.buffStack1;
        int[] buff2 = GameManager.instance.playerStat.buffStack2;
        int[] buff3 = GameManager.instance.playerStat.buffStack3;
        int[] buff4 = GameManager.instance.playerStat.buffStack4;
        buffBox = new int[][] { buff1, buff2, buff3, buff4 };

        deBuffBox = GameManager.instance.playerStat.debuff;

        int itemBox1 = GameManager.instance.playerStat.itemStack1;
        int itemBox2 = GameManager.instance.playerStat.itemStack2;
        int itemBox3 = GameManager.instance.playerStat.itemStack3;
        int itemBox4 = GameManager.instance.playerStat.itemStack4;
        ItemBox = new int[] { itemBox1, itemBox2, itemBox3, itemBox4 };

    }
    void TravelCanvasText () {
        HPText.text = "HP: " + playerHP;
        AtkText.text = "Atk: " + LookUpItem.WeaponDmg (PlayerWeapon) + " + " + BuffScript.DmgBuff ();
        DefText.text = "Def: " + LookUpItem.ArmorDef (PlayerArmor) + " + " + BuffScript.DefBuff ();
        BulletText.text = "Bullet: " + playerBullet;
        BuffStack1.text = "Buff1: " + LookUpItem.LookupBuff (buffBox[0][0]) + " " + buffBox[0][1] + "t";
        BuffStack2.text = "Buff2: " + LookUpItem.LookupBuff (buffBox[1][0]) + " " + buffBox[1][1] + "t";
        BuffStack3.text = "Buff3: " + LookUpItem.LookupBuff (buffBox[2][0]) + " " + buffBox[2][1] + "t";
        BuffStack4.text = "Buff4: " + LookUpItem.LookupBuff (buffBox[3][0]) + " " + buffBox[3][1] + "t";
        DebuffStack.text = "Debuff: " + LookUpItem.LookupDebuff (deBuffBox[0]) + " " + deBuffBox[1] + "t";
        itemSlot1.text = "itemSlot1: " + LookUpItem.LookupItem (ItemBox[0]);
        itemSlot2.text = "itemSlot2: " + LookUpItem.LookupItem (ItemBox[1]);
        itemSlot3.text = "itemSlot3: " + LookUpItem.LookupItem (ItemBox[2]);
        itemSlot4.text = "itemSlot4: " + LookUpItem.LookupItem (ItemBox[3]);
        Armor.text = "Armor: " + LookUpItem.LookupArmor (PlayerArmor);
        Weapon.text = "Weapon: " + LookUpItem.LookupWeapon (PlayerWeapon);
        ReloadSpeed.text = "ReloadSpeed: " + (LookUpItem.WeaponSpeed (PlayerWeapon) + LookUpItem.ArmorSpeed (PlayerArmor)) + " + " + BuffScript.reloadBuff ();
    }

    void BattleCanvasText () {
        BattleHPText.text = "HP: " + playerHP;
        BattleAtkText.text = "Atk: " + LookUpItem.WeaponDmg (PlayerWeapon) + " + " + BuffScript.DmgBuff ();
        BattleDefText.text = "Def: " + LookUpItem.ArmorDef (PlayerArmor) + " + " + BuffScript.DefBuff ();
        BattleBulletText.text = "Bullet: " + playerBullet;
        BattleitemSlot1.text = "itemSlot1: " + LookUpItem.LookupItem (ItemBox[0]);
        BattleitemSlot2.text = "itemSlot2: " + LookUpItem.LookupItem (ItemBox[1]);
        BattleitemSlot3.text = "itemSlot3: " + LookUpItem.LookupItem (ItemBox[2]);
        BattleitemSlot4.text = "itemSlot4: " + LookUpItem.LookupItem (ItemBox[3]);
    }

    public void MonsterHPText (int monsterHP) {
        MonsterHP.text = "MonsterHP: " + monsterHP;
    }
    
    public Text Announcement;
    public void CombatAnnouncement (string message) {
        Announcement.text = message;

    }

}