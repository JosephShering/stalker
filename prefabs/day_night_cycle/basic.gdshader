shader_type spatial;
render_mode unshaded;

uniform sampler2D noise : filter_nearest;
uniform float pixel_size = 128.0;
uniform float levels = 12.0;

void fragment() {
	float scroll = fract(0.005 * TIME);
	vec2 uv = vec2(UV.x + scroll, UV.y) + scroll;
	uv = floor(uv * pixel_size) / pixel_size;
	
	vec4 t = texture(noise, uv);
	
	vec3 quantized = floor(t.rgb * levels) / levels;
	
	ALBEDO = quantized;
}
