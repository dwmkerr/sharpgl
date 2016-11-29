#version 330 core
out vec4 FragColor;

uniform vec3 PickingColor;
 
void main()
{
	FragColor = vec4(PickingColor, 1.0);
}