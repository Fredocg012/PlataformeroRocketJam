using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorDeCamara : MonoBehaviour
{
    public Transform personaje;

    private float tamanioCamara;
    private float anchoPantalla;

    // Start is called before the first frame update
    void Start()
    {
        tamanioCamara = Camera.main.orthographicSize;
        anchoPantalla = tamanioCamara * 2;
    }

    // Update is called once per frame
    void Update()
    {
        CalcularPosicionCamara();
    }

    void CalcularPosicionCamara()
    {
        int pantallaPersonaje = (int)(personaje.position.x / anchoPantalla);
        float anchoCamara = (pantallaPersonaje * anchoPantalla) + tamanioCamara;

        transform.position = new Vector3(anchoCamara, transform.position.y, transform.position.z);

       // Debug.Log(pantallaPersonaje);
       // Debug.Log(anchoCamara);
    }
}
