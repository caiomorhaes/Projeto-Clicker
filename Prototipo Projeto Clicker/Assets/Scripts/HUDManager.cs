using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("Referências das HUDs")]
    public GameObject hudPrincipal;
    public GameObject hudSecundaria;
    public GameObject hudMenu;
    public GameObject hudconfig;

    public bool hudprincipaloneoff;
    public bool hudsecundariaoneoff;
    public bool hudmenuoneoff;
    public bool hudconfigoneoff;

    [Header("Referência do Player")]
    public Player player;
    public Camera playerCamera;

    [Header("Botões")]
    public Button botãoinicio;
    public Button botãosair;
    public Button botãoconfig;
    public Button botãovoltar;

    public bool Upgrade;

    [Header("Sliders")]
    public Slider slidersensi;
    public Slider slideFOV;

    [Header("Textos (TMP)")]
    public TextMeshProUGUI textoFOV;
    public TextMeshProUGUI textoSens;

    void Start()
    {
        // Estado inicial
        hudMenu.SetActive(true);
        hudPrincipal.SetActive(false);
        hudSecundaria.SetActive(false);
        hudconfig.SetActive(false);

        hudprincipaloneoff = false;
        hudsecundariaoneoff = false;
        hudmenuoneoff = true;
        hudconfigoneoff = false;

        Upgrade = false;

        // Camera
        playerCamera = Camera.main;

        // Valores iniciais
        slideFOV.value = playerCamera.fieldOfView;
        slidersensi.value = player.sensibilidade;

        // Atualiza textos na inicialização
        AtualizarTextoFOV(slideFOV.value);
        AtualizarTextoSens(slidersensi.value);

        // Eventos sliders
        slideFOV.onValueChanged.AddListener(MudarFOV);
        slidersensi.onValueChanged.AddListener(MudarSensibilidade);

        // Botões
        botãoinicio.onClick.AddListener(IniciarJogo);
        botãoconfig.onClick.AddListener(Configuracoes);
        botãosair.onClick.AddListener(SairDoJogo);
        botãovoltar.onClick.AddListener(VoltarMenuPrincipal);
    }

    void Update()
    {
        if (hudmenuoneoff)
        {
            player.TravarControle(true);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            AlternarHUD();
        }
    }

    // =========================
    // BOTÕES
    // =========================

    void IniciarJogo()
    {
        hudMenu.SetActive(false);
        hudPrincipal.SetActive(true);
        hudconfig.SetActive(false);

        hudprincipaloneoff = true;
        hudmenuoneoff = false;
        hudconfigoneoff = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Upgrade = false;

        if (player != null)
        {
            player.TravarControle(false);
        }
    }

    void Configuracoes()
    {
        hudMenu.SetActive(false);
        hudconfig.SetActive(true);

        hudconfigoneoff = true;

        // Atualiza sliders e textos
        slideFOV.value = playerCamera.fieldOfView;
        slidersensi.value = player.sensibilidade;

        AtualizarTextoFOV(slideFOV.value);
        AtualizarTextoSens(slidersensi.value);
    }

    void SairDoJogo()
    {
        Application.Quit();
    }

    // =========================
    // SLIDERS
    // =========================

    void MudarFOV(float valor)
    {
        playerCamera.fieldOfView = valor;
        AtualizarTextoFOV(valor);
    }

    void MudarSensibilidade(float valor)
    {
        player.sensibilidade = valor;
        AtualizarTextoSens(valor);
    }

    // =========================
    // TEXTOS
    // =========================

    void AtualizarTextoFOV(float valor)
    {
        textoFOV.text = "FOV: " + valor.ToString("F0");
    }

    void AtualizarTextoSens(float valor)
    {
        textoSens.text = "Sens: " + valor.ToString("F0");
    }

    // =========================
    // HUD SECUNDÁRIA
    // =========================

    void AlternarHUD()
    {
        if (hudprincipaloneoff)
        {
            hudPrincipal.SetActive(false);
            hudSecundaria.SetActive(true);

            hudsecundariaoneoff = true;
            hudprincipaloneoff = false;

            Upgrade = true;
        }
        else if (hudsecundariaoneoff)
        {
            hudPrincipal.SetActive(true);
            hudSecundaria.SetActive(false);

            hudprincipaloneoff = true;
            hudsecundariaoneoff = false;

            Upgrade = false;
        }

        if (player != null)
        {
            player.TravarControle(hudsecundariaoneoff);
        }
    }
    
    void VoltarMenuPrincipal()
    {
        hudMenu.SetActive(true);
        hudPrincipal.SetActive(false);
        hudSecundaria.SetActive(false);
        hudconfig.SetActive(false);
        hudprincipaloneoff = false;
        hudsecundariaoneoff = false;
        hudmenuoneoff = true;
        hudconfigoneoff = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Upgrade = false;
        if (player != null)
        {
            player.TravarControle(true);
        }
    }
}