using System;
using System.Collections.Generic;

namespace Library
{
    /*
    La clase Board permite el cumplimiento de SRP, siendo su única responsabilidad el modificar la lista de experiencias, 
    asi como tambien es la Experta en información de dicha responsabilidad, permitiendo asi realizar un cambio únicamente 
    en esta clase si se quiere modifica dicha responsabilidad.
    */
    public class Board
    {
        public List<IExperiencia> ExperienciasTablero{get;private set;}
        /// <summary>
        /// Dada una lista de experiencias, se checkea que dicha lista tenga como primer elemento un Inicio y como último un Final
        /// y se crea la instancia de Board.
        /// </summary>
        /// <param name="experienciasTablero"></param>
        public Board(List<IExperiencia> experienciasTablero)
        { 
            if(experienciasTablero[0] is Inicio && experienciasTablero[experienciasTablero.Count - 1] is Final )
            {
                this.ExperienciasTablero = experienciasTablero;
            }
            else
            {
                throw new InvalidBoardFormatException("El tablero de juego no tiene el inicio o el final en sus respectivos lugares");
            }
        }

        public void addExperiencia(IExperiencia experiencia)
        {
            if (!(experiencia.Name == "Final" || experiencia.Name == "Inicio"))
            {
                ExperienciasTablero.Insert(ExperienciasTablero.Count -1, experiencia);
            }else
            {
                throw new InvalidBoardFormatException(message: "No puedes agregar un inicio o un final distinto a los proporcionados al crearse el tablero");
            } 
        }

        public void removeExperiencia(IExperiencia experiencia)
        {
            if (!(experiencia.Name == "Final" || experiencia.Name == "Inicio"))
            {
                ExperienciasTablero.Remove(experiencia);
            }else
            {
                throw new InvalidBoardFormatException(message: "No puedes quitar el final o el inicio del tablero");
            }
        }
        
    }
}