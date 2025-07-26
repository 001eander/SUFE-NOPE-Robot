using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject obj;
    public GameObject player;
    public GameObject stopItemObj;
    public GameObject rocketObj;

    public float distLimit = 5f;

    public float splitLimiMax = 12f;
    public float splitLimiMin = 9f;

    public float nextSpawn;

    private float stopItemColdDown = 5f;
    private float rocketColdDown = 5f;
    private float nextStopItemTime = 0f;
    private float nextRocketTime = 0f;

    private float stopItemProb = 0.6f;
    private float rocketProb = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = transform.position.x;
        nextStopItemTime = Time.time + stopItemColdDown;
        nextRocketTime = Time.time + rocketColdDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSpawn - player.transform.position.x < distLimit)
        {
            float temp = Random.Range(splitLimiMin, splitLimiMax);
            nextSpawn = nextSpawn + temp;
            Vector3 spawnP = new Vector3(nextSpawn, transform.position.y, transform.position.z) ;
            Instantiate(obj, spawnP, transform.rotation);

            if (nextRocketTime< Time.time && Random.Range(0f,1f)<rocketProb)
            {
                
                Vector3 r_spawnP = new Vector3(nextSpawn, 3.93f, transform.position.z) ;
                Instantiate(rocketObj, r_spawnP, transform.rotation);
                nextRocketTime = Time.time + rocketColdDown;
            }
            if (nextStopItemTime< Time.time && Random.Range(0f,1f)<stopItemProb)
            {
                Vector3 s_spawnP = new Vector3(nextSpawn, 0.76f, transform.position.z) ;
                Instantiate(stopItemObj, s_spawnP, transform.rotation);
                nextStopItemTime = Time.time + stopItemColdDown;
            }
        }
    }
}