using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmisAnimate : MonoBehaviour
{
    [Header("!!Use Standard Shader (No sprites)!!")]

    [Header("Material1")]
    public bool AnimateEmis = false;
    public float Speed = 0f;
    private Color TargetColor;
    public bool goingUp = false;

    private Material rendererMat1;

    // Use this for initialization
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)//if it found a renderer
        {
            rendererMat1 = rend.material;//get material 0 from renderer
            Material[] mats = rend.materials;//gets array of all materials
            TargetColor = rendererMat1.GetColor("_EmissionColor");
        }
        else
            Debug.LogError("EmisAnimate: NO RENDERER ON " + gameObject.name, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (AnimateEmis)
        {
            if (rendererMat1 != null)
            {
                Color emis = rendererMat1.GetColor("_EmissionColor");
                if (goingUp)
                {
                    if (emis.r < TargetColor.r)
                    {
                        emis.r += Speed * Time.deltaTime;
                        if (emis.r > TargetColor.r)
                            emis.r = TargetColor.r;
                    }
                    if (emis.g < TargetColor.g)
                    {
                        emis.g += Speed * Time.deltaTime;
                        if (emis.g > TargetColor.g)
                            emis.g = TargetColor.g;
                    }
                    if (emis.b < TargetColor.b)
                    {
                        emis.b += Speed * Time.deltaTime;
                        if (emis.b > TargetColor.b)
                            emis.b = TargetColor.b;
                    }
                }
                else
                {
                    if (emis.r > 0f)
                    {
                        emis.r -= Speed * Time.deltaTime;
                        if (emis.r < 0f)
                            emis.r = 0f;
                    }

                    if (emis.g > 0f)
                    {
                        emis.g -= Speed * Time.deltaTime;
                        if (emis.g < 0f)
                            emis.g = 0f;
                    }

                    if (emis.b > 0f)
                    {
                        emis.b -= Speed * Time.deltaTime;
                        if (emis.b < 0f)
                            emis.b = 0f;
                    }
                }
                if (goingUp && emis.r == TargetColor.r && emis.g == TargetColor.g && emis.b == TargetColor.b)
                    goingUp = !goingUp;
                if (!goingUp && emis.r == 0f && emis.g == 0f && emis.b == 0f)
                    goingUp = !goingUp;
                rendererMat1.SetColor("_EmissionColor", emis);
                GetComponent<Renderer>().UpdateGIMaterials();
            }
            else
                Debug.LogError("EmisAnimate: Renderer Material 1 missing on " + gameObject.name, this);
        }
    }
}
