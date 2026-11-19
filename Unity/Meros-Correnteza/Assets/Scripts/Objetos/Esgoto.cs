using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using FMODUnity;
public class Esgoto : MonoBehaviour
{
    [SerializeField] private ParticleSystem esgotoParticleSystem;
    [SerializeField] private float mercurioTime;
    [SerializeField] StudioEventEmitter mercurio; //som do mercúrio sendo jorrado
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        var main = esgotoParticleSystem.main;
        main.startDelay = mercurioTime;
        StartCoroutine(MercurioJorra(mercurioTime));
    }
    public float GetTime()
    {
        return mercurioTime;
    }
     private IEnumerator MercurioJorra(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        mercurio.Play();
    }
}
