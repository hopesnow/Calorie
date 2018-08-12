Shader "Unlit/ToonWave"
{
	Properties
	{
		_MainColor("MainColor",Color) = (0,0,0,1)
		_SubColor("SubColor",Color) = (0,0,0,1)
		_Sub2Color("Sub2Color",Color)=(0,0,0,1)
		_SubTex("Sub",2D ) = "white"{}
		_WhiteWaveSpd("WhiteWaveSpd",float) = 0.5
		_BlueWaveSpd("BlueWaveSpd", float) = 0.5
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
			};
			float4 _MainColor;
			sampler2D _SubTex;
			float4 _SubTex_ST;
			float4 _SubColor;
			float4 _Sub2Color;
			
			float _WhiteWaveSpd;
			float _BlueWaveSpd;
			v2f vert (appdata v)
			{
				v2f o;

				float y = 0.5 *sin(_Time.y + v.vertex.x * 100);
				float y2 = 0.5 * cos(_Time.z + v.vertex.z * 100);

				v.vertex.y += y + y2;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _SubTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv = i.uv * 3+ _Time.y*_WhiteWaveSpd;
				float2 uv2 = i.uv * 3 + _Time.y*_BlueWaveSpd;
				// sample the texture
				float4 col;

				if (tex2D(_SubTex, uv).a != 0)
				{
					col = _SubColor;
				}
				else if (tex2D(_SubTex, uv2).a != 0)
				{
					col = _Sub2Color;
				}
				else
				{
					col = _MainColor;
				}


				return col;
			}
			ENDCG
		}
	}
}
