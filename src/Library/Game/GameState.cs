using System;
using System.Collections.Generic;

namespace Library
{
    /*
    Esta clase permite el cumplimiento de SRP siendo asi su única razón de cambio el modificar como funciona el sistema de movimientos.
    Asimismo, cumple con expert ya que tiene toda la información necesaria para cada una de las funciones.
    Por último permite el cumplimiento de DIP al depender de la abstracción IWinCondition en lugar de una clase de bajo nivel
    de esta forma pudiendo implementar cambios en dicha interfaz sin tener que hacer ningún cambio en esta clase.
    */
    public class GameState
    {
        public List<IExperiencia> ExperienciasTotales{get;private set;}
        private IWinCondition WinCondition{get;}
        public GameState(Board tablero, IWinCondition condition)
        { 
            this.ExperienciasTotales = tablero.ExperienciasTablero;
            this.WinCondition = condition;
        }
        /// <summary>
        /// Devuelve la lista de todos los viajeros que hay en el juego. 
        /// </summary>
        public List<IViajero> viajerosTotales()
        {
            List<IViajero> resultado = new List<IViajero>();
            foreach(IExperiencia experiencia in ExperienciasTotales)
            {
                foreach(IViajero viajero in experiencia.Viajeros)
                {
                    resultado.Add(viajero);
                }
            }
            return resultado;
        }
        /// <summary>
        /// Devuelve el viajero que tiene el turno para moverse.
        /// </summary>
        public IViajero turno()
        {
            foreach (IExperiencia experiencia in ExperienciasTotales)
            {
                if(experiencia.Viajeros.Count >0)
                {
                    return experiencia.Viajeros[0];
                }
            }
            throw new InvalidArgumentException(message: "El juego no ha comenzado");  
        }

        /// <summary>
        /// Dado un viajero y un número de casillas, dicho viajero avanza el número de casillas dadas, solo si es su turno
        /// y el número de casillas es mayor a cero y menor que el número de casillas restantes hasta que el viajero llegue a la meta.
        /// </summary>
        /// <param name="viajero"></param>
        /// <param name="casillasAMoverse"></param>
        public void aplicarMovimiento(IViajero viajero, int casillasAMoverse)
        {
            try{
            int contador = -1;
            IExperiencia lastExperience = viajero.ExperienciasPasadas[viajero.ExperienciasPasadas.Count-1];
            if (this.turno() == viajero && !isFinished() && casillasAMoverse > 0)
            {
                //Se recorre cada experiencia del tablero
                foreach (IExperiencia experiencia in ExperienciasTotales)
                {
                    //el contador aumenta para saber en que posicion se encuentra cada experiencia.
                    contador+=1;
                    //Se checkea si la experiencia en el tablero es en la que se encuentra el viajero
                    if(experiencia.Equals(lastExperience))
                    {
                        //Se checkea si el viajero puede agregarse a la experiencia destino, haciendolo en caso que se pueda.
                        if (ExperienciasTotales[contador + casillasAMoverse].addViajero(viajero))
                        {
                            //Si se pudo mover el viajero, lo quita de la experiencia en la que se encuentra
                            experiencia.removeViajero();
                        }else
                        {
                            //En caso de que no se haya podido mover, se llama a la misma función pero para la casilla siguiente.
                            aplicarMovimiento(viajero, casillasAMoverse+1);
                        }
                    }
                }
            }else
            {
                throw new InvalidArgumentException(message: "Argumentos dados incorrectos o juego finalizado");
            }
            }catch(ArgumentOutOfRangeException)
            {
                throw new IndexOutOfRangeException(message: "Ingrese un número de casillas a moverse distinto, ya que el actual se va de rango");
            }
        }
        /// <summary>
        /// Checkea si el juego ha terminado
        /// </summary>
        /// <returns>
        /// <c>true<c> en caso de que el juego haya terminado, <c>false<c> en caso contrario.
        /// </returns>
        public bool isFinished()
        {
            IExperiencia experienciaFinal = ExperienciasTotales[ExperienciasTotales.Count - 1];
            return experienciaFinal.Viajeros.Count == this.viajerosTotales().Count;
            
        }

        /// <summary>
        /// Dependiendo de la condición de victoria, devuelve un string anunciando al ganador
        /// </summary>
        /// Gracias a la interfaz IWinCondition se pudo cumplir Polimorfismo, 
        /// teniendo diferentes resultados dependiendo de la implementación de dicha interfaz que se use sin necesidad de preguntar por el tipo
        /// permitiendo crear nuevas implementaciones sin modificar esta función.
        public string finishGame()
        {
            if(this.isFinished())
            {
                IViajero winner = WinCondition.Winner(this.viajerosTotales());
                return "El ganador de la partida es "+winner.Name;
            }else
            {
                return "La partida aún no ha terminado";
            }
        }
        /// <summary>
        /// Dada una lista de viajeros, que serán los jugadores de la partida, inicia dicha partida 
        /// agregando los viajeros a la experiencia inicial.
        /// </summary>
        /// <param name="viajeros"></param>
        public void startGame(List<IViajero> viajeros)
        {
            if(this.viajerosTotales().Count == 0 && viajeros.Count >= 2)
            {
                foreach(IViajero viajero in viajeros)
                {
                    if (!(ExperienciasTotales[0].Viajeros.Contains(viajero))){
                        ExperienciasTotales[0].addViajero(viajero);
                    }
                }
            }else{
                throw new InvalidArgumentException(message: "La partida ya estaba comenzada o has intentado iniciarla con menos de 2 jugadores");
            }
        }
    }
}