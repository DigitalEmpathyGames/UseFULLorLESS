using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perroControler : MonoBehaviour {
	
	[SerializeField]
	float moveSpeed =  1f;

	[SerializeField]
	private bool letAvance = false;

	[SerializeField]
	private float posAnterior = -5.5f;

	[SerializeField]
	private float cantAvance = 1f;

	private float posicion = 0f;

	public static perroControler instance = null;

	void Start () {
		
		if (instance == null) {
			instance = this;
		}else if(instance != this){
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		posicion = transform.position.x;
		if (TUTORIALController.instance.tutorialActivado) {
			if (PlayerPrefs.GetInt ("numeroNivel") == 1) {

			}
		} else {
			
			if(transform.position.x < -5.5f){
				transform.position = new Vector2 (transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
			}
			if (PlayerPrefs.GetInt ("tipoJuego") == 1 && JuegoOnlineScript.instance.gettipoJugador() == 1) {
				//Online Game Player 1
				if(letAvance){
						JuegoOnlineScript.instance.setFlujoPerro ("Njgo: " + GameController.instance.noJugo + " psAnt: " + posAnterior + "\ncntAvc: " + JuegoOnlineScript.instance.getCantAvance());
					if (JuegoOnlineScript.instance.getCantAvance () > 0f) {
						if (transform.position.x < posAnterior + JuegoOnlineScript.instance.getCantAvance ()) {
							float posicionX = transform.position.x + (moveSpeed * Time.deltaTime);
							transform.position = new Vector2 (posicionX, transform.position.y);
							string mensaje = "posXPerro_" + posicionX;
							JuegoOnlineScript.instance.enviarMensaje (mensaje);
						} else {
							posAnterior = transform.position.x; 
							letAvance = false;
						}
					} else if (JuegoOnlineScript.instance.getCantAvance () < 0f) {
						if (transform.position.x > posAnterior + JuegoOnlineScript.instance.getCantAvance ()) {
							float posicionX = transform.position.x + (moveSpeed * Time.deltaTime * -1);
							transform.position = new Vector2 (posicionX, transform.position.y);
							string mensaje = "posXPerro_" + posicionX;
							JuegoOnlineScript.instance.enviarMensaje (mensaje);
						} else {
							posAnterior = transform.position.x; 
							letAvance = false;
						}
					} else {
						posAnterior = transform.position.x; 
						letAvance = false;
						JuegoOnlineScript.instance.setPermitirJugarOnline (true);
					}
				}

			} else if (PlayerPrefs.GetInt ("tipoJuego") == 0) {
				//Offline Game
				if(letAvance){
					if (transform.position.x < posAnterior + cantAvance) {
						transform.position = new Vector2 (transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
					} else {
						posAnterior = transform.position.x; 
						letAvance = false;
					}
				}
			} else {
				//Online Game Player others
				if(!(transform.position.x < -5.5f)){
					if(JuegoOnlineScript.instance.getPosXPerro() != -999f){
						transform.position = new Vector2 (JuegoOnlineScript.instance.getPosXPerro(), transform.position.y);
					}
				}
			}


		}



	}
		
	public float getPosicion(){
		return posicion;
	}

	public bool getLetAvance(){
		return letAvance;
	}

	public void setAvance(bool nuevoLetAvance){
		letAvance = nuevoLetAvance;
	}

}
