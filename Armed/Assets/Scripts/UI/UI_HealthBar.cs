using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour {

	public Image content;
	public PlayerHealth health;

	// Use this for initialization
	void Start () {
		health = GameObject.FindObjectOfType<PlayerHealth> ();
		content.fillAmount = 0;
	}

	// Update is called once per frame
	void Update () {
		content.fillAmount = health.curr_health / health.maxhealth;
	}
}