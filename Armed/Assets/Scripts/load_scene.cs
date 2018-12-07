using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_scene : MonoBehaviour {
    public string load_level;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine("level_change");
        Debug.Log("I'm colliding");
    }

    IEnumerator level_change()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(load_level); //Load Level
        //Wait until fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
