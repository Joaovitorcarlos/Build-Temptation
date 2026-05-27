using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MovimentoSoldado : NetworkBehaviour
{
    //private float movimentoHorizontal;
    //private float movimentoVertical;
    //public float velocidadeMov = 5f;
    //public float velocidadeRot = 200f;
    //private Animator anim;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject arma;
    [SerializeField] private GameObject bala;
    [SerializeField] private float velocidade = 5f;
    [SerializeField] private float velocidadeRotacao = 100f;
    private float movimentoHorizontal;
    private float movimentoVertical;


    public override void FixedUpdateNetwork()
    {
        if (GetInput(out InputNetwork input))
        {
            if (input.atirando)
            {
                var balaObj = Instantiate(bala, arma.transform.position, arma.transform.rotation);
                Destroy(balaObj, 4f);
                anim.SetBool("podeAtirar", true);
            }
            else
            {
                anim.SetBool("podeAtirar", false);
                movimento(input);
            }
        }
    }
    void movimento(InputNetwork input)
    {
        Vector3 direcao = new Vector3(input.horizontal, 0f, input.vertical).normalized;
        if (direcao.magnitude >= 0.1f)
        {
            //movimentar o personagem
            transform.Translate(transform.forward * input.vertical * velocidade * Runner.DeltaTime, Space.World);
            //rotacionar o personagem
            transform.Rotate(transform.up * input.horizontal * velocidadeRotacao * Runner.DeltaTime);
            anim.SetBool("podeCorrer", true);
        }

        else
        {
            anim.SetBool("podeCorrer", false);
        }
    }





    //void Update()
    //{
    //    movimento();
    //}

    //void movimento()
    //{
    //    movimentoHorizontal = Input.GetAxis("Horizontal");
    //    movimentoVertical = Input.GetAxis("Vertical");

    //    Vector3 direcao = 
    //        new Vector3(movimentoHorizontal, 0, movimentoVertical).normalized;

    //    if (direcao.magnitude >= 0.1f)
    //    {
    //        //movimento vertical
    //        transform.Translate(
    //            transform.forward *
    //            movimentoVertical *
    //            Time.deltaTime * velocidadeMov, Space.World);

    //        //rotacionar o personagem
    //        transform.Rotate(0, movimentoHorizontal *
    //            Time.deltaTime *
    //            velocidadeRot, 0);
    //        anim.SetBool("podeCorrer", true);
    //    }
    //    else
    //    {
    //        anim.SetBool("podeCorrer", false);
    //    }


    //}
}
