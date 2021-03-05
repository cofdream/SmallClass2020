Shader "Example/cubeShader"
{
	Properties
	{
		_MainTex("Texture",2D) = "white" {}
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		UsePass "Example/RedColorShader/REDCOLOR"
	}
}