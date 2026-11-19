using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using FMODUnity;
using UnityEngine.TerrainTools;

public class PlayerVidas : MonoBehaviour
{
    [SerializeField] private int vidaMax; // Vida maxima do player
    private int vidaAtual; // Vida atual
    [SerializeField] private int pontosHealMax = 3; // Quantidade máxima do medidor de "regeneração"
    // Jogador pega pontos e o valor de "regeneração" aumenta até o máximo, então o plyaer recupera vida
    private int pontosHeal = 0; // Valor atual de regeneração
    private bool imortal = false; // Player pode tomar dano?
    private Animator playerAnim;
    private UI_Manager placar; // Script do placar
    [SerializeField] StudioEventEmitter pontos,entulhos,tinta,pesca,rede; // som dos pontos, dos entulhos, das tintas, etc

    void Start()
    {
        vidaAtual = vidaMax;
        playerAnim = GetComponent<Animator>();
        placar = FindAnyObjectByType<UI_Manager>();
        placar.VidasManager().Init(vidaMax);
    }
    public void MudarVida(int vidaMod) // Altera a vida do player
    {
        if (!imortal) // Verifica se ele pode tomar dano
        {
            vidaAtual += vidaMod; // Altera a vida
            placar.AtualizarVida(vidaAtual); // Altera a vida no UI
            placar.VidasManager().SetVida(vidaMax, vidaAtual);
            if (vidaAtual <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            if (vidaMod < 0) // Quando o player toma dano, deixa ele imortal
            {
                StartCoroutine(Imortal());
            }
        }
    }
    private IEnumerator Imortal() // Corrotina do player imotal
    {
        float tempoImortal = 2; // Tempo que fica imortal
        imortal = true; // Player não pode mais tomar dano
        playerAnim.SetBool("isFlashed", true); // Parametro de animação
        yield return new WaitForSeconds(tempoImortal);
        imortal = false;
        playerAnim.SetBool("isFlashed", false);
    }
    public void HealPoints(int pontos) // Função de "regeneração"
    {
        pontosHeal += pontos;
        if (pontosHeal >= pontosHealMax) // Quando os pontosheal atinge seu máximo
        {
            pontosHeal = 0; // Reseta os pontos
            if (vidaAtual < vidaMax) // Aumenta a vida se ela não estiver no máximo
            {
                MudarVida(1);
            }
        }
        placar.HealPoints(pontosHeal); // Altera o HealPoints no placar
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Entulhos"))
        {
            EntulhoColider entulhoColider = other.gameObject.GetComponent<EntulhoColider>();
            string tipoEntulho = entulhoColider.GetTipo();
            switch(tipoEntulho)
            {
                case "entulho":
                    MudarVida(entulhoColider.GetDano());
                    entulhos.Play();
                    break;
                case "ponto":
                    placar.AtualizarPontos(1);
                    HealPoints(1);
                    pontos.Play();
                    break;
                case "splash":
                    other.gameObject.GetComponent<SplashColision>().OnSplash();
                    tinta.Play();
                    break;
                case "rede":
                    rede.Play();
                    other.gameObject.GetComponent<PrendeCollider>().OnPrender();
                    break;
                case "anzolB":
                    MudarVida(entulhoColider.GetDano());
                    Debug.Log("Anzol do bom "+entulhoColider.GetDano());
                    pesca.Play();
                    return;
                case "anzolM":
                    MudarVida(-vidaMax);
                    Debug.Log("Anzol do mal");
                    pesca.Play();
                    return;
                case "mercurio":
                    MudarVida(entulhoColider.GetDano());
                    other.gameObject.GetComponent<SplashColision>().OnSplash();
                    Debug.Log("Mec");
                    return;
            }
            //Debug.Log("Gayl");
            Destroy(other.gameObject);
        }
    }
}
