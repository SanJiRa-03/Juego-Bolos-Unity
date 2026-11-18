using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movimiento : MonoBehaviour
{
    [Header("Bola")]
    public Rigidbody rbBola; //Llamamos al rigidbody de la bola de bolos para luego arrastrar el objeto a este campo.
    public float velocidadLateral = 10f; //le imponemos a la bola una velocidad a la hora de moverla lateralmente y una fuerza que usaremos a la hora de impulsarla.
    public float fuerza = 40f;

    [Header("Sonidos")] 
    public AudioSource altavoz; //Hacemos referencia al AudioSource al que denominamos "altavoz" para reproducir los clips más adelante.
    public AudioClip sonidoLanzar, sonidoChoque;

    [Header("Bolos")]
    public GameObject[] bolos; //Creamos un array llamado bolos y un array de variables booleanas para comprobar más adelante si el bolo ha sido contado o no para luego sumar los puntos.
    public bool[] boloYaContado;

    [Header("HUD")]
    public TMP_Text tiradas, puntuacion; //Creamos las variables de tipo TextMesh Pro para modificarlas más adelante.
    public int contadorTiradas = 0, contadorPuntuacion = 0;
    public int bolosCaidosEstaTirada = 0;

    [Header("Recogedor")]
    public GameObject recogedor; //Llamamos al objeto "recogedor" para más adelante modificar su eje Y y moverlo simulando un recogedor funcional real.
    public float alturaBaja = 0f;
    public float velocidadRecogedor = 2f;
    public Vector3 posOriginalRecogedor;

    [Header("Interfaz")]
    public GameObject panel; //Llamamos también al objeto panel que muestra el Canvas inicial (Jugar o Salir).
    public GameObject mensajeDisparo;
    //Listas dinámicas para almacenar la posición (Vector3) y rotación (Quaternion) de cada bolo.
    //Se usan listas porque no sabemos de antemano cuántos bolos habrá en el array.
    public List<Vector3> posIniciales = new List<Vector3>();
    public List<Quaternion> rotIniciales = new List<Quaternion>();
    public Vector3 bolaPosOrigen; //Guarda la posición de "spawn" de la bola para devolverla ahí tras cada tirada.
    public bool yaLanzada = false, reseteando = false; //Para saber si la bola está en pista o si el juego está recolocando bolos.

    void Start()
    {
        //Registramos las posiciones de origen de la bola y del brazo recogedor.
        bolaPosOrigen = rbBola.position;
        posOriginalRecogedor = recogedor.transform.position;
        //Inicializamos el array de control de puntos con el mismo tamaño que el de los bolos.
        boloYaContado = new bool[bolos.Length];
        //Bucle "for": recorre cada bolo para guardar su estado inicial y "dormir" su física.
        for (int i = 0; i < bolos.Length; i++)
        {
            posIniciales.Add(bolos[i].transform.position);
            rotIniciales.Add(bolos[i].transform.rotation);
            bolos[i].GetComponent<Rigidbody>().Sleep();
        }
        //Pausamos el juego y activamos el panel inicial:
        if (panel != null)
        {
            panel.SetActive(true);
            Time.timeScale = 0;            
        }
        //Si el mensaje que se ve en pantalla para indicar al jugador cómo tirar la bola, no está iniciado, lo hacemos visibles.
        if (mensajeDisparo != null) mensajeDisparo.SetActive(true);
    }

    void Update()
    {
        //Si el tiempo está en 0 (pausa), ignoramos todo lo que viene debajo.
        if (Time.timeScale == 0) return;
        //De lo contrario, solo si la bola no ha sido lanzada y no estamos reseteando, movemos la bola horizontalmente.
        if (!yaLanzada && !reseteando)
        {
            float moverH = Input.GetAxis("Horizontal");
            rbBola.linearVelocity = new Vector3(moverH * velocidadLateral, 0, 0);
            //Disparamos al hacer clic izquierdo, siempre que no estemos sobre un botón del menú UI.
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                LanzarBola();
            }
        }
        //Si la bola está rodando, comprobamos cuántos bolos han caído.
        if (yaLanzada)
        {
            ActualizarPuntosEnVivo();
        }
        //Detección de fin de pista: Si la bola supera los 5 metros en el eje Z, iniciamos el reseteo.
        if (yaLanzada && !reseteando && rbBola.transform.position.z > 5f)
        {
            StartCoroutine(ResetearSecuencia());
        }
    }

    void ActualizarPuntosEnVivo()
    {
        //Recorremos todos los bolos para ver su inclinación. Si es mayor de 15º cuenta como bolo caído.
        for (int i = 0; i < bolos.Length; i++)
        {
            //Si no hemos contado el bolo ya y si el ángulo entre el eje vertical del bolo y el vertical del mundo sea > 15º:
            if (!boloYaContado[i] && Vector3.Angle(bolos[i].transform.up, Vector3.up) > 15f)
            {
                //Se cuenta ese bolo y aumentamos los contadores, además de actualizar el contador de puntuación.
                boloYaContado[i] = true;
                contadorPuntuacion++;
                bolosCaidosEstaTirada++;
                puntuacion.text = "Puntuación: " + contadorPuntuacion;
            }
        }
    }
    //Disparamos la bola:
    void LanzarBola()
    {
        yaLanzada = true;
        bolosCaidosEstaTirada = 0;

        if (mensajeDisparo != null) mensajeDisparo.SetActive(false);

        contadorTiradas++;
        tiradas.text = "Tiradas: " + contadorTiradas;
        //Empujamos la bola hacia delante multiplicándola por la fuerza.
        rbBola.AddForce(Vector3.forward * fuerza, ForceMode.Impulse);
        //Reproducimos el sonido al lanzar la bola.
        if (sonidoLanzar != null) altavoz.PlayOneShot(sonidoLanzar, 0.8f);
    }

    IEnumerator ResetearSecuencia()
    {
        //Reseteamos y esperamos:
        reseteando = true;
        yield return new WaitForSeconds(0.25f);
        //El recogedor va bajando:
        Vector3 posDestinoBajo = new Vector3(posOriginalRecogedor.x, alturaBaja + 1, posOriginalRecogedor.z);
        while (Vector3.Distance(recogedor.transform.position, posDestinoBajo) > 0.01f)
        {
            recogedor.transform.position = Vector3.MoveTowards(recogedor.transform.position, posDestinoBajo, velocidadRecogedor * Time.deltaTime);
            yield return null; //Espera al siguiente frame.
        }

        yield return new WaitForSeconds(0.1f);
        //El boleano "fuePleno" se pone en true/false si los bolos caídos son = o > al número total de bolos.
        bool fuePleno = (bolosCaidosEstaTirada >= bolos.Length);
        //Reseteamos la variable:
        yaLanzada = false;
        LimpiarFisica(rbBola, bolaPosOrigen, Quaternion.identity); //Y limpiamos las físicas de la bola.

        for (int i = 0; i < bolos.Length; i++)
        {
            Vector3 puntoSeguro = posIniciales[i] + Vector3.up * 0.05f;
            LimpiarFisica(bolos[i].GetComponent<Rigidbody>(), puntoSeguro, rotIniciales[i]);
            boloYaContado[i] = false;
        }
        //Sincroniza manualmente el motor de colisiones (PhysX) con las nuevas posiciones de los objetos.
        Physics.SyncTransforms();
        //Pausa la ejecución hasta el próximo paso de cálculo físico para garantizar la estabilidad de los cuerpos.
        yield return new WaitForFixedUpdate();
        rbBola.isKinematic = false; //Hacemos que la bola vuelva a tener una física normal que no ignore la gravedad y otras fuerzas.
        //Hacemos con un foreach que cada bolo contenido en la lista de bolos, no ignoren la gravedad y otras fuerzas.
        foreach (GameObject bolo in bolos)
        {
            Rigidbody rb = bolo.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.Sleep(); //Hacemos que el rigidbody de los bolos, se "duerma" para evitar que al estar de pie bailen o reciban fuerzas innecesarias hasta que sena colisionados.
        }
        //Hacemos que el recogedor vuelva a su posición de arriba.
        while (Vector3.Distance(recogedor.transform.position, posOriginalRecogedor) > 0.01f)
        {
            recogedor.transform.position = Vector3.MoveTowards(recogedor.transform.position, posOriginalRecogedor, velocidadRecogedor * Time.deltaTime);
            yield return null;
        }
        //Comprobamos si ha terminado la partida:
        if (contadorTiradas >= 3 || bolosCaidosEstaTirada >= bolos.Length)
        {
            panel.SetActive(true); //Mostramos el panel para volver a jugar o salir del juego.
            Time.timeScale = 0; //Congelamos el juego.
            
            yaLanzada = false; //Reseteamos la variable.
            reseteando = true; 
        }
        else
        {
            reseteando = false;

            if (mensajeDisparo != null)
                mensajeDisparo.SetActive(true);
        }
    }
    //Detección de colisiones. Todos aquellos objetos con el tag 'Bolo' que sean colisionados por la bola, hacen que se reproduzca el sonido del choque una sola vez.
    void OnCollisionEnter(Collision colsion)
    {
        if (colsion.gameObject.CompareTag("Bolo") && sonidoChoque != null)
            altavoz.PlayOneShot(sonidoChoque, 0.1f);
    }
    //Creamos una función que no devuelve nada que lo que hace es resetear los bolos.
    void LimpiarFisica(Rigidbody rb, Vector3 pos, Quaternion rot)
    {
        /*Al activarlo, el objeto ignora la gravedad y fuerzas, esto se hace para poder "teletransportar"
        el bolo sin que salga volando*/
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero; //Frenamos cualquier movimiento y rotación previa. 
        rb.angularVelocity = Vector3.zero;
        rb.position = pos; //Teletransportamos el bolo y lo rotamos correctamente.
        rb.rotation = rot;
    }
}