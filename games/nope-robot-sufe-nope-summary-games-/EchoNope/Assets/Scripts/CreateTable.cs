using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTable : MonoBehaviour
{
    public GameObject RowPrefab;

    public GameObject table;
    // Start is called before the first frame update
    void Start()
    {
        var data = RankKeep.GetRankTable();
        for (int i = 0; i < data.Count; i++)
        {
            GameObject row = Instantiate(RowPrefab,table.transform.position,table.transform.rotation) as GameObject;
            row.name = "row" + i + 1;
            row.transform.SetParent(table.transform);

            row.transform.localScale = Vector3.one;
            row.transform.Find("Cell2").GetComponent<Text>().text = (i+1).ToString();
            row.transform.Find("Cell1").GetComponent<Text>().text = data[i].Score.ToString();
            row.transform.Find("Cell0").GetComponent<Text>().text = data[i].UserName;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
