using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGameOverText : MonoBehaviour
{
    public Text ScoreText;

    public Text RankText;

    public Text UserNameText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        string userName = SetUserName.UserName;
        ScoreText.text = RankKeep.GetScore(userName).ToString();
        RankText.text = RankKeep.GetRank(userName).ToString();
        UserNameText.text = userName;
    }
}