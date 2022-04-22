using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;



public class JuegoOnlineScript : MonoBehaviour, RealTimeMultiplayerListener {

	public static JuegoOnlineScript instance = null;

	public GameObject signin;
	public GameObject randomPlayers;
	public GameObject friends;
	public GameObject maincanvas;
	public GameObject jugador;
	public Canvas canvasLoading;
	public Canvas mainMenu;
	public Canvas batallaOnline;
	public Text textoLoad;
	public Text txtNombreJugador2;
	public Text txtNombreJugador;
	public Text txtLog;
	public Text txtLogOnline;
	public Text txtScoreRival;
	private string nombreJugador;
	private string nombreJugadorRival;
	private string idRival;
	private int tipoJugador = -999;
	private int jugadorSugerido;
	private byte[] messageInicial;
	private bool iniciarJuegoOnline = false;
	private List<string> jugadasJugador = new List<string>();
	private int numeroJugada = 0;
	private int numeroJugadaOnline = 0;
	private float cantAvance = 0.3f;
	private bool permitirJugadaOnline = true;
	private int mnjResi = 0;
	private int msjTmLft = 0;
	private int msjPrro = 0;
	private int msjJgda = 0;
	private string flujoPerro ="";
	private string jgdaRvalLog = "";
	public Button salirEsperar;



	//datos enviables
	private float tiempoRestante = -999f;
	private float tiempoAgotado = -999f;
	private float posXPerro = -999f;
	private List<string> jugadasOponente = new List<string>();

	void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.InitializeInstance (config);
		PlayGamesPlatform.Activate ();
		if (instance == null) {
			instance = this;
		}else if(instance != this){
			Destroy (gameObject);
		}

