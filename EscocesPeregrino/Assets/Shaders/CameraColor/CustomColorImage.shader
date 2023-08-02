Shader "Dani/CustomColorImage"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _redValue("Red Value", Range(0, 1)) = 1.0
        _greenValue("Green Value", Range(0, 1)) = 1.0
        _blueValue("Blue Value", Range(0, 1)) = 1.0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;
            float _redValue;
            float _greenValue;
            float _blueValue;

            struct Input
            {
                float2 uv_MainTex;
            };

            //half _Glossiness;
            //half _Metallic;
            //fixed4 _Color;

            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                // Sample the texture at the given UV coordinate
                fixed4 color = tex2D(_MainTex, IN.uv_MainTex);

                // Subtract the RGB values according to the provided float values
                color.rgb -= float3(_redValue, _greenValue, _blueValue);

                // Clamp the result to keep it within valid color range
                color.rgb = saturate(color.rgb);

                // Set the output color
                o.Albedo = color.rgb;
            }
            ENDCG
        }
            FallBack "Diffuse"
}