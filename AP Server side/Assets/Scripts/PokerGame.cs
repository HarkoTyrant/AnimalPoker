using UnityEngine;
using System.Collections;

public class PokerGame : MonoBehaviour {

	Baraja b;			// crear la baraja

	// Use this for initialization
	void Start () {
		Debug.Log("pokergame");

		Evaluador.initTablas();				// inicializar tablas de valores
		b = new Baraja();
		b.barajar();						// barajar... lol
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
