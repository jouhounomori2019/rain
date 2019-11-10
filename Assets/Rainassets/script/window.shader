/*
メモ
ボックス　上部　0.5 　=　0
　　　　　下部　-0.5　=　1

//でメモを残すとき　プログラムの後に空白が入るとエラーが出る




*/





Shader "Unlit/window " //名前
{
	Properties //マテリアルから変更できるようになる値
	{
		_MainTex("Texture", 2D) = "white" {} //テクスチャ　インスペクタで変更可能
		_Size("Size", float) = 1 //_Size　float型　
		_T("Time", float) = 1
		_Distortion("Distortion", range(-5, 5)) = 1
		_Blur("Blur", range(0, 1)) = 1
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque"  "Queue" = "Transparent"}
			LOD 100

			GrabPass{ "_GrabTexture"}
			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog
				#define S(a, b, t) smoothstep(a, b, t) //雨粒を描画するための値
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;//頂点１つ１つの空間座標
					float2 uv : TEXCOORD0;//頂点１つ１つのUV空間座標
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 grabUv : TEXCOORD1;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex, _GrabTexture;
				float4 _MainTex_ST;
				float _Size, _T, _Distortion, _Blur;

				//バーテクスシェーダー
				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					o.grabUv = UNITY_PROJ_COORD((ComputeGrabScreenPos(o.vertex));

					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				float N21(float2 p) {
					p = frac(p*float2(123.34, 345.45));
					p += dot(p, p + 34.345);
					return frac(p.x*p.y);
				}


				float3 Layer(float2 UV, float t) {

					float2 aspect = float2(2, 1); //雨描画ボックスのアスペクト比　
					float2 uv = UV * _Size*aspect;//UV座標作成　_Sizeと乗算しサイズを決める
					uv.y += t * .25;//雨粒を描画するボックスが時間とともに下に下がる
					float2 gv = frac(uv) - .5;//uvの少数部分-0.5 描画ボックスの原点が真ん中になる
					float2 id = floor(uv);

					float n = N21(id); //0 1
					t += n * 6.2831;

					float w = UV.y * 10;
					float x = (n - .5)*.8; //-.4 .4　雨粒の位置変更　x
					x += (.4 - abs(x)) * sin(3 * w)*pow(sin(w), 6)*.45;
					float y = -sin(t + sin(t + sin(t)*.5))*.45;//雨粒の位置変更 y　「sin」を使って表現
					y -= (gv.x - x)*(gv.x - x);


					float2 dropPos = (gv - float2(x, y)) / aspect;//雨粒の位置変更　gvの位置x､yを減算
					float drop = S(.05, .03, length(dropPos));// 雨粒の描画　

					float2 trailPos = (gv - float2(x, t * .25)) / aspect;//雨の後の位置変更　雨の跡がその場でとどまるyはt*.25
					trailPos.y = (frac(trailPos.y * 8) - .5) / 8;//雨の跡ボックス内で8個に複製(*8)　歪みを直す(frac,-0.5,/8)　　
					float trail = S(.03, .01, length(trailPos));//雨の跡の描画
					float fogTrail = S(-.05, .05, dropPos.y);//雨の跡が雨粒の上にできる　雨粒が特定の位置まで来たら雨の跡を消す　スムーズな補完
					fogTrail *= S(.2, y, gv.y);//gv.y = 0.5～-0.5
					trail *= fogTrail;
					fogTrail *= S(.05, .04, abs(dropPos.x));

					//col += fogTrail * .5;
					//col += trail;
					//col += drop;

					//col *= 0; col.rg += dropPos;
					float2 offs = drop * dropPos + trail * trailPos;
					//if (gv.x > .48 || gv.y > .49) col = float4(1, 0, 0, 1);

					return float3(offs, fogTrail);

				}


				//フラグメントシェーダー
				fixed4 frag(v2f i) : SV_Target
				{
					float t = fmod(_Time.y + _T, 7200);//時間定義　time.yは時間＊1倍　＿Tで外部から時間の操作

					float4 col = 0;

					float3 drops = Layer(i.uv, t);
					drops += Layer(i.uv*1.23 + 7.54, t);
					drops += Layer(i.uv*1.35 + 1.54, t);
					drops += Layer(i.uv*1.57 - 7.54, t);

					//col *= 0; col += N21(id);  // id * .1;
					float blur = _Blur * 7 * (1 - drops.z);


					float fade = fwidth(i.uv);
					//col = tex2Dlod(_MainTex, float4(i.uv+drops.xy*_Distortion,0,blur));

					float2 projUv = i.grabUv.xy / i.grabUv.w;
					projUv += drops.xy * _Distortion;
					blur *= .01;

					const float numSamples = 32;
					float a = N21(i.uv)*6.2831;
					for (float i = 0; i < numSamples; i++) {
						float2 offs = float2(sin(a), cos(a))*blur;
						float d = frac(sin((i + 1)*546.)*5424.);
						d = sqrt(d);
						offs *= d;
						col += tex2D(_GrabTexture, projUv + offs);
						a++;
					}

					col /= numSamples;



					return col;
				}
				ENDCG
			}
						}
}
