using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_properties : MonoBehaviour
{
    public static Cube_properties instance;

    public int point;
    public int sec;

    public bool auto_destroy;
    public float auto_destroy_time;
    public bool auto_destroy_time_random;
    public Vector2 auto_destroy_random_time;

    private bool can_play = true;

    public GameObject explosion;
    public GameObject fire;
    public GameObject small_explosion;

    private MeshRenderer mesh;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (auto_destroy)
        {
            if (auto_destroy_time_random)
            {
                float random = Random.Range(auto_destroy_random_time.x, auto_destroy_random_time.y);
                StartCoroutine(DestroyCube(random));
                
            }
            else
            {
                StartCoroutine(DestroyCube(auto_destroy_time));
            }   
        }

        mesh = this.GetComponent<MeshRenderer>();
    }

    IEnumerator DestroyCube(float sec)
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(sec);

            GameObject effect = Instantiate(fire, transform.position, fire.transform.rotation);
            effect.transform.parent = gameObject.transform;

            GameManager.instance.cubes.Remove(this.gameObject);

            Destroy(this.gameObject,1.2f);
            Destroy(effect.gameObject, 1f);

            yield return new WaitForSeconds(.7f);

            mesh.enabled = false;

            effect = Instantiate(small_explosion, transform.position, transform.rotation);
            effect.transform.parent = gameObject.transform;
            Destroy(effect.gameObject, 0.5f);
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.transform.tag == "Cube")
        {
            if (can_play)
            {
                can_play = false;
                float volume = PlayerPrefs.GetFloat(GameManager.gameplay_sound_PP);
                if (volume != 0)
                {
                    GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(GameManager.gameplay_sound_PP) / 1.1f;
                }
                
                GetComponent<AudioSource>().Play();
                StartCoroutine(Reset());
            }
            
        }*/
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1f);
        can_play = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Projectile")
        { 
            GameManager.instance.SetScore(point);
            GameManager.instance.CubeTime(sec);

            GameObject effect = Instantiate(explosion, transform.position, explosion.transform.rotation);

            GameManager.instance.cubes.Remove(this.gameObject);
            Destroy(this.gameObject);
            Destroy(effect,2f);
        }

        
    }
}
