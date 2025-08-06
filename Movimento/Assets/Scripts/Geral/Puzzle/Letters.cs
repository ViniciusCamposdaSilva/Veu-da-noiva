using UnityEngine;

public class Letters : MonoBehaviour
{
    //variaveis para o controle de cartas totais e atuais
    public static int quantidadeTotalCartas = 8;
    public static int quantidadeAtualCartas = 0;

    private void OnDestroy()
    {
        quantidadeAtualCartas++;
        Debug.Log("Pegou uma carta");

        if (quantidadeAtualCartas >= quantidadeTotalCartas)
        {
            Debug.Log("Todas as cartas foram obtidas");

        }
    }
}
