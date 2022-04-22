using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageGameScript : MonoBehaviour {

	public int numeroDeEstaImagen;
	private int puntajeTope;
	private int puntajeActual;

	public Text txtCombo;

	private bool prue = true;

	public Button btnClickimagen;



	void Start () {
		btnClickimagen.gameObject.SetActive (false);
	}


	void Update () {
		/*
		Debug.Log ("Borrar Seccion");
		if(prue){
			prue = false;
			calcularPuntaje(true);
		}
		*/



		if (TUTORIALController.instance.tutorialActivado) {
			if (PlayerPrefs.GetInt ("numeroNivel") == 1) {

			}
		} else {
			if(!GameController.instance.gameOver){
				if (Input.touchCount > 0) {
					Touch touch = Input.GetTouch (0);
					Vector2 touchPosWorld2D = Camera.main.ScreenToWorldPoint (touch.position);
					if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPosWorld2D)) {
						if (GameController.instance.getImagenCorrecta () == numeroDeEstaImagen) {
							if (GameController.instance.permitirJugar) {
								soundControl.playSoundCorrect ();
								GameController.instance.jugadaPerdida = false;
								calcularPuntaje (false);
								//	TimerScript.instance.setTimeLeft (0.1f);
								GameController.instance.permitirJugar = false;
							}
						} else {
							if (GameController.instance.permitirJugar) {
								soundControl.playSoundWrong ();
								GameController.instance.jugadaPerdida = true;
								calcularPuntaje (true);
								//	TimerScript.instance.setTimeLeft (0.1f);
								GameController.instance.permitirJugar = false;
							}
						}

					}

				}
			}
		}
	}


	public void prbClickimgn(){
		calcularPuntaje(true);
	}

	public void calcularPuntaje(bool perdida){
		int puntajeSumar;
		float tiempoReaccion = TimerScript.instance.maxTime - TimerScript.instance.getTimeLeft();
		float tiempoSobra = TimerScript.instance.getTimeLeft();
		float tiempoPERFECT = (TimerScript.instance.maxTime / 3) * 2;
		float tiempoGOOD = TimerScript.instance.maxTime / 3;
		float factorJugada = 0f;
		int factorMultiplicador = 0;
		if (perdida) {
			factorJugada = factorJugada - 100f;
			GameController.instance.setActlTipo ("perdida");
			if (!(tiempoSobra < tiempoPERFECT)) {
				factorMultiplicador = 5;
				puntajeSumar = 50;
				factorJugada = factorJugada + 150f;
				GameController.instance.setActualMultiplicador ("PERFECT");
			} else if (!(tiempoSobra < tiempoGOOD)) {
				GameController.instance.setActualMultiplicador ("GREAT");
				factorJugada = factorJugada + 100f;
				puntajeSumar = 100;
				factorMultiplicador = 10;
			} else {
				GameController.instance.setActualMultiplicador ("GOOD");
				factorJugada = factorJugada + 50f;
				puntajeSumar = 150;
				factorMultiplicador = 15;
			}
			if (GameController.instance.getActualMultiplicadorr () == GameController.instance.getAntMltplcdr () && GameController.instance.getActlTipo () == GameController.instance.getAntrTipo ()){
				int multiplicador = GameController.instance.getMultiplicador ();
				multiplicador = multiplicador + 1;
				GameController.instance.setMultiplicador (multiplicador);
			} else {
				GameController.instance.setMultiplicador (1);
			}
			puntajeSumar = puntajeSumar  + (factorMultiplicador * GameController.instance.getMultiplicador ());
			puntajeSumar = GameController.instance.getPuntajeASumar () - puntajeSumar;
			if(puntajeSumar < 0){
				puntajeSumar = 0;
			}
		} else {
			GameController.instance.setActlTipo ("ganada");
			factorJugada = factorJugada + 100f;
			if (!(tiempoSobra < tiempoPERFECT)) {
				GameController.instance.setActualMultiplicador ("PERFECT");
				puntajeSumar = 300;
				factorJugada = factorJugada + 300f;
				factorMultiplicador = 30;
			} else if (!(tiempoSobra < tiempoGOOD)) {
				GameController.instance.setActualMultiplicador ("GREAT");
				puntajeSumar = 200;
				factorJugada = factorJugada + 200f;
				factorMultiplicador = 20;
			} else {
				GameController.instance.setActualMultiplicador ("GOOD");
				puntajeSumar = 100;
				factorJugada = factorJugada + 100f;
				factorMultiplicador = 10;
			}


			if (GameController.instance.getActualMultiplicadorr () == GameController.instance.getAntMltplcdr () && GameController.instance.getActlTipo () == GameController.instance.getAntrTipo ()) {
				int multiplicador = GameController.instance.getMultiplicador ();
				multiplicador = multiplicador + 1;
				GameController.instance.setMultiplicador (multiplicador);
			} else {
				GameController.instance.setMultiplicador (1);
			}
			puntajeSumar = puntajeSumar + (factorMultiplicador * GameController.instance.getMultiplicador ());
			puntajeSumar = GameController.instance.getPuntajeASumar () + puntajeSumar;

		}


		factorJugada = factorJugada + (float)GameController.instance.getMultiplicador ();

		if (GameController.instance.getMultiplicador () > 2) {
			if (GameController.instance.getActlTipo () == "perdida") {
				txtCombo.color = Color.red;
			} else {
				switch(GameController.instance.getActualMultiplicadorr ()){
				case "PERFECT":
					txtCombo.color = Color.green;
					break;
				case "GREAT":
					txtCombo.color = Color.magenta;
					break;
				default:
					txtCombo.color = Color.grey;
					break;
				}
			}
			txtCombo.text = GameController.instance.getActualMultiplicadorr () + " X " + GameController.instance.getMultiplicador ();
		} else {
			txtCombo.text = "";
		}
		string actlTipo = GameController.instance.getActlTipo();
		GameController.instance.setAntrTipo (actlTipo);
		actlTipo = GameController.instance.getActualMultiplicadorr();
		GameController.instance.setAntMltplcdr (actlTipo);

		if(PlayerPrefs.GetInt ("tipoJuego") == 1){
			string tipo = "";
			int puntajeJugador;
			if (perdida) {
				tipo = "perdida";
				puntajeJugador = GameController.instance.puntajeNivel - puntajeSumar;
			} else {
				tipo = "ganada";
				puntajeJugador = GameController.instance.puntajeNivel + puntajeSumar;
			}
			string mensaje = "";
			//0
			mensaje = mensaje + "jugada";
			//1
			mensaje = mensaje + "_numero:" + JuegoOnlineScript.instance.getNumeroJugada ();
			//2
			mensaje = mensaje + "_jugador:" + JuegoOnlineScript.instance.gettipoJugador ();
			//3
			mensaje = mensaje + "_tipo:" + tipo;
			//4
			mensaje = mensaje + "_reaccion:" + tiempoReaccion;
			//5
			mensaje = mensaje + "_puntaje:" + puntajeJugador;
			//6
			mensaje = mensaje + "_tipoMuliplicador:" + GameController.instance.getActualMultiplicadorr();
			//7
			mensaje = mensaje + "_cantMuliplicador:" +GameController.instance.getMultiplicador();
			//8
			mensaje = mensaje + "_factorJugada:" + factorJugada;
			if(JuegoOnlineScript.instance.gettipoJugador() == 1){
				JuegoOnlineScript.instance.agregarJugadasJugador (mensaje);
			}else{
				JuegoOnlineScript.instance.enviarMensaje (mensaje);
			}
		}

		GameController.instance.setPuntajeASumar (puntajeSumar);
	}





}
