using UnityEngine;

public class Pescador : MonoBehaviour
{
    
    [SerializeField] private GameObject linha;
    [SerializeField] private int goodFisherChance; // Chance do pescador ser bom em %
    [SerializeField] private BarcoSprite barcoSprite;
    private float linhaScaleY;
    private float linhaHigh;
    [SerializeField] private GameObject anzol;
    [SerializeField] private Vector2 anzolRangeY = new Vector2();
    [SerializeField] private float anzolYOffSet;
    void Start()
    {
        linhaScaleY = Random.Range(anzolRangeY.x, anzolRangeY.y);
        linha.transform.localScale = new Vector3(linha.transform.localScale.x, linhaScaleY, linha.transform.localScale.z);
        linhaHigh = linha.GetComponent<SpriteRenderer>().bounds.size.y;
        linha.transform.position = new Vector2(linha.transform.position.x, linha.transform.position.y-linhaHigh/2);
        anzol.transform.position = new Vector2(anzol.transform.position.x, linha.transform.position.y-(linhaHigh/2)-anzolYOffSet);
        DefinePescador(goodFisherChance);
    }

    private void DefinePescador(int pescChance)
    {
        int lucky = Random.Range(1, 101);
        EntulhoColider anzolScript = anzol.GetComponent<EntulhoColider>();
        if (lucky <= pescChance)
        {
            //Pescador bom
            barcoSprite.SetPescadorSprite("bom");
            anzolScript.SetTipo("anzolB");
            //anzolScript.SetDano(-2);
        }else
        {
            //Pescador ruim
            barcoSprite.SetPescadorSprite("ruim");
            anzolScript.SetTipo("anzolM");
        }
    }

    // Update is called once per frame
}
