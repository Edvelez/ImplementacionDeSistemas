using System;
using System.Net.Sockets;

namespace JuegoBriscas
{
	public class JuegoServidor
	{
		TcpListener listener;

		public JuegoServidor ()
		{
			listener = new TcpListener (8008);
			listener.Start ();
		}

		public void comenzar()
		{

			Console.WriteLine ("Esperando coneccion ...");

			TcpClient jugador1 = listener.AcceptTcpClient ();
			Console.WriteLine ("Jugador 1 se ha conectado!");
			TcpClient jugador2 = listener.AcceptTcpClient ();
			Console.WriteLine ("Jugador 2 se ha conectado!");

			Console.WriteLine ("Iniciando Juego!");

			NetworkStream stream1 = jugador1.GetStream ();
			NetworkStream stream2 = jugador2.GetStream ();

			stream1.WriteByte (1);
			stream2.WriteByte (2);

			byte seed = (byte) new Random().Next();

			stream1.WriteByte (seed);
			stream2.WriteByte (seed);

			//Implemetar logica
			do
			{
				int vaPrimero = stream1.ReadByte();

				//remover byte del buffer
				stream2.ReadByte();

				if (vaPrimero != 0) 
				{
					stream2.WriteByte((byte) stream1.ReadByte ());
					stream1.WriteByte((byte) stream2.ReadByte ());
				} 
				else 
				{
					stream1.WriteByte((byte) stream2.ReadByte ());
					stream2.WriteByte((byte) stream1.ReadByte ());
				}

			}while(stream1.ReadByte () == 0 && stream2.ReadByte () == 0);}

		public static void Main (string[] args)
		{
			JuegoServidor j = new JuegoServidor ();
			j.comenzar ();
		}
	}
}

