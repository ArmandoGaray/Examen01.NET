using JuegoDados.Models;

namespace JuegoDados
{
    class Juego
    {
        private Jugador jugador1;

        private List<Jugador> _jugadores;

        private List<int> tiradas;

        private List<int> ganadas;

        private List<int> perdidas;

        private List<int> resultadosExtremos;

        private List<int> resultadosMedios;

        private List<int> pares;

        private List<int> impares;

        private List<int> ordenado;

        private Random r;

        public Juego(string nombreJugador)
        {
            _jugadores = new List<Jugador>();
            r = new Random();
            jugador1 = new Jugador(_jugadores.Count() + 1, nombreJugador, 300);
            tiradas = new List<int>();
            ganadas = new List<int>();
            perdidas = new List<int>();
            resultadosExtremos = new List<int>();
            resultadosMedios = new List<int>();
            pares = new List<int>();
            impares = new List<int>();
            ordenado = new List<int>();
        }

        private bool validaMenu(int opciones, ref int opcionSeleccionada)
        {
            int n;
            if (int.TryParse(Console.ReadLine(), out n))
            {
                if (n <= opciones)
                {
                    opcionSeleccionada = n;
                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Opción Invalida.");
                    return false;
                }
            }
            else
            {
                Console.Clear();
                Console
                    .WriteLine("El valor ingresado no es válido, debes ingresar un número.");
                return false;
            }
        }

        public void showMenuPrincipal()
        {
            int opcionSeleccionada = 0;
            Console.Clear();
            do
            {
                Console.WriteLine("Bienvenido al Juego de dados:");
                Console.WriteLine("1.- Apostar");
                Console.WriteLine("2.- Revisar Estadisticas");
                Console.WriteLine("3.- Salir del juego");
            }
            while (!validaMenu(4, ref opcionSeleccionada));

            switch (opcionSeleccionada)
            {
                case 1:
                    menuApuestas();
                    break;
                case 2:
                    menuEstadisticas();
                    break;
                case 3:
                    datosFinalesJugador();
                    break;
            }
        }

        public int pedirApuesta()
        {
            int valor = -1;
            int dinero = jugador1.montoDinero;
            if (dinero == 0)
            {
                Console.WriteLine("Su monto de dinero ha llegado a 0.");
                Console.WriteLine("Presiona 'Enter' para continuar...");
                Console.ReadLine();
                datosFinalesJugador();
            }
            else
            {
                do
                {
                    Console.Clear();
                    Console
                        .WriteLine(" ¿Cuanto quiere apostar?, su monto es de: " +
                        jugador1.montoDinero);
                    int.TryParse(Console.ReadLine(), out valor);
                    if (valor % 10 != 0)
                    {
                        Console
                            .WriteLine("El monto apostado debe ser multiplo de 10.");
                    }
                    if (valor < 1)
                    {
                        Console
                            .WriteLine("El monto apostado debe ser mayor a 0.");
                    }
                    if (valor > jugador1.montoDinero)
                    {
                        Console
                            .WriteLine("El monto apostado debe ser menor o igual a la cantidad de dinero que tiene.");
                    }
                    if (
                        valor % 10 == 0 &&
                        valor < jugador1.montoDinero &&
                        valor > 1
                    )
                    {
                        Console
                            .WriteLine("La apuesta se ha realizado correctamente.");
                        jugador1.montoDinero = jugador1.montoDinero - valor;
                    }

                    Console.WriteLine("Presiona 'Enter' para continuar...");
                    Console.ReadLine();
                }
                while (valor % 10 != 0 |
                    valor > jugador1.montoDinero |
                    valor < 1
                );
            }
            return valor;
        }

        private void menuApuestas()
        {
            int opcionSeleccionada = 0;
            int dineroApostado = 0;
            Console.Clear();

            do
            {
                Console
                    .WriteLine("Bienvenido, ¿que forma de apuesta quiere tomar?");
                Console.WriteLine("1.- Apostar numero especifico (2-12) x10");
                Console
                    .WriteLine("2.- Apostar a numero extremo (2,3,4,10,11 o 12) x8");
                Console
                    .WriteLine("3.- Apostar a numero medio (5,6,7,8 o 9) x4");
                Console.WriteLine("4.- Apostar por numero Par x2");
                Console.WriteLine("5.- Apostar por numero Impar x2");
                Console.WriteLine("6.- regresar");
            }
            while (!validaMenu(6, ref opcionSeleccionada));
            Console.Clear();

            switch (opcionSeleccionada)
            {
                case 1:
                    dineroApostado = pedirApuesta();
                    apuestaEspecifica (dineroApostado);
                    menuApuestas();
                    break;
                case 2:
                    dineroApostado = pedirApuesta();
                    apuestaExtremos (dineroApostado);
                    menuApuestas();
                    break;
                case 3:
                    dineroApostado = pedirApuesta();
                    apuestaMedios (dineroApostado);
                    menuApuestas();
                    break;
                case 4:
                    dineroApostado = pedirApuesta();
                    apuestaPar (dineroApostado);
                    menuApuestas();
                    break;
                case 5:
                    dineroApostado = pedirApuesta();
                    apuestaImpar (dineroApostado);
                    menuApuestas();
                    break;
                case 6:
                    showMenuPrincipal();
                    break;
            }
        }

