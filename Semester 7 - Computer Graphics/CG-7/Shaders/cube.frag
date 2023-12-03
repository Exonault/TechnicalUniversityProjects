#version 330 core
out vec4 FragColor;

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;    
	float ka; //ambient coefficient
	float kd; //diffuse coefficient
	float ks; //specular coefficient
    float shininess;
}; 

struct Light {
    vec3 position;
    vec3 diffuse; //the color of the light
};

in vec3 FragPos;  
in vec3 Normal;  
  
uniform vec3 viewPos;

uniform Material material1;
uniform Light light1;

uniform Light light2;

vec3 getColor(Material, Light);

vec3 getAmbient(Material);
vec3 getDiffuse(Material, Light);
vec3 getSpecular(Material, Light);

void main()
{
   //FragColor = vec4(getColor(material1, light1), 1.0);
   
  /* vec3 light1 = getColor(material1, light1);
   vec3 light2 = getColor(material2, light2);
   vec3 result = light1 + light2;*/
   

   vec3 ambient1 = getAmbient(material1);
   vec3 diffuse1 = getDiffuse(material1, light1);
   vec3 specular1 = getSpecular(material1, light1);

   vec3 ambient2 = getAmbient(material1);
   vec3 diffuse2 = getDiffuse(material1, light2);
   vec3 specular2 = getSpecular(material1, light2);

   vec3 result = ambient1 + diffuse1 + specular1 + ambient2 + diffuse2 + specular2;
   FragColor = vec4(result, 1.0);

} 

vec3 getColor(Material material, Light light)
{
    // ambient
    vec3 ambient = material.ka * material.ambient;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0); //cos to light direction
    vec3 diffuse = light.diffuse * material.kd * (diff * material.diffuse);
    
    // specular
    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
	//cos to the power of shininess of angle between light reflected ray and view direction
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess); 
    vec3 specular = material.ks * (spec * material.specular);  
        
    vec3 result = ambient + diffuse + specular;

    return result;
//    FragColor = vec4(result, 1.0);
//	FragColor = vec4(specular, 1.0);
}

vec3 getAmbient(Material material)
{
    vec3 ambient = material.ka * material.ambient;
    return ambient;
}


vec3 getDiffuse(Material material, Light light)
{
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0); //cos to light direction
    vec3 diffuse = light.diffuse * material.kd * (diff * material.diffuse);

    return diffuse;
}


vec3 getSpecular(Material material, Light light)
{
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(light.position - FragPos);


    vec3 viewDir = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
	//cos to the power of shininess of angle between light reflected ray and view direction
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess); 
    vec3 specular = material.ks * (spec * material.specular);  
    return specular;
}