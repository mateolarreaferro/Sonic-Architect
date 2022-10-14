using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedPlay : MonoBehaviour
{
    [SerializeField] private float _delay = 1f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(_delay);
        this.GetComponent<AudioSource>().Play();
    }
}