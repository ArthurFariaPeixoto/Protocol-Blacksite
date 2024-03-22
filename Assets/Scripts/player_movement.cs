using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    //Velocidade do jogador
    public float playerSpeed = 10f;

    //Controller do jogador
    private CharacterController playerController;

    //Posicao  do eixo X, Y, Z do jogador registrada via input (teclas)
    private Vector3 inputPositionPlayer;

    //Posicao do eixo X, Y, Z do jogador apos manipulacoes
    private Vector3 MovementPositionPlayer;

    //variavel de animacao da camera
    public Animator camAnimation;
    private bool isWalking;

    //Velocidade da gravidade
    private float gravity = -10f;
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
        checkForHeadBob();
        camAnimation.SetBool("isWalking", isWalking);
    }

    void getInput()
    {
        //Le posicao  X, Z do jogador
        inputPositionPlayer = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        inputPositionPlayer.Normalize();
        inputPositionPlayer = transform.TransformDirection(inputPositionPlayer);

        MovementPositionPlayer = (inputPositionPlayer * playerSpeed) + (Vector3.up * gravity);
    }

    void movePlayer()
    {
        //Movimenta o jogador para o X, Y, Z respectivos 
        playerController.Move(MovementPositionPlayer * Time.deltaTime);
    }

    void checkForHeadBob()
    {
        if (playerController.velocity.magnitude > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
}
