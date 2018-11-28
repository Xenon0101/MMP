using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollectorScript : MonoBehaviour {

    private GameObject[] pipeHolders;
    private float distance = 3f;
    private float lastPipeX;
    private float pipeMin = -1f;
    private float pipeMax = 2.3f;

    // Use this for initialization
    void Awake () {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");
        for (int i = 0; i < pipeHolders.Length; i++) {
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);
            pipeHolders[i].transform.position = temp;
        }

        lastPipeX = pipeHolders[0].transform.position.x;
        for (int i = 0; i < pipeHolders.Length; i++) {
            if(lastPipeX < pipeHolders[i].transform.position.x) {
                lastPipeX = pipeHolders[i].transform.position.x;
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "PipeHolder") {
            Vector3 temp = target.transform.position;

            temp.x = lastPipeX + distance;
            temp.y = Random.Range(pipeMin, pipeMax);

            target.transform.position = temp;
            lastPipeX = temp.x;
        }
    }
}
