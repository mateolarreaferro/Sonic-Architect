using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    positiveX, negativeX, positiveZ, negativeZ
}


public class Slider : MonoBehaviour
{
    [Header("Direction")]
    [Tooltip("Select the desired direction")]
    public Direction direction;


    // Variables to set the range of movement of the slider
    [SerializeField] float _min, _max;

    // Variable that will go to Csound
    float sliderDistance;

    // Lock X & Z
    [SerializeField] float xOriginalPos, zOrigialPos, yOriginalPos;

    // Csound
    [SerializeField] CsoundUnity csound;
    [SerializeField] string [] parameters;
    [SerializeField] float [] _offset;



    // Property to update value only when it changes (opposed to every frame)
    public float SliderDistance
    {
        get
        {
            return sliderDistance;
        }
        set
        {
            if (sliderDistance == value) return;
            sliderDistance = value;
            if (OnVariableChange != null)
            {
                OnVariableChange(sliderDistance);

                for (int i = 0; i < parameters.Length; i++)
                {
                    csound.SetChannel(parameters[i], sliderDistance * _offset[i]);
                }
                 
            }   
            
        }
    }

    public delegate void OnVariableChangeDelegate(float newVal);
    public event OnVariableChangeDelegate OnVariableChange;




    private void Start()
    {
        csound = GameObject.Find("Object").GetComponent<CsoundUnity>();

        // Subscribing 
        OnVariableChange += VariableChangeHandler;

    }

    private void VariableChangeHandler(float newVal)
    { }


    // Update is called once per frame
    void Update()
    {
        float xValue = transform.localPosition.x;
        float zValue = transform.localPosition.z;

        switch (direction)
        {

            case Direction.positiveX:
                
                SliderDistance = SliderValue(xValue, _min, _max, true);
                break;

            case Direction.negativeX:
               
                SliderDistance = SliderValue(xValue, _max, _min, true);
                break;
            
            case Direction.positiveZ:
                
                SliderDistance = SliderValue(zValue, _min, _max, false);
                break;

            case Direction.negativeZ:
                
                SliderDistance = SliderValue(zValue, _max, _min, false);
                break;

        }

    }


    float SliderValue(float axis, float min, float max, bool xDisplacement)
    {
        float rawValue = Mathf.Clamp(axis, min, max); // clamps slider values into the permitted range

        if (xDisplacement)
        {
            transform.localPosition = new Vector3(rawValue, yOriginalPos, zOrigialPos); // physical movement of slider
        }
        else
        {
            transform.localPosition = new Vector3(xOriginalPos, yOriginalPos, rawValue); // physical movement of slider
        }

        return 0.01f + Mathf.InverseLerp(_min, _max, rawValue);

    }
   
}
