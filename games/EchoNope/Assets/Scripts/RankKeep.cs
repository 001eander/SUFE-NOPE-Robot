using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

[Serializable]
public class RankDataType
{
    public string UserName;
    public int Score;
}

public class RankKeep : MonoBehaviour
{
    // Start is called before the first frame update
    private static string userName = "test";

    private void Update()
    {
        userName = SetUserName.UserName;
    }

    public static void SaveRecord(int score)
    {
        string rankString;
        RankDataType[] rank;

        if (PlayerPrefs.HasKey("leaderRank"))
        {
            rankString = PlayerPrefs.GetString("leaderRank");
            rank = JsonHelper.FromJson<RankDataType>(rankString);
        }
        else
        {
            rank = Array.Empty<RankDataType>();
        }

        bool flag = true;
        foreach (var temp in rank)
        {
            if (temp.UserName == userName) flag = false;
            if (temp.UserName != userName || temp.Score >= score) continue;
            temp.Score = score;
            flag = false;
        }

        if (flag)
        {
            var rankList = rank.ToList();
            rankList.Add(new RankDataType() { UserName = userName, Score = score });
            rank = rankList.ToArray();
        }


        rankString = JsonHelper.ToJson(rank, false);
        PlayerPrefs.SetString("leaderRank", rankString);
    }

    public static void DeleteAllRecord()
    {
        if (PlayerPrefs.HasKey("leaderRank"))
        {
            PlayerPrefs.DeleteKey("leaderRank");
        }

        SceneManager.LoadScene(0);
    }

    public static int GetRank(string username)
    {
        string rankString;
        RankDataType[] rank;
        if (PlayerPrefs.HasKey("leaderRank"))
        {
            rankString = PlayerPrefs.GetString("leaderRank");
            rank = JsonHelper.FromJson<RankDataType>(rankString);
            List<RankDataType> rankList = rank.ToList();
            rankList.Sort((x, y) => { return -x.Score.CompareTo(y.Score); });
            return rankList.FindIndex(x => x.UserName == username) + 1;
        }

        return -1;
    }

    public static List<RankDataType> GetRankTable()
    {
        var data = new List<RankDataType>();
        data.Add(new RankDataType(){UserName = "暂无排名", Score = 0});
        string rankString;
        RankDataType[] rank;
        if (PlayerPrefs.HasKey("leaderRank"))
        {
            rankString = PlayerPrefs.GetString("leaderRank");
            rank = JsonHelper.FromJson<RankDataType>(rankString);
            data = rank.ToList();
            data.Sort((x, y) => { return -x.Score.CompareTo(y.Score); });
        }
        return data;
    }

    public static int GetScore(string username)
    {
        string rankString;
        RankDataType[] rank;
        if (PlayerPrefs.HasKey("leaderRank"))
        {
            rankString = PlayerPrefs.GetString("leaderRank");
            rank = JsonHelper.FromJson<RankDataType>(rankString);
            List<RankDataType> rankList = rank.ToList();
            RankDataType ob = rankList.Find(x => x.UserName == username);
            return ob.Score;
        }

        return 0;
    }
}