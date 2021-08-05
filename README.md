# tokaidoExamen

Decisiones de diseño:
-Las partidas tienen como máximo 4 jugadores y como mínimo 2.
-Los viajeros solo pueden obtener tanto monedas como puntos.
-Cada viajero tiene un nombre de un bonus que refiere al nombre de la experiencia de la cual recibe el bonus
-Dicho bonus es siempre obtener el doble de puntos de lo que da la experiencia.
-El tablero (Board) se crea antes de inciar el juego y no puede ser cambiado durante el juego.
-Se puede crear cualquier condicion de victoria, pero siempre debe haber un y solo un ganador.
-Un viajero no puede perder puntos o monedas.
-Para finalizar un juego se debe llamar a la funcion isFinished de GameState
-Pueden haber tantas experiencias como se quiera en el tablero, pero siempre debe iniciar con un Inicio y Finalizar con un Final.
-Todas las experiencias se pueden crear de forma que de tantos puntos y/o monedas como se quiera.

Problemas de diseño de código:
-Se me generó una doble dependencia entre IExperiencia e IViajero donde ambos tienen una lista compuesta del otro
esto se debe a que los viajeros deben tener una lista que diga por donde pasaron, y las experiencias deben tener una lista
que contenga todos los viajeros que se encuentren en dicha experiencia.
Creo que si o si los viajeros deben tener una lista de por donde pasaron, pero quizá no sea necesario que las experiencias
tengan una lista de los viajeros, pero cuando me di cuenta ya estaba por terminar de hacer el código asi que no tuve tiempo de cambiarlo.

-El BonusName de IViajero debería ser una IExperiencia en lugar de un string, pero si lo hacia de esa forma se habría incumplido el patrón Creator
al tener que crearle una IExperiencia a cada instancia de IViajero.  
