using System;

namespace JuegoBriscas
{
	public class Juego
	{
		Jugador jugador1;
		Jugador jugador2;


		public Juego ()
		{
			
		}

		public void pantallaIincio()
		{
			Console.WriteLine ("Bienvenido al juego de Briscas");
		}

		public void inicializar()
		{
			Random rand = new Random ();
			int seed = rand.Next ();
			jugador1 = new Jugador (true);
			jugador2 = new Jugador (false);
			jugador1.barajar (seed);
			jugador2.barajar (seed);
			jugador1.repartir ();
			jugador2.repartir ();
		}

		public void comenzarJuego()
		{
			do
			{
				jugador1.mostrarJuego ();
				jugador2.mostrarJuego();
				if (jugador1.juegaPrimero ()) 
				{
					jugador2.recibirJugada (jugador1.hacerJugada ());
					jugador1.recibirJugada (jugador2.hacerJugada ());
				} 
				else 
				{
					jugador1.recibirJugada (jugador2.hacerJugada ());
					jugador2.recibirJugada (jugador1.hacerJugada ());
				}
				jugador1.procesarJugada ();
				jugador2.procesarJugada ();
				if(!jugador1.finDeJuego() && !jugador2.finDeJuego())
				{
					jugador1.robarCartas();
					jugador2.robarCartas();
				}
			}while(!jugador1.finDeJuego() && !jugador2.finDeJuego());
		}
	}
}

