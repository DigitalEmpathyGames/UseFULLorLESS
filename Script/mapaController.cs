using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mapaController : MonoBehaviour {

	public Canvas canvasLoading;
	private AsyncOperation operacion;
	[SerializeField]
	Text textoLoad;

	public Text nombreNivel;
	public Text mensaje;
	public Text puntajeNivel;

	[SerializeField]
	GameObject canvasNivel;
	[SerializeField]
	GameObject canvasSalir;

	[SerializeField]
	GameObject estrellaSucces1;

	[SerializeField]
	GameObject estrellaSucces2;

	[SerializeField]
	GameObject estrellaSucces3;

	[SerializeField]
	GameObject[] niveles;

	[SerializeField]
	GameObject imageUseFULL;
	[SerializeField]
	GameObject imageUseLESS;
	[SerializeField]
	GameObject imageUseFULLorLESS;

	public bool ocultarCanvas = false;

	public bool dejartocarnivel;

	public static mapaController instance = null;

	// Use this for initialization
	void Start () {
		operacion = null;
		canvasLoading.gameObject.SetActive (false);
		PlayerPrefs.SetInt ("nivelesCreados", niveles.Length);
		if (instance == null) {
			instance = this;
		}else if(instance != this){
			Destroy (gameObject);
		}
		dejartocarnivel = true;
		estrellaSucces1.SetActive (false);
		estrellaSucces2.SetActive (false);
		estrellaSucces3.SetActive (false);

		imageUseFULL.SetActive (false);
		imageUseLESS.SetActive (false);
		imageUseFULLorLESS.SetActive (false);
		activarNiveles ();
	}

	public void activarNiveles (){
		bool buscar = true;
		int numeroNivel = 0;
		int sumaPuntaje = 0;
		string nombrePuntaje = "score";
		while(buscar){
			int nivelActual = numeroNivel + 1;
			string nombrePuntajeNivel = nombrePuntaje + nivelActual ;
			sumaPuntaje = sumaPuntaje + PlayerPrefs.GetInt (nombrePuntajeNivel);
			if (numeroNivel == 0) {
				niveles [numeroNivel].SetActive (true);
			} else {
				int novelAnterior = numeroNivel;
				string estrellaAnterior = "estrella" + novelAnterior;
				if (PlayerPrefs.GetInt (estrellaAnterior) > 0) {
					niveles [numeroNivel].SetActive (true);
				} else {
					buscar = false;
				}
			}
			numeroNivel = numeroNivel + 1;
			if(numeroNivel > niveles.Length){
				buscar = false;
			}
		}

		PlayerPrefs.SetInt ("puntajeTotal",sumaPuntaje);
	}

	// Update is called once per frame
	void Update () {
		if(ocultarCanvas){
			canvasSalir.SetActive (true);
			canvasNivel.SetActive (false);
			ocultarCanvas = false;
		}
	}

	public void salirAlMenu(){
		canvasLoading.gameObject.SetActive (true);
		actualizarProgreso (0f);
		StartCoroutine (empezarLoad("MenuScene"));
	}

	public void jugarNivel(){
		canvasLoading.gameObject.SetActive (true);
		actualizarProgreso (0f);
		StartCoroutine (empezarLoad("escenaJuego"));
	}

	public void cerrarCanvas(){
		dejartocarnivel = true;
		PlayerPrefs.SetInt ("animar", 0);
		ocultarCanvas = true;

	}

	public void abrirCanvasNivel(string nomNivel, int estrellas,int scoreNivel, string modoJuego){
		canvasSalir.SetActive (false);
		//pintando estrellas
		switch (estrellas) {
			case 1:
				estrellaSucces1.SetActive (true);
				estrellaSucces2.SetActive (false);
				estrellaSucces3.SetActive (false);
				break;
			case 2:
				estrellaSucces1.SetActive (true);
				estrellaSucces2.SetActive (true);
				estrellaSucces3.SetActive (false);
				break;
			case 3:
				estrellaSucces1.SetActive (true);
				estrellaSucces2.SetActive (true);
				estrellaSucces3.SetActive (true);
				break;
			default:
				estrellaSucces1.SetActive (false);
				estrellaSucces2.SetActive (false);
				estrellaSucces3.SetActive (false);
				break;
		}

		nombreNivel.text = "";
		puntajeNivel.text = "";
		puntajeNivel.text = "PUNTAJE : " + scoreNivel + " / 6300";
		nombreNivel.text = nomNivel;
		int modoJuegonumber;
		switch (modoJuego) {
		case "FULL":
				modoJuegonumber = 1;
				imageUseFULL.SetActive (true);
				imageUseLESS.SetActive (false);
				imageUseFULLorLESS.SetActive (false);
				mensaje.text = "Marca lo UTIL";
				break;
			case "LESS":
				modoJuegonumber = 2;
				imageUseFULL.SetActive (false);
				imageUseLESS.SetActive (true);
				imageUseFULLorLESS.SetActive (false);
				mensaje.text = "Marca lo INUTIL";
				break;
			case "FULLorLESS":
				modoJuegonumber = 3;
				imageUseFULL.SetActive (false);
				imageUseLESS.SetActive (false);
				imageUseFULLorLESS.SetActive (true);
				mensaje.text = "Marca lo UTIL o INUTIL";
				break;
			default:
				modoJuegonumber = 1;
				break;
		}

		PlayerPrefs.SetInt ("modoJuego", modoJuegonumber);
	}

	private void actualizarProgreso(float progreso){
		textoLoad.text = "Cargando " + ((int)(progreso * 100f)) + " %";
	}

	private IEnumerator empezarLoad(string nombreEscena){
		operacion = SceneManager.LoadSceneAsync (nombreEscena);
		while(!operacion.isDone){
			actualizarProgreso (operacion.progress);
			yield return null;
		}
		actualizarProgreso (operacion.progress);
		operacion = null;
		canvasLoading.gameObject.SetActive (false);

	}

}
