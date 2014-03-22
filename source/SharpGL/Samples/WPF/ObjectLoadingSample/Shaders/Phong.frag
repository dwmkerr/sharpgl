#version 150

// Original source:
// https://github.com/tomdalling/opengl-series/blob/master/windows/07_more_lighting/

uniform mat4 Modelview;
uniform vec3 cameraPosition;

// material settings
uniform sampler2D materialTex;
uniform float materialShininess;
uniform vec3 materialDiffuseColor;		// todo wire it in
uniform vec3 materialAmbientColor;		// todo wire it in
uniform vec3 materialSpecularColor;

uniform struct Light {
   vec3 position;
   vec3 intensities; //a.k.a the color of the light
   float attenuation;
   float ambientCoefficient;
} light;

// Brought in from the vertex shader.
in vec2 fragTexCoord;
in vec3 fragNormal;
in vec3 fragVert;
in vec3 EyespaceNormal;
in vec3 Diffuse;

out vec4 finalColor;

void main() {
 
	vec3 N = normalize(EyespaceNormal);
    vec3 L = normalize(light.position);
    vec3 E = vec3(0, 0, 1);
    vec3 H = normalize(L + E);
    
    float df = max(0.0, dot(N, L));
    float sf = max(0.0, dot(N, H));
    sf = pow(sf, materialShininess);

    vec3 color = materialAmbientColor + df * Diffuse + sf * materialSpecularColor;
    finalColor = vec4(color, 1.0);

//
//    vec3 normal = normalize(transpose(inverse(mat3(Modelview))) * fragNormal);
//    vec3 surfacePos = vec3(Modelview * vec4(fragVert, 1));
//    vec4 surfaceColor = vec4(materialAmbientColor, 1);//texture(materialTex, fragTexCoord);
//    vec3 surfaceToLight = normalize(light.position - surfacePos);
//    vec3 surfaceToCamera = normalize(cameraPosition - surfacePos);
//    
//    //ambient
//    vec3 ambient = light.ambientCoefficient * surfaceColor.rgb * light.intensities;
//
//    //diffuse
//    float diffuseCoefficient = max(0.0, dot(normal, surfaceToLight));
//    vec3 diffuse = diffuseCoefficient * surfaceColor.rgb * light.intensities;
//    
//    //specular
//    float specularCoefficient = 0.0;
//    if(diffuseCoefficient > 0.0)
//        specularCoefficient = pow(max(0.0, dot(surfaceToCamera, reflect(-surfaceToLight, normal))), materialShininess);
//    vec3 specular = specularCoefficient * materialSpecularColor * light.intensities;
//    
//    //attenuation
//    float distanceToLight = length(light.position - surfacePos);
//    float attenuation = 1.0 / (1.0 + light.attenuation * pow(distanceToLight, 2));
//
//    //linear color (color before gamma correction)
//    vec3 linearColor = ambient + attenuation*(diffuse + specular);
//    
//    //final color (after gamma correction)
//    vec3 gamma = vec3(1.0/2.2);
//    finalColor = vec4(pow(linearColor, gamma), surfaceColor.a);
}