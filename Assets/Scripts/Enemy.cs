using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour {

    public float lifeTime = 5.9f;
    public GameObject path;
    public ParticleSystem deathEffect;

    public AudioClip deathmelody;

    public float speed = 1.0F;

    private Moving moving;

    private AudioSource aSource;


    private float startTime;
    private float journeyLength;

    void Start ()
    {
        moving = GetComponent<CircleMoving>();

        aSource = GetComponent<AudioSource>();

        transform.position = moving.GetFirts();
    }

    private void OnDestroy()
    {

        aSource = Camera.main.GetComponent<AudioSource>();
        
        aSource.PlayOneShot(deathmelody);

        ParticleSystem explosionEffect = Instantiate(deathEffect) as ParticleSystem;

        explosionEffect.transform.position = transform.position;

        //play it
        explosionEffect.loop = false;
        explosionEffect.Play();

        Destroy(explosionEffect.gameObject, explosionEffect.duration);

    }

    void Update ()
    {
        Tuple<Vector3, Quaternion> next = moving.GetNext(transform.position);

        transform.position = next.Item1;
        transform.rotation = next.Item2;

        /*if(Time.time > 10)
        {
            moving = GetComponent<PathMoving>();
        }*/

    }
}
