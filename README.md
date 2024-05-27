# Galaxies and Candies (Nombre preliminar)

El videojuego se enmarca en el desarrollo de mi memoria titulada "Prototipado de intervenciones mediadas en realidad virtual para controlar el manejo del dolor agudo en pacientes con secuelas de quemaduras", se pretende buscar alternativas efectivas para reducir la percepción de dolor en pacientes de COANIQUEM que han sufrido quemaduras.
Se opta por usar tecnologías VR, las cuales han sido estudiadas previamente con resultados satisfactorios. Al presentarse la necesidad de incluir a personas con quemaduras en manos y brazos, se piensa en la solución usando el casco Meta Quest Pro, que posee eye-tracking y se crean dos minijuegos simples para probar la efectividad del software.

## Estado del proyecto

En este momento el videojuego cuenta con un menú inicial, que permite usar eye-tracking para interactuar con los botones y elegir el minijuego en cuestión. En este momento (se actualizará el README) se cuenta con una version simplificada del primer minijuego, que consiste en atrapar portales y esquivar meteoritos que se acercan hacia el jugador.
Se debe mirar en la dirección del portal y no al meteorito, acumulando puntos y un combo, que aumenta la velocidad de los elementos en pantalla y acelera la música.

## WIP

Se está desarrollando el segundo minijuego, motivado por el popular Fruit Ninja, existirán frutas y dulces que aparecerán desde diferentes direcciones de la pantalla y el jugador deberá mirar a las frutas por una cantidad X de tiempo para obtener puntos, si mira los dulces este perderá una vida.

## Configuración previa

Para lanzar el juego se necesita contar con el casco Meta Quest Pro, con detección de ojos activada (obligatoria) y calibrada con tus ojos (opcional) para la mejor experiencia de juego. Es necesario que, durante la experiencia, el casco esté bien colocado y en una habitación con buena iluminación.

## Instalación y lanzamiento

Para instalar el juego en el casco se hace uso de la herramienta [Sidequest](https://sidequestvr.com/setup-howto). Es necesario vincular el casco a Sidequest siguiendo la guía en la parte superior izquierda de la app, luego de la vinculación se selecciona la opción de "Install APK file from folder on computer" y se elige la APK en este proyecto.

Finalmente, es necesario mostrar las apps de origen desconocido en el casco, donde aparecerá el juego seleccionable.

## Bugs conocidos y trabajo a futuro

El primer minijuego no posee la precisión suficiente con la detección de los ojos, la cual podría ser mejorada cambiando el objeto con el que chocan los rayos emitidos desde los ojos. La idea sería cambiar el plano por una pantalla curva, similar a un lente, para ajustar la posición del Player de manera más precisa. 
