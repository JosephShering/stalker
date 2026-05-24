shader_type spatial;

uniform vec3 dirt_color : source_color;
uniform vec3 grass_color : source_color;

uniform sampler2D u_transition_mask;

varying vec3 vnormal;

void vertex() {
	vnormal = NORMAL;
	// Called for every vertex the material is visible on.
}

void fragment() {
	float a = dot(vnormal, vec3(0.0, 1.0, 0.0));
	//a = pow(a, 10.0);
	a = smoothstep(a, 0.88, 0.97);
	
	vec3 ground_color = mix(grass_color, dirt_color, a);
	ALBEDO = texture(u_transition_mask, UV).rgb;
}