        public void datosFinalesJugador()
        {
            Console.Clear();
            int dinero = jugador1.montoDinero;
            int ganancias = 0;
            ganancias = dinero - 300;

            Console.WriteLine("El juego ha terminado.");
            Console.WriteLine("Sus ganancias son de " + ganancias);
            Console.WriteLine("Tomando en cuenta que empezo con 300");
            Environment.Exit(0);
        }

        private void apuestaEspecifica(int valor)
        {
            int apuesta = 0;
            int dados = 0;
            do
            {
                Console.WriteLine("¿A que numero desea apostar entre 2 a 12?");
                int.TryParse(Console.ReadLine(), out apuesta);
            }
            while (apuesta < 2 & apuesta > 12);
            dados = lanzarDados();
            Console.WriteLine("El resultado al lanzar los dados es: " + dados);
            if (apuesta == dados)
            {
                Console.WriteLine("Has ganado " + valor * 10);
                jugador1.montoDinero = jugador1.montoDinero + valor * 10;
                ganadas.Add (dados);
            }
            else
            {
                int dinero = jugador1.montoDinero;
                Console.WriteLine("Has perdido " + valor);
                if (dinero == valor)
                {
                    jugador1.montoDinero = 0;
                }
                perdidas.Add (dados);
            }
            registrarJugadas (dados);
            Console.WriteLine("Presiona 'Enter' para continuar...");
            Console.ReadLine();
        }

        private void apuestaExtremos(int valor)
        {
            int dados = 0;
            dados = lanzarDados();
            Console.WriteLine("El resultado al lanzar los dados es: " + dados);
            if (
                dados == 2 |
                dados == 3 |
                dados == 4 |
                dados == 10 |
                dados == 11 |
                dados == 12
            )
            {
                Console.WriteLine("Has ganado " + valor * 8);
                jugador1.montoDinero = jugador1.montoDinero + valor * 8;
                ganadas.Add (dados);
            }
            else
            {
                int dinero = jugador1.montoDinero;
                Console.WriteLine("Has perdido " + valor);
                if (dinero == valor)
                {
                    jugador1.montoDinero = 0;
                }
                perdidas.Add (dados);
            }
            registrarJugadas (dados);
            Console.WriteLine("Presiona 'Enter' para continuar...");
            Console.ReadLine();
        }

        private void apuestaMedios(int valor)
        {
            int dados = 0;
            dados = lanzarDados();
            Console.WriteLine("El resultado al lanzar los dados es: " + dados);
            if (dados == 5 | dados == 6 | dados == 7 | dados == 8 | dados == 9)
            {
                Console.WriteLine("Has ganado " + valor * 4);
                jugador1.montoDinero = jugador1.montoDinero + valor * 4;
                ganadas.Add (dados);
            }
            else
            {
                int dinero = jugador1.montoDinero;
                Console.WriteLine("Has perdido " + valor);
                if (dinero == valor)
                {
                    jugador1.montoDinero = 0;
                }
                perdidas.Add (dados);
            }
            registrarJugadas (dados);
            Console.WriteLine("Presiona 'Enter' para continuar...");
            Console.ReadLine();
        }

        private void apuestaPar(int valor)
        {
            int dados = 0;
            dados = lanzarDados();
            Console.WriteLine("El resultado al lanzar los dados es: " + dados);
            if (dados % 2 == 0)
            {
                Console.WriteLine("Has ganado " + valor * 2);
                jugador1.montoDinero = jugador1.montoDinero + valor * 2;
                ganadas.Add (dados);
            }
            else
            {
                int dinero = jugador1.montoDinero;
                Console.WriteLine("Has perdido " + valor);
                if (dinero == valor)
                {
                    jugador1.montoDinero = 0;
                }
                perdidas.Add (dados);
            }
            registrarJugadas (dados);
            Console.WriteLine("Presiona 'Enter' para continuar...");
            Console.ReadLine();
        }

