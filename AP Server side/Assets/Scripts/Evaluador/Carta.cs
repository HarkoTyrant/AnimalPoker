using UnityEngine;
using System;
using System.Collections;

public class Carta {

	private int numero;
	private int color;


	public Carta() {

		numero = 0;
		color  = 0;
	}

	public Carta(int num) {
	
		numero = num;
		color  = 0;
	}

	public Carta(int num, int col) {
	
		numero = num;
		color  = col;
	}

	public Carta(Carta c) {
	
		numero = c.numero;
		color  = c.color;
	}

	public void setNumero(int num) {
	
		numero = num;
	}

	public int getNumero() {
	
		return numero;
	}

	public void setColor(int col) {

		color = col;
	}

	public int getColor() {
	
		return color;
	}

	// crea carta aleatoria
	public static Carta cartaAleatoria() {

		Carta c = new Carta ();
		c.numero = UnityEngine.Random.Range (1, 14);
		c.color  = UnityEngine.Random.Range (1,  5);

		return c;
	}

	// crea numero de cartas distintas aleatorias.
	public static Carta[] cartaAleatoria(int num) {

		if (num < 52)	return null;

		Baraja b = new Baraja ();
		b.barajar ();
		Carta[] c = new Carta[num];

		for (int i = 0; i<num; ++i) {
		
			c[i] = b.sacarCarta ();
		}

		return c;
	}

	public String valueToString() {

		String s;

		switch(this.getNumero()) {

			case 1:
				s = "A";
				break;
			case 11:
				s = "J";
				break;
			case 12:
				s = "Q";
				break;
			case 13:
				s = "K";
				break;
			default:
				s = this.getNumero().ToString();
				break;
		}

		int codigoAscii = 0;
		bool colorRojo = false;

		switch(getColor()) {

			case 1:
				codigoAscii = 9829;
				colorRojo = true;
				break;
			case 2:
				codigoAscii = 9830;
				colorRojo = true;
				break;
			case 3:
				codigoAscii = 9824;
				break;
			case 4:
				codigoAscii = 9827;
				break;
		}
		s = s + Char.ConvertFromUtf32 (codigoAscii);
		return s;
	}
	
	public static void escribir(Carta[] cartas) {

		if (cartas.Length == 0) {

			Debug.Log ("no hay cartas en la baraja");
		}

		String s = cartas [0].valueToString ();

		for (int i = 1; i < cartas.Length; i++) {

			s = s + " " + cartas [i].valueToString ();
		}

		Debug.Log (s);
	}

}












