
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

class ControlesDelPersonaje: MonoBehaviour
{
    public Animator _animator;
    public Rigidbody2D _rigidbody2D;
    public CharacterController2D _characterController2D;
    public Transform firepoint;
    public GameObject bullet;

    MisAccionesDeEntrada _misAccionesDeEntrada;

    public float _velocidad_de_reproduccion_juego = 1f;
    
    public bool _agachar = false;
    public bool _saltar = false;
    public bool _per_corriendo = false;

    public float _orientacion = 0;
    public float _velocidad_caminar = 40f;
    public float _velocidad_correr = 100f;
    public float _velocidad = 0f;
    public float _movimiento_horizontal = 0f;

    public bool _botonAtrasPresionado = false;
    public bool _botonAdelantePresionado = false;

    public float _fuerza_rebote = 400f;

    public bool _recibir_herida = false;

    public bool _per_orientacion_derecha_bool = true;
    public float _per_orientacion_derecha_float = 1f;
    public float _per_velocidad_x = 0f;
    public float _per_velocidad_y = 0f;
    public float _per_revivir_espera = 2f;
    public bool _personaje_en_suelo = false;
    public bool _personaje_agachado = false;
    
    




    // botones 
    private void Awake()
    {

        _misAccionesDeEntrada = new MisAccionesDeEntrada();

        _misAccionesDeEntrada.MiMapaDeAcciones.caminaradelante.performed += Bot_CaminarAdelantePresionado;
        _misAccionesDeEntrada.MiMapaDeAcciones.caminaradelante.canceled += Bot_CaminarAdelanteLiberado;

        _misAccionesDeEntrada.MiMapaDeAcciones.caminaratras.performed += Bot_CaminarAtrasPresionado;
        _misAccionesDeEntrada.MiMapaDeAcciones.caminaratras.canceled += Bot_CaminarAtrasLiberado;

        _misAccionesDeEntrada.MiMapaDeAcciones.agachar.performed += Bot_AgacharPresionado;
        _misAccionesDeEntrada.MiMapaDeAcciones.agachar.canceled += Bot_AgacharLiberado;

        _misAccionesDeEntrada.MiMapaDeAcciones.saltar.performed += Bot_saltarPresionado;
        _misAccionesDeEntrada.MiMapaDeAcciones.saltar.canceled += Bot_saltarLiberado;

        _misAccionesDeEntrada.MiMapaDeAcciones.correr.performed += Bot_CorrerPresionado;
        _misAccionesDeEntrada.MiMapaDeAcciones.correr.canceled += Bot_CorrerLiberado;
        _misAccionesDeEntrada.MiMapaDeAcciones.disparar.performed += Bot_Disparar;
    }

    private void Bot_Disparar(InputAction.CallbackContext obj)
    {
        Instantiate(bullet, firepoint.position, firepoint.rotation);
    }




    // QUE HACEN ESOS BOTONES 

    private void Bot_CaminarAdelantePresionado(InputAction.CallbackContext obj)
    {

        CaminarAdelanteAtrasPrelib("BotCaminarAdelantePresionado");
        firepoint.eulerAngles = new Vector3(0, 0, 0);
    }

    private void Bot_CaminarAdelanteLiberado(InputAction.CallbackContext obj)
    {
        CaminarAdelanteAtrasPrelib("BotCaminarAdelanteLiberado");
    }

    private void Bot_CaminarAtrasPresionado(InputAction.CallbackContext obj)
    {
        CaminarAdelanteAtrasPrelib("BotCaminarAtrasPresionado");
        firepoint.eulerAngles = new Vector3(0, 180, 0);

    }

    private void Bot_CaminarAtrasLiberado(InputAction.CallbackContext obj)
    {
        CaminarAdelanteAtrasPrelib("BotCaminarAtrasLiberado");
    }

    private void Bot_AgacharPresionado(InputAction.CallbackContext obj)
    {
        _agachar = true;
    }

    private void Bot_AgacharLiberado(InputAction.CallbackContext obj)
    {
        _agachar = false;
    }

    private void Bot_saltarPresionado(InputAction.CallbackContext obj)
    {
        if (_personaje_agachado == true)
        {
            _saltar = false;
        }
        else
        {
            
            _saltar = true;
        }
        
    }

    private void Bot_saltarLiberado(InputAction.CallbackContext obj)
    {
        
        _rigidbody2D.gravityScale = 5;
        
    }

    private void Bot_CorrerPresionado(InputAction.CallbackContext obj)
    {
        _animator.SetBool("Correr", true);
    }

    private void Bot_CorrerLiberado(InputAction.CallbackContext obj)
    {
        _animator.SetBool("Correr", false);
    }

    // VOID DE CAMINAR (suiche)

