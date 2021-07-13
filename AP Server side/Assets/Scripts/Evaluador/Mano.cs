using UnityEngine;
using System.Collections;


public class Mano {
	
	private Carta[] cartas;
	private int valorMano;
	
	public Mano() {

		cartas = null;
		valorMano = -1;
	}
	
	public Mano(int numCartas) {

		cartas = new Carta[numCartas];
		valorMano = -1;
	}
	
	public Mano(Carta c) {

		cartas    = new Carta[1];
		cartas[0] = c;
		valorMano = -1;
	}
	
	public Mano(Carta[] c) {

		cartas = new Carta[c.Length];
		System.Array.Copy (c, cartas, c.Length);
		valorMano = -1;
	}
	
	public int getNumCartas() {

		return cartas.Length;
	}
	
	public Carta[] getCartas() {

		return cartas;
	}
	
	public void setCarta(Carta c, int indice) {

		if (indice < cartas.Length) {
			cartas [indice] = c;
		}
		else addCarta(c);

		valorMano = -1;
	}
	
	public void setCartas(Carta[] c) {
		cartas = c;
	}
	
	public void setCopiaCartas(Carta[] c) {

		cartas = new Carta[c.Length];
		System.Array.Copy (c, cartas, c.Length);
		valorMano = -1;
	}
	
	public void addCarta(Carta c) {

		Carta [] cTemp = new Carta[cartas.Length + 1];
		
		System.Array.Copy (cartas, cTemp, cartas.Length);
		cTemp [cartas.Length] = c;
		
		cartas = cTemp;
		valorMano = -1;
	}

	public void addCarta(Carta[] c) {

		Carta [] cTemp = new Carta[cartas.Length + c.Length];

		System.Array.Copy (cartas, cTemp, cartas.Length);
		System.Array.Copy (c, 0, cTemp, cartas.Length, c.Length);

		cartas = cTemp;
		valorMano = -1;
	}
	
	public int getValorMano() {

		return valorMano;
	}
	
	public void setValorMano(int valor) {

		valorMano = valor;
	}
	
	// resumen basico de la mano
	public void escribir() {

		Carta.escribir(cartas);
		if(valorMano != -1) Debug.Log ("El valor de la mano es de: " + valorMano);
	}
}
