#version 330 core
in vec4 Position;
in vec3 Normal;
in vec4 TCol1; 
in vec4 TCol2; 
in vec4 TCol3; 
in vec4 TCol4; 
in vec3 AmbientMaterial;
in vec3 DiffuseMaterial;
in vec3 SpecularMaterial;
in float ShininessValue;

uniform mat4 ModelviewProjection;
uniform mat3 NormalMatrix;

out vec3 EyespaceNormal;
out vec3 Ambient;
out vec3 Diffuse;
out vec3 Specular;
out float Shininess;

void main()
{
	// Put the transformation matrix back together. 
	mat4 transformation = mat4(0);
	transformation[0] = TCol1;
	transformation[1] = TCol2;
	transformation[2] = TCol3;
	transformation[3] = TCol4;

	// Get the transformed position and normal. 
	// NOTE! This is not the correct way to transform a mat3, but it's done like this for simplicity.
    vec4 pos = Position * transformation;
	vec4 norm4 = vec4(Normal.x, Normal.y, Normal.z, 0) * transformation;


	vec3 norm3 = norm4.xyz;
    EyespaceNormal = NormalMatrix * norm3;

	gl_Position = ModelviewProjection * pos;
	
    Ambient = AmbientMaterial;
    Diffuse = DiffuseMaterial;
	Specular = SpecularMaterial;
	Shininess = ShininessValue;
}