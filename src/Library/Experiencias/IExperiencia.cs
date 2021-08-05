using System;
using System.Collections.Generic;

namespace Library
{
    /*
    Esta interfaz permite el cumplimiento de OCP, pudiendo crear nuevas experiencias sin modificar el codigo ya escrito.
    Asimismo, cumple con Expert siendo la experta en información de la cantidad de puntos que se deben otorgar y del viajero que debe recibirlos.

    Soy consciente de que no cumple SRP ya que tiene más de una razón de cambio, pero bajo mi criterio vale la pena no cumplirlo para poder tener
    un código más compacto y organizado.
    */
    public interface IExperiencia
    {
        string Name{get;}
        int MonedasOtorgadas{get;}
        int PuntosOtorgados{get;}
        int CantMaxViajeros{get;}
        List<IViajero> Viajeros{get;}
        void otorgarPuntosyMonedas(IViajero v);
        /// <summary>
        /// Dado un viajero, lo agrega a la lista de Viajeros y al mismo tiempo le otorga los puntos.
        /// En algunos casos se le aplicará también un bonus dependiendo de si el viajero debe reibirlo.
        /// </summary>
        /// <returns>
        /// <c>true<c> en caso de que se haya podido agregar al viajero, <c>false<c> en caso contrario.
        /// </returns>
        /// <param name="v"></param>
        bool addViajero(IViajero v);
        
        /// <summary>
        /// Elimina el viajero en la primera posición de la experiencia, usualmente es el que tiene el turno para moverse,
        /// por lo que la función no será llamada a menos que sea para mover a dicho viajero.
        /// </summary>
        void removeViajero();
    }

}