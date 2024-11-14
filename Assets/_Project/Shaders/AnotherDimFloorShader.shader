Shader "Unlit/AnotherDimFloorShader"
{
    Properties
    {
        _Dist ("Distance", float) = 5.0
        //_MainTex ("Texture", 2D) = "white" {}
        ///_SecondaryTex ("Secondary texture", 2D) = "white"{}
        
        _MainColor ("Color", Color) = (1,1,1,1)
        _SecondaryColor ("Secondary Color", Color) = (1,1,1,1)
        
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
       
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag 
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 worldPos : TEXCOORD1;
                float4 pos : SV_POSITION;
                 
               
            };
 
            v2f vert(appdata_base v)
            {
                v2f o;
                
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }
 
            float4 _MainColor;
            float4 _SecondaryColor;
            float _Dist;
            uniform int _NumberGrenade;
            uniform float4 _GrenadesPositions [15]; 
 
            fixed4 frag(v2f i) : SV_Target
            {
                bool isNotAnotherDim = true;
                
                for(int k = 0; k < _NumberGrenade; k++ )
                {
                    
                    isNotAnotherDim = isNotAnotherDim && (distance(_GrenadesPositions[k].xyz, i.worldPos.xyz) > _Dist);
                }
                
                
                //float4 mainColor = tex2D(_MainTex, i.uv);
                //float4 secondaryColor = tex2D(_SecondaryTex, i.uv);

                if(isNotAnotherDim)
                    return _MainColor;
                else
                    return _SecondaryColor;


                
                //return (1 - isAnotherDim) * mainColor + isAnotherDim * secondaryColor;
/*
                if(distance(_GranadesPositons[k].xyz, i.worldPos.xyz) > _Dist)
                    return tex2D(_MainColor, i.uv);
                else
                    return tex2D(_SecondaryColor, i.uv);*/
            }
 
            ENDCG
        }
        
        //UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
        
        
    }
}
