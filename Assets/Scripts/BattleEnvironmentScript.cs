using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnvironmentScript : MonoBehaviour {
    public GameObject BattleWindow;
    public Transform BattleSceneCanvas;
    public GameObject m_cam;
    public GameObject HUDCanvas;
    public GameObject BattleCanvas;
    public GameObject BlackScreen;
    public GameObject playerObj;
    public GameObject[] enemyPrefab;
    GameObject enemyObject;
    GameObject playerCombatObj;
    GameObject enemyObjectForDelete;
    bool DoneSceneSetting = false;
    // Start is called before the first frame update
    public void iniBattleScene (GameObject enemyObj) {
        enemyObjectForDelete = enemyObj;
        HUDCanvas = GameObject.FindGameObjectWithTag ("HUDCanvas");
        HUDCanvas.GetComponent<Canvas> ().enabled = false;
        BattleCanvas = GameObject.FindGameObjectWithTag ("BattleCanvas");
        BattleCanvas.GetComponent<Canvas> ().enabled = true;

        m_cam = GameObject.FindGameObjectWithTag ("MainCamera");
        m_cam.GetComponent<DarkScreenScript> ().setGrab (true);
        BlackScreen = GameObject.FindGameObjectWithTag ("BlackScreen");
        BlackScreen.GetComponent<SpriteRenderer> ().enabled = true;
        DoneSceneSetting = true;
        Debug.Log ("Start Fight Scene");
        if (DoneSceneSetting) {
            initCharacter ();
        }
        DoneSceneSetting = false;
    }

    public void endCombat () {
        Destroy (enemyObjectForDelete);
        Destroy (enemyObject);
        playerCombatObj.GetComponent<PlayerBattleScript> ().givePlayerStat ();
        playerCombatObj.SetActive (false);

        HUDCanvas = GameObject.FindGameObjectWithTag ("HUDCanvas");
        HUDCanvas.GetComponent<Canvas> ().enabled = true;
        BattleCanvas = GameObject.FindGameObjectWithTag ("BattleCanvas");
        BattleCanvas.GetComponent<Canvas> ().enabled = false;
        BlackScreen = GameObject.FindGameObjectWithTag ("BlackScreen");
        BlackScreen.GetComponent<SpriteRenderer> ().sprite = null;
        BlackScreen.GetComponent<SpriteRenderer> ().enabled = false;
        Debug.Log ("BattleEnd");
    }

    void Update () {
        if (!GameManager.instance.playerCombatTurn) {
            return;
        }

    }

    void initCharacter () {
        playerCombatObj = Instantiate (playerObj, new Vector3 (-5.26f, 2.25f, 0f), Quaternion.identity) as GameObject;
        int monstertype = enemyObjectForDelete.GetComponent<EventScript> ().GetMonsterType ();
        enemyObject = Instantiate (enemyPrefab[(monstertype - 1)], new Vector3 (5.15f, 2.25f, 0f), Quaternion.identity) as GameObject;
        enemyObject.GetComponent<SpriteRenderer> ().sortingLayerName = "FightScene";
        playerCombatObj.GetComponent<PlayerBattleScript> ().startCombatScript (enemyObject);
        enemyObject.GetComponent<CombatScript> ().SetPlayerObject (playerCombatObj);
    }
}