using UnityEngine;
using System.Collections;

namespace RyanDStandard
{
    public class UVAnimate : MonoBehaviour
    {
        [Header("!!Use Standard Shader (No sprites)!!")]

        [Header("Material1")]
        public bool AnimateUV1 = false;
        public float U1Speed = 0f;
        public float V1Speed = 0f;
        [Space(15f)]
        [Space(0f)]
        [Header("Material2")]
        public bool AnimateUV2 = false;
        public float U2Speed = 0f;
        public float V2Speed = 0f;

        private Material rendererMat1;
        private Material rendererMat2;

        // Use this for initialization
        void Start()
        {
            Renderer rend = GetComponent<Renderer>();
            if (rend != null)//if it found a renderer
            {
                rendererMat1 = rend.material;//get material 0 from renderer
                Material[] mats = rend.materials;//gets array of all materials
                if (mats.Length > 1)//if there are more than one material
                {
                    rendererMat2 = mats[1];//find the second material
                }
            }
            else
                Debug.LogError("UVAnimate: NO RENDERER ON " + gameObject.name, this);
        }

        // Update is called once per frame
        void Update()
        {
            if (AnimateUV1)
            {
                if (rendererMat1 != null)
                {
                    Vector2 newOffset = rendererMat1.GetTextureOffset("_MainTex");
                    newOffset += new Vector2(U1Speed, V1Speed) * Time.deltaTime;
                    rendererMat1.SetTextureOffset("_MainTex", newOffset);
                }
                else
                    Debug.LogError("UVAnimate: Renderer Material 1 missing on " + gameObject.name, this);
            }

            if (AnimateUV2)
            {
                if (rendererMat2 != null)
                {
                    Vector2 newOffset = new Vector2(U2Speed, V2Speed) * Time.deltaTime;
                    rendererMat2.SetTextureOffset("_MainTex", newOffset);
                }
                else
                    Debug.LogError("UVAnimate: Renderer Material 2 missing on " + gameObject.name, this);
            }
        }
    }
}