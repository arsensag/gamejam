using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class Moving : MonoBehaviour
{

    // Use this for initialization


    void Start()
    {

    }

    abstract public Vector3 GetFirts();

    abstract public Tuple<Vector3, Quaternion> GetNext(Vector3 curr_pos);

    // Update is called once per frame
    void Update()
    {

    }
}
