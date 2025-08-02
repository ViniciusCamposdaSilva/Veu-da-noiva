using UnityEngine;

public class Letters : MonoBehaviour
{
    
    public static int quantidadeTotalCartas = 1; 

    public static int quantidadeAtualCartas = 0;

    void OnDestroy()
    {
        quantidadeAtualCartas++;
        Debug.Log("Pegou uma carta");

        if (quantidadeAtualCartas >= quantidadeTotalCartas)
        {
            Debug.Log("Todas as cartas foram obtidas");

        }
    }
}
