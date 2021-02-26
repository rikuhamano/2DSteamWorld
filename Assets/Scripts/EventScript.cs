/////using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScript : MonoBehaviour {

    public int ObjectType = 0; //0:Null 1:Enemy 2:Item
    // Start is called before the first frame update
    public int monsterType = 0;
    public CombatScript combatScript;
    public PlayerBattleScript playerBattleScript;

    private void OnTriggerEnter2D (Collider2D player) {
        if (player.tag == "Player") {
            switch (ObjectType) {
                case 1:
                    GameManager.instance.StartBattle (this.gameObject);
                    // Debug.Log ("Monster");
                    this.enabled = false;
                    break;
                case 2:
                    // Debug.Log ("item");
                    int ItemFromBox = randomItem ();
                    player.GetComponent<PlayerScript> ().getItem (ItemFromBox);
                    this.gameObject.SetActive (false);
                    break;
                case 0:
                    Debug.Log ("Null");
                    break;
            }
        }
    }
    public int GetMonsterType () {
        return monsterType;
    }

    int randomItem () {
        float probHP = 40;
        float probBullet = 20;
        float probskill = 20;
        float probbuff = 15;
        float probdebuff = 5;
        float RandomProb = 200;
        float allprob = probHP + probBullet + probskill + probdebuff + probbuff;
        int item = 0;

        //0:HP 1:bullet 2:skill 3:buff 4:debuff
        while (RandomProb > allprob) {
            RandomProb = allprob * Random.value;
        }
        if (RandomProb < probHP) {
            float RandSubHP = Random.value;
            if (RandSubHP < 0.7) item = 1;
            else item = 1;
        } else if (RandomProb < (probHP + probBullet)) {
            item = 2;
        } else if (RandomProb < (probHP + probBullet + probskill)) {
            item = 3;
        } else if (RandomProb < (probHP + probBullet + probskill + probbuff)) {
            item = 4;
        } else if (RandomProb < (probHP + probBullet + probskill + probbuff + probdebuff)) {
            item = 5;
        }
        return item;
    }
}