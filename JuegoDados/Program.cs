using JuegoDados.Models;
using JuegoDados;

Console.Clear();
List<Jugador> _jugadores = new List<Jugador>();
// Pedir nombre de jugador
Console.WriteLine("Ingrese el nombre del jugador");
string nombre = Console.ReadLine();

Juego juego = new Juego(nombre);
juego.inicializarDatos();
juego.showMenuPrincipal();