using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        //Calculate offset value between camera and player
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
	}
	
	// Update is called after Update (Better for cameras, etc.)
	void LateUpdate () {
        //Set offset position of camera
        transform.position = player.transform.position + offset;
	}
}
