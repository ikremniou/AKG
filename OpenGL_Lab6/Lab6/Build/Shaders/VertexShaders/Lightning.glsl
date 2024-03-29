﻿#version 330 core
layout(location = 0) in vec3 inPosition;
layout (location = 1) in vec3 inNormal;

out vec3 Normal;
out vec3 FragPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	FragPos = vec3(model * vec4(inPosition, 1.0));
	Normal = mat3(transpose(inverse(model))) * inNormal;

	gl_Position = projection * view * model * vec4(inPosition, 1.0f);
	
}