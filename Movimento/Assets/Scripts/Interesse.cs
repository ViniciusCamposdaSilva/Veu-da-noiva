using UnityEngine;
using System.Collections;

public class Interesse : MonoBehaviour
{
    [Header("Configura��es")]
    [SerializeField] private float interesseMaximo = 100f;
    [SerializeField] private float interesseMinimo = 1f;
    [SerializeField] private float taxaDecaimentoPassivo = 0.1f; // Decaimento por segundo

    private float interesseAtual;
    private bool jogoAtivo = true;

    void Start()
    {
        interesseAtual = interesseMaximo;
        StartCoroutine(DecaimentoPassivo());
    }

    // Decaimento passivo com o tempo
    private IEnumerator DecaimentoPassivo()
    {
        while (jogoAtivo)
        {
            yield return new WaitForSeconds(1f);
            ModificarInteresse(-taxaDecaimentoPassivo);
        }
    }

    // Modifica o interesse (positivo ou negativo)
    public void ModificarInteresse(float quantidade)
    {
        if (!jogoAtivo) return;

        interesseAtual = Mathf.Clamp(interesseAtual + quantidade, interesseMinimo, interesseMaximo);
        Debug.Log($"Interesse atual: {interesseAtual:F1}");

        // Verifica derrota
        if (interesseAtual <= interesseMinimo)
        {
            StartCoroutine(SequenciaFimDeJogo());
        }
    }

    // Sequ�ncia de fim de jogo
    private IEnumerator SequenciaFimDeJogo()
    {
        jogoAtivo = false;

        Debug.Log("In�meros questionamentos surgindo...");
        yield return new WaitForSeconds(2f);

        Debug.Log("Tela bugando...");
        yield return new WaitForSeconds(1.5f);

        Debug.Log("Vazio preto...");
        Debug.Log("� melhor eu parar por aqui");
        // Aqui voc� implementaria a l�gica visual real
    }
}