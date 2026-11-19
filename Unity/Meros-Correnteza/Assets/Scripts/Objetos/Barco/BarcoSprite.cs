using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class BarcoSprite : MonoBehaviour
{
    [SerializeField] private Sprite pescadorBom;
    [SerializeField] private Sprite pescadorRuim;
    
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer);
    }
    public void SetPescadorSprite(string pesc)
    {
        Debug.Log(spriteRenderer);
        // 1 pescador bom, 2 pescador ruim
        switch(pesc)
        {
            case "bom":
                spriteRenderer.sprite = pescadorBom;
                break;
            case "ruim":
                spriteRenderer.sprite = pescadorRuim;
                break;
        }
    }
}
