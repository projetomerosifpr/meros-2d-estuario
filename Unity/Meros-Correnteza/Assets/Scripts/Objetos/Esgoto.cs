using Unity.VisualScripting;
using UnityEngine;

public class Esgoto : MonoBehaviour
{
    [SerializeField] private ParticleSystem esgotoParticleSystem;
    [SerializeField] private float mercurioTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        var main = esgotoParticleSystem.main;
        main.startDelay = mercurioTime;
    }
    public float GetTime()
    {
        return mercurioTime;
    }
}
