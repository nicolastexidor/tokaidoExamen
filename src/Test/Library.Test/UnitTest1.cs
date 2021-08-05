using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Library.Test
{
    public class Tests
    {
        private ViajeroGranjero viajero1;
        private Viajero viajero2;
        private List<IViajero> listaViajeros;
        private AguasTermales aguasTermales;
        private Granja granja;
        private Inicio inicio;
        private Final final;
        private Montaña montaña;
        private Oceano oceano;
        private Board board;
        private GameState estadoDeJuego;
        private MostPoints mostPoints;
        private MostCoins mostCoins;

        [SetUp]
        public void Setup()
        {
            viajero1 = new ViajeroGranjero("Pepe");
            viajero2 = new Viajero("Carlos");
            listaViajeros = new List<IViajero>();
            listaViajeros.Add(viajero1);
            listaViajeros.Add(viajero2);
            aguasTermales = new AguasTermales(10,10,1);
            granja = new Granja(2,1,2);
            inicio = new Inicio();
            final = new Final();
            montaña = new Montaña(0,0,3);
            oceano = new Oceano(0,0,2);
            List<IExperiencia> listaTablero = new List<IExperiencia>();
            listaTablero.Add(inicio);
            listaTablero.Add(aguasTermales);
            listaTablero.Add(granja);
            listaTablero.Add(montaña);
            listaTablero.Add(oceano);
            listaTablero.Add(final);
            
            board = new Board(listaTablero);
            mostPoints = new MostPoints();
            estadoDeJuego = new GameState(board,mostPoints);

        }

        [Test]
        public void entregaDePuntosTest()
        {
            aguasTermales.addViajero(viajero1);
            Assert.AreEqual(10, viajero1.Points);
        }

        [Test]
        public void entregaDeCoinsTest()
        {
            aguasTermales.addViajero(viajero1);
            Assert.AreEqual(12, viajero1.Coins);
        }
        [Test]
        public void entregaDePuntosNegativosTest()
        {
            IExperiencia aguasTermales2 = new AguasTermales(-10,-10,1);
            Assert.Throws(typeof(NegativePointsOrCoinsException),delegate {aguasTermales2.addViajero(viajero1);});
        }

        [Test]
        public void vecesPasadaTest()
        {
            viajero1.ExperienciasPasadas.Add(montaña);
            viajero1.ExperienciasPasadas.Add(montaña);
            viajero1.ExperienciasPasadas.Add(montaña);
            viajero1.ExperienciasPasadas.Add(montaña);

            Assert.AreEqual(viajero1.vecesPasada(montaña), 4);
        }
        [Test]
        public void paisajePointsTest()
        {
            viajero1.ExperienciasPasadas.Add(montaña);
            viajero1.ExperienciasPasadas.Add(montaña);
            montaña.addViajero(viajero1);
            Assert.AreEqual(viajero1.Points, 3);
        }
        [Test]
        public void paisajePointsSinPasar()
        {
            montaña.addViajero(viajero1);
            Assert.AreEqual(viajero1.Points,0);
        }
        [Test]
        public void bonusPointsTest()
        {
            aguasTermales.addViajero(viajero2);
            Assert.AreEqual(viajero2.Points, 20);
        }
        [Test]
        public void removeViajeroTest()
        {
            montaña.addViajero(viajero1);
            montaña.removeViajero();
            Assert.IsEmpty(montaña.Viajeros);
        }
        [Test]
        public void removeViajeroVacioTest()
        {
            Assert.Throws(typeof(EmptyExperienciaException),delegate {montaña.removeViajero();});
        }
        [Test]
        public void viajeroRepetido()
        {
            montaña.addViajero(viajero1);
            montaña.addViajero(viajero1);
            Assert.AreEqual(montaña.Viajeros.Count, 1);
        }
        [Test]
        public void cantMaxViajerosTest()
        {
            aguasTermales.addViajero(viajero1);
            aguasTermales.addViajero(viajero2);
            Assert.AreEqual(aguasTermales.Viajeros.Count, 1);
        }
        [Test]
        public void boardCreationTest()
        {
            Assert.IsNotEmpty(board.ExperienciasTablero);
        }
        [Test]
        public void invalidBoardCreationTest()
        {
            List<IExperiencia> listaExperiencias = new List<IExperiencia>();
            listaExperiencias.Add(montaña);
            listaExperiencias.Add(inicio);
            listaExperiencias.Add(aguasTermales);
            listaExperiencias.Add(final);
            listaExperiencias.Add(oceano);
             Assert.Throws(typeof(InvalidBoardFormatException),delegate {new Board(listaExperiencias);});
        }
        [Test]
        public void addExperienciaBoardTest()
        {
            IExperiencia granja2 = new Granja(0,0,0);
            board.addExperiencia(granja2);
            Assert.AreEqual(board.ExperienciasTablero[board.ExperienciasTablero.Count-2],granja2);
        }
        [Test]
        public void removeExperienciaBoardTest()
        {
            board.removeExperiencia(montaña);
            int contador = 0;
            foreach(IExperiencia experiencia in board.ExperienciasTablero)
            {
                if(experiencia == montaña)
                {
                    contador+=1;
                }
            }
            Assert.AreEqual(0,contador);
        }
        [Test]
        public void addExperienciaInicioTest()
        {
            Assert.Throws(typeof(InvalidBoardFormatException),delegate {board.addExperiencia(inicio);});
        }
        [Test]
        public void removeExperienciaInicioTest()
        {
            Assert.Throws(typeof(InvalidBoardFormatException),delegate {board.removeExperiencia(inicio);}); 
        }
        [Test]
        public void removeExperienciaNoExistente()
        {
            List<IExperiencia> tableroAntes = board.ExperienciasTablero; 
            IExperiencia expPrueba = new Granja(0,0,0);
            board.removeExperiencia(expPrueba);
            Assert.AreEqual(tableroAntes, board.ExperienciasTablero);
        }

        [Test]
        public void startGameTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            Assert.AreEqual(estadoDeJuego.ExperienciasTotales[0].Viajeros, listaViajeros);
        }
        [Test]
        public void startGameRepetido()
        {
            List<IViajero> listaViajeros2 = new List<IViajero>();
            listaViajeros2.Add(viajero1);
            listaViajeros2.Add(viajero1);
            listaViajeros2.Add(viajero1);
            listaViajeros2.Add(viajero2);
            estadoDeJuego.startGame(listaViajeros2);
            Assert.AreEqual(estadoDeJuego.ExperienciasTotales[0].Viajeros.Count, 2);
        }
        [Test]
        public void startGameOnePlayerTest()
        {
            List<IViajero> listaViajeros2 = new List<IViajero>();
            listaViajeros2.Add(viajero1);
            Assert.Throws(typeof(InvalidArgumentException),delegate {estadoDeJuego.startGame(listaViajeros2);}); 
        }
        [Test]
        public void turnoTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            Assert.AreEqual(estadoDeJuego.turno(), viajero1);
        }
        [Test]
        public void invalidTurnTest()
        {
            Assert.Throws(typeof(InvalidArgumentException),delegate {estadoDeJuego.turno();}); 
        }
        [Test]
        public void viajerosTotalesTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            Assert.AreEqual(estadoDeJuego.viajerosTotales(), listaViajeros);
        }
        [Test]
        public void moveTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            estadoDeJuego.aplicarMovimiento(viajero1,1);
            Assert.AreEqual(aguasTermales.Viajeros[0], viajero1);
        }
        [Test]
        public void dosMovimientosTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            estadoDeJuego.aplicarMovimiento(viajero1,2);
            estadoDeJuego.aplicarMovimiento(viajero2,2);
            Assert.AreEqual(granja.Viajeros, listaViajeros);
        }
        [Test]
        public void movimientoMuyGrandeTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            Assert.Throws(typeof(IndexOutOfRangeException),delegate {estadoDeJuego.aplicarMovimiento(viajero1,50);});
        }
        [Test]
        public void moverseSinTurno()
        {  
            estadoDeJuego.startGame(listaViajeros);
            Assert.Throws(typeof(InvalidArgumentException),delegate {estadoDeJuego.aplicarMovimiento(viajero2,1);});
        }
        [Test]
        public void moverseHaciaAtrasTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            Assert.Throws(typeof(InvalidArgumentException),delegate {estadoDeJuego.aplicarMovimiento(viajero1,-1);});
        }
        [Test]
        public void isFinishedTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            estadoDeJuego.aplicarMovimiento(viajero1,5);
            estadoDeJuego.aplicarMovimiento(viajero2,5);
            Assert.True(estadoDeJuego.isFinished());
        }
        [Test]
        public void unFinishedGameTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            Assert.AreEqual("La partida aún no ha terminado",estadoDeJuego.finishGame());
        }
        [Test]
        public void finishedGameTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            estadoDeJuego.aplicarMovimiento(viajero1,5);
            estadoDeJuego.aplicarMovimiento(viajero2,5);
            Assert.AreEqual("El ganador de la partida es "+viajero1.Name,estadoDeJuego.finishGame());
        }
        [Test]
        public void mostPointsWinnerTest()
        {
            estadoDeJuego.startGame(listaViajeros);
            estadoDeJuego.aplicarMovimiento(viajero1,1);
            estadoDeJuego.aplicarMovimiento(viajero2,5);
            estadoDeJuego.aplicarMovimiento(viajero1,2);
            estadoDeJuego.aplicarMovimiento(viajero1,2);
            Assert.AreEqual("El ganador de la partida es "+viajero1.Name,estadoDeJuego.finishGame());
        }
        
    }
}