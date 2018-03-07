// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Unlit/Card"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_SecondTex ("Back Texture", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		LOD 100

		Pass
		{
			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			
			#include "UnityCG.cginc"


			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				fixed dirnormal : DATA;
			};


			sampler2D _MainTex;
			sampler2D _SecondTex;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos (v.vertex);

				float3 viewVector = normalize(_WorldSpaceCameraPos);
 
          		o.dirnormal = dot(viewVector,v.normal);
          		o.uv = v.texcoord;
         
	          	return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col;
				if(i.dirnormal > 0.0f)
				{
					col = tex2D(_MainTex, i.uv);
				}
				else
				{
					col = tex2D(_SecondTex, i.uv);
				}

				col.rgb *= col.a; //Si no, no aplica bien el alpha :S
				return col;
			}
			ENDCG
		}
	}
}
