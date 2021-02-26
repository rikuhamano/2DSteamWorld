using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloorScript : MonoBehaviour {

    public GameObject[] enemy;
    public GameObject[] item;
    GameObject toInstantiate;


    // public void FloorEvent () {
    //     if (FloorType == 1) {
    //         //EnemyEvent();
    //     } else if (FloorType == 2) {
    //         randomItem ();
    //     }
    // }


    public void EnemyEvent () {
        Debug.Log ("Enemy!");
    }

}