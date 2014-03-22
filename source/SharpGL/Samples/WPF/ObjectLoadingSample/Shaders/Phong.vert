#version 150

// Original source:
// https://github.com/tomdalling/opengl-series/blob/master/windows/07_more_lighting/

uniform mat4 Projection;
uniform mat4 Modelview;
uniform mat3 NormalMatrix;
uniform vec3 materialDiffuseColor;

in vec3 Position;
in vec2 TexCoord;
in vec3 Normal;

out vec3 fragVert;
out vec2 fragTexCoord;
out vec3 fragNormal;
out vec3 EyespaceNormal;
out vec3 Diffuse;

void main() {
    // Pass some variables to the fragment shader
    fragTexCoord = TexCoord;
    fragNormal = Normal;
    fragVert = Position;
    
    // Apply all matrix transformations to vert
    gl_Position = Projection * Modelview * vec4(Position, 1);
	
    EyespaceNormal = NormalMatrix * Normal;
	Diffuse = materialDiffuseColor;
}


//in vec4 Position;
//in vec3 Normal;
//
//uniform mat4 Projection;
//uniform mat4 Modelview;
//uniform mat3 NormalMatrix;
//uniform vec3 DiffuseMaterial;
//
//out vec3 EyespaceNormal;
//out vec3 Diffuse;
//
//void main()
//{
//    EyespaceNormal = NormalMatrix * Normal;
//    gl_Position = Projection * Modelview * Position;
//    Diffuse = DiffuseMaterial;
//}