		if (PlayerPrefs.GetInt ("tipoJuego") == 1) {
			//Online game
			canvasLoading.gameObject.SetActive (true);
			salirEsperar.gameObject.SetActive (true);
			if(PlayerPrefs.GetInt ("invitarAmigo") == 2){
				PlayGamesPlatform.Instance.RealTime.AcceptFromInbox (this);
				textoLoad.text = "Esperando rival";
				PlayerPrefs.SetInt ("invitarAmigo", 0);
			}else if (PlayerPrefs.GetInt ("invitarAmigo") == 1) {
				PlayerPrefs.SetInt ("invitarAmigo", 0);
				textoLoad.text = "Esperando rival";
				crearJuegoconinvitacion ();
			} else {
				textoLoad.text = "Buscando rival";
				crearJuegoRapido ();
			}

		} else {
			//Offline game
			canvasLoading.gameObject.SetActive (false);
		}

	}

	// Update is called once per frame
	void Update () {
		string log = "tipJueg : " + PlayerPrefs.GetInt ("tipoJuego");
		log = log +  "\n" + "tipJuga : " + tipoJugador;
		log = log +  "\n" + "jugMia : " + jugadasJugador.Count;
		log = log +  "\n" + "jugRiv : " + jugadasOponente.Count;
		log = log +  "\n" + "mnjResi : " + mnjResi;
		log = log +  "\n" + "msjTmLft : " + msjTmLft;
		log = log +  "\n" + "msjPrro : " + msjPrro;
		log = log +  "\n" + "msjJgda : " + msjJgda;
		log = log +  "\n" + "posPerro : " + perroControler.instance.getPosicion();
		log = log +  "\n" + "OnlineInic : " + iniciarJuegoOnline;
		log = log +  "\n" + "flujoPerro : " + flujoPerro;
	//	txtLog.text = log;
		if (PlayerPrefs.GetInt ("tipoJuego") == 1) {

			if (jugadasJugador.Count > numeroJugadaOnline && jugadasOponente.Count > numeroJugadaOnline) {
				if(permitirJugadaOnline){
					if(tipoJugador == 1){
						jgdaRvalLog = "";
						string jugada = jugadasJugador [numeroJugadaOnline];
						string jugadaRival = jugadasOponente [numeroJugadaOnline];

						string[] informacion = jugada.Split ('_');
						string[] informacionRival = jugadaRival.Split ('_');
						string[] datos = informacion [3].Trim ().Split (':');
						string[] datosRival = informacionRival [3].Trim ().Split (':');
						string tipoJugada = datos [1].Trim ();
						string tipoJugadaRival = datosRival [1].Trim ();
						jgdaRvalLog = jgdaRvalLog +  "\n" + "nJgdaR: " + numeroJugadaOnline;
						datos = informacion [4].Trim ().Split (':');
						datosRival = informacionRival [4].Trim ().Split (':');
						string tiempoReaccion = datos [1].Trim ();
						string tiempoReaccionRival = datosRival [1].Trim ();
						jgdaRvalLog = jgdaRvalLog +  "\n" + "tmpRccnR: " + tiempoReaccion;
						datos = informacion [5].Trim ().Split (':');
						datosRival = informacionRival [5].Trim ().Split (':');
						string puntaje = datos [1].Trim ();
						string puntajeRival = datosRival [1].Trim ();
						jgdaRvalLog = jgdaRvalLog +  "\n" + "pntjR: " + puntaje;
						datos = informacion [6].Trim ().Split (':');
						datosRival = informacionRival [6].Trim ().Split (':');
						string multiplicador = datos [1].Trim ();
						string multiplicadorRival = datosRival [1].Trim ();
						jgdaRvalLog = jgdaRvalLog +  "\n" + "mltplR: " + multiplicador;
						datos = informacion [7].Trim ().Split (':');
						datosRival = informacionRival [7].Trim ().Split (':');
						string cantMultiplicador = datos [1].Trim ();
						string cantMultiplicadorRival = datosRival [1].Trim ();
						jgdaRvalLog = jgdaRvalLog +  "\n" + "CnMltplR: " + cantMultiplicador;
						datos = informacion [8].Trim ().Split (':');
						datosRival = informacionRival [8].Trim ().Split (':');
						string factorJugadaStr = datos [1].Trim ();
						string factorJugadaRivalStr = datosRival [1].Trim ();
						float factorJugada = float.Parse (factorJugadaStr);
						float factorJugadaRival = float.Parse (factorJugadaRivalStr);
						jgdaRvalLog = jgdaRvalLog +  "\n" + "FctrR: " + factorJugadaRival;
						jgdaRvalLog = jgdaRvalLog +  "\n" + "Fctr: " + factorJugada;

						if(factorJugada > factorJugadaRival){
							permitirJugadaOnline = false;
							cantAvance = 0.3f;
							perroControler.instance.setAvance (true);
						}else if(factorJugada < factorJugadaRival){
							permitirJugadaOnline = false;
							cantAvance = -0.3f;
							perroControler.instance.setAvance (true);
						}else{
							permitirJugadaOnline = true;
							perroControler.instance.setAvance (false);
						}
						numeroJugadaOnline = numeroJugadaOnline + 1;
					}
				}
			}
			txtLog.text = log + jgdaRvalLog;
		} else {
			//Offline game
		}
	}

	public void setPermitirJugarOnline(bool permiso){
		permitirJugadaOnline = permiso;
	}

	public float getCantAvance(){
		return cantAvance;
	}
		
	public void setCantAvance(float avance){
		cantAvance = avance;
	}

	public void setFlujoPerro (string datos){
		flujoPerro = datos;
	}

	public void agregarJugadasJugador(string datos){
		jugadasJugador.Add (datos);
	}

	public int getNumeroJugada(){
		return numeroJugada;
	}

	public void sumarJugadaOnline (){
		numeroJugada = numeroJugada + 1;
	}

	public int gettipoJugador (){
		return tipoJugador;
	}

	public bool getIniciarJuegoOnline(){
		return iniciarJuegoOnline;
	}
		
	public float getTiempoRestante(){
		return tiempoRestante;
	}

	public float getTiempoAgotado(){
		return tiempoAgotado;
	}

	public float getPosXPerro(){
		return posXPerro;
	}

	public void abrirBatallaOnLine(){
		//
		if (!Social.localUser.authenticated) {
			Social.localUser.Authenticate ((bool success) => {
				if (success) {
					mainMenu.gameObject.SetActive (false);
					batallaOnline.gameObject.SetActive (true);
				} else {
					//mostrar mensaje si no
				}
			});
		} else {
			mainMenu.gameObject.SetActive (false);
			batallaOnline.gameObject.SetActive (true);
		}
	}

	public void progresoLogIn(){
		txtLogOnline.text =  "\n"+ txtLogOnline.text + "PrgrsLogin";
		Social.localUser.Authenticate ((bool success) => {
			if(success){
				
			}else{
			
			}
		});
	}

	public void crearJuegoRapido(){
		const int minOponentes = 1;
		const int maxOponentes = 1;
		const int tipoJuego = 0;
		PlayGamesPlatform.Instance.RealTime.CreateQuickGame (minOponentes,maxOponentes,tipoJuego,this);
	}

	public void crearJuegoconinvitacion(){
		const int minOponentes = 1;
		const int maxOponentes = 1;
		const int tipoJuego = 0;
		PlayGamesPlatform.Instance.RealTime.CreateWithInvitationScreen(minOponentes,maxOponentes,tipoJuego,this);
	}
		


	public void OnRoomSetupProgress(float percent){
		List<Participant> participantes = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants ();
		txtLogOnline.text =  txtLogOnline.text +"\n"  + "RoomPrgrs " + "P:" + participantes.Count;


	}

	public void OnRoomConnected(bool success){
		List<Participant> participantes = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants ();
		txtLogOnline.text =  txtLogOnline.text +"\n"  + "RoomCnctd " + "P:" + participantes.Count;
		Participant myself = PlayGamesPlatform.Instance.RealTime.GetSelf ();
		if(myself.ParticipantId == participantes[0].ParticipantId){
			idRival = participantes [1].ParticipantId;
			jugadorSugerido = 1;
			txtNombreJugador.text = myself.DisplayName;
			txtNombreJugador2.text = participantes [1].DisplayName;

		}else{
			jugadorSugerido = 2;
			txtNombreJugador.text = participantes [0].DisplayName;
			txtNombreJugador2.text = myself.DisplayName;
		}
		tipoJugador = jugadorSugerido;
		iniciarJuegoOnline = true;
		canvasLoading.gameObject.SetActive (false);

	}

	public void enviarMensaje (string mensaje){
		messageInicial = System.Text.ASCIIEncoding.Default.GetBytes (mensaje);
		PlayGamesPlatform.Instance.RealTime.SendMessageToAll (true,messageInicial);
		//PlayGamesPlatform.Instance.RealTime.SendMessage (true,idRival,messageInicial);
	}

	public int elegirJugador (){
		return Random.Range (1, 3);
	}

	public void OnLeftRoom(){
		List<Participant> participantes = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants ();
		txtLogOnline.text =  txtLogOnline.text +"\n"  + "LftRoom " + "P:" + participantes.Count;
		volveralMenuDesdeOnline ();
	}
		
	public void OnParticipantLeft(Participant participant){
		List<Participant> participantes = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants ();
		txtLogOnline.text =  txtLogOnline.text +"\n"  + "PrtcpLft " + "P:" + participantes.Count;
	}

	public void OnPeersConnected(string[] participantIds){
		List<Participant> participantes = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants ();
		txtLogOnline.text =  txtLogOnline.text +"\n"  + "PeerCnctd " + "P:" + participantes.Count;
		
	}

	public void OnPeersDisconnected(string[] participantIds){
		List<Participant> participantes = PlayGamesPlatform.Instance.RealTime.GetConnectedParticipants ();
		txtLogOnline.text =  txtLogOnline.text +"\n"  + "PeerDscnctd " + "P:" + participantes.Count;
		volveralMenuDesdeOnline();
	}

	public void volveralMenuDesdeOnline(){
		PlayGamesPlatform.Instance.RealTime.LeaveRoom ();
		GameController.instance.volverAlMenu ();
	}

	public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data){
		string mensaje = System.Text.Encoding.Default.GetString (data);
		string[] informacion = mensaje.Split ('_');
		mnjResi = mnjResi + 1;
		switch(informacion[0].Trim()){
		case "jugadorRival":
			break;
		case "timeLeft":
			msjTmLft = msjTmLft + 1;
				tiempoRestante = float.Parse (informacion[1].Trim());
			break;
		case "posXPerro":
			msjPrro = msjPrro + 1;
			posXPerro = float.Parse (informacion[1].Trim());
			break;
		case "jugada":
			msjJgda = msjJgda + 1;
			string[] datos = informacion[2].Trim().Split (':');
			if(datos[1].Trim() != tipoJugador.ToString()){
				jugadasOponente.Add (mensaje);

			}
			break;
		default:
			break;
		}
	}
}
