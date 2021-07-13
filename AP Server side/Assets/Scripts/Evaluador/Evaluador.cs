using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class Evaluador {

	private static int[] VALORC = {1, 8, 57, 400};
	private static int[] VALORN = {0, 1, 5, 24, 112, 521, 2421, 11248, 52256, 242769, 1127845, 5239688, 24342288};

	private static Dictionary<int, int> TABLAC;		// valor de color

	private static Dictionary<int, int> TABLAN5;	// valor mano por relacion equivalencia (ignorando color)
	private static Dictionary<int, int> TABLAN6;
	private static Dictionary<int, int> TABLAN7;

	private static Dictionary<int, int> TABLAC5;	// valor relacion equivalencia mismo color
	private static Dictionary<int, int> TABLAC6;
	private static Dictionary<int, int> TABLAC7;

	private static bool tablasON = false;


	// evalua mano de entre 5 y 7 cartas, devuelve valor relacion equivalencia
	public static int eval(Mano m) {
	
		if (!tablasON) initTablas();

		if      (m.getValorMano()    != -1) return m.getValorMano();
		else if (m.getCartas().Length == 7) return eval7(m);
		else if (m.getCartas().Length == 5) return eval5(m);
		else if (m.getCartas().Length == 6) return eval6(m);

		return -1;		
	}
	
	// mano 5 cartas, encuentra valor segun tabla relaciones de equivalencia 1-7462
	public static int eval5(Mano m) {

		int vNumero = 0;
		int vColor	= 0;
		
		for (int i = 0; i < 5; i++) {

			vNumero += VALORN[m.getCartas()[i].getNumero()-1];
			vColor  += VALORC[m.getCartas()[i].getColor()-1];
		}
		
		if (TABLAC[vColor] == 0) {

			m.setValorMano(TABLAN5[vNumero]);
			return m.getValorMano();
		}
		
		m.setValorMano(TABLAC5[vNumero]);
		return m.getValorMano();
	}
	
	// idem sin tener en cuenta en color
	public static int eval5NoColor(Mano m) {

		int vNumero = 0;
		
		for (int i = 0; i < 5; i++) {
			vNumero += VALORN[m.getCartas()[i].getNumero()-1];
		}

		m.setValorMano(TABLAN5[vNumero]);
		return m.getValorMano();
	}

	// para manos de 6 cartas
	public static int eval6(Mano m) {

		int vNumero = 0,
		vColor  = 0, col;
		
		for (int i = 0; i < 6; i++) {

			vNumero += VALORN[m.getCartas()[i].getNumero()-1];
			vColor  += VALORC[m.getCartas()[i].getColor()-1];
		}
		
		col     = TABLAC[vColor];
		vNumero = TABLAN6[vNumero];
		
		// si no hay color:
		if (col == 0) {

			m.setValorMano(vNumero);
			return vNumero;
		}
		
		// si hay color:
		// sumar valores de las que tienen el mismo color y saber cuantas hay con el mismo color
		int numCartasColor = 0;
		
		vColor = 0;
		
		for (int i = 0; i < 6; i++) {

			if (m.getCartas()[i].getColor() == col) {

				vColor += VALORN[m.getCartas()[i].getNumero()-1];
				numCartasColor++;
			}
		}
		
		if (numCartasColor == 5) {

			m.setValorMano(TABLAC5[vColor]);
			return m.getValorMano();
		}
		
		m.setValorMano(TABLAC6[vColor]);
		return m.getValorMano();
	}
	
	
	// 6 cartas sin tener en cuenta color
	public static int eval6NoColor(Mano m) {

		int vNumero = 0;
		
		for (int i = 0; i < 6; i++) {

			vNumero += VALORN[m.getCartas()[i].getNumero()-1];
		}
		
		vNumero = TABLAN6[vNumero];		
		m.setValorMano(vNumero);
		return vNumero;
	}

	// 7 cartas
	public static int eval7(Mano m) {

		int vNumero = 0, vColor  = 0, col, valorMano, valorMano2;
		
		for (col = 0; col < 7; col++) {

			vNumero += VALORN[m.getCartas()[col].getNumero()-1];
			vColor  += VALORC[m.getCartas()[col].getColor()-1];
		}
		col = TABLAC[vColor];
		
		valorMano = TABLAN7[vNumero];
		
		// si no hay color:
		if (col == 0) {
			m.setValorMano(valorMano);
			return valorMano;
		}
		
		// si hay color:
		// sumar valores de las que tienen el mismo color!
		// (y saber cuantas hay con el mismo color!)
		vNumero = vColor = 0;
		
		for (int i = 0; i < 7; i++) {
			if (m.getCartas()[i].getColor() == col) {

				vColor += VALORN[m.getCartas()[i].getNumero()-1];
				vNumero++;
			}
		}

		if (vNumero == 5)
			valorMano2 = TABLAC5[vColor];
		else if (vNumero == 6)
			valorMano2 = TABLAC6[vColor];
		else
			valorMano2 = TABLAC7[vColor];
		
		if(valorMano2 < valorMano)
			m.setValorMano(valorMano2);
		
		return m.getValorMano();
	}
	
	public static int eval7(Carta[] c1, Carta[] c2) {
		Mano m = new Mano(c1);
		m.addCarta(c2);
		return eval7(m);
	}
	
	
	// devuelve valor numerico en tabla segun numero
	public static int getValorN(Mano m) {
		
		int valor = 0;
		
		for (int i = 0; i < m.getCartas().Length; ++i) {
			
			valor += VALORN[m.getCartas()[i].getNumero()-1];
		}
		
		return valor;
	}
	
	// devuelve el valor numerico segun tabla de colores
	public static int getValorC(Mano m) {
		
		int Valor = 0;
		
		for (int i = 0; i < m.getCartas().Length; ++i) {
			
			Valor += VALORC[m.getCartas()[i].getColor()-1];
		}

		return Valor;
	}

	// devuelve en texto de que jugada se trata
	public static string identJugada(int valor) {
	
		if(valor <= 10)			return " escalera de color";
		else if(valor <= 166)	return " poker";
		else if(valor <= 322)	return " full";
		else if(valor <= 1599)	return " color";
		else if(valor <= 1609)	return " escalera";
		else if(valor <= 2467)	return " trio";
		else if(valor <= 3325)	return " dobles";
		else if(valor <= 6185)	return " pareja";
		else 					return " cartaAlta";
	}
	
	// carga en diccionarios las tablas desde ficharo
	public static void initTablas() {
		
		tablasON = true;
		
		TABLAC   = ficheroToDictionary ("TC.dat");
		
		TABLAN5  = ficheroToDictionary ("TN5.dat");
		TABLAN6  = ficheroToDictionary ("TN6.dat");
		TABLAN7  = ficheroToDictionary ("TN7.dat");

		TABLAC5 = ficheroToDictionary ("TN5C.dat");
		TABLAC6 = ficheroToDictionary ("TN6C.dat");
		TABLAC7 = ficheroToDictionary ("TN7C.dat");

	}

	// parse file data values to returned dictionary
	public static Dictionary<int, int> ficheroToDictionary(string nombre) {
	
		Dictionary<int, int> d = new Dictionary<int, int> ();

		string[] lines = File.ReadAllLines (Application.dataPath + "/Datafiles/" + nombre);	

		if (lines == null) Debug.Log ("no file found");				// WARNING TEST PATH

		foreach (string line in lines) {
		
			string[] row = line.Split(' ');							// WARNING ONLY 2 ELEMENTS
			d.Add(Int32.Parse(row[0]), Int32.Parse(row[1]));
		}

		return d;
	}

}






