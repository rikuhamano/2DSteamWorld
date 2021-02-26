using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour {

    public int monsterHP;
    public int monsterAtk;
    public int monsterSpeed;
    GameObject player;
    bool inCountdown = false;

    public void SetPlayerObject (GameObject playerObject) {
        player = playerObject;
        GameManager.instance.MonsterHPtext (this.monsterHP);
    }

    public void modifyMonsterHP (int HP) {
        this.monsterHP += HP;
        GameManager.instance.MonsterHPtext (this.monsterHP);
    }

    public void setMonsterAtk (int atk) {
        this.monsterAtk += atk;
    }

    public void setMonsterSpeed (int speed) {
        this.monsterSpeed += speed;
    }

    void isEnd () {
        if (this.monsterHP <= 0) {
            player.GetComponent<PlayerBattleScript> ().battleEnd ();
            GameManager.instance.EndBattle ();
        } else {
            return;
        }
    }

    void Update () {
        if (!inCountdown && GameManager.instance.inBattle) {
            StartCoroutine (Countdown (3));
        }
        isEnd ();
    }

    IEnumerator Countdown (int second) {
        inCountdown = true;
        int counter = second + 1;
        while (counter > 0) {
            yield return new WaitForSeconds (1);
            counter--;
        }
        player.GetComponent<PlayerBattleScript> ().PlayerGetHit (monsterAtk);
        GameManager.instance.Announcement ("EnemyTurn");
        inCountdown = false;
    }

}