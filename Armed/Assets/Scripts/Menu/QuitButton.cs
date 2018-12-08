using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour {
    public Button quitbutton;
void Start()
    {
        //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        quitbutton.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Application.Quit();
        Debug.Log("it do");
    }
}
