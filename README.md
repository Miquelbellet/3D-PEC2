# PAC2 - First Person Shooter

Esta PAC2 se basa en la creación de un videojuego en primera persona en el que el jugador puede escoger entre dos armas, hay enemigos por todo el mapa y se tiene que llegar hasta un punto final para superar el mapa.

## Desarrollo del juego

### Pantallas
El juego consta de dos pantallas, una de Menú para poder seleccionar el mapa que se quiere jugar o salir del juego, i otra que es la principal dónde se produce toda la acción del mapa.


### Construcción del terreno
Al haber dos mapas creados para poder jugar, se han creado dos escenarios distintos en los que predominan los pasadizos simulando una nave espacial o una construcción futurista en el espacio.

### Tipos de arma
El jugador puede escoger entre dos tipos de arma. La principal es una AK-47, ametralladora de larga distancia y rápida cadencia, pero poco daño y el arma secundaria es una pistola de gran daño, pero menos alcance, menos cadencia y menos munición.

### Vida y Escudo
El jugador tiene un nivel de vida y escudo. Cuando un enemigo dispara al jugador, tiene un 75% de posibilidades de que le dé la bala y cuando le da la bala, el daño lo absorbe en un 80% el escudo y 20% la vida. En caso de no tener escudo le restaría todo a la vida.

### HUD
- En la pantalla podemos ver la mira donde el jugador está apuntando, que siempre es el centro dela pantalla.
- La barra de escudo en color marrón, ya que es el color de la caja de escudo que se puede recoger.
- La barra de vida en color verde, que marca la vida que le queda restante.
- El tipo de munición que estás disparando y cuanta le queda al jugador de el arma seleccionada. Las imágenes de balas que aparecen indican el tipo de arma que tiene seleccionada y cuanta munición le queda en el cargador.
- Número de llaves disponibles para abrir puertas.
- Texto de aviso de recarga cuando queda poca munición en el cargador.
- Texto cuando se está recargando el arma.

### Plataformas Móviles
Se han implementado plataformas móviles que se mueven tanto horizontalmente como verticalmente para poder cambiar de escenario y de nivel.

### Puertas y llaves
En el juego hay múltiples puertas que dificultan completar el mapa para el jugador. Estas puertas se abren con llaves que se consiguen destruyendo a enemigos, cada vez que destruyes a un enemigo tienes un 50% de conseguir una llave. El jugador puede llevar un máximo de tres llaves encima.

### Enemigos
Solo hay un tipo de enemigos, un dron que patrulla zonas concretas del mapa. Si el jugador entra en el rango del enemigo, este dará una vuelta de 360 grados buscando al jugador y si no lo encuentra seguirá patrullando. Si lo encuentra o si el jugador lo dispara, este pasará a disparar al jugador.

### Items
Existen tres tipos de items que se pueden recoger del suelo, uno que te da más vida, uno que se da más escudo y uno de munición que te da más balas de el arma que tienes en las manos.
Cada vez que eliminas a un enemigo, dejará caer una de estas tres cajas que podrá recoger el jugador.

### Sonidos
Se han implementado sonidos a todas las acciones que pasan durante el juego. Se han utilizado algunos de los sonidos incorporados en el template del asset "SoldierSoundsPack" pero la mayoría se han sacado de la web "https://freesound.org".

## Vídeo
