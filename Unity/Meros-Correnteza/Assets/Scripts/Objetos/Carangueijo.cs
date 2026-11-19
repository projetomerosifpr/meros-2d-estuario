using UnityEngine;

public class Carangueijo : MonoBehaviour
{
    [SerializeField] private float genChance;
    [SerializeField] private  Vector2 rangeMin;
    [SerializeField] private Vector2 rangemax;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        float luckuy = Random.Range(1, 101);
        if (luckuy <= genChance)
        {
            gameObject.SetActive(false);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = Random.value < 0.5f ? 1 : -1;
        float randomX = Random.Range(rangeMin.x, rangemax.x);
        float randomY = Random.Range(rangeMin.y, rangemax.y);
        Vector3 newPos = new Vector3(randomX, randomY, 0);
        transform.position = transform.position + newPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    // Verifica se o outro objeto também tem o script "Carangueijo"
    if (other.TryGetComponent<Carangueijo>(out Carangueijo outroCarangueijo))
    {
        Debug.Log("Encostou em outro caranguejo!");
        Destroy(other.gameObject);
    }
}
}
