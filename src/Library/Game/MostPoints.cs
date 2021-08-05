using System;
using System.Collections.Generic;

namespace Library
{
    public class MostPoints: IWinCondition
    {
        /// <summary>
        /// Dada una lista de viajeros, devuelve cuál de ellos es el ganador basado en cuál tiene más puntos,
        /// si hay dos o más viajeros con la misma cantidad de puntos, se devolverá el que se encuentre antes en la lista.
        /// </summary>
        /// <param name="viajeros"></param>
        /// <returns></returns>
        public IViajero Winner(List<IViajero> viajeros)
        {
            IViajero ganador = viajeros[0];
            int contador=0;
            foreach (IViajero v in viajeros)
            {
                if(v.Points > contador)
                {
                    contador = v.Points;
                    ganador = v;
                }
            }
            return ganador;

        }
    }
}