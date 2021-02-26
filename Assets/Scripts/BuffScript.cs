using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : MonoBehaviour {

    static int[] buff1;
    static int[] buff2;
    static int[] buff3;
    static int[] debuff;
    static int[] buffbox;
    public static void getStat () {
        buff1 = GameManager.instance.playerStat.buffStack1;
        buff2 = GameManager.instance.playerStat.buffStack2;
        buff3 = GameManager.instance.playerStat.buffStack3;
        buffbox = new int[] { buff1[0], buff2[0], buff3[0] };
        debuff = GameManager.instance.playerStat.debuff;
    }
    public static int DmgBuff () {
        getStat ();
        int CumulativeAtkBuff = 0;
        foreach (int Buff in buffbox) {
            if (Buff == 1) { CumulativeAtkBuff += 5; }
            if (Buff == 2) { CumulativeAtkBuff += 10; }
            if (Buff == 3) { CumulativeAtkBuff += 15; }
            if (Buff == 4) { CumulativeAtkBuff -= 5; }
            if (Buff == 5) { CumulativeAtkBuff += 10; }
        }
        return CumulativeAtkBuff;
    }

    public static int DefBuff () {
        getStat ();
        int CumulativeDefBuff = 0;
        foreach (int Buff in buffbox) {
            if (Buff == 4) { CumulativeDefBuff += 5; }
            if (Buff == 5) { CumulativeDefBuff -= 10; }
        }
        if (debuff[0] == 2)
            CumulativeDefBuff -= 5;
        return CumulativeDefBuff;
    }

    public static int reloadBuff () {
        getStat ();
        int cumulativeSpeed = 0;
        foreach (int Buff in buffbox) {
            if (Buff == 6)
                cumulativeSpeed += 5;
            if (Buff == 5) cumulativeSpeed += 10;
        }
        return cumulativeSpeed;
    }

}