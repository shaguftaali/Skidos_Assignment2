using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reciver : MonoBehaviour
{

    //bool hasExtra;

    public Text hasExtraText;
    public Text text;
    public string saveData;
    // Start is called before the first frame update
    void Start()
    {
        saveData = PlayerPrefs.GetString("Value", "");
        text.text = "[" + saveData + "]"; 
        ReceiveData();
    }

   public void ReceiveData()
    {
        string data = "";
        AndroidJavaClass unityPlayer=new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity=unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
        bool hasExtra = intent.Call<bool>("hasExtra", "arg1");

        if (hasExtra)
        {
            AndroidJavaObject extras=intent.Call<AndroidJavaObject>("getExtras");
            data = extras.Call<string>("getString", "arg1");
            string newData;
            if (saveData == "")
            {
                newData = data;
            }
            else
            {
                newData= saveData + "," + data;

            }
            text.text = "[" + newData + "]";
            PlayerPrefs.SetString("Value", newData);
            PlayerPrefs.Save();
        }
        hasExtraText.text = "has : " + hasExtra;
        //text.text = "Text : "+data+" XX";
    }

   
}
