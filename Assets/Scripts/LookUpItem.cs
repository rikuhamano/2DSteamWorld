using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUpItem : MonoBehaviour {

    public static string LookupWeapon (int weap) {
        string waeponString = "none";
        switch (weap) {
            case 0:
                waeponString = "Pistol"; //Dmg = 10 Speed = 25
                break;
            case 1:
                waeponString = "Repeater"; //Dmg = 20 Speed = 10
                break;
            case 2:
                waeponString = "Rifle"; //Dmg = 30 Speed = 0
                break;
            case 3:
                waeponString = "Hand Cannon"; //Dmg = 40 Speed = -5
                break;
        }
        return waeponString;
    }
    public static int WeaponDmg (int weap) {
        int weaponDmg = 0;
        switch (weap) {
            case 0:
                // "Pistol" Dmg = 10 Speed = 25
                weaponDmg = 10;
                break;
            case 1:
                // "Repeater" Dmg = 20 Speed = 10
                weaponDmg = 20;
                break;
            case 2:
                // "Rifle" Dmg = 30 Speed = 0
                weaponDmg = 30;
                break;
            case 3:
                // "Hand Cannon" //Dmg = 40 Speed = -5
                weaponDmg = 40;
                break;
        }
        return weaponDmg;
    }
    public static int WeaponSpeed (int weap) {
        int weaponSpeed = 0;
        switch (weap) {
            case 0:
                // "Pistol" Dmg = 10 Speed = 25
                weaponSpeed = 25;
                break;
            case 1:
                // "Repeater" Dmg = 20 Speed = 10
                weaponSpeed = 10;
                break;
            case 2:
                // "Rifle" Dmg = 30 Speed = 0
                weaponSpeed = 0;
                break;
            case 3:
                // "Hand Cannon" //Dmg = 40 Speed = -5
                weaponSpeed = -5;
                break;
        }
        return weaponSpeed;
    }

    public static string LookupArmor (int wear) {
        string ArmorString = "none";
        switch (wear) {
            case 0:
                ArmorString = "Adventure suit"; //Armor 0 speed 10
                break;
            case 1:
                ArmorString = "Light Armor plate"; // Armor 5 speed 5
                break;
            case 2:
                ArmorString = "Medium Armor plate"; // Armor 10 speed 0
                break;
            case 3:
                ArmorString = "Full Plate"; // Armor 15 speed -5
                break;
            default:
                ArmorString = "Adventure suit";
                break;
        }
        return ArmorString;
    }
    public static int ArmorDef (int wear) {
        int armorDef = 0;
        switch (wear) {
            case 0:
                // "Adventure suit"Armor 0 speed 10
                armorDef = 0;
                break;
            case 1:
                // "Light Armor plate" Armor 5 speed 5
                armorDef = 5;
                break;
            case 2:
                // "Medium Armor plate" Armor 10 speed 0
                armorDef = 10;
                break;
            case 3:
                // "Full Plate" Armor 15 speed -5
                armorDef = 15;
                break;
            default:
                // "Adventure suit";
                armorDef = 0;
                break;
        }
        return armorDef;
    }
    public static int ArmorSpeed (int wear) {
        int armorSpeed = 0;
        switch (wear) {
            case 0:
                // "Adventure suit"Armor 0 speed 10
                armorSpeed = 10;
                break;
            case 1:
                // "Light Armor plate" Armor 5 speed 5
                armorSpeed = 5;
                break;
            case 2:
                // "Medium Armor plate" Armor 10 speed 0
                armorSpeed = 0;
                break;
            case 3:
                // "Full Plate" Armor 15 speed -5
                armorSpeed = -5;
                break;
            default:
                // "Adventure suit";
                armorSpeed = 10;
                break;
        }
        return armorSpeed;
    }

    public static string LookupItem (int itemNumber) {
        string itemString = "none";
        switch (itemNumber) {
            case 1:
                itemString = "HPpotion";
                break;
            case 2:
                itemString = "Bullet";
                break;
            case 3:
                itemString = "Skill";
                break;
            case 4:
                itemString = "Buff";
                break;
            case 5:
                itemString = "Debuff";
                break;
            default:
                itemString = "None";
                break;
        }
        return itemString;
    }
    public static string LookupBuff (int buffNumber) {
        string BuffString = "none";
        switch (buffNumber) {
            case 1:
                BuffString = "Dmg up(s)";
                break;
            case 2:
                BuffString = "Dmg up(M)";
                break;
            case 3:
                BuffString = "Dmg up(L)";
                break;
            case 4:
                BuffString = "Def up";
                break;
            case 5:
                BuffString = "Frenzy";
                break;
            case 6:
                BuffString = "Reload Speed Up";
                break;
            case 7:
                BuffString = "Regenerate";
                break;
            default:
                BuffString = "None";
                break;
        }
        return BuffString;
    }

    public static string LookupDebuff (int DebuffNumber) {
        string deBuffString = "none";
        switch (DebuffNumber) {
            case 1:
                deBuffString = "Dmg down";
                break;
            case 2:
                deBuffString = "Def down";
                break;
            case 3:
                deBuffString = "Poison";
                break;
            case 4:
                deBuffString = "Reload Speed Down";
                break;
            default:
                deBuffString = "None";
                break;
        }
        return deBuffString;
    }

}