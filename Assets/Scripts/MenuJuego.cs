using UnityEngine;

public class MenuJuego : MonoBehaviour
{
    //Hacemos mención a la clase llamada "Movimiento" y a su hijo "scriptMovimiento" para así poder modificar sus valores más adelante.
    public Movimiento scriptMovimiento;
    //Llamamos también al objeto panel, para hacer que se desactive al iniciar el juego.
    public GameObject panel;

    public void Jugar()
    {
        //Se oculta el panel y se pone el tiempo a 1 (inicia el juego).
        panel.SetActive(false);
        Time.timeScale = 1;
        //Se establecen los valores de las variables: tiradas, puntuación y nº de bolos caídos a 0 (se resetean cada vez que inicia el juego).
        scriptMovimiento.contadorTiradas = 0;
        scriptMovimiento.contadorPuntuacion = 0;
        scriptMovimiento.bolosCaidosEstaTirada = 0;
        //Hacemos lo mismo para los textos de estas variables (se resetean los textos visualmente).
        scriptMovimiento.tiradas.text = "Tiradas: 0";
        scriptMovimiento.puntuacion.text = "Puntuación: 0";
        //También reseteamos las variables "yaLanzada" y "reseteando", ya que estamos iniciando el juego desde 0.
        scriptMovimiento.yaLanzada = false;
        scriptMovimiento.reseteando = false;
        /*Además, hacemos una comprobación, si el mensaje que sale en mitad de pantalla, indicando al jugador
        cómo debe activar la bola, es nulo/está vacío, lo hacemos visible.*/
        if (scriptMovimiento.mensajeDisparo != null)
            scriptMovimiento.mensajeDisparo.SetActive(true);
    }
    /*Creamos una función llamada "Salir()", que indica por un mensaje en consola que se está saliendo del juego (testeo)
    y mediante el comando "Application.Quit()", se cierra la aplicación*/
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}