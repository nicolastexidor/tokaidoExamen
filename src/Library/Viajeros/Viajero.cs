using System;
using System.Collections.Generic;

namespace Library
{
    public class Viajero : IViajero
    {
        public Viajero(string name)
        {
            this.Name = name;
            this.Points = 0;
            this.Coins = 1;
            this.BonusName = "AguasTermales";
            this.ExperienciasPasadas = new List<IExperiencia>();
        }


    }
}