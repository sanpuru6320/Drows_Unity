using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverLifetime : MonoBehaviour {

    public float lifetime; //敵の攻撃時の注目エフェクト表示時間
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, lifetime);
	}
}
