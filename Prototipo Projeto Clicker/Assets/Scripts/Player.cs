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
    
    int clicksAuto = 0;
    float tempoAuto = 0f;
    float intervaloAuto = 1f;
    public TextMeshProUGUI textoAutoClick;

    int pontosMaximos = 100;
    public TextMeshProUGUI textoLimite;


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
                    pontos += multiplicadorPontos;
                    pontos = Mathf.Clamp(pontos, 0, pontosMaximos);
                    Debug.Log("Pontos: " + pontos);
                    textoPontos.text = "Pontos: " + pontos;
                }
                else if (hit.collider.gameObject.name == "Multiplicador")
                {
                    if (pontos >= 25 * multiplicadorPontos)
                    {
                        pontos -= 25 * multiplicadorPontos;
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
                else if (hit.collider.gameObject.name == "AutoClick")
                {
                    int custo = 20;

                    if (pontos >= custo + 10 * clicksAuto)
                    {
                        pontos -= custo + 10 + clicksAuto;
                        clicksAuto++;

                        Debug.Log("Clicks automáticos: " + clicksAuto);
                        Debug.Log("Pontos restantes: " + pontos);

                        textoPontos.text = "Pontos: " + pontos;
                        textoAutoClick.text = "Clicks Automaticos: " + clicksAuto;
                    }
                    else
                    {
                        Debug.Log("Pontos insuficientes!");
                    }
                }
                else if (hit.collider.gameObject.name == "Limite")
                {
                    int custo = pontosMaximos;

                    if (pontos >= pontosMaximos)
                    {
                        pontos -= pontosMaximos;
                        pontosMaximos += 50;

                        Debug.Log("Novo limite: " + pontosMaximos);
                        Debug.Log("Pontos restantes: " + pontos);

                        textoLimite.text = "Limite: " + pontosMaximos;
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

        tempoAuto += Time.deltaTime;

        if (tempoAuto >= intervaloAuto)
        {
            tempoAuto = 0f;

            pontos += clicksAuto;
            pontos = Mathf.Clamp(pontos, 0, pontosMaximos);
            textoPontos.text = "Pontos: " + pontos;
        }

    }
}
