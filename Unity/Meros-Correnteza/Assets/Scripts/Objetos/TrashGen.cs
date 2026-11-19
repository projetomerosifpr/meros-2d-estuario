using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public enum EntulhosNomes
{
    Entulho,  // 0
    Tinta,  // 1
    Ponto,  // 2
    Rede,  // 3

    Mobilia, // 4
    Pescador, // 5
    Esgoto, //
}
[System.Serializable]
public class ObjectsObstaculos
{
    public EntulhosNomes nome;
    public GameObject objectPrefab;
    public Vector2 tempo;
    public float tempoMin {get; private set;}
    public float tempoMax {get; private set;}
    public void StartObject()
    {
        tempoMin = tempo.x;
        tempoMax = tempo.y;
        if (tempoMin < 0)
        {
            tempoMin = 0;
        }
    }
}

public class TrashGen : MonoBehaviour
{
    [SerializeField] private List<ObjectsObstaculos> objetos;
    [SerializeField] private float genX; // Posição inicial X
    [SerializeField] private Vector2 genYrange = new Vector2(); // Variação de altura (x = min, y = max)
    [SerializeField] private float genYMobilia;
    [SerializeField] private float genYPescador;
    [SerializeField] private float genYEsgoto;
    [SerializeField] private int fase; // Numero da fase
    //[SerializeField] private GameObject[] entulhosPrefabs; // Prefabs dos inimigos flutuantes
    [SerializeField] private GameObject splashUI;
    [SerializeField] private bool mobiliaOn;
    [SerializeField] private bool pescadorOn;
    [SerializeField] private bool esgotOn;
    private float genY;
    void OnEnable()
    {
        foreach(ObjectsObstaculos objeto in objetos)
        {
            objeto.StartObject();
        }
        StartCoroutine(GerarEntulhos());
        StartCoroutine(GerarTintas());
        StartCoroutine(GerarPontos());
        StartCoroutine(GerarRedes());
        if (mobiliaOn)
        {
            StartCoroutine(GerarMobilia());
        }
        if (pescadorOn)
        {
            StartCoroutine(GerarPescador());
        }
        if (esgotOn)
        {
            StartCoroutine(GerarEsgoto());
        }
    }
    private IEnumerator GerarEntulhos() // Corrotina de geração de entulhos
    {
        ObjectsObstaculos entulho;
        entulho = GetObject(EntulhosNomes.Entulho);
        float genTime; // Intervalo de spawn
        while (entulho != null)
        {
            genTime = Random.Range(entulho.tempoMin, entulho.tempoMax); // Define um intervalo aleatorio
            yield return new WaitForSeconds(genTime);
            genY = Random.Range(genYrange.x, genYrange.y); // Define a posição Y
            Instantiate(entulho.objectPrefab, new Vector2(genX, genY), entulho.objectPrefab.transform.rotation); // Instancia a prefab
        }
        Debug.Log("Entulho vazio");
    }
    private IEnumerator GerarTintas()
    {
        ObjectsObstaculos tinta;
        tinta = GetObject(EntulhosNomes.Tinta);
        float genTime; // Intervalo de spawn
        while (tinta != null)
        {
            genTime = Random.Range(tinta.tempoMin, tinta.tempoMax); // Define um intervalo aleatorio
            yield return new WaitForSeconds(genTime);
            genY = Random.Range(genYrange.x, genYrange.y); // Define a posição Y
            GameObject instance = Instantiate(tinta.objectPrefab, new Vector2(genX, genY), tinta.objectPrefab.transform.rotation); // Instancia a prefab
            SplashColision splashColision = instance.GetComponent<SplashColision>();
            splashColision.Init(splashUI);
        }
        Debug.Log("Tinta vazio");
    }
    private IEnumerator GerarPontos() // Corrotina de geração de entulhos
    {
        ObjectsObstaculos ponto;
        ponto = GetObject(EntulhosNomes.Ponto);
        float genTime; // Intervalo de spawn
        while (ponto != null)
        {
            genTime = Random.Range(ponto.tempoMin, ponto.tempoMax); // Define um intervalo aleatorio
            yield return new WaitForSeconds(genTime);
            genY = Random.Range(genYrange.x, genYrange.y); // Define a posição Y
            Instantiate(ponto.objectPrefab, new Vector2(genX, genY), ponto.objectPrefab.transform.rotation); // Instancia a prefab
        }
        Debug.Log("Ponto vazio");
    }
    private IEnumerator GerarRedes() // Corrotina de geração de entulhos
    {
        ObjectsObstaculos rede;
        rede = GetObject(EntulhosNomes.Rede);
        float genTime; // Intervalo de spawn
        while (rede != null)
        {
            genTime = Random.Range(rede.tempoMin, rede.tempoMax); // Define um intervalo aleatorio
            yield return new WaitForSeconds(genTime);
            genY = Random.Range(genYrange.x, genYrange.y); // Define a posição Y
            Instantiate(rede.objectPrefab, new Vector2(genX, genY), rede.objectPrefab.transform.rotation); // Instancia a prefab
        }
        Debug.Log("Entulho vazio");
    }
    private ObjectsObstaculos GetObject(EntulhosNomes alvo)
    {
        foreach (ObjectsObstaculos objeto in objetos)
        {
            if (objeto.nome == alvo)
            {
                return objeto;
            }
        }
        return null;
    }
    private IEnumerator GerarMobilia() // Corrotina de geração de entulhos
    {
        ObjectsObstaculos mobilia;
        mobilia = GetObject(EntulhosNomes.Mobilia);
        float genTime; // Intervalo de spawn
        while (mobilia != null)
        {
            genTime = Random.Range(mobilia.tempoMin, mobilia.tempoMax); // Define um intervalo aleatorio
            Debug.Log(genTime + " Mobilia");
            yield return new WaitForSeconds(genTime);
            genY = Random.Range(genYMobilia-0.2f, genYMobilia+0.2f); // Define a posição Y
            Instantiate(mobilia.objectPrefab, new Vector2(genX, genY), mobilia.objectPrefab.transform.rotation); // Instancia a prefab
        }
        Debug.Log("Mobilia vazio");
    }
    private IEnumerator GerarPescador() // Corrotina de geração de entulhos
    {
        ObjectsObstaculos pescador;
        pescador = GetObject(EntulhosNomes.Pescador);
        float genTime; // Intervalo de spawn
        while (pescador != null)
        {
            genTime = Random.Range(pescador.tempoMin, pescador.tempoMax); // Define um intervalo aleatorio
            Debug.Log(genTime + " Pescador");
            yield return new WaitForSeconds(genTime);
            genY = genYPescador; // Define a posição Y
            Instantiate(pescador.objectPrefab, new Vector2(genX, genY), pescador.objectPrefab.transform.rotation); // Instancia a prefab
        }
        Debug.Log("Pescador vazio");
    }
    private IEnumerator GerarEsgoto() // Corrotina de geração de entulhos
    {
        ObjectsObstaculos esgoto;
        esgoto = GetObject(EntulhosNomes.Esgoto);
        float genTime; // Intervalo de spawn
        while (esgoto != null)
        {
            genTime = Random.Range(esgoto.tempoMin, esgoto.tempoMax); // Define um intervalo aleatorio
            yield return new WaitForSeconds(genTime);
            genY = genYEsgoto; // Define a posição Y
            GameObject instance = Instantiate(esgoto.objectPrefab, new Vector2(genX, genY), esgoto.objectPrefab.transform.rotation); // Instancia a prefab
            SplashColision splashColision = instance.GetComponentInChildren<SplashColision>();
            Debug.Log(splashUI);
            Debug.Log(splashColision);
            splashColision.Init(splashUI);
        }
        Debug.Log("Esgoto vazio");
    }
}
