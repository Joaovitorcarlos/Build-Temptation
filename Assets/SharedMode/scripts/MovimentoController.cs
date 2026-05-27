using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MovimentoController : NetworkBehaviour
{
    private CharacterController characterController;
    public float velocidade = 5f;
    public Animator animator;

    [Networked] public int Score { get; set; }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_AddScore(int points)
    {
        Score += points;
    }


    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    public override void FixedUpdateNetwork()
    {
        if (HasStateAuthority)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direcao = new Vector3(horizontal, 0, vertical);
            if (direcao.magnitude > 0.1f)
            {
                #region 1ª forma de movimentação
                ////movimento do personagem
                //characterController.Move(direcao * velocidade * Runner.DeltaTime);
                ////rotacao do personagem
                //transform.rotation = Quaternion.LookRotation(direcao);
                #endregion

                #region 2ª forma de movimentação
                //movimento do personagem
                characterController.Move(
                    transform.forward * vertical * velocidade * Runner.DeltaTime);
                //rotacao do personagem
                float velocidadeRotacao = velocidade * 50f;
                transform.Rotate(new Vector3(0, 
                    horizontal * velocidadeRotacao * Runner.DeltaTime, 
                    0));
                #endregion

                //animacao do personagem
                animator.SetBool("podeAndar", true);
            }
            else
            {
                animator.SetBool("podeAndar", false);
            }


        }
    }
}
