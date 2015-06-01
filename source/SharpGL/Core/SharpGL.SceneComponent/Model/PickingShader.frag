#version 150 core
flat in vec4 pass_Color; // glShadeMode(GL_FLAT); in legacy opengl.
out vec4 out_Color;

void main(void) {
	out_Color = pass_Color;
}