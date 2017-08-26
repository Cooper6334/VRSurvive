Shader "Custom/FishShader"
{
	Properties
	{
		_MainTex ("Raw Texture", 2D) = "white" {}
		_RipeTex ("Ripe Texture", 2D) = "white" {}
		_BurnedTex ("Burned Texture", 2D) = "white" {}

		_LeftRawWeight ("Left Raw Weight", Range(0,1)) = 1
		_LeftRipeWeight ("Left Ripe Weight", Range(0,1)) = 1
		_LeftBurnedWeight ("Left Burned Weight", Range(0,1)) = 1
		_RightRawWeight ("Right Raw Weight", Range(0,1)) = 1
		_RightRipeWeight ("Right Ripe Weight", Range(0,1)) = 1
		_RightBurnedWeight ("Right Burned Weight", Range(0,1)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float2 leftRightWeight : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _RipeTex;
			float4 _RipeTex_ST;
			sampler2D _BurnedTex;
			float4 _BurnedTex_ST;

			float _LeftRawWeight;
			float _LeftRipeWeight;
			float _LeftBurnedWeight;
			float _RightRawWeight;
			float _RightRipeWeight;
			float _RightBurnedWeight;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				float rightWeight = (max(-0.01, min(0.01, v.vertex.z)) + 0.01) / 0.02;
				o.leftRightWeight = float2(1 - rightWeight, rightWeight);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = (tex2D(_MainTex, i.uv) * _LeftRawWeight + tex2D(_RipeTex, i.uv) * _LeftRipeWeight + tex2D(_BurnedTex, i.uv) * _LeftBurnedWeight) * i.leftRightWeight.x
					+ (tex2D(_MainTex, i.uv) * _RightRawWeight + tex2D(_RipeTex, i.uv) * _RightRipeWeight + tex2D(_BurnedTex, i.uv) * _RightBurnedWeight) * i.leftRightWeight.y;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
