using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float lifeTime = 5.9f;
	void Start () {
		
	}
	
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0) Destroy(gameObject);
        Vector3 spawnPosition = new Vector3(Random.Range(-19f, 19f), 0f, Random.Range(-19f, 19f));

        transform.position = spawnPosition;
    }
}
