#version 330 core
out vec4 FragColor;
 
in vec3 color;
in vec2 texCoords;
 
uniform float alpha;
uniform sampler2D diffuse;

void main()
{ 
	//FragColor = vec4(color.rgb, alpha); 
	//FragColor = vec4(texture(diffuse, texCoords).rgb,alpha);
	FragColor = texture(diffuse, vec2(texCoords.s + alpha, texCoords.t));
}
		