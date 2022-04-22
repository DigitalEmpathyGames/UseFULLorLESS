using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_animated_control : MonoBehaviour {

	[SerializeField]
	GameObject canvasNivel;

	[SerializeField]
	public int  numeroNivel;

	//FULL,LESS,FULLorLESS
	[SerializeField]
	public string  modoJuego;

	[SerializeField]
	public string  nombreNivel;

	public mapaController controladorMapa;

	Animator animador;
	void Start () {
		animador = GetComponent<Animator>();
		canvasNivel.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			Vector2 touchPosWorld2D = Camera.main.ScreenToWorldPoint (touch.position);
			if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPosWorld2D)) {
				if(controladorMapa.dejartocarnivel){
					canvasNivel.SetActive (false);
					PlayerPrefs.SetInt ("animar", 1);

					PlayerPrefs.SetInt ("numeroNivel", numeroNivel);
					animador.SetBool ("tocandoObjeto",  true);
					string modoDelJuego;
					if (numeroNivel < 4) {
						modoDelJuego = modoJuego;
					} else {
						modoDelJuego = buscarModoJuego ();
					}
					controladorMapa.abrirCanvasNivel(nombreNivel, PlayerPrefs.GetInt ("estrella" + numeroNivel),PlayerPrefs.GetInt ("score" + numeroNivel),modoDelJuego);
					canvasNivel.SetActive (true);
					controladorMapa.dejartocarnivel = false;
				}
			}
		}
			
		if( PlayerPrefs.GetInt ("animar") != 1){
			animador.SetBool ("tocandoObjeto", false);
		}

		if (animador.GetBool ("tocandoObjeto")) {
			PlayerPrefs.SetInt ("animar", 1);

			PlayerPrefs.SetInt ("numeroNivel", numeroNivel);
			animador.SetBool ("tocandoObjeto",  true);
			canvasNivel.SetActive (true);
			string modoDelJuego;
			if (numeroNivel < 4) {
				modoDelJuego = modoJuego;
			} else {
				modoDelJuego = buscarModoJuego ();
			}
			controladorMapa.abrirCanvasNivel(nombreNivel, PlayerPrefs.GetInt ("estrella" + numeroNivel),PlayerPrefs.GetInt ("score" + numeroNivel),modoDelJuego);

		}
	}


	private string buscarModoJuego(){
		string nombreModo;
		if (numeroNivel % 5 == 0) {
			nombreModo = "FULLorLESS";
		} else if (numeroNivel % 2 == 0) {
			nombreModo = "FULL";
		} else {
			nombreModo = "LESS";
		}
		return nombreModo;
	}

}