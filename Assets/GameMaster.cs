using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    void Start () {
        if (gm == null) {
            gm = this;
        }
    }    
    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2f;
    public Transform spawnPrefab;

    public IEnumerator RespawnPlayer () {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds (spawnDelay);

        Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone.gameObject, 3f);
    }
    public static void KillPlayer (Player player) {
        Destroy (player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }
}
