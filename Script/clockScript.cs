using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockScript : MonoBehaviour {

	public static clockScript instance = null;
	Animator animador;

	private bool animar = false;


	// Use this for initialization
	void Start () {

			animador = GetComponent<Animator>();
			if (instance == null) {
				instance = this;
			}else if(instance != this){
				Destroy (gameObject);
			}


	
	}
	
	// Update is called once per frame
	void Update () {
		if (TUTORIALController.instance.tutorialActivado) {
			if (PlayerPrefs.GetInt ("numeroNivel") == 1) {
			}
		} else {
			animador.SetBool ("tocandoObjeto", animar);
		}
	}

	public void setAnimar (bool permitirAnimar){
		animar = permitirAnimar;
	}

}
