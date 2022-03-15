namespace JuegoDados.Models
{
    class Jugador
    {
        private int _id_jugador;
        private string _nombre_jugador;
        private int _monto_dinero;

        public Jugador(int id, string nombre, int monto)
        {
            this._id_jugador = id;
            this._nombre_jugador = nombre;
            this._monto_dinero = monto;
        }

        public int idJugador
        {
            get { return _id_jugador; }
            set { _id_jugador = value; }
        }

        public string nombreJugador
        {
            get { return _nombre_jugador; }
            set { _nombre_jugador = value; }
        }

        public int montoDinero
        {
            get { return _monto_dinero; }
            set { _monto_dinero = value; }
        }

        public override string ToString()
        {
            return $"Jugador: {_id_jugador} \n Nombre: {_nombre_jugador} \n Su dinero es: {_monto_dinero}\n";
        }
    }
}