using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour {
    void ExitGame()
    {
        Application.Quit();
        Debug.Log("it do");
    }
}
