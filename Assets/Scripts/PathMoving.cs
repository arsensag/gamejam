using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Enemy))]
public class PathMoving : Moving
{

    // Use this for initialization
    public GameObject path;
    public bool shuffle;

    private List<Vector3> dots;
    private Vector3 next_pos;
    private Vector3 start_pos;
    private Vector3 curr_pos;

    private float startTime;
    private float journeyLength;

    private static System.Random rng = new System.Random();

    private Vector3 spawnPosition;
    private Enemy enemy;

    public static void ShuffleFun<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();

        dots = new List<Vector3>();

        foreach (Transform child in enemy.path.transform)
        {
            dots.Add(child.transform.position);
        }

        if (shuffle)
        {
            ShuffleFun(dots);
        }

    }

    public override Vector3 GetFirts()
    {
        start_pos = next_pos = dots.First();
        dots.Remove(start_pos);

        return start_pos;
    }

    public override Tuple<Vector3, Quaternion> GetNext(Vector3 curr_pos)
    {

        curr_pos = transform.position;

        startTime = Time.time;
        journeyLength = Vector3.Distance(curr_pos, next_pos);

        
        if ((next_pos - curr_pos).magnitude <= 3)
        {

            if (dots.Count == 0)
            {
                Destroy(gameObject, 0.5f);
                Debug.Log("Destroying!!!");

                //System.Tuple<Vector3, Quaternion> result2 = System.Tuple.Create(Vector3.zero, Quaternion.);

                //return result2;
            }

            start_pos = next_pos;

            next_pos = dots.First();
            dots.Remove(next_pos);

            journeyLength = Vector3.Distance(start_pos, next_pos);
        }

        float distCovered = (Time.time - startTime) * enemy.speed;
        float fracJourney = distCovered / journeyLength;

        Vector3 result_pos = Vector3.Lerp(start_pos, next_pos, fracJourney);

        /*
        Vector3 dir = next_pos - this.transform.localPosition;
        Quaternion targetRotation2 = Quaternion.LookRotation(dir);
        Quaternion next_rotation = Quaternion.Lerp(this.transform.rotation, targetRotation2, Time.deltaTime * 5);
        */

        Quaternion next_rotation = Quaternion.LookRotation(Camera.main.transform.position);

        Tuple<Vector3, Quaternion> result = Tuple.Create(result_pos, next_rotation);

        return result;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
