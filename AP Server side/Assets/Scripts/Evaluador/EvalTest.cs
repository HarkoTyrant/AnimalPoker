using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Text;

public class EvalTest : MonoBehaviour {
	

	void OnGUI () {
		
		if(GUI.Button(new Rect(50,  50, 200, 80), "TEST JUEGO" )) Test2();
		if(GUI.Button(new Rect(50, 160, 200, 80), "TEST FUERZA")) Test1();
	}

	// ejemplo uso en motor del servidor
	public void Test2() {
		
		Stopwatch sw = new Stopwatch ();

		/***********************************/

		Evaluador.initTablas();				// inicializar tablas de valores
		Baraja b = new Baraja();			// crear la baraja
		b.barajar();						// barajar... lol

		int whoWin = -1;
		int bestGame = int.MaxValue;
				
		Mano [] PlayersManos = new Mano[6];				// cartas de cada jugador

		for (int i = 0; i < PlayersManos.Length; i++) {	
		
			PlayersManos[i] = new Mano();
			PlayersManos[i].setCartas(b.sacarCartas(2));
		}
		
		Mano manoMesa = new Mano ();
		manoMesa.setCartas(b.sacarCartas(5));							// 5 cartas en la mesa

		sw.Start ();													// CLOCK START

		for (int i = 0; i < PlayersManos.Length; i++) {					// Agrupa y evalua manos

			Mano manoTmp = new Mano();									// cartas mesa + player para evaluar
			manoTmp.setCartas(manoMesa.getCartas());

			manoTmp.addCarta (PlayersManos [i].getCartas ());
			Carta.escribir (manoTmp.getCartas ());						// DEBUG

			PlayersManos[i].setValorMano(Evaluador.eval7(manoTmp));		// sacamos el valor de la jugada

			if(bestGame > PlayersManos[i].getValorMano()) {

				bestGame = PlayersManos[i].getValorMano();
				whoWin = i;
			}

			string jugada = Evaluador.identJugada(PlayersManos[i].getValorMano());
			UnityEngine.Debug.Log("player " + i + " valor mano " + PlayersManos[i].getValorMano() + jugada);
		}

		UnityEngine.Debug.Log(" Player: " + whoWin + " win with " + Evaluador.identJugada(PlayersManos[whoWin].getValorMano()));

		/***********************************/
		sw.Stop ();
		
		UnityEngine.Debug.Log ("Time: " + sw.Elapsed.Milliseconds + " ms");
	}
	
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	// ejemplo bruto para tests
	public void Test1() {

		Stopwatch sw = new Stopwatch ();
		sw.Start ();
		
		Evaluador.initTablas();
		Baraja b = new Baraja();
		b.barajar();
		
		Carta[] cartas = new Carta[7];
		Mano m = new Mano();
		
		int valor;
		int escColor = 0, full=0, poker=0, escalera=0, color=0, trio=0;
		int cartaAlta = 0, dobles=0, pareja=0, totales=0;

		//sw.Start ();

		for(int i1=0; i1<b.getNumCartas(); i1++) { 
			
			cartas[0] = b.verCarta(i1);
			UnityEngine.Debug.Log("Iteración: "+i1+" de 52");
			
			for(int i2=i1+1; i2<b.getNumCartas(); i2++) { 
				
				cartas[1] = b.verCarta(i2);
				
				for(int i3=i2+1; i3<b.getNumCartas(); i3++) { 
					
					cartas[2] = b.verCarta(i3);
					
					for(int i4=i3+1; i4<b.getNumCartas(); i4++) { 
						
						cartas[3] = b.verCarta(i4);
						
						for(int i5=i4+1; i5<b.getNumCartas(); i5++) { 
							
							cartas[4] = b.verCarta(i5);
							
							for(int i6=i5+1; i6<b.getNumCartas(); i6++) { 
								
								cartas[5] = b.verCarta(i6);
								
								for(int i7=i6+1; i7<b.getNumCartas(); i7++) {
									
									cartas[6] = b.verCarta(i7);
									m.setCartas(cartas);

									valor = Evaluador.eval7(m);
																		
									if(valor <= 10)
										escColor++;
									else if(valor <= 166)
										poker++;
									else if(valor <= 322)
										full++;
									else if(valor <= 1599)
										color++;
									else if(valor <= 1609)
										escalera++;
									else if(valor <= 2467)
										trio++;
									else if(valor <= 3325)
										dobles++;
									else if(valor <= 6185)
										pareja++;
									else
										cartaAlta++;
									
									totales++;
									
								}
							}
						}
					}
				}
			}
		}
		
		sw.Stop ();
		
		UnityEngine.Debug.Log ("\n");
		UnityEngine.Debug.Log ("\n");
		
		UnityEngine.Debug.Log ("Resultados: ");
		UnityEngine.Debug.Log ("Esc Color: " +escColor);
		UnityEngine.Debug.Log ("Poker: " +poker);
		UnityEngine.Debug.Log ("Full: " +full);
		UnityEngine.Debug.Log ("Color: " +color);
		UnityEngine.Debug.Log ("Escalera: " +escalera);
		UnityEngine.Debug.Log ("Trio: " +trio);
		UnityEngine.Debug.Log ("Dobles parejas: " +dobles);
		UnityEngine.Debug.Log ("Pareja: " +pareja);
		UnityEngine.Debug.Log ("Carta alta: "+cartaAlta);
		
		UnityEngine.Debug.Log ("\n");
		UnityEngine.Debug.Log ("Totales: "+totales);
		UnityEngine.Debug.Log ("Tiempo: " + sw.Elapsed.Seconds + " seconds");
	}
	
}
