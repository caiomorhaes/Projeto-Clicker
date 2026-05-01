using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public GameObject textoPrefab;

    public int valorBase = 1;
    public int multiplicador = 1;
    public int multiplicadorClasse = 1;
    public int multiplicadorCiclo = 1;
    public Transform textspaw;
    private HUDManager HUDmanager;
     
    void Start()
    {
        HUDmanager = FindObjectOfType<HUDManager>();
    }

    void OnMouseDown()
    {
        if (HUDmanager.hudprincipaloneoff == true)
        {
            Vector3 pos = transform.position;
            pos.y += 3f;

            GameObject textoObj = Instantiate(textoPrefab, textspaw.position, Quaternion.identity);

            int valorFinal = valorBase * multiplicador * multiplicadorCiclo * multiplicadorClasse;

            TextoFlutuante tf = textoObj.GetComponent<TextoFlutuante>();
            tf.DefinirValor(valorFinal);
        }
    }
}