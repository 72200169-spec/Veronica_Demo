using UnityEngine;
using UnityEngine.InputSystem;

public class VeronicaController : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;

    [Header("Detección de Suelo")]
    public float radioDeteccion = 0.3f;
    public Vector3 offsetSuelo = new Vector3(0, -1f, 0); // Ajusta esto para que la esfera baje a los pies
    public LayerMask capaSuelo;

    private Vector2 inputMovimiento;
    private Rigidbody rb;
    private bool mirandoDerecha = true;
    private bool estaEnSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Congelar rotaciones para que no se caiga de cara
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    // Recibe el movimiento de WASD o Flechas
    void OnMove(InputValue value)
    {
        inputMovimiento = value.Get<Vector2>();
    }

    // Recibe el salto de la tecla Espacio
    void OnJump()
    {
        if (estaEnSuelo)
        {
            // Reseteamos la velocidad vertical antes de saltar para que el salto sea siempre igual
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Detectar el suelo de forma precisa
        estaEnSuelo = Physics.CheckSphere(transform.position + offsetSuelo, radioDeteccion, capaSuelo);

        // Aplicar movimiento 2.5D
        Vector3 movimiento = new Vector3(inputMovimiento.x, 0, inputMovimiento.y) * velocidad;
        rb.linearVelocity = new Vector3(movimiento.x, rb.linearVelocity.y, movimiento.z);

        // Girar el personaje
        if (inputMovimiento.x > 0 && !mirandoDerecha) Flip();
        else if (inputMovimiento.x < 0 && mirandoDerecha) Flip();
    }

    void Flip()
    {
        mirandoDerecha = !mirandoDerecha;
        transform.Rotate(0, 180, 0);
    }

    // Esto dibuja la esfera roja en la ventana 'Scene' para que veas dónde detecta el suelo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offsetSuelo, radioDeteccion);
    }
}