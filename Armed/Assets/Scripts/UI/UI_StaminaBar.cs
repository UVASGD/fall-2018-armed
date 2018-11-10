using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StaminaBar : MonoBehaviour {

	public Image content;
	public PlayerMovement move;

	// Use this for initialization
	void Start () {
		move = GameObject.FindObjectOfType<PlayerMovement> ();
		content.fillAmount = 0;
	}

	// Update is called once per frame
	void Update () {
		content.fillAmount = move.getCurStamina() / move.getMaxStamina();
	}
}