    private void CaminarAdelanteAtrasPrelib(string caso)
    {

        switch (caso)
        {
            case "BotCaminarAdelantePresionado":
                _botonAdelantePresionado = true;
                if (_botonAtrasPresionado == true)
                {
                    _animator.SetBool("Caminar", false);
                    _animator.SetBool("Ocio", true);
                    _orientacion = 0;
                }
                else if (_botonAtrasPresionado == false)
                {
                    _animator.SetBool("Caminar", true);
                    _animator.SetBool("Ocio", false);
                    _orientacion = 1;
                }
                break;

            case "BotCaminarAdelanteLiberado":
                _botonAdelantePresionado = false;

               if (_botonAtrasPresionado == true)
                {
                    _animator.SetBool("Caminar", true);
                    _animator.SetBool("Ocio", false);
                    _orientacion = -1;
                }
                else if (_botonAtrasPresionado == false)
                {
                    _animator.SetBool("Caminar", false);
                    _animator.SetBool("Ocio", true);
                    _orientacion = 0;
                }
                break;

            case "BotCaminarAtrasPresionado":
                _botonAtrasPresionado = true;

                if (_botonAdelantePresionado == true)
                {
                    _animator.SetBool("Caminar", false);
                    _animator.SetBool("Ocio", true);
                    _orientacion = 0;
                }
                else if (_botonAdelantePresionado == false)
                {
                    _animator.SetBool("Caminar", true);
                    _animator.SetBool("Ocio", false);
                    _orientacion = -1;
                }
                break;
            case "BotCaminarAtrasLiberado":
                _botonAtrasPresionado = false;

                if (_botonAdelantePresionado == true)
                {
                    _animator.SetBool("Caminar", true);
                    _animator.SetBool("Ocio", false);
                    _orientacion = 1;
                }
                else if (_botonAdelantePresionado == false)
                {
                    _animator.SetBool("Caminar", false);
                    _animator.SetBool("Ocio", true);
                    _orientacion = 0;
                }
                break;
        }

    }


    // VOID DE UNITY

    private void Update()
    {
        _personaje_en_suelo = _characterController2D.m_Grounded;
        _personaje_agachado = _characterController2D.m_Crouched;
        _per_velocidad_x = _rigidbody2D.linearVelocity.x;
        _per_velocidad_y = _rigidbody2D.linearVelocity.y;
        _per_orientacion_derecha_bool = _characterController2D.m_FacingRight;
        _per_corriendo = _animator.GetBool("Correr");

        _animator.SetBool("EnSuelo", _personaje_en_suelo);
        _animator.SetBool("Agachar",_personaje_agachado);
        //_animator.SetFloat("VelocidadX", _per_velocidad_x);
        _animator.SetFloat("VelocidadY", _per_velocidad_y);


        if (_orientacion != 0)
        {
            _animator.SetBool("Ocio", false);
        }

        if (_animator.GetBool("EnSuelo") == true)
        {
            _rigidbody2D.gravityScale = 2;
        }

        if (_per_orientacion_derecha_bool == true)
        {
            _per_orientacion_derecha_float = 1;
        }
        else if (_per_orientacion_derecha_bool == false)
        {
            _per_orientacion_derecha_float = -1;
        }

        
    }
    private void LateUpdate()
    {
        if (_animator.GetBool("Correr")== true && 
            _animator.GetBool("EnSuelo")== true)
        {
            _velocidad = _velocidad_correr;
        }
        else
        {
            _velocidad = _velocidad_caminar;
        }

        _movimiento_horizontal = Time.fixedDeltaTime * _velocidad * _orientacion;
        //print(_agachar);
        _characterController2D.Move(_movimiento_horizontal, _agachar, _saltar);
        _saltar = false; // implementar 2 saltos

        if (_rigidbody2D.linearVelocity.y < 0)
        {
            _rigidbody2D.gravityScale = 5f;
            if(_animator.GetBool("HerirBool") == true)
            {
                _characterController2D.GetComponent<Rigidbody2D>().gravityScale = 10;
            }
        }
    }







    // ENABLE AND DISABLE

    private void OnEnable()
    {
        _misAccionesDeEntrada.Enable();
    }

    private void OnDisable()
    {
        _misAccionesDeEntrada.Disable();
    }




    private void OnTriggerEnter2D(Collider2D recolectable_colision)
    {
        if (recolectable_colision.CompareTag("Recolectable"))
        {
            recolectable_colision.gameObject.GetComponent<IRecolectable>().IncrementarEnergia();
            recolectable_colision.gameObject.GetComponent<IRecolectable>().Eliminar();

        }
        if (recolectable_colision.CompareTag("Enemigo"))
        {
            
            HerirPersonaje();
        }
    }

    // PERSONAJE MUERTE
    [ContextMenu("Herir Personaje")]
   public void HerirPersonaje()
    {
        _misAccionesDeEntrada.MiMapaDeAcciones.Disable();
        if(_recibir_herida == false)
        {
            _recibir_herida = true;
            _animator.SetBool("HerirBool", true);
            if (_animator.GetBool("EnSuelo")== true && _animator.GetBool("HerirBool")== true)
            {
                _characterController2D.m_Rigidbody2D.AddForce(new Vector2(0f, _fuerza_rebote));
            }
            if (_animator.GetBool("EnSuelo")== false && _animator.GetBool("HerirBool")== false)
            {
                if (_animator.GetFloat("VelocidadY") <= 0f)
                {
                    _characterController2D.m_Rigidbody2D.AddForce(new Vector2(0f, _fuerza_rebote));
                }
            }
            StartCoroutine(RevivirPersonajeCorrutina());
        }
    }

    [ContextMenu("Rebotar Personaje")]
    public void RebotarPersonaje()
    {
        _rigidbody2D.AddForce(new Vector2(0f, _fuerza_rebote));
    }

   public void RevivirPersonaje()
    {
        _animator.SetTrigger("Revivir");
        _animator.SetBool("HerirBool", false);
        _recibir_herida = false;
        _misAccionesDeEntrada.MiMapaDeAcciones.Enable();
    }

   IEnumerator RevivirPersonajeCorrutina()
    {
        yield return new WaitForSeconds(_per_revivir_espera);
        RevivirPersonaje();
    }

    //plataformas que se mueven

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "plataformamovible")
        {
            transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "plataformamovible")
        {
            transform.parent = null;
        }
    }
}