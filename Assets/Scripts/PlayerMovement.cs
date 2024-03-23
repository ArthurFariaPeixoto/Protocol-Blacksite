using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Velocidade do jogador
    public float playerSpeed = 10f;

    //Momentum da movimentacao do jogador
    public float momentumDamping = 6f;

    //Velocidade da gravidade
    private float gravity = -10f;

    //Controller do jogador
    private CharacterController playerController;

    //Posicao  do eixo X, Y, Z do jogador registrada via input (teclas)
    private Vector3 inputPositionPlayer;

    //Posicao do eixo X, Y, Z do jogador apos manipulacoes
    private Vector3 MovementPositionPlayer;

    //variavel de animacao da camera
    public Animator camAnimation;
    private bool isWalking;


    void Start()
    {
        //Iniciando Controller
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Leitor de inputs via teclado
        getInput();

        //Atualiza Posicao do jogador
        movePlayer();

        //Verifica se player ta andando
        camAnimation.SetBool("isWalking", isWalking);
    }

    // void getInput()
    // {
    //     //Le posicao  X, Z do jogador
    //     if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
    //     {
    //         inputPositionPlayer = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
    //         inputPositionPlayer.Normalize();
    //         inputPositionPlayer = transform.TransformDirection(inputPositionPlayer);

    //         isWalking = true;
    //     }
    //     else
    //     {
    //         inputPositionPlayer = Vector3.Lerp(inputPositionPlayer, Vector3.zero, momentumDamping * Time.deltaTime);
    //         isWalking = false;
    //     }

    //     MovementPositionPlayer = (inputPositionPlayer * playerSpeed) + (Vector3.up * gravity);
    // }

    void getInput()
    {
        // Verifica se alguma das teclas de movimentação foi pressionada
        bool isMoving = Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f;

        if (isMoving)
        {
            inputPositionPlayer = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputPositionPlayer.Normalize();
            inputPositionPlayer = transform.TransformDirection(inputPositionPlayer);

            isWalking = true;
        }
        else
        {
            inputPositionPlayer = Vector3.Lerp(inputPositionPlayer, Vector3.zero, momentumDamping * Time.deltaTime);
            isWalking = false;
        }

        MovementPositionPlayer = (inputPositionPlayer * playerSpeed) + (Vector3.up * gravity);
    }

    void movePlayer()
    {
        //Movimenta o jogador para o X, Y, Z respectivos 
        playerController.Move(MovementPositionPlayer * Time.deltaTime);
    }
}
