using System;
using System.Collections;
using UnityEngine;

public class EsgotoCollider : MonoBehaviour
{
    private Esgoto esgotoScript;
    [SerializeField] private float timeEnd;
    [SerializeField] private GameObject colliderEsgoto;
    void Start()
    {
       esgotoScript =  GetComponentInChildren<Esgoto>();
        StartCoroutine(StartColision(esgotoScript.GetTime()));
    }
    private IEnumerator StartColision(float time)
    {
        colliderEsgoto.SetActive(false);
        yield return new WaitForSeconds(time);
        colliderEsgoto.SetActive(true);
        StartCoroutine(StopColision(timeEnd));
    }
    private IEnumerator StopColision(float time)
    {
        yield return new WaitForSeconds(time);
        colliderEsgoto.SetActive(false);
    }
}
