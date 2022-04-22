using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	public Image timeBar;
	public float maxTime = 5f;
	private float timeLeft;
	private bool tiempoRelojAgotado = false;
	private bool animar = false;



	public static TimerScript instance = null;

	// Use this for initialization
	void Start () {
		
		timeLeft = maxTime;
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
				switch(TUTORIALController.instance.textoAMostrar){
				case 2:
					timeBar.fillAmount = ((maxTime/3) * 2) / maxTime;
					break;
				case 3:
					timeBar.fillAmount = (maxTime/3) / maxTime;
					break;
				case 4:
					timeBar.fillAmount = ((maxTime/3) * 2) / maxTime;
					break;
				case 5:
					timeBar.fillAmount = (maxTime/3) / maxTime;
					break;
				default:
					timeBar.fillAmount = 1;
					break;
				}
			}
		} else {
			if(!GameController.instance.gameOver){
				if (timeLeft > 0) {
					if(animar){
						tiempoRelojAgotado = false;
						timeLeft = timeLeft - Time.deltaTime;
						timeBar.fillAmount = timeLeft / maxTime;
						/*
						if (PlayerPrefs.GetInt ("tipoJuego") == 1 && JuegoOnlineScript.instance.gettipoJugador() == 2) {
							// Online Game
							timeBar.fillAmount = timeLeft / maxTime;
							string mensaje = "timeLeft_" + timeLeft;
							JuegoOnlineScript.instance.enviarMensaje (mensaje);
							//enviar timeLeft / maxTime;
						} else if (PlayerPrefs.GetInt ("tipoJuego") == 0) {
							timeBar.fillAmount = timeLeft / maxTime;
						} else {
							if (!(JuegoOnlineScript.instance.getTiempoRestante() > 0) || JuegoOnlineScript.instance.getTiempoRestante() == -999f) {
								timeBar.fillAmount = 1f;
							} else {
								timeBar.fillAmount = JuegoOnlineScript.instance.getTiempoRestante();
							}
						}
						*/

					}
				} else {
					timeLeft = maxTime; 
					tiempoRelojAgotado = true;
				}
			}
		}



	}

	public bool getTiempoAgotadoReloj(){
		return tiempoRelojAgotado;
	}

	public void setAnimar (bool permitirAnimar){
		animar = permitirAnimar;
	}

	public bool getAnimar(){
		return animar;
	}

	public float getTimeLeft(){
		return timeLeft;
	}
		
	public void setTimeLeft(float nuevoTimeLeft){
		timeLeft = nuevoTimeLeft;
	}

}
