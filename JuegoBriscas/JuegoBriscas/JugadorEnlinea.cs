using System;
using System.Net.Sockets;

namespace JuegoBriscas
{
	public class JugadorEnlinea : Jugador
	{
		private TcpClient tcpClient;
		private NetworkStream stream;

		private Jugador jugador;

		public JugadorEnlinea (string servidor, int puerto)
		{
			tcpClient = new TcpClient (servidor, puerto);
			stream = tcpClient.GetStream ();

			byte primerJugador = stream.ReadByte ();

			jugador = new Jugador (primerJugador == 1);
		}

		public void comenzarJuego()
		{
			jugador.pantallaIincio ();

			//terminar de implementar logica
			do
			{
				jugador1.mostrarJuego ();
				jugador2.mostrarJuego ();
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

				jugador1.robarCartas();
				jugador2.robarCartas();

				jugador1.mostrarPuntos();
				jugador2.mostrarPuntos();

			}while(!jugador1.finDeJuego() && !jugador2.finDeJuego());
		}
	}
}

