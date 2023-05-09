using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_spawner : MonoBehaviour
{
    public static Cube_spawner instace;

    public GameObject[] cube_prefabs;

    public Vector3 center;
    public Vector3 range;

    private void Awake()
    {
        if (instace == null)
        {
            instace = this;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, range);
    }

    public void SpawnCube()
    {

        float x = Random.Range(-range.x / 2, range.x / 2);
        float y = Random.Range(-range.y / 2, range.y / 2);
        float z = Random.Range(-range.z / 2, range.z / 2);

        Vector3 pos = transform.localPosition + new Vector3(x, -1, z);

        int randomCube = Random.Range(0, 3);

        GameObject cube = Instantiate(cube_prefabs[randomCube], pos, Quaternion.identity);
        GameManager.instance.cubes.Add(cube);
    }
}