        private void apuestaImpar(int valor)
        {
            int dados = 0;
            dados = lanzarDados();
            Console.WriteLine("El resultado al lanzar los dados es: " + dados);
            if (dados % 2 != 0)
            {
                Console.WriteLine("Has ganado " + valor * 2);
                jugador1.montoDinero = jugador1.montoDinero + valor * 2;
                ganadas.Add (dados);
            }
            else
            {
                int dinero = jugador1.montoDinero;
                Console.WriteLine("Has perdido " + valor);
                if (dinero == valor)
                {
                    jugador1.montoDinero = 0;
                }
                perdidas.Add (dados);
            }
            registrarJugadas (dados);
            Console.WriteLine("Presiona 'Enter' para continuar...");
            Console.ReadLine();
        }

        public void registrarJugadas(int valor)
        {
            tiradas.Add (valor);
            if (
                valor == 2 |
                valor == 3 |
                valor == 4 |
                valor == 10 |
                valor == 11 |
                valor == 12
            )
            {
                resultadosExtremos.Add (valor);
            }
            if (valor == 5 | valor == 6 | valor == 7 | valor == 8 | valor == 9)
            {
                resultadosMedios.Add (valor);
            }
            if (valor % 2 == 0)
            {
                pares.Add (valor);
            }
            if (valor % 2 != 0)
            {
                impares.Add (valor);
            }
        }

        public int lanzarDados()
        {
            int dado1 = r.Next(1, 7);
            int dado2 = r.Next(1, 7);
            Console.Clear();
            int dados = dado1 + dado2;
            return dados;
        }

        public void menuEstadisticas()
        {
            int opcionSeleccionada = 0;
            Console.Clear();
            do
            {
                Console
                    .WriteLine("Bienvenido a las estadisticas del juego del dado");
                Console.WriteLine("1.- Balance");
                Console.WriteLine("2.- Cantidad de tiradas realizadas");
                Console.WriteLine("3.- Numero mas veces tirado");
                Console.WriteLine("4.- Numero menos veces tirado");
                Console.WriteLine("5.- Cantidad de resultados extremos");
                Console.WriteLine("6.- Cantidad de resultados medios");
                Console.WriteLine("7.- Cantidad de resultados pares");
                Console.WriteLine("8.- Cantidad de resultados impares");
                Console.WriteLine("9.- regresar");
            }
            while (!validaMenu(9, ref opcionSeleccionada));
            Console.Clear();
            switch (opcionSeleccionada)
            {
                case 1:
                    Console
                        .WriteLine("Las partidas ganadas son: " +
                        ganadas.Count());
                    Console
                        .WriteLine("Las partidas perdidas son: " +
                        perdidas.Count());
                    Console.WriteLine("Presiona 'Enter' para continuar...");
                    Console.ReadLine();
                    menuEstadisticas();
                    break;
                case 2:
                    Console
                        .WriteLine("Las tiradas totales son: " +
                        tiradas.Count());
                    Console.WriteLine("Presiona 'Enter' para continuar...");
                    Console.ReadLine();
                    menuEstadisticas();
                    break;
                case 3:
                    int _masRepetido = masRepetido();
                    Console
                        .WriteLine("El valor mas repetido es: " + _masRepetido);
                    Console.WriteLine("Presiona 'Enter' para continuar...");
                    Console.ReadLine();
                    menuEstadisticas();
                    break;
                case 4:
                    int _menosRepetido = menosRepetido();
                    Console
                        .WriteLine("El valor mas repetido es: " +
                        _menosRepetido);
                    Console.WriteLine("Presiona 'Enter' para continuar...");
                    Console.ReadLine();
                    menuEstadisticas();
                    break;
                case 5:
                    Console
                        .WriteLine("La cantidad de resultados extremos son: " +
                        resultadosExtremos.Count());
                    Console.WriteLine("Presiona 'Enter' para continuar...");
                    Console.ReadLine();
                    menuEstadisticas();
                    break;
                case 6:
                    Console
                        .WriteLine("La cantidad de resultados medios son: " +
                        resultadosMedios.Count());
                    Console.WriteLine("Presiona 'Enter' para continuar...");
                    Console.ReadLine();
                    menuEstadisticas();
                    break;
                case 7:
                    Console
                        .WriteLine("La cantidad de resultados pares son: " +
                        pares.Count());
                    Console.WriteLine("Presiona 'Enter' para continuar...");
                    Console.ReadLine();
                    menuEstadisticas();
                    break;
                case 8:
                    Console
                        .WriteLine("La cantidad de resultados impares son: " +
                        impares.Count());
                    Console.WriteLine("Presiona 'Enter' para continuar...");
                    Console.ReadLine();
                    menuEstadisticas();
                    break;
                case 9:
                    showMenuPrincipal();
                    break;
            }
        }

