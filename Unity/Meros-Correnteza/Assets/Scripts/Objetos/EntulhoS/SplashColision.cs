using UnityEngine;

public class SplashColision : MonoBehaviour
{
   private GameObject splashUI; // Imagem de splash no canvas
   private SplashAnim splashAnim;
   private EntulhoSprite entulhoSprite;
   private MercurioSplash mercurioSplash;
    public void OnSplash()
    {
        mercurioSplash = GetComponentInChildren<MercurioSplash>();
        entulhoSprite = GetComponentInChildren<EntulhoSprite>();
        splashAnim = splashUI.GetComponent<SplashAnim>();
        Debug.Log(splashAnim);
        splashUI.SetActive(false); // Desativa o splash
        //Debug.Log("Change colors "+entulhoSprite.CorSprite());
        Debug.Log(entulhoSprite);
        splashAnim.CorTinta(entulhoSprite == null ? mercurioSplash.CorSprite() : entulhoSprite.CorSprite());
        splashUI.SetActive(true); // Ativa o splash
    }
    public void Init(GameObject splash)
    {
        splashUI = splash;
    }
}
