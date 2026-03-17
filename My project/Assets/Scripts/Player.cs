using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform _transform;
    public Transform cameraTransform;

    Vector2 rotacaoMouse;
    public int sensibilidade;
    public float velocidade = 5.0f;

    public float maxDistance = 10f;
    public LayerMask hitLayers;

    int pontos = 0;
    public TextMeshProUGUI textoPontos;
    public TextMeshProUGUI textoMultiplicador;
    int multiplicadorPontos = 1;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Screen.fullScreen = true;
    }

    void Update()
    { //Camera
        Vector2 controleMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        rotacaoMouse = new Vector2(rotacaoMouse.x + controleMouse.x * sensibilidade * Time.deltaTime, rotacaoMouse.y + controleMouse.y * sensibilidade * Time.deltaTime);

        _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, rotacaoMouse.x, _transform.eulerAngles.z);

        rotacaoMouse.y = Mathf.Clamp(rotacaoMouse.y, -80, 80);

        cameraTransform.localEulerAngles = new Vector3(-rotacaoMouse.y,
                                                       cameraTransform.localEulerAngles.y,
                                                       cameraTransform.localEulerAngles.z);

        //Movimentação
        float moverHorizontal = Input.GetAxis("Horizontal");
        float moverVertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(moverHorizontal, 0.0f, moverVertical);


        transform.Translate(movimento * velocidade * Time.deltaTime);


        if (Input.GetMouseButtonDown(0)) // só dispara quando clicar
        {
            Ray ray = cameraTransform.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, hitLayers))
            {
                Debug.Log("Acertou: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject.name == "Computador")
                {
                    pontos+= multiplicadorPontos;
                    Debug.Log("Pontos: " + pontos);
                    textoPontos.text = "Pontos: " + pontos;
                }
                else if (hit.collider.gameObject.name == "Multiplicador")
                {
                    if (pontos >= 25)
                    {
                        pontos -= 25; 
                        multiplicadorPontos++; 

                        Debug.Log("Multiplicador: " + multiplicadorPontos);
                        Debug.Log("Pontos restantes: " + pontos);

                        
                        textoMultiplicador.text = "Multiplicador: " + multiplicadorPontos;
                        textoPontos.text = "Pontos: " + pontos;
                    }
                    else
                    {
                        Debug.Log("Pontos insuficientes!");
                    }
                }
            }
            else
            {
                Debug.Log("Não acertou nada");
            }
        }

    }
}
