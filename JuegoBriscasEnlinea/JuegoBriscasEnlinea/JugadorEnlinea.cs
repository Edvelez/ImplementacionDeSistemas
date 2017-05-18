using System;
using System.Net.Sockets;

namespace JuegoBriscas
{
	public class JugadorEnlinea : Jugador
	{
		private TcpClient tcpClient;
		private NetworkStream stream;

		private Jugador jugador;

		public JugadorEnlinea (string servidor, int puerto) : base(true)
		{
			tcpClient = new TcpClient (servidor, puerto);
			stream = tcpClient.GetStream ();

			byte primerJugador = (byte) stream.ReadByte ();

			jugador = new Jugador (primerJugador == 1);
		}

		public static void Main (string[] args)
		{
			JugadorEnlinea je = new JugadorEnlinea (args[0], Int32.Parse(args[1]));
			je.comenzarJuego ();
		}

		public void comenzarJuego()
		{
			jugador.pantallaIincio ();
			jugador.barajar (stream.ReadByte ());
			jugador.repartir ();

			do
			{
				jugador.mostrarJuego ();

				stream.WriteByte ((byte) juegaPrimero ());

				if (jugador.juegaPrimero ()) 
				{
					stream.WriteByte ((byte) jugador.hacerJugada ());
					jugador.recibirJugada (stream.ReadByte ());
				} 
				else 
				{
					jugador.recibirJugada (stream.ReadByte ());
					stream.WriteByte ((byte) jugador.hacerJugada ());
				}
				jugador.procesarJugada ();

				jugador.robarCartas();

				jugador.mostrarPuntos();

				stream.WriteByte ((byte) finDeJuego());

			}while(!jugador.finDeJuego());
		}

		public int finDeJuego()
		{
			return jugador.finDeJuego () ? 1 : 0;
		}

		public void pantallaIincio ()
		{
			jugador.pantallaIincio ();
			Console.WriteLine ("Esperando tu oponente");
		}

		public int juegaPrimero()
		{
			return jugador.juegaPrimero () ? 1 : 0;
		}
	}
}

