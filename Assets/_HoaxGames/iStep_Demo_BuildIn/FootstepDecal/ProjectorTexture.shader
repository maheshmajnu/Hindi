Shader "Projector/Projector Texture" {
	Properties {
		_Color("Main Color", Color) = (1,1,1,1)
		_ShadowTex ("Cookie", 2D) = "gray" {}
	}
	Subshader {
		Tags {"Queue"="Transparent"}
		Pass {
			ZWrite Off
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha // Traditional transparency
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			
			struct v2f {
				float4 uvShadow : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 pos : SV_POSITION;
			};
			
			float4x4 unity_Projector;
			
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(vertex);
				o.uvShadow = mul (unity_Projector, vertex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			fixed4 _Color;
			sampler2D _ShadowTex;
			
			fixed4 frag(v2f i) : SV_Target
			{
				if (i.uvShadow.x < -0) discard;
				if (i.uvShadow.x > 1) discard;
				if (i.uvShadow.y < -0) discard;
				if (i.uvShadow.y > 1) discard;
				if (i.uvShadow.z < -1) discard;
				if (i.uvShadow.z > 1) discard;

				fixed4 texS = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow)) * _Color;
				UNITY_APPLY_FOG_COLOR(i.fogCoord, texS, fixed4(1,1,1,1));
				return texS;
			}
			ENDCG
		}
	}
}
