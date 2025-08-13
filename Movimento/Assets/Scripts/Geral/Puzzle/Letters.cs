using UnityEngine;

public class Letters : MonoBehaviour
{
    //variaveis para o controle de cartas totais e atuais
    public static int quantidadeTotalCartas = 8;
    public static int quantidadeAtualCartas = 0;

    // variaveis para o destque da mesa:
    [SerializeField] private GameObject objetoParaDestacar;
    private Color corDestaque = Color.yellow;
    private static Renderer rendererDoObjeto;
    private static bool destaqueFeito = false;
    private static Color corOriginal;

    private void Start()
    {
        if (objetoParaDestacar != null && rendererDoObjeto == null)
        {
            rendererDoObjeto = objetoParaDestacar.GetComponent<Renderer>();
            corOriginal = rendererDoObjeto.material.color;
        }
    }

    private void OnDestroy()
    {
        quantidadeAtualCartas++;
        Debug.Log("Pegou uma carta");

        if (quantidadeAtualCartas >= quantidadeTotalCartas && !destaqueFeito)
        {
            Debug.Log("Todas as cartas foram obtidas");
            Renderer r = objetoParaDestacar.GetComponent<Renderer>();
            r.material.color = corDestaque;
            destaqueFeito = true;
        }
    }
    public static void RemoverDestaque()
    {
        if (rendererDoObjeto != null)
        {
            rendererDoObjeto.material.color = corOriginal;
            destaqueFeito = false;
        }
    }
}