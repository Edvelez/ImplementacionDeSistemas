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
			jugador1.mostrarJuego ();
		}

		public void comenzarJuego()
		{
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
		}
		//Lo que falte
	}
}

