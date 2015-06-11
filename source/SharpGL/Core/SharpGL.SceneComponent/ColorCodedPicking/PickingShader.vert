#version 150 core

in vec3 in_Position;
in vec3 in_Color;  
flat out vec4 pass_Color; // glShadeMode(GL_FLAT); in legacy opengl.
uniform mat4 projectionMatrix;
uniform mat4 viewMatrix;
uniform mat4 modelMatrix;
uniform int pickingBaseID; // how many vertices have been coded so far?

void main(void) {
	gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(in_Position, 1.0);

	int objectID = pickingBaseID + gl_VertexID;
	pass_Color = vec4(
		float(objectID & 0xFF) / 255.0, 
		float((objectID >> 8) & 0xFF) / 255.0, 
		float((objectID >> 16) & 0xFF) / 255.0, 
		float((objectID >> 24) & 0xFF) / 255.0);
}