using UnityEngine;
using System.Collections;


public class Baraja {

	private Carta[] cartas;
	private int numCartas;

	public Baraja() {
	
		inicializar ();
	}

	//init baraja cartas ordenadas
	public void inicializar() {
	
		cartas = new Carta[52];

		for (int i = 0; i<4; i++) {	
			for(int j = 0; j<13; j++){
			
				cartas[13*i+j] = new Carta(j+1, i+1);
			}
		}
		numCartas = 52;
	}

	// reordena aleatoriamente las cartas que haya en la baraja
	public void barajar () {
	
		Carta c;
		int aleatorio;

		for (int i = 0; i < numCartas; ++i) {
		
			aleatorio = UnityEngine.Random.Range(0, numCartas);
			c = cartas[i];
			cartas[i] = cartas[aleatorio];
			cartas[aleatorio] = c;
		}
	}

	// extrae una carta
	public Carta sacarCarta() {

		if (numCartas > 0) {
		
			numCartas--;
			return cartas[numCartas];
		}
		return null;
	}

	// extrae "numeroCartas" solicitado
	public Carta[] sacarCartas(int numeroCartas) {
	
		Carta[] c = new Carta[numeroCartas];

		for (int i = 0; i < numeroCartas; i++) {
		
			c[i] = this.sacarCarta();
		}

		return c;
	}

	// sacar carta por indice si no ha sido extraida
	public Carta sacarCarta(int indice){
	
		if (indice >= numCartas) return null;

		intercambiar (numCartas - 1, indice);

		return sacarCarta ();	// indice ahora es la ultima y zas
	}

	// elimina carta para que no pueda volver a salir (puede desordenar baraja)
	public void sacarCarta(Carta c) {

		for (int i = 0; i<numCartas; i++) {
		
			if(c.getColor() == cartas[i].getColor() && c.getNumero() == cartas[i].getNumero()) {
			
				intercambiar (i, numCartas-1);
				i = numCartas;
				numCartas--;
			}
		}
	}

	// intercambia posicion cartas de la baraja
	public void intercambiar(int indice1, int indice2) {
	
		Carta c1 = cartas[indice1];
		cartas [indice1] = cartas [indice2];
		cartas [indice2] = c1;
	}

	// mirar una carta de la baraja, sin quitarla
	public Carta verCarta(int indice) {
	
		if (indice >= numCartas) return new Carta ();
		return cartas [indice];

	}

	// true si la baraja esta vacia
	public bool estaVacia() {
	
		if (numCartas == 0)	return true;
		return true;
	}

	//cuantas cartas quedan
	public int getNumCartas() {
	
		return numCartas;
	}

}










