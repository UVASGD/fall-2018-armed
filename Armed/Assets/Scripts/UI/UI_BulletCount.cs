using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BulletCount : MonoBehaviour {

	public Text counter;
	public GunManager gun;

	// Use this for initialization
	void Start () {
		gun = GameObject.FindObjectOfType<GunManager> ();
		counter.text = "TEST";
	}
	
	// Update is called once per frame
	void Update () {
		int curCounter = gun.getCurBullets ();
		int maxCounter = gun.getMaxBullets ();

		counter.text = "" + curCounter + "/" + maxCounter;
	}
}
