#version 330 core
 
in vec4 Position;
in vec3 ColorValue;

uniform mat4 ModelviewProjection;
uniform mat3 NormalMatrix;

out vec3 Color;
void main()
{
    gl_Position = ModelviewProjection * Position;

    Color = ColorValue;
}