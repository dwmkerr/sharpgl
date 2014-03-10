#define NNOISE 4

#define PI 3.141592653

#define PALE_BLUE vec4(0.25, 0.25, 0.35, 1.0)
//#define PALE_BLUE vec4(0.90, 0.90, 1.0, 1.0)
#define MEDIUM_BLUE vec4(0.10, 0.10, 0.30, 1.0)
#define DARK_BLUE vec4(0.05, 0.05, 0.26, 1.0)
#define DARKER_BLUE vec4(0.03, 0.03, 0.20, 1.0)

varying vec3 normal;
varying vec4 pos;
varying vec4 rawpos;

uniform float scale;

float noise(vec4);
float snoise(vec4);
float noise(vec3);
float snoise(vec3);
vec4 marble_color(float);
vec4 spline(float x, int y, vec4 z[]);

void main() {
	//vec4 color = gl_FrontMaterial.diffuse;
	vec4 matspec = gl_FrontMaterial.specular;
	float shininess = gl_FrontMaterial.shininess;
	vec4 lightspec = gl_LightSource[0].specular;
	vec4 lpos = gl_LightSource[0].position;
	vec4 s = -normalize(pos-lpos); 	//not sure why this needs to 
									// be negated, but it does.
	vec3 light = s.xyz;
	vec3 n = normalize(normal);
	vec3 r = -reflect(light, n);
	r = normalize(r);
	vec3 v = -pos.xyz; // We are in eye coordinates,
					   // so the viewing vector is
					   // [0.0 0.0 0.0] - pos
	v = normalize(v);
	
	float scalelocal;
	if (scale == 0.0) {
		scalelocal = 1.0; //default value
	} else {
		scalelocal = scale;
	}

	vec4 tp = gl_TexCoord[0] * scalelocal;
	vec3 rp = rawpos.xyz * scalelocal;
	
	// create the grayscale marbling here
	float marble=0.0;
	float f = 1.0;
	for(int i=0; i < NNOISE; i++) {
		marble += noise(rp*f)/f;
		f *= 2.17;
	}
	
	vec4 color;
	color = marble_color(marble);
	
	// for some reason the colors are awfully dark
	// I think it looks better this way
	color *= 2.85;
/*	
	float x = pow(sin(marble * PI) * 0.5 + 0.5, 10.0);
	float y=0.1;
	vec4 matdiffcol= gl_FrontMaterial.diffuse;
	vec4 othercolor = vec4( vec3(1.0)-((vec3(1.0)-matdiffcol.rgb)*y), 1.0);
	//vec4 color = mix(vec4(1.0, 1.0, 1.0, 1.0), matdiffcol, x);
	color = mix(othercolor, matdiffcol, x);
*/
	
	//color = mix(vec4(1.0, 1.0, 1.0, 1.0), vec4(0.4, 0.6, 1.0, 1.0), vec4(marble, marble, marble, 1.0));
//	color = vec4(marble, marble, marble, 1.0);
	
	vec4 diffuse  = color * max(0.0, dot(n, s.xyz)) * gl_LightSource[0].diffuse;
	vec4 specular;
	if (shininess != 0.0) {
		specular = lightspec * matspec * pow(max(0.0, dot(r, v)), shininess);
	} else {
		specular = vec4(0.0, 0.0, 0.0, 0.0);
	}
	
	gl_FragColor = diffuse + specular;
//	gl_FragColor = noise4(pos) != 0.0 ? vec4(1.0, 0.0, 0.0, 1.0) : vec4(0.0, 0.0, 1.0, 1.0);

}

vec4 marble_color(float m) {
	vec4 c[25];
	
	c[0] = PALE_BLUE;
	c[1] = PALE_BLUE;
	c[2] = MEDIUM_BLUE;
	c[3] = MEDIUM_BLUE;
	c[4] = MEDIUM_BLUE;
	c[5] = PALE_BLUE;
	c[6] = PALE_BLUE;
	c[7] = DARK_BLUE;
	c[8] = DARK_BLUE;
	c[9] = DARKER_BLUE;
	c[10] = DARKER_BLUE;
	c[11] = PALE_BLUE;
	c[12] = DARKER_BLUE;
	
	vec4 res = spline(clamp(2.0*m + 0.75, 0.0, 1.0), 13, c);
	
	return res;
}