using UnityEngine;

public class PortalP : MonoBehaviour
{
    public Transform jogador;

    private Vector3 posicaoA = new Vector3(-0.422f, 1.109f, 5.538f); // posição de volta
    private Vector3 posicaoB = new Vector3(31.6f, 1.0f, -71.1f);     // posição do portal

    private bool estaNoDestino = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (jogador != null)
            {
                if (!estaNoDestino)
                {
                    jogador.position = posicaoB; // vai
                    estaNoDestino = true;
                }
                else
                {
                    jogador.position = posicaoA; // volta
                    estaNoDestino = false;
                }
            }
        }
    }
}