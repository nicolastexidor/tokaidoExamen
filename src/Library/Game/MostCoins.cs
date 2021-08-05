using System;
using System.Collections.Generic;

namespace Library
{
    public class MostCoins: IWinCondition
    {
        /// <summary>
        /// Dada una lista de viajeros, devuelve cuál de ellos es el ganador basado en cuál tiene más monedas,
        /// si hay dos o más viajeros con la misma cantidad de monedas, se devolverá el que se encuentre antes en la lista.
        /// </summary>
        /// <param name="viajeros"></param>
        /// <returns></returns>
        public IViajero Winner(List<IViajero> viajeros)
        {
            IViajero ganador = viajeros[0];
            int contador=0;
            foreach (IViajero v in viajeros)
            {
                if(v.Coins > contador)
                {
                    contador = v.Coins;
                    ganador = v;
                }
            }
            return ganador;

        }
    }
}