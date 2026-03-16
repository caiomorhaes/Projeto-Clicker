using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform _transform;
    public Transform cameraTransform;

    Vector2 rotacaoMouse;
    public int sensibilidade;
    public float velocidade = 5.0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
      

    }
}
