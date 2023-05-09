using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    private bool justone=false;
    public GameObject objectToSpawn;

    private void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag=="Respawn" && justone==false)
        {
            // Spawn the new object at the same position as the picked-up object
            Instantiate(objectToSpawn, transform.position + new Vector3(0f,1.88f,0f), transform.rotation);
            justone = true;
            StartCoroutine(PausWait());
        }
    }

    IEnumerator PausWait()
    {
        justone = true;

        yield return new WaitForSeconds(1);
        justone = false;
    }
}
