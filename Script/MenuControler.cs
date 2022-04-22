using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class MenuControler : MonoBehaviour {

	public static MenuControler instance = null;

	public Canvas canvasLoading;
	public Canvas canvasProximamente;
	public Canvas canvasRanking;
	public Canvas canvasMenu;
	public Button btnVerRanking;
	private AsyncOperation operacion;
	public bool ocultarCanvas;
	[SerializeField]
	Text textoLoad;

	[SerializeField]
	Text txtPuntajeTotal;


	private int puntajeTotal;
	private string idTablaPuntaje = "CgkIz8O17cAIEAIQAA";

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}else if(instance != this){
			Destroy (gameObject);
		}

		puntajeTotal = PlayerPrefs.GetInt ("puntajeTotal");
		txtPuntajeTotal.text = "PUNTAJE TOTAL : " + puntajeTotal;
		btnVerRanking.gameObject.SetActive (false);
		canvasRanking.gameObject.SetActive (false);
		ocultarCanvas = false;
		canvasLoading.gameObject.SetActive (false);
		canvasProximamente.gameObject.SetActive (false);
		canvasMenu.gameObject.SetActive (true);
		PlayerPrefs.SetInt ("animar", 0);
		PlayerPrefs.SetInt ("tipoJuego", 0);
	}
		

	// Update is called once per frame
	void Update () {
		if(ocultarCanvas){
			canvasProximamente.gameObject.SetActive (false);
			canvasRanking.gameObject.SetActive (false);
			btnVerRanking.gameObject.SetActive (false);
			canvasMenu.gameObject.SetActive (true);
			ocultarCanvas = false;
		}
	}

	public void loginRanking(){
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();
		if (!Social.localUser.authenticated) {
			Social.localUser.Authenticate ((bool success) => {
				btnVerRanking.gameObject.SetActive (true);
			});
		} else {
			btnVerRanking.gameObject.SetActive (true);
		}

	}

	public void verRanking(){
		if (Social.localUser.authenticated) {
			Social.ReportScore (puntajeTotal, idTablaPuntaje, (bool success) => {
				((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (idTablaPuntaje);
			});
		}
	}

	public void cerrarCanvasProxomamente(){
		ocultarCanvas = true;
	}

	public void abrirRanking (){
		canvasMenu.gameObject.SetActive (false);
		canvasRanking.gameObject.SetActive (true);
	}



	public void empezarJuegoNormal(){
		PlayerPrefs.SetInt ("tipoJuego", 0);
		PlayerPrefs.SetInt ("invitarAmigo", 0);
		canvasLoading.gameObject.SetActive (true);
		actualizarProgreso (0f);
		StartCoroutine (empezarLoad("EscenaMundo"));
	}

	public void empezarJuegoOnline(){
		PlayerPrefs.SetInt ("tipoJuego", 1);
		PlayerPrefs.SetInt ("invitarAmigo", 0);
		canvasLoading.gameObject.SetActive (true);
		actualizarProgreso (0f);
		StartCoroutine (empezarLoad("escenaJuego"));
	}

	public void empezarRetarJugador(){
		PlayerPrefs.SetInt ("tipoJuego", 1);
		PlayerPrefs.SetInt ("invitarAmigo", 1);
		canvasLoading.gameObject.SetActive (true);
		actualizarProgreso (0f);
		StartCoroutine (empezarLoad("escenaJuego"));
	}

	public void empezarFuiInvitado(){
		PlayerPrefs.SetInt ("tipoJuego", 1);
		PlayerPrefs.SetInt ("invitarAmigo", 2);
		canvasLoading.gameObject.SetActive (true);
		actualizarProgreso (0f);
		StartCoroutine (empezarLoad("escenaJuego"));
	}

	public void mostrarRanking(){
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
