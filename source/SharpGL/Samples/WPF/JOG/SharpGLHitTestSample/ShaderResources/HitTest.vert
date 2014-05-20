#version 330 core
 
in vec4 Position;
in vec3 HTColorId;

uniform mat4 ModelviewProjection;
uniform mat3 NormalMatrix;

out vec3 Color;
void main()
{
    gl_Position = ModelviewProjection * Position;

    Color = HTColorId;
}