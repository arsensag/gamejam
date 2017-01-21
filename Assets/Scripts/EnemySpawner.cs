using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float DelayToNextWave = 8.25f;
    public float DelayVave = 50.25f;
    public List<Wave> WaveParts;
    public GameObject prefab;
    public int waveNumber = 0;

    public void Spawn() {

    }

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
        {
            Wave temp = child.GetComponent(typeof(Wave)) as Wave;
            WaveParts.Add(temp);
        }
	}
	
	// Update is called once per frame
	void Update () {
        DelayToNextWave -= Time.deltaTime;
        if (DelayToNextWave < 0)
        {
            DelayToNextWave = DelayVave;
            foreach(var temp in WaveParts)
            {
                if (temp.isSpawnedAll) continue;
                if (temp != null)
                {
                    temp.isStartSpawn = true;
                    break;
                }
            }

        }

    }
}
