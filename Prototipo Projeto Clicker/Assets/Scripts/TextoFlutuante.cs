using UnityEngine;
using TMPro;

public class TextoFlutuante : MonoBehaviour
{
    public float velocidade = 3f;
    public TextMeshPro texto;
    ClickSpawner clickSpawner;

    private void Start()
    {
        clickSpawner = FindFirstObjectByType<ClickSpawner>();
    }
    public void DefinirValor(int valor)
    {
        texto.text = "+" + valor.ToString();
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
        this.GetComponent<TMP_Text>().text =  "+ " + clickSpawner.valorBase * clickSpawner.multiplicador + "";


        transform.Translate(Vector3.up * velocidade * Time.deltaTime);

        Destroy(gameObject, 2f);
    }
}