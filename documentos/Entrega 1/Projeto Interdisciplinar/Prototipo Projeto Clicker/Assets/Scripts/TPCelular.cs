using UnityEngine;

public class TeleportZ : MonoBehaviour
{
    Vector3 posicao1 = new Vector3(31.62f, 0.51f, -68.9f);
    Vector3 posicao2 = new Vector3(-0.422f, 1.109f, 5.538f);

    bool noPonto1 = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!noPonto1)
            {
                transform.position = posicao1;
                noPonto1 = true;
            }
            else
            {
                transform.position = posicao2;
                noPonto1 = false;
            }
        }
    }
}