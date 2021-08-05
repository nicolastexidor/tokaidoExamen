using System;
using System.Collections.Generic;

namespace Library
{
    /*
    Esta interfaz permte el cumplimiento de OCP pudiendo asi crear nuevas condiciones de victoria sin tener que modificar el codigo ya escrito.
    Por otro lado también se cumple SRP para sus implementaciones, siendo la única razón de cambio la forma de elegir al ganador.
    */
    public interface IWinCondition
    {
        IViajero Winner(List<IViajero> viajeros);
    }
}