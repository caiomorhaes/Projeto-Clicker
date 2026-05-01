using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [Header("Referências das HUDs")]
    public GameObject hudPrincipal;
    public GameObject hudSecundaria;
    public bool hudprincipaloneoff;
    public bool hudsecundariaoneoff;

    [Header("Referência do Player")]
    public Player player;


    void Start()
    {
        // Estado inicial

        hudPrincipal.SetActive(true);
        hudprincipaloneoff = true;
        hudSecundaria.SetActive(false);
        hudsecundariaoneoff = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Tecla C pressionada");

            AlternarHUD();
        }
    }

    void AlternarHUD()
    {

        // Alterna as HUDs
        if (hudprincipaloneoff == true)
        {
            hudPrincipal.SetActive(false);
            hudSecundaria.SetActive(true);
            hudsecundariaoneoff = true;
            hudprincipaloneoff = false;
        }
        else if (hudsecundariaoneoff == true)
        {
            hudPrincipal.SetActive(true);
            hudSecundaria.SetActive(false);
            hudprincipaloneoff = true;
            hudsecundariaoneoff = false;
        }
        // Atualiza cursor

        // Trava/destrava player
        if (player != null)
        {
            player.TravarControle(hudsecundariaoneoff);
        }
        else
        {
            Debug.LogWarning("Player não atribuído no HUDManager!");
        }
    }
}