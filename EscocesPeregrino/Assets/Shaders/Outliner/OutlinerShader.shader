Shader "Dani/OutlinerShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Space(10)]
        _OutColor("OutLine Color",Color) = (1,1,1,1)
        _InsideColor("Inside Color",Color) = (1,1,1,1)
        _OutValue("OutLine Value", Range(0,0.2)) = 0.2
            }
            SubShader
        {
            //OUTLINE PASS
            Pass
            {
                Tags
                {
                    "Queue" = "Transparent"
                }

                Blend SrcAlpha OneMinusSrcAlpha
                ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutColor;
            float _OutValue;

            float4 Outline(float4 vertexPos, float outValue)
            {
                float4x4 scale = float4x4(
                    1+ outValue,0,0,0,
                    0, 1 + outValue,0,0,
                    0,0, 1 + outValue,0,
                    0,0,0, 1 + outValue
                );

                return mul(scale, vertexPos);
            }


            v2f vert (appdata v)
            {
                v2f o;

                float4 vertexPos = Outline(v.vertex, _OutValue);

                o.vertex = UnityObjectToClipPos(vertexPos);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

            return float4(_OutColor.r, _OutColor.g, _OutColor.b, col.a);
            }
            ENDCG
        }


        //TEXTURE PASS
        Pass
        {
                Tags
                {
                    "Queue" = "Transparent+1"
                }
                
                Blend SrcAlpha OneMinusSrcAlpha

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag


                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float4 _InsideColor;


                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);
                    fixed4 finalColor = col * _InsideColor;

                    return finalColor;
                }
                ENDCG
            }

    }
}
