#version 330
in vec3 EyespaceNormal;
in vec3 Diffuse;
in vec4 Ambient;
in vec3 Specular;
in float Shininess;

uniform vec3 LightPosition;

out vec4 FragColor;

void main()
{
    vec3 N = normalize(EyespaceNormal);
    vec3 L = normalize(LightPosition);
    vec3 E = vec3(0, 0, 1);
    vec3 H = normalize(L + E);
    
    float df = max(0.0, dot(N, L));
    float sf = max(0.0, dot(N, H));
    sf = pow(sf, Shininess);

    vec3 color = Ambient.xyz + df * Diffuse + sf * Specular;
    FragColor = vec4(color, Ambient.w);
}