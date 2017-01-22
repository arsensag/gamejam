using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour {

    public float lifeTime = 5.9f;
    public GameObject path;
    public ParticleSystem deathEffect;
    private List<Vector3> dots;
    private Vector3 next_pos;
    private Vector3 start_pos;
    private AudioSource aSource;

    public AudioClip deathmelody;


    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(IList<T> list)
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



    void Start () {

        aSource = GetComponent<AudioSource>();

        Vector3 spawnPosition = new Vector3(Random.Range(-19f, 19f), 0f, Random.Range(-19f, 19f));

        dots = new List<Vector3>();

        foreach (Transform child in path.transform)
        {
            dots.Add(child.transform.position);
        }

        next_pos = start_pos = dots.First();

        dots.Remove(next_pos);

        transform.position = next_pos;

        //startTime = Time.time;
        //journeyLength = Vector3.Distance(transform.position, next_pos);

        //Shuffle(dots);

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


    void Update () {
        Vector3 curr_pos = transform.position;
        //Debug.Log(Time.deltaTime);

        if ((next_pos - curr_pos).magnitude <= speed * Time.deltaTime)
        {
            Debug.Log(dots.Count);

            if (dots.Count==0)
            {
                Destroy(gameObject, 1.0f);
                return;
            }

            start_pos = next_pos;

            next_pos = dots.First();
            dots.Remove(next_pos);

            startTime = Time.time;
            journeyLength = Vector3.Distance(start_pos, next_pos);
        }

        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        transform.position = Vector3.Slerp(start_pos, next_pos, fracJourney);


        Vector3 dir = next_pos - this.transform.localPosition;
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
    }
}
