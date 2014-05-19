#version 330 core
 
in vec4 Position;
in vec3 Normal;
in vec3 DiffuseMaterial;
in vec4 AmbientMaterial;
in vec3 SpecularMaterial;
in float ShininessValue;

uniform mat4 ModelviewProjection;
uniform mat3 NormalMatrix;

out vec3 EyespaceNormal;
out vec3 Diffuse;
out vec4 Ambient;
out vec3 Specular;
out float Shininess;
void main()
{
    EyespaceNormal = NormalMatrix * Normal;
    gl_Position = ModelviewProjection * Position;

    Diffuse = DiffuseMaterial;
	Ambient = AmbientMaterial;
	Specular = SpecularMaterial;
	Shininess = ShininessValue;
}