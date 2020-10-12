using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject objectToSpawn;
    public float spawnTimer = 3f;
    public bool needToSpawn = true;

    void Update() {
        if (needToSpawn) {
            StartCoroutine(Spawn(spawnTimer));
            needToSpawn = false;
        }
    }

    IEnumerator Spawn(float waitTime) {
        Instantiate(objectToSpawn, transform.position, transform.rotation);
        yield return new WaitForSeconds(waitTime);
        needToSpawn = true;
    }
}
