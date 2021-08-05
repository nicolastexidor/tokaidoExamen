using System;
using System.Collections.Generic;

namespace Library
{
    public class ViajeroGranjero : IViajero
    {
        public ViajeroGranjero(string name)
        {
            this.Name = name;
            this.Points = 0;
            this.Coins = 2;
            this.BonusName = "Granja";
            this.ExperienciasPasadas = new List<IExperiencia>();
        }


    }
}