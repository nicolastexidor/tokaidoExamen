using System;
using System.Collections.Generic;

namespace Library
{
    /*
    Esta clase abstracta permite el cumplimiento de OCP para de esta forma permitir crear nuevos viajeros sin cambiar el código antes escrito
    y al mismo tiempo reutlizar el código ya escrito para no repetirse a uno mismo.
    También permite el cumplimiento de Expert, ya que que es el que tiene la información para realizar todas sus responsabilidades.
    */
    public abstract class IViajero
    {
        public string Name{get;protected set;}
        public int Points{get;protected set;}
        public int Coins{get;protected set;}
        /// <summary>
        /// Nombre de la experiencia de la cual recibirá un bonus cuando pase por ella.
        /// </summary>
        /// <value></value>
        public string BonusName{get;protected set;}
        public List<IExperiencia> ExperienciasPasadas{get;protected set;}
        /// <summary>
        /// Dada una cantidad de monedas, las suma a la cantidad de monedas totales.
        /// </summary>
        /// <param name="monedas"></param>
        public void obtainCoins(int monedas)
        {
            if(monedas >= 0){
            this.Coins += monedas;
            }else
            {
                throw new NegativePointsOrCoinsException(message: "No se puede quitar monedas");
            }
        }
        /// <summary>
        /// Dada una cantidad de puntos, los suma a la cantidad de puntos totales.
        /// </summary>
        /// <param name="puntos"></param>
        public void obtainPoints(int puntos)
        {
            if(Points >= 0){
            this.Points += puntos;
            }else
            {
                throw new NegativePointsOrCoinsException(message: "No se puede quitar puntos");
            }
        }
        /// <summary>
        /// Dada una experiencia, duevuelve la cantidad de veces que el viajero ha pasado por una del mismo tipo.
        /// </summary>
        /// <param name="experiencia"></param>
        /// <returns></returns>
        public int vecesPasada(IExperiencia experiencia)
        {
            int resultado = 0;
            foreach (IExperiencia exp in ExperienciasPasadas)
            {
                if(experiencia.Equals(exp))
                {
                    resultado += 1;
                }
            }
            return resultado;
        }
        

    }
}
