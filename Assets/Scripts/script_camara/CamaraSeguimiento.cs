using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    public Transform objetivo; // Aquí arrastraremos a Verónica
    public Vector3 offset = new Vector3(0, 3, -10); // Distancia cámara-personaje
    public float suavizado = 0.125f; // Qué tan rápido reacciona la cámara

    void LateUpdate()
    {
        if (objetivo == null) return;

        // Calculamos la posición deseada (Solo seguimos en X, mantenemos Y y Z fijos)
        // O si quieres que la cámara también suba cuando ella salta, usa: objetivo.position + offset
        Vector3 posicionDeseada = new Vector3(objetivo.position.x + offset.x, offset.y, offset.z);

        // Creamos un movimiento suave
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, suavizado);

        // Aplicamos la posición
        transform.position = posicionSuavizada;
    }
}