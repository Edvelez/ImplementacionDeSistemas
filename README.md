# ImplementacionDeSistemas

Juego de Briscas

1. Las cartas seran un arreglo de enteros de tamaño 40 de los numeros desde el 0 hasta el 39 ejemplo [0|1|2|...|37|38|39]
2. De 0-9 seran Oro, 10-19 seran espada, 20-29 seran copa, 30-39 seran basto.
3. Se definiran constantes ORO, ESPADA, COPA y BASTO que seran igual a 0, 1, 2 y 3 respectivamente.
4. Los numeros desde el 0-6 represantaran las cartas desde el 1-7 de manera que si el numero es el 2 este representa la carta numero 2 + 1 = 3.
5. Los numeros 7, 8 y 9 representan las cartas 10, 11 y 12 de las briscas.
6. Para saber el numero y tipo de carta se considerara el cociente y el residuo del numero cuando es dividido entre 10, ejemplo el numero 27. Al utilizar divicion de enteros y dividirlo entre 10 (27/10) obtenemos como resultado 2 que es la constante para copa. Al tomar el reciduo (27%10) obtenemos 7 que representa el 10 en las briscas de modo que el numero 27 representa el 10 de copa.
