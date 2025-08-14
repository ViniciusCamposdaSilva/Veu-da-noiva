using UnityEngine;

public class Porta : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public bool hasKey = false;

    // Variáveis para destacar a porta
    [SerializeField] private GameObject objetoParaDestacar;
    private Color corDestaque = Color.yellow;
    private static Renderer rendererDoObjeto;
    private static bool destaqueFeito = false;
    private static Color[] coresOriginais;

    // Variável para chamar a tela final
    [SerializeField] private FinalizarDemo _finalizarDemo;

    private void Start()
    {
        if (objetoParaDestacar != null && rendererDoObjeto == null)
        {
            rendererDoObjeto = objetoParaDestacar.GetComponent<Renderer>();

            // Salva a cor original de cada material
            Material[] materiais = rendererDoObjeto.materials;
            coresOriginais = new Color[materiais.Length];
            for (int i = 0; i < materiais.Length; i++)
            {
                coresOriginais[i] = materiais[i].color;
            }
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (hasKey == false)
        {
            Debug.Log("Abriu a porta");
            _finalizarDemo.ShowEndScreen();
        }
        else
        {
            Debug.Log("Ainda precisa da chave");
        }
        return true;
    }

    public void AtivarDestaque()
    {
        if (objetoParaDestacar == null) return;

        Renderer r = objetoParaDestacar.GetComponent<Renderer>();
        Material[] materiais = r.materials;

        for (int i = 0; i < materiais.Length; i++)
        {
            materiais[i].color = corDestaque;
        }

        destaqueFeito = true;
    }

    public static void RemoverDestaque()
    {
        if (rendererDoObjeto == null || coresOriginais == null) return;

        Material[] materiais = rendererDoObjeto.materials;

        for (int i = 0; i < materiais.Length; i++)
        {
            materiais[i].color = coresOriginais[i];
        }

        destaqueFeito = false;
    }
}
