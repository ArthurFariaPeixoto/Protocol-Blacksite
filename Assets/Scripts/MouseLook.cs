using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //Valor de sensibilidade da camera
    public float sensitivity = 1f;

    //Valor do efeito de smooth
    public float smoothing = 1f;

    //Posicao do mouse
    private float posMouseX;

    //Posicao ja manipulada
    private float smoothedMousePos;

    //Posicao atual da camera
    private float currentLookingPos;

    private void Start()
    {
        //Travar cursor no centro da tela e o  esconder
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Inputs do mouse
        getInput();

        //Aplica sensibilidade e efeito de smooth na camera
        modifyInput();

        //Movimenta o eixo de visao do player
        movePlayer();
    }

    void getInput()
    {
        //Recebe valor do eixo X do mouse
        posMouseX = Input.GetAxisRaw("Mouse X");
    }

    void modifyInput()
    {
        //Aplica sensibilidade e smooth na posicao da camera
        posMouseX *= sensitivity * smoothing;
        smoothedMousePos = Mathf.Lerp(smoothedMousePos, posMouseX, 1f / smoothing);
    }

    void movePlayer()
    {
        //Atualiza posicao da camera de acordo com os valores ja modificados
        currentLookingPos += smoothedMousePos;
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);
    }
}
