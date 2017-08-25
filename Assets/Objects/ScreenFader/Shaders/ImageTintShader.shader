Shader "Custom/ImageFadeEffect" {
    Properties {
        _MainTex ("Screen Texture", 2D) = "white" {}
        _FadeInWeight ("Fade in weight", Range (0, 1)) = 0
        _BwBlend ("Black & White blend", Range (0, 1)) = 0
    }
    SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
 
			#include "UnityCG.cginc"
 
			uniform sampler2D _MainTex;
			uniform float _FadeInWeight;
			uniform float _BwBlend;
 
			float4 frag(v2f_img i) : COLOR {
				float4 c = tex2D(_MainTex, i.uv);
				
				float lum = c.r*.3 + c.g*.59 + c.b*.11;
				float3 bw = float3( lum, lum, lum ); 
				
				float4 result = c;
				result.rgb = lerp(c.rgb, bw, _BwBlend) * _FadeInWeight;
				return result;
			}
			ENDCG
		}
	}
}