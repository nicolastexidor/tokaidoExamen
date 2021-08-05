using System;

namespace Library
{
    /*
    Esta interfaz permite el cumplimiento de OCP para asi poder agregar nuevos paisajes sin modificar el codigo ya escrito.
    Por otro lado, gracias a esta interfaz, se pudo cumplir ISP, pudiendo crear tanto clases que son unicamente de tipo IExperiencia
    y tambien clases que son de tipo IExperiencia e IPaisaje al mismo tiempo, de esta forma no se hace necesario forzar a ciertas clases a depender de tipos que no usan.

    Asimismo, cumple con SRP al ser su única razon de cambio la forma de otorgar los puntos.
    */
    public interface IPaisaje
    {
        /// <summary>
        /// Dado un viajero, se le otorgan puntos en relación a la cantidad de veces que haya pasado por la experiencia misma.
        /// </summary>
        /// <param name="v"></param>
        void paisajePoints(IViajero v);
    }
}