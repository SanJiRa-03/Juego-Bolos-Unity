# Juego de los bolos · Unity 3D

Simulador de bolera hecho en **Unity 3D** con **C#** para la asignatura *Tecnologías de Desarrollo de Videojuegos y Entornos Virtuales I* (UDIT, curso 2025/2026).

Lanza la bola, derriba los bolos y consigue pleno. Cada partida tiene 3 tiradas, salvo que hagas pleno (strike) en la primera.

## Qué incluye

| Apartado | Cómo está hecho |
|---|---|
| Menú y estados de juego | `MenuJuego`: pausa técnica con `Time.timeScale` hasta pulsar Jugar |
| Control de la bola | Movimiento lateral con `Input.GetAxis` sobre el `Rigidbody` y lanzamiento con `AddForce` (`ForceMode.Impulse`) |
| Detección de bolos caídos | Se calcula el ángulo de cada bolo con `Vector3.Angle`; si pasa de 15° respecto a la vertical, cuenta como derribado |
| Recogedor de bolos | Corrutina que mueve la barredora, limpia la física y restaura los bolos a su posición inicial |
| Audio | Efectos de sonido al chocar la bola y en los plenos, con `PlayOneShot` |

## Estructura

```
Assets/
├── Scripts/   MenuJuego.cs, Movimiento.cs
├── ...
```

## Cómo jugar

Descarga el build para Windows desde la página del juego en itch.io (enlace pendiente) y ejecuta el `.exe`.

## Cómo abrir el proyecto

1. Instala Unity con el mismo número de versión que indica `ProjectSettings/ProjectVersion.txt`.
2. Abre la carpeta del repositorio desde Unity Hub.

## Documentación

La memoria del proyecto, con la descripción técnica y el diario de desarrollo, está en [`docs/`](docs/).

## Créditos

- Modelo 3D de la bolera: *Bowling Club*, de tiunov.se.
- Efectos de sonido: Pixabay (bola de bolos y strike).
- Tutoriales de apoyo: *Unity Beginner Tutorial – Finishing the Game!* y *Bowling With The Unity Game Engine* (YouTube).

Autora: Sandra Jiménez Ramiro
