using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float DurationNextUnit = 5;
    public float DurationNext = 5;
    public string Title = "Wave!!!!";
    public GameObject Path;
    public bool isSpawnedAll = false;
    public bool isStartSpawn = false;

    [System.Serializable]
    public class WavePart
    {
        public GameObject EnemyPrefab;
        public int countInWave = 1;
    }

    public WavePart[] WaveOption;
    
    public void Spawn(GameObject prefab)
    {
        Instantiate(prefab, this.transform.position, this.transform.rotation);
    }
    // Update is called once per frame
    void Update () {

        if (!isStartSpawn) return;
        DurationNextUnit -= Time.deltaTime;
        if (DurationNextUnit < 0)
        {
            DurationNextUnit = DurationNext;

            bool didSpawn = false;

            foreach (var wc in WaveOption)
            {
                if (wc.countInWave >= 0)
                {
                    // Spawn it!
                    wc.countInWave--;
                    Spawn(wc.EnemyPrefab);
                    didSpawn = true;
                    break;
                }
                isSpawnedAll = true;
            }

            if (didSpawn == false)
            {

                if (transform.parent.childCount > 1)
                {
                    transform.parent.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    // That was the last wave -- what do we want to do?
                    // What if instead of DESTROYING wave objects,
                    // we just made them inactive, and then when we run
                    // out of waves, we restart at the first one,
                    // but double all enemy HPs or something?
                }

               // Destroy(gameObject);
            }
        }
    }
}
