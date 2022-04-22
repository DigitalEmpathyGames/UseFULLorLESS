using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreBarr : MonoBehaviour {

	[SerializeField]
	Text scoreNivelGanado;

	[SerializeField]
	public GameObject estrelaSuces1;
	[SerializeField]
	public GameObject estrelaSuces2;
	[SerializeField]
	public GameObject estrelaSuces3;

	[SerializeField]
	public GameObject btnVolverAJugar;

	[SerializeField]
	public GameObject btnIrAlMapa;

	private bool star1Got = false;
	private bool star2Got = false;
	private bool star3Got = false;

	public Image pointBar;
	float	pointLeft = 100f;
	int puntajeGanado ;
	int puntajeMaximo ;
	int sumaEstrella;

	// Use this for initialization
	void Start () {
		btnVolverAJugar.SetActive (false);
		btnIrAlMapa.SetActive (false);
		estrelaSuces1.SetActive (false);
		estrelaSuces2.SetActive (false);
		estrelaSuces3.SetActive (false);
		sumaEstrella = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.instance.mpstrarPuntajeFinal) {
			soundControl.playSoudPoint ();
			puntajeGanado = GameController.instance.puntajeNivel;
			puntajeMaximo = GameController.instance.puntajeMaximo;
			pointLeft = pointLeft - 0.5f;
			pointBar.fillAmount = pointLeft / 100f;


			float porcAMostrarDeTotal = (100f - pointLeft)/100f;
			int porcAMostrar = Mathf.RoundToInt((float)puntajeMaximo  * porcAMostrarDeTotal);  

			if(porcAMostrar > puntajeGanado){
				btnVolverAJugar.SetActive (true);
				btnIrAlMapa.SetActive (true);
				porcAMostrar = puntajeGanado;
				GameController.instance.mpstrarPuntajeFinal = false;
			}else if(puntajeGanado < 1){
				btnVolverAJugar.SetActive (true);
				btnIrAlMapa.SetActive (true);
				GameController.instance.mpstrarPuntajeFinal = false;
			}
			scoreNivelGanado.text = "PUNTAJE : " + porcAMostrar;

			if(porcAMostrarDeTotal > 0.5f && !star1Got){
				sumaEstrella = sumaEstrella + 1;
				soundControl.playSoundStar ();
				estrelaSuces1.SetActive (true);
				GameController.instance.estrellaGanada1 = 1;
				star1Got = true;
			}
			if(porcAMostrarDeTotal > 0.75f && !star2Got){
				sumaEstrella = sumaEstrella + 1;
				soundControl.playSoundStar ();
				estrelaSuces2.SetActive (true);
				GameController.instance.estrellaGanada2 = 1;
				star2Got = true;
			}

			if(!(porcAMostrar < puntajeMaximo) && !star3Got){
				sumaEstrella = sumaEstrella + 1;
				soundControl.playSoundStar ();
				estrelaSuces3.SetActive (true);
				GameController.instance.estrellaGanada3 = 1;
				star3Got = true;
			}

			if(sumaEstrella >  PlayerPrefs.GetInt (GameController.instance.nombreEstrellaNivel)){
				PlayerPrefs.SetInt (GameController.instance.nombreEstrellaNivel, sumaEstrella);
			}
		}
	}
		
}
