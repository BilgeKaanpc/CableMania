using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWire : MonoBehaviour
{
    [SerializeField] GameManager _GameManager;
    [SerializeField] ParticleSystem[] brokeEffects;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("socket"))
        {
            brokeEffects[0].gameObject.SetActive(true);
            brokeEffects[0].Play();
            brokeEffects[1].gameObject.SetActive(true);
            brokeEffects[1].Play();
            _GameManager.Lose();
        }
    }
}
