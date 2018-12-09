using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLvlButton : MonoBehaviour
{
    public Button button;
    public int level;
    void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        StartCoroutine("level_change");
    }
    IEnumerator level_change()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level); //Load Level
        //Wait until fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
