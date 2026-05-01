using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : MonoBehaviour
{
    // Referências para a câmera e o transform do jogador
    public Transform _transform;
    public Transform cameraTransform;
    Vector2 rotacaoMouse;
    public int sensibilidade;
    public float velocidade = 5.0f;
    ClickSpawner clickSpawner;

    // Configurações para o raycast
    public float maxDistance = 10f;
    public LayerMask hitLayers;

    // Variáveis para o sistema de pontos e upgrades
    int pontos = 0;
    public TextMeshProUGUI textoPontos;
    public TextMeshProUGUI textoPontos2;
    public TextMeshProUGUI textoMultiplicador;
    int multiplicadorPontos = 1;
    int clicksAuto = 0;
    float tempoAuto = 0f;
    float intervaloAuto = 1f;
    public TextMeshProUGUI textoAutoClick;
    int pontosMaximos = 500;
    public TextMeshProUGUI textoLimite;

    //HUD para mostrar os preços dos upgrades
    public TextMeshPro precoMulti;
    public TextMeshPro precoAuto;
    public TextMeshPro precoLimite;
    int custoMulti;
    int custoAuto;
    int custoLimite;

    // Classes
    public TextMeshProUGUI ClasseTexto;
    int multiplicadorClasse;
    int clicksautomaticosclasse;
    int limiteclasse;

    // Variáveis para as luzes e a janela
    public Light luzQuarto;
    public Light luzSol;
    public Light luzComputador;
    public Renderer janelaRenderer;


    int multiplicadorCiclo = 1;

    //Pra travar a câmera no portal
    public bool travarCamera = false;
    public bool moveble = true;
    public bool moverhorizontal = true;

    //materiais padrao
    public Material materialparedepadrao;
    public Material materialchaopadrao;
    public Texture portapadrao;
    public Material forrocamapadrao;
    public Material janeladiapadrao;
    public Material janelanoitepadrao;

    //materais realistas
    public Material materialparederealista;
    public Material materialchaorealista;
    public Material janelarealistadia;
    public Material janelarealistanoite;
    public Texture portarealista;
    public Material forrocamarelista;
    public Material janeladiarealista;
    public Material janelanoiterealista;
    int realista;

    //materiais mono
    int mono;
    public Material materialparedemono;
    public Material materialchaomono;
    public Material janeladiamono;
    public Material janelanoitemono;
    public Texture portamono;
    public Material forrocamamono;

    //materiais hyperpop
    int hyperpop;
    public Material materialparedehyperpop;
    public Material materialchaohyperpop;
    public Material janeladiahyperpop;
    public Material janelanoitehyperpop;
    public Texture portahyperpop;
    public Material forrocamahyperpop;

    //renderer dos objetos
    public Renderer portaRenderer;
    public Renderer parede1Renderer;
    public Renderer parede2Renderer;
    public Renderer parede3Renderer;
    public Renderer parede4Renderer;
    public Renderer parede5Renderer;
    public Renderer parede6Renderer;
    public Renderer parede7Renderer;
    public Renderer parede8Renderer;
    public Renderer chaoRenderer;
    public Renderer forroRenderer;
    public Renderer forro2Renderer;
    public Renderer cama1Renderer;
    public Renderer cama2Renderer;

    //HUDManager
    private HUDManager HUDManager;

    void Start()
    // Configurações iniciais do cursor e tela cheia
    {
        // Configurações iniciais do sistema de pontos e upgrades
        pontosMaximos = 500;
        multiplicadorClasse = 1;
        clicksautomaticosclasse = 1;
        limiteclasse = 1;
        //travar a camera e esconder o cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Screen.fullScreen = true;
        // Configurações iniciais dos custos dos upgrades
        custoMulti = 25 * multiplicadorPontos;
        custoAuto = 10;
        custoLimite = pontosMaximos;
        textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
        textoAutoClick.text = " (J) Clicks Automaticos: " + clicksAuto * clicksautomaticosclasse;
        textoLimite.text = " (K) Limite: " + pontosMaximos;
        precoAuto.text = "Preço: " + custoAuto;
        precoMulti.text = "Preço: " + custoMulti;
        precoLimite.text = "Preço: " + custoLimite;
        //padroniza as texturas no começo do jogo
        TexturasPadrao();
        janelaRenderer.material = janeladiapadrao;
        clickSpawner = FindFirstObjectByType<ClickSpawner>();

        HUDManager = FindAnyObjectByType<HUDManager>();
    }

    void Update()
    { //Camera

        if (travarCamera == false)
        {
            Vector2 controleMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            rotacaoMouse = new Vector2(rotacaoMouse.x + controleMouse.x * sensibilidade * Time.deltaTime, rotacaoMouse.y + controleMouse.y * sensibilidade * Time.deltaTime);

            _transform.eulerAngles = new Vector3(_transform.eulerAngles.x, rotacaoMouse.x, _transform.eulerAngles.z);

            rotacaoMouse.y = Mathf.Clamp(rotacaoMouse.y, -80, 80);

            cameraTransform.localEulerAngles = new Vector3(-rotacaoMouse.y,
                                                           cameraTransform.localEulerAngles.y,
                                                           cameraTransform.localEulerAngles.z);
        }

        //Movimenta  o player
        if (moveble == true)
        {
            float moverVertical = Input.GetAxis("Vertical");
            float moverHorizontal = Input.GetAxis("Horizontal");

            Vector3 movimento = new Vector3(moverHorizontal, 0.0f, moverVertical);


            transform.Translate(movimento * velocidade * Time.deltaTime);
        }
        //Compra de itens com teclado
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (pontos >= custoMulti)
            {
                pontos -= custoMulti;
                multiplicadorPontos += 1;

                Debug.Log("Multiplicador: " + multiplicadorPontos);
                Debug.Log("Pontos restantes: " + pontos);


                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
                textoPontos.text = "Pontos: " + pontos;
                custoMulti = 25 * multiplicadorPontos;
                precoMulti.text = "Preço: " + custoMulti;
            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (pontos >= custoAuto)
            {
                pontos -= custoAuto;
                clicksAuto += 1;
                Debug.Log("Clicks automáticos: " + clicksAuto);
                Debug.Log("Pontos restantes: " + pontos);
                textoPontos.text = "Pontos: " + pontos;
                textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
                custoAuto = 10 * clicksAuto;
                precoAuto.text = "Preço: " + custoAuto;
            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (pontos >= custoLimite)
            {
                pontos -= custoLimite;
                pontosMaximos += 500 * limiteclasse;
                Debug.Log("Novo limite: " + pontosMaximos);
                Debug.Log("Pontos restantes: " + pontos);
                textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
                textoPontos.text = "Pontos: " + pontos;
                custoLimite = pontosMaximos / limiteclasse;
                precoLimite.text = "Preço: " + custoLimite;
            }
            else
            {
                Debug.Log("Pontos insuficientes!");
            }
        }

        //Compra de itens com clique
        if (Input.GetMouseButtonDown(0)) // só dispara quando clicar
        {
            if (HUDManager.hudsecundariaoneoff == false)
            { 
                Ray ray = cameraTransform.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxDistance, hitLayers))
                {
                    Debug.Log("Acertou: " + hit.collider.gameObject.name);

                    if (hit.collider.gameObject.name == "Computador")
                    {
                        pontos += multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse;
                        pontos = Mathf.Clamp(pontos, 0, pontosMaximos);
                        Debug.Log("Pontos: " + pontos);
                        textoPontos.text = "Pontos: " + pontos;
                        textoPontos2.text = textoPontos.text;
                    }
                }

                // Verifica se o objeto clicado é o interruptor e alterna a luz do quarto
                if (hit.collider.gameObject.name == "Interruptor")
                {
                    if (luzQuarto.intensity > 0f)
                    {

                        luzQuarto.intensity = 0f;
                    }
                    else
                    {
                        luzQuarto.intensity = 50f;
                    }
                }
                else
                {
                    Debug.Log("Não acertou nada");
                }

            }

        }

        tempoAuto += Time.deltaTime;
        if (tempoAuto >= intervaloAuto)
        {
            tempoAuto = 0f;

            pontos += clicksAuto * clicksautomaticosclasse;
            pontos = Mathf.Clamp(pontos, 0, pontosMaximos);
            textoPontos.text = "Pontos: " + pontos;
        }

        //----------------------------------------------- SEÇÃO DE DIA E NOITE --------------------------------------------------------------
        // Verifica se ambas as luzes estão apagadas para acender a luz do computador
        if (luzSol.intensity == 0f && luzQuarto.intensity == 0f)
        {
            luzComputador.intensity = 500f;
        }
        else
        {
            luzComputador.intensity = 0f;
        }

        // Só template para mim mesmo como interruptor de sol 
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (luzSol.intensity > 0f)
            {
                luzSol.intensity = 0f;
            }
            else
            {
                luzSol.intensity = 500f;
            }
        }

        //Texturas da janela dependendo da luz do sol
        if (luzSol.intensity == 0f)
        {
            //coloca a textura dependente do pacote de texturas
            if (realista == 1)
            {
                janelaRenderer.material = janelanoiterealista;
                multiplicadorCiclo = 2; // Dobra o multiplicador de pontos quando a luz do sol estiver apagada
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
            else if (mono == 1)
            {
                janelaRenderer.material = janelanoitemono;
                multiplicadorCiclo = 2; // Dobra o multiplicador de pontos quando a luz do sol estiver apagada
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
            else if (hyperpop == 1)
            {
                janelaRenderer.material = janelanoitehyperpop;
                multiplicadorCiclo = 2; // Dobra o multiplicador de pontos quando a luz do sol estiver apagada
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
            else
            {
                janelaRenderer.material = janelanoitepadrao;
                multiplicadorCiclo = 2; // Dobra o multiplicador de pontos quando a luz do sol estiver apagada
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
        }
        else
        {
            //coloca a textura dependente do pacote de texturas
            if (realista == 1)
            {
                janelaRenderer.material = janeladiarealista;
                multiplicadorCiclo = 1; // Restaura o multiplicador de pontos para o normal quando a luz do sol estiver acesa
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
            else if (mono == 1)
            {
                janelaRenderer.material = janeladiamono;
                multiplicadorCiclo = 1; // Restaura o multiplicador de pontos para o normal quando a luz do sol estiver acesa
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);  
            }
            else if (hyperpop == 1)
            {   
                janelaRenderer.material = janeladiahyperpop;
                multiplicadorCiclo = 1; // Restaura o multiplicador de pontos para o normal quando a luz do sol estiver acesa
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);  
            }
            else
            {
                janelaRenderer.material = janeladiapadrao;
                multiplicadorCiclo = 1; // Restaura o multiplicador de pontos para o normal quando a luz do sol estiver acesa
                textoMultiplicador.text = " (H) Multiplicador: " + (multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse);
            }
        }

        // -------------------------------------------------------------- SEÇÃO DE TEXTURAS --------------------------------------------------------------
        //Compra de texturas e etc
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TexturasRealistas();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TexturasPadrao();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TexturasMono();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Texturashyperpop();
        }

        // -------------------------------------------------------------- SEÇÃO DE CLASSES --------------------------------------------------------------
        //Compra de classes
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ClasseTexto.text = "Classe: Nenhuma";
            //trava de segurança para evitar bugs envolvendo o limite de pontos ao trocar de classe, dividindo o limite atual pelo limite da classe anterior
            if (limiteclasse == 3)
            {
                pontosMaximos = pontosMaximos / limiteclasse;
            }
            else if (limiteclasse == 2)
            {
                pontosMaximos = pontosMaximos / limiteclasse;
            }
            //atributos da classe
            multiplicadorClasse = 1;
            clicksautomaticosclasse = 1;
            limiteclasse = 1;
            //alterando a hud ao trocar de classe
            textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
            textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
            pontosMaximos = pontosMaximos * limiteclasse;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ClasseTexto.text = "Classe: Python";
            if (limiteclasse == 3)
            {
                pontosMaximos = pontosMaximos / limiteclasse;
            }
            else if (limiteclasse == 2)
            {
                pontosMaximos = pontosMaximos / limiteclasse;
            }
            multiplicadorClasse = 1;
            clicksautomaticosclasse = 5;
            limiteclasse = 1;
            textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
            textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
            pontosMaximos = pontosMaximos * limiteclasse;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ClasseTexto.text = "Classe: C#";
            if (limiteclasse == 3)
            {
                pontosMaximos = pontosMaximos / limiteclasse;
            }
            else if (limiteclasse == 2)
            {
                pontosMaximos = pontosMaximos / limiteclasse;
            }
            multiplicadorClasse = 1;
            clicksautomaticosclasse = 1;
            limiteclasse = 3;
            textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
            textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
            pontosMaximos = pontosMaximos * limiteclasse;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ClasseTexto.text = "Classe: Java";
            if (limiteclasse == 3)
            {
                pontosMaximos = pontosMaximos / limiteclasse;
            }
            else if (limiteclasse == 2)
            {
                pontosMaximos = pontosMaximos / limiteclasse;
            }
            multiplicadorClasse = 5;
            clicksautomaticosclasse = 1;
            limiteclasse = 1;
            textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
            textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
            pontosMaximos = pontosMaximos * limiteclasse;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ClasseTexto.text = "Classe: Holy C";
            if (limiteclasse == 3)
            {
                pontosMaximos = pontosMaximos / limiteclasse;
            }
            else if (limiteclasse == 2)
            {
                pontosMaximos = pontosMaximos / limiteclasse;
            }
            multiplicadorClasse = 2;
            clicksautomaticosclasse = 2;
            limiteclasse = 2;
            textoLimite.text = " (K) Limite: " + (pontosMaximos * limiteclasse);
            textoAutoClick.text = " (J) Clicks Automaticos: " + (clicksAuto * clicksautomaticosclasse);
            pontosMaximos = pontosMaximos * limiteclasse;
        }

        clickSpawner.multiplicador = multiplicadorPontos * multiplicadorCiclo * multiplicadorClasse;

    }
    public void ResetarCamera()
    {
        rotacaoMouse = Vector2.zero;

        // Zera rotação do player (eixo Y)
        _transform.eulerAngles = Vector3.zero;

        // Zera rotação da câmera
        cameraTransform.localEulerAngles = Vector3.zero;
    }


    public void TexturasRealistas()
    {
        //funcao para colocar as texturas realistas
        portaRenderer.material.mainTexture = portarealista;
        parede1Renderer.material = materialparederealista;
        parede2Renderer.material = materialparederealista;
        parede3Renderer.material = materialparederealista;
        parede4Renderer.material = materialparederealista;
        parede5Renderer.material = materialparederealista;
        parede6Renderer.material = materialparederealista;
        parede7Renderer.material = materialparederealista;
        parede8Renderer.material = materialparederealista;
        chaoRenderer.material = materialchaorealista;
        forroRenderer.material = forrocamarelista;
        forro2Renderer.material = forrocamarelista;
        cama1Renderer.material = materialchaorealista;
        cama2Renderer.material = materialchaorealista;
        realista = 1;
        mono = 0;
        hyperpop = 0;

    }

    public void TexturasPadrao()
    {
        //funcao para colocar as texturas padrao
        portaRenderer.material.mainTexture = portapadrao;
        parede1Renderer.material = materialparedepadrao;
        parede2Renderer.material = materialparedepadrao;
        parede3Renderer.material = materialparedepadrao;
        parede4Renderer.material = materialparedepadrao;
        parede5Renderer.material = materialparedepadrao;
        parede6Renderer.material = materialparedepadrao;
        parede7Renderer.material = materialparedepadrao;
        parede8Renderer.material = materialparedepadrao;
        chaoRenderer.material = materialchaopadrao;
        forro2Renderer.material = forrocamapadrao;
        forroRenderer.material = forrocamapadrao;
        cama1Renderer.material = materialchaopadrao;
        cama2Renderer.material = materialchaopadrao;
        realista = 0;
        mono = 0;
        hyperpop = 0;
    }

    public void TexturasMono()
    {
        //funcao para colocar as texturas mono
        portaRenderer.material.mainTexture = portamono;
        parede1Renderer.material = materialparedemono;
        parede2Renderer.material = materialparedemono;
        parede3Renderer.material = materialparedemono;
        parede4Renderer.material = materialparedemono;
        parede5Renderer.material = materialparedemono;
        parede6Renderer.material = materialparedemono;
        parede7Renderer.material = materialparedemono;
        parede8Renderer.material = materialparedemono;
        chaoRenderer.material = materialchaomono;
        forro2Renderer.material = forrocamamono;
        forroRenderer.material = forrocamamono;
        cama1Renderer.material = materialchaomono;
        cama2Renderer.material = materialchaomono;
        mono = 1;
        realista = 0;
        hyperpop = 0;
    }

    public void Texturashyperpop()
    {
        //funcao para colocar as texturas hyperpop
        portaRenderer.material.mainTexture = portahyperpop;
        parede1Renderer.material = materialparedehyperpop;
        parede2Renderer.material = materialparedehyperpop;
        parede3Renderer.material = materialparedehyperpop;
        parede4Renderer.material = materialparedehyperpop;
        parede5Renderer.material = materialparedehyperpop;
        parede6Renderer.material = materialparedehyperpop;
        parede7Renderer.material = materialparedehyperpop;
        parede8Renderer.material = materialparedehyperpop;
        chaoRenderer.material = materialchaohyperpop;
        forro2Renderer.material = forrocamahyperpop;
        forroRenderer.material = forrocamahyperpop;
        cama1Renderer.material = materialchaohyperpop;
        cama2Renderer.material = materialchaohyperpop;
        mono = 0;
        realista = 0;
        hyperpop = 1;
    }

    public void TravarControle(bool estado)
    {
        travarCamera = estado;
        moveble = !estado;
    }
}