        private int masRepetido()
        {
            int contador = 1;
            int contador1 = -999;
            int dato = 0;
            int i;
            tiradas.Sort();

            for (i = 0; i < tiradas.Count() - 1; i++)
            {
                if (i + 1 < tiradas.Count())
                {
                    if (tiradas[i] == tiradas[i + 1])
                    {
                        contador = contador + 1;
                    }
                    else
                    {
                        if (contador > contador1)
                        {
                            contador1 = contador;
                            dato = tiradas[i];
                        }
                        contador = 1;
                    }
                }
            }
            if (contador > contador1)
            {
                contador1 = contador;
                dato = tiradas[i];
            }
            return dato;
        }

        private int menosRepetido()
        {
            int contador2 = 0;
            int contador3 = 0;
            int contador4 = 0;
            int contador5 = 0;
            int contador6 = 0;
            int contador7 = 0;
            int contador8 = 0;
            int contador9 = 0;
            int contador10 = 0;
            int contador11 = 0;
            int contador12 = 0;
            int menorRepeticion = 0;

            for (int i = 0; i < tiradas.Count(); i++)
            {
                if (tiradas[i] == 2)
                {
                    contador2 = contador2 + 1;
                }
                if (tiradas[i] == 3)
                {
                    contador3 = contador3 + 1;
                }
                if (tiradas[i] == 4)
                {
                    contador4 = contador4 + 1;
                }
                if (tiradas[i] == 5)
                {
                    contador5 = contador5 + 1;
                }
                if (tiradas[i] == 6)
                {
                    contador6 = contador6 + 1;
                }
                if (tiradas[i] == 7)
                {
                    contador7 = contador7 + 1;
                }
                if (tiradas[i] == 8)
                {
                    contador8 = contador8 + 1;
                }
                if (tiradas[i] == 9)
                {
                    contador9 = contador9 + 1;
                }
                if (tiradas[i] == 10)
                {
                    contador10 = contador10 + 1;
                }
                if (tiradas[i] == 11)
                {
                    contador11 = contador11 + 1;
                }
                if (tiradas[i] == 12)
                {
                    contador12 = contador12 + 1;
                }
            }
            if (
                contador2 <= contador3 &&
                contador2 <= contador4 &&
                contador2 <= contador5 &&
                contador2 <= contador6 &&
                contador2 <= contador7 &&
                contador2 <= contador8 &&
                contador2 <= contador9 &&
                contador2 <= contador10 &&
                contador2 <= contador11 &&
                contador2 <= contador12
            )
            {
                menorRepeticion = 2;
            }
            if (
                contador3 <= contador2 &&
                contador3 <= contador4 &&
                contador3 <= contador5 &&
                contador3 <= contador6 &&
                contador3 <= contador7 &&
                contador3 <= contador8 &&
                contador3 <= contador9 &&
                contador3 <= contador10 &&
                contador3 <= contador11 &&
                contador3 <= contador12
            )
            {
                menorRepeticion = 3;
            }
            if (
                contador4 <= contador3 &&
                contador4 <= contador2 &&
                contador4 <= contador5 &&
                contador4 <= contador6 &&
                contador4 <= contador7 &&
                contador4 <= contador8 &&
                contador4 <= contador9 &&
                contador4 <= contador10 &&
                contador4 <= contador11 &&
                contador4 <= contador12
            )
            {
                menorRepeticion = 4;
            }
            if (
                contador5 <= contador3 &&
                contador5 <= contador4 &&
                contador5 <= contador2 &&
                contador5 <= contador6 &&
                contador5 <= contador7 &&
                contador5 <= contador8 &&
                contador5 <= contador9 &&
                contador5 <= contador10 &&
                contador5 <= contador11 &&
                contador5 <= contador12
            )
            {
                menorRepeticion = 5;
            }
            if (
                contador6 <= contador3 &&
                contador6 <= contador4 &&
                contador6 <= contador5 &&
                contador6 <= contador2 &&
                contador6 <= contador7 &&
                contador6 <= contador8 &&
                contador6 <= contador9 &&
                contador6 <= contador10 &&
                contador6 <= contador11 &&
                contador6 <= contador12
            )
            {
                menorRepeticion = 6;
            }
            if (
                contador7 <= contador3 &&
                contador7 <= contador4 &&
                contador7 <= contador5 &&
                contador7 <= contador6 &&
                contador7 <= contador2 &&
                contador7 <= contador8 &&
                contador7 <= contador9 &&
                contador7 <= contador10 &&
                contador7 <= contador11 &&
                contador7 <= contador12
            )
            {
                menorRepeticion = 7;
            }
            if (
                contador8 <= contador3 &&
                contador8 <= contador4 &&
                contador8 <= contador5 &&
                contador8 <= contador6 &&
                contador8 <= contador7 &&
                contador8 <= contador2 &&
                contador8 <= contador9 &&
                contador8 <= contador10 &&
                contador8 <= contador11 &&
                contador8 <= contador12
            )
            {
                menorRepeticion = 8;
            }
            if (
                contador9 <= contador3 &&
                contador9 <= contador4 &&
                contador9 <= contador5 &&
                contador9 <= contador6 &&
                contador9 <= contador7 &&
                contador9 <= contador8 &&
                contador9 <= contador2 &&
                contador9 <= contador10 &&
                contador9 <= contador11 &&
                contador9 <= contador12
            )
            {
                menorRepeticion = 9;
            }
            if (
                contador10 <= contador3 &&
                contador10 <= contador4 &&
                contador10 <= contador5 &&
                contador10 <= contador6 &&
                contador10 <= contador7 &&
                contador10 <= contador8 &&
                contador10 <= contador9 &&
                contador10 <= contador2 &&
                contador10 <= contador11 &&
                contador10 <= contador12
            )
            {
                menorRepeticion = 10;
            }
            if (
                contador11 <= contador3 &&
                contador11 <= contador4 &&
                contador11 <= contador5 &&
                contador11 <= contador6 &&
                contador11 <= contador7 &&
                contador11 <= contador8 &&
                contador11 <= contador9 &&
                contador11 <= contador10 &&
                contador11 <= contador2 &&
                contador11 <= contador12
            )
            {
                menorRepeticion = 11;
            }
            if (
                contador12 <= contador3 &&
                contador12 <= contador4 &&
                contador12 <= contador5 &&
                contador12 <= contador6 &&
                contador12 <= contador7 &&
                contador12 <= contador8 &&
                contador12 <= contador9 &&
                contador12 <= contador10 &&
                contador12 <= contador11 &&
                contador12 <= contador2
            )
            {
                menorRepeticion = 12;
            }
            return menorRepeticion;
        }

