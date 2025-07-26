using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUserName : MonoBehaviour
{
    public static string UserName = "Default";

    public InputField nameinput;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UserName = PlayerPrefs.GetString("username");
    }

    public void SetUserNameText()
    {
        UserName = nameinput.text;
        PlayerPrefs.SetString("username", UserName);
    }
}