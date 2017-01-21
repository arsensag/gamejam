using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float lifeTime = 5.9f;
    public GameObject path;

    private Queue<Vector3> dots;
    private Vector3 next_pos;
    public float speed = 5;

    void Start () {
        Vector3 spawnPosition = new Vector3(Random.Range(-19f, 19f), 0f, Random.Range(-19f, 19f));
        transform.position = spawnPosition;

        foreach (Transform child in path.transform)
        {
            dots.Enqueue(child.transform.position);
        }
        Debug.Log(dots);
        next_pos = dots.Dequeue();
    }
	
	void Update () {
        Destroy(this);
        Debug.Log(dots);

        Vector3 curr_pos = transform.position;

        transform.position = Vector3.Lerp(curr_pos, next_pos, speed);

        if (next_pos==curr_pos)
        {
            if (dots.Count==0)
            {
                Destroy(this);
            }
            next_pos = dots.Dequeue();
        }
    }
}