        public void inicializarDatos()
        {
            ganadas.Add(8);
            ganadas.Add(8);
            ganadas.Add(6);
            ganadas.Add(6);
            ganadas.Add(10);
            ganadas.Add(2);
            ganadas.Add(8);
            ganadas.Add(12);
            ganadas.Add(8);
            ganadas.Add(4);
            ganadas.Add(4);
            ganadas.Add(6);

            perdidas.Add(9);
            perdidas.Add(9);
            perdidas.Add(7);

            tiradas.Add(8);
            tiradas.Add(8);
            tiradas.Add(6);
            tiradas.Add(6);
            tiradas.Add(10);
            tiradas.Add(2);
            tiradas.Add(8);
            tiradas.Add(12);
            tiradas.Add(8);
            tiradas.Add(4);
            tiradas.Add(4);
            tiradas.Add(6);
            tiradas.Add(9);
            tiradas.Add(9);
            tiradas.Add(7);

            resultadosExtremos.Add(10);
            resultadosExtremos.Add(2);
            resultadosExtremos.Add(12);
            resultadosExtremos.Add(4);
            resultadosExtremos.Add(4);

            resultadosMedios.Add(8);
            resultadosMedios.Add(8);
            resultadosMedios.Add(9);
            resultadosMedios.Add(6);
            resultadosMedios.Add(6);
            resultadosMedios.Add(9);
            resultadosMedios.Add(8);
            resultadosMedios.Add(8);
            resultadosMedios.Add(7);
            resultadosMedios.Add(6);

            pares.Add(8);
            pares.Add(8);
            pares.Add(6);
            pares.Add(6);
            pares.Add(10);
            pares.Add(2);
            pares.Add(8);
            pares.Add(12);
            pares.Add(8);
            pares.Add(4);
            pares.Add(4);
            pares.Add(6);

            impares.Add(9);
            impares.Add(9);
            impares.Add(7);

            jugador1.montoDinero = 390;
        }
    }
}
