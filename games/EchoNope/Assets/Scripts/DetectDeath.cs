using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectDeath : MonoBehaviour
{
    public GameObject GameOverPanel;
    public Transform Player;
    public bool ifFollow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ifFollow)
        {
            Vector3 position = new Vector3(Player.position.x, transform.position.y, transform.position.z);
            transform.position = position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RankKeep.SaveRecord(PlayerScore.Score);
            PlayerScore.Score = 0;
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            GameOverPanel.SetActive(true);
            
        }
    }
}
