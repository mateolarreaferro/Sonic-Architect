using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToAmplitude : MonoBehaviour
{
    AudioSource _source;
    float _amplitude;
    float scaleToAmplitude;



    // Property to update value only when it changes (opposed to every frame)
    public float ScaleToAmplitudeGS
    {
        get
        {
            return scaleToAmplitude;
        }
        set
        {
            if (scaleToAmplitude == value) return;
            scaleToAmplitude = value;
            if (OnVariableChange != null)
            {
                OnVariableChange(scaleToAmplitude);
                _source = GetComponent<AudioSource>();
                _source.volume = scaleToAmplitude;
                
            }
        }
    }

    public delegate void OnVariableChangeDelegate(float newVal);
    public event OnVariableChangeDelegate OnVariableChange;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();

        // Subscribing 
        OnVariableChange += VariableChangeHandler;
    }

    private void VariableChangeHandler(float newVal)
    { }

    // Update is called once per frame
    void Update()
    {
        ScaleToAmplitudeGS = transform.localScale.x / 2; // max scale is 2
    }
}
