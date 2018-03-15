Shader "CICEShaders/SimpleLightning"
{
    Properties
    {
        _Distance ("Distance", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
       
        Pass
        {
            Cull Front
 
            CGPROGRAM
            #pragma vertex vertexShader1
            #pragma fragment fragmentShader1
           
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
 
            struct parametrosVertexShader
            {
                float4 vertex : POSITION;              
                float4 normal : NORMAL;
            };
 
            struct parametrosFragmentShader
            {                          
                float4 vertex : SV_POSITION;
            };
           
            float _Distance;
           
            parametrosFragmentShader vertexShader1 (parametrosVertexShader v)
            {
                parametrosFragmentShader o;
                o.vertex = UnityObjectToClipPos(v.vertex + v.normal * _Distance);
                return o;
            }
           
            fixed4 fragmentShader1 (parametrosFragmentShader i) : SV_Target
            {
                return fixed4(0,0,0,1);
            }
 
            ENDCG
        }
 
        Pass {
           
            Lighting On
            Cull Back
 
            CGPROGRAM
            #pragma vertex vertexShader2
            #pragma fragment fragmentShader2
 
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
 
            struct parametrosVertexShader
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
            };
 
            struct parametrosFragmentShader
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };
 
            parametrosFragmentShader vertexShader2(parametrosVertexShader v)
            {
                parametrosFragmentShader o;
                o.vertex = UnityObjectToClipPos(v.vertex);             
                o.color = unity_AmbientSky + dot(_WorldSpaceLightPos0, v.normal) * _LightColor0;
                return o;
            }
           
            fixed4 fragmentShader2(parametrosFragmentShader i) : SV_Target
            {
                return i.color;
            }
            ENDCG
 
        }
 
    }
}