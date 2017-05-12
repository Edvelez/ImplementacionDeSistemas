using System;

namespace JuegoBriscas
{
	public class Jugador
	{
		private int[] cartas;
		private int[] mano;
		private int vida;
		private int puntos;

		private int cartaARobar;

		private int jugadaTuya;
		private int jugadaDeEl;

		private bool tienePrimerTurno;



		public Jugador (bool isPlayerOne)
		{
			cartas = new int[40];
			for (int i = 0; i < 40; ++i) 
			{
				cartas [i] = i;
			}

			mano = new int[3];
			puntos = 0;
			this.tienePrimerTurno = isPlayerOne;
			cartaARobar = this.tienePrimerTurno ? 0 : 1;
		}

		public void barajar(int seed)
		{
			Random rand = new Random (seed);
			for (int i = 0; i < 1000; ++i) 
			{
				int pos = rand.Next () % 40;
				int temp = cartas [0];
				cartas [0] = cartas [pos];
				cartas [pos] = temp;
			}
		}

		public void repartir()
		{
			int cartaARobar = tienePrimerTurno ? 0 : 1;

			for (int i = 0; i < 3; ++i) 
			{
				mano [i] = cartas [cartaARobar];
				cartaARobar += 2;
			}

			cartaARobar = 5;

			vida = cartas[cartaARobar + 1];

			cartas [cartaARobar + 1] = cartas [39];
			cartas [39] = vida;
		}

		public void recibirJugada(int carta)
		{
			jugadaDeEl = carta;
			Console.WriteLine ("Tu oponente jugo: " + cartaEnTexto (jugadaDeEl));
		}

		public int hacerJugada()
		{
			bool selecVal = false;
			int seleccion = -1;
			do
			{
				Console.WriteLine ("Escoje una carta (0, 1 o 2): ");

				for (int i = 0; i < 3; ++i) 
				{
					if(mano[i] != -1)
						Console.WriteLine (i + ") " + cartaEnTexto (mano [i]));
				}

				try
				{
					seleccion = Int32.Parse(Console.ReadLine());
					if(seleccion > -1 && seleccion < 3)
						selecVal = true;
				}
				catch(Exception e)
				{
					selecVal = false;
				}

			}while(!selecVal || mano[seleccion] == -1);

			int temp = mano [seleccion];
			mano [seleccion] = mano [0];
			mano [0] = temp;

			jugadaTuya = mano [0];

			return jugadaTuya;	
		}

		public void procesarJugada()
		{
			int primeraJugada;
			int segundaJugada;
			if (tienePrimerTurno) 
			{
				primeraJugada = jugadaTuya;
				segundaJugada = jugadaDeEl;
			} 
			else 
			{
				segundaJugada = jugadaDeEl;
				primeraJugada = jugadaTuya;
			}

			if (primeraJugada / 10 == segundaJugada / 10) 
			{
				if (primeraJugada > segundaJugada && tienePrimerTurno)
				{
					puntos += puntosDeCarta (primeraJugada) + puntosDeCarta (segundaJugada);
				} 
				else if (segundaJugada > primeraJugada && !tienePrimerTurno)
				{
					puntos += puntosDeCarta (primeraJugada) + puntosDeCarta (segundaJugada);
					tienePrimerTurno = true;
				}
				return;
			}
			else if((primeraJugada/10 != vida/10) && (segundaJugada/10 != vida/10))
			{
				if ((primeraJugada / 10 != segundaJugada / 10) && tienePrimerTurno)
				{
					puntos += puntosDeCarta (primeraJugada) + puntosDeCarta (segundaJugada);
				}
				else
				{
					if (primeraJugada > segundaJugada && tienePrimerTurno)
					{
						puntos += puntosDeCarta (primeraJugada) + puntosDeCarta (segundaJugada);
					} 
					else if (segundaJugada > primeraJugada && !tienePrimerTurno)
					{
						puntos += puntosDeCarta (primeraJugada) + puntosDeCarta (segundaJugada);
						tienePrimerTurno = true;
					}
				}
				return;
			}
			else
			{
				if(primeraJugada/10 == vida/10 && tienePrimerTurno)
				{
					puntos += puntosDeCarta (primeraJugada) + puntosDeCarta (segundaJugada);
				}
				else if(segundaJugada/10 == vida/10 && !tienePrimerTurno)
				{
					puntos += puntosDeCarta (primeraJugada) + puntosDeCarta (segundaJugada);
					tienePrimerTurno = true;
				}
			}
		}

		private int puntosDeCarta(int carta)
		{
			switch (carta % 10)
			{
			case 1:
			case 3:
			case 4:
			case 5:
			case 6:
				return 0;
			case 0:
				return Palo.UNO;
			case 2:
				return Palo.TRES;
			case 7:
				return Palo.DIEZ;
			case 8:
				return Palo.ONCE;
			case 9:
				return Palo.DOCE;
			default:
				return 0;
			}
		}

		public void robarCartas()
		{
			if (cartaARobar < 38)
			{
				if (tienePrimerTurno)
				{
					mano [0] = cartas [cartaARobar + 1];
				} 
				else
				{
					mano [0] = cartas [cartaARobar + 2];
				}
				cartaARobar += 2;
			}
			else
			{
				int cuenta = 0;
				for (int i = 0; i < 3; ++i)
				{
					if (mano [i] == -1)
						++cuenta;
				}

				for (int i = 1; i < 3; ++i)
				{
					mano [i - 1] = mano [i];
				}

				for (int i = 2 - cuenta; i < cuenta; ++i)
				{
					mano [i] = -1;
				}
			}
		}

		private String cartaEnTexto(int carta)
		{
			String s;
			s = carta / 10 == Palo.ORO ? "Oro" : carta / 10 == Palo.ESPADA ? "Espada" : carta / 10 == Palo.COPA ? "Copa" : "Basto";
			s += " " + (carta % 10 < 7 ? carta % 10 + 1 : carta % 10 == 7 ? 10 : carta % 10 == 8 ? 11 : 12);
			return s;
		}

		public bool finDeJuego()
		{
			bool fin = true;
			for (int i = 0; i < 3; ++i)
			{
				if (mano [i] != -1)
				{
					fin = false;
				}
			}
			return fin;
		}

		public void mostrarJuego()
		{
			for (int i = cartaARobar; i < 40; ++i) 
			{
				Console.Write ("#");
			}

			Console.Write ("\n");
			Console.WriteLine ("Vida: " + cartaEnTexto(vida));
		}

		public bool juegaPrimero()
		{
			return tienePrimerTurno;
		}
	}
}

