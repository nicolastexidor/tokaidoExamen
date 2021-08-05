using System;
using System.Collections.Generic;
namespace Library
{
    public class AguasTermales : IExperiencia
    {
        public AguasTermales(int monedas, int puntos, int cantMaxViajeros)
        {
            this.Name = "AguasTermales";
            this.MonedasOtorgadas = monedas;
            this.PuntosOtorgados = puntos;
            this.CantMaxViajeros = cantMaxViajeros;
            this.Viajeros = new List<IViajero>();
        }

        public string Name{get;}
        public int MonedasOtorgadas{get;}

        public int PuntosOtorgados{get;}

        public int CantMaxViajeros{get;}

        public List<IViajero> Viajeros{get;}
        public bool addViajero(IViajero viajero)
        {
            if (this.Viajeros.Count < CantMaxViajeros && !(Viajeros.Contains(viajero)))
            {
                Viajeros.Add(viajero);
                this.otorgarPuntosyMonedas(viajero);
                viajero.ExperienciasPasadas.Add(this);
                if(viajero.BonusName == this.Name)
                {
                    this.otorgarPuntosyMonedas(viajero);
                }
                return true;
            }
            return false;
        }
        public void removeViajero()
        {
            if(Viajeros.Count > 0)
            {
                Viajeros.RemoveAt(0);
            }else
            {
                throw new EmptyExperienciaException(message: "Esta experiencia no tiene viajeros");
            }
        }
        public void otorgarPuntosyMonedas(IViajero viajero)
        {
            viajero.obtainCoins(this.MonedasOtorgadas);
            viajero.obtainPoints(this.PuntosOtorgados);
        }

    }
}