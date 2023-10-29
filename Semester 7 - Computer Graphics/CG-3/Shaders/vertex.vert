#version 330 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in vec3 aColor; 
layout(location = 2) in vec2 aTexCoords;
  
out vec2 texCoords;
out vec3 color;

void main()
{ 
	gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0); 
	color = aColor; 
	texCoords = aTexCoords;
}