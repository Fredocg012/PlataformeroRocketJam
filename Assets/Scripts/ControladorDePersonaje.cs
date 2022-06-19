using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDePersonaje : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public LayerMask capaSuelo;
    public int saltosMaximos;
    
    public AudioClip sonidoSalto;


    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private int saltosRestantes;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();
    }

    void ProcesarMovimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");

        if(inputMovimiento != 0f)
        {
            animator.SetBool("isRuning", true);
        }

        else
        {
            animator.SetBool("isRuning", false);
        }


        rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);

        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        if( (mirandoDerecha == true && inputMovimiento < 0) || (mirandoDerecha == false && inputMovimiento > 0))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    void ProcesarSalto()
    { 
        if(EstaSuelo())
        {
            saltosRestantes = saltosMaximos;
        }

        if((Input.GetButtonDown("Jump")|| Input.GetButtonDown("Fire1")|| Input.GetButtonDown("Fire2")|| Input.GetButtonDown("Fire3 ")) && saltosRestantes > 0)
        {
            saltosRestantes--;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0f);
            rigidbody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSonido(sonidoSalto);
        }
    }

    bool EstaSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }
}
