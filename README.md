# 🎳 Juego de los Bolos en Unity 3D

Simulador de bolera desarrollado con **Unity 3D** y **C#**. El jugador mueve la bola, la lanza por la pista y derriba los bolos en una partida de tres tiradas. 🎮

📌 **Contexto:** práctica individual de la asignatura *Tecnologías de Desarrollo de Videojuegos y Entornos Virtuales I* (UDIT, curso 2025/2026). La memoria del proyecto la redacté yo y está en la carpeta `docs/`.

## 🚀 Funcionalidades

* 🕹️ **Menú inicial:** el juego arranca en pausa con los botones *Jugar* y *Salir*.
* 🎳 **Lanzamiento de la bola:** se mueve de lado a lado y se lanza con un impulso físico.
* 💥 **Puntuación en vivo:** cuenta los bolos derribados en cada tirada.
* 🏁 **Fin de partida:** termina a las 3 tiradas, o antes si se hace pleno (*strike*) en la primera.
* 🧹 **Recogedor automático:** una barredora retira los bolos y los devuelve a su sitio, como en una bolera real.
* 🔊 **Sonido:** efectos al chocar la bola y al hacer pleno.

## 🧠 Cómo funciona por dentro

| 🧩 Apartado | 🎯 Cómo está hecho |
|---|---|
| ⏸️ Estados del juego | `Time.timeScale` mantiene la pausa técnica hasta pulsar *Jugar*. |
| ⚽ Movimiento de la bola | Lectura de `Input.GetAxis` y cambio directo de la velocidad del `Rigidbody`. |
| 🚀 Lanzamiento | `AddForce` con `ForceMode.Impulse`. |
| 📐 Bolos derribados | Se compara el ángulo de cada bolo con `Vector3.up`; si supera los **15°**, cuenta como caído. |
| 🔄 Reseteo de pista | Corrutina que mueve el recogedor, limpia la física y restaura posiciones guardadas. |
| 🔔 Audio | `AudioSource` con `PlayOneShot`. |

## 🛠️ Tecnologías y conceptos

* 💻 **C#** como lenguaje y **Unity 3D** como motor.
* ⚙️ **Físicas:** `Rigidbody`, colisiones, `isKinematic` y `Physics.SyncTransforms()`.
* ⏱️ **Corrutinas** (`IEnumerator`) para los procesos repartidos en el tiempo.
* 🧭 **Vectores y ángulos:** `Vector3.Angle`, `Vector3.MoveTowards` y `Quaternion`.
* 🖼️ **Interfaz de usuario** con menú y marcador.
* 🔊 **Audio** con efectos de sonido.

## 🗂️ Piezas del proyecto

| 📄 Archivo | 🎯 Función |
|---|---|
| `MenuJuego.cs` | Menú, estados de la partida, puntuación y secuencia de reseteo. |
| `Movimiento.cs` | Movimiento y lanzamiento de la bola. |

## ▶️ Cómo jugarlo

1. Descarga el build para **Windows** desde la página del juego en itch.io (enlace pendiente).
2. Descomprime el archivo `.zip`.
3. Ejecuta el `.exe` y pulsa **Jugar**.

## 🧑‍💻 Cómo abrir el proyecto

1. Instala **Unity** con la versión indicada en `ProjectSettings/ProjectVersion.txt`.
2. Abre la carpeta del repositorio desde **Unity Hub**.
3. Abre la escena principal y pulsa **Play**.

## 📎 Documentación

La carpeta [`docs/`](docs/) incluye la **memoria** del proyecto: descripción general, descripción técnica, diario de desarrollo y bibliografía.

## 🙌 Créditos

* 🏟️ Modelo 3D de la bolera: *Bowling Club*, de tiunov.se.
* 🔊 Efectos de sonido: Pixabay (bola de bolos y strike).
* 📺 Tutoriales de apoyo: *Unity Beginner Tutorial – Finishing the Game!* y *Bowling With The Unity Game Engine* (YouTube).

---
🎓 *Práctica académica para trabajar físicas, colisiones, corrutinas y gestión de estados en Unity.*
