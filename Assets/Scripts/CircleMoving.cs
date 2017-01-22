using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class CircleMoving : Moving
{

    // Use this for initialization

    public float minSpawnDistance = 30f;
    public float maxSpawnDistance = 50f;
    public bool randomSpawn = true;
    public bool randomAtitude = true;

    private Vector3 spawnPosition;
    private Enemy enemy;

    void Start()
    {

        enemy = GetComponent<Enemy>();

    }

    public override Vector3 GetFirts()
    {
        Vector3 random = new Vector3(Random.Range(minSpawnDistance, maxSpawnDistance), 0f, Random.Range(minSpawnDistance, maxSpawnDistance));
        spawnPosition = Camera.main.transform.position + random * System.Convert.ToInt32(randomSpawn);

        return spawnPosition;
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle, float alpha)
    {
        return angle * (point - pivot) + pivot + new Vector3(0, alpha * Mathf.Sin(point.x + point.z) / 3, 0);
    }

    public override Tuple<Vector3, Quaternion> GetNext(Vector3 curr_pos)
    {
        Vector3 next_pos;

        float alpha = enemy.speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0, alpha, 0);
        next_pos = RotatePointAroundPivot(transform.position, Camera.main.transform.position, targetRotation, alpha);

        Vector3 dir = next_pos - this.transform.localPosition;
        Quaternion targetRotation2 = Quaternion.LookRotation(dir);
        Quaternion next_rotation = Quaternion.Lerp(this.transform.rotation, targetRotation2, Time.deltaTime * 5);

        Tuple<Vector3, Quaternion> result = Tuple.Create(next_pos, next_rotation);

        return result;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
