#version 330 core
out vec4 FragColor;
in vec3 color;
in vec2 texCoords;
uniform float alpha;

uniform sampler2D diffuse;
uniform sampler2D diffuse2;
uniform sampler2D alpha_mask;

void main()
{
	vec4 texel = texture(diffuse,texCoords);
	vec4 texel2 = texture(diffuse2, texCoords);
	float mask_alpha = texture(alpha_mask, texCoords).a;
	vec3 mixed_color = mix(texel2.rgb, texel.rgb, mask_alpha);
	if(mask_alpha < 0.1)
	{
		//discard;
	}else
	{
		
	}
	FragColor = vec4(mixed_color, 1.0);
}