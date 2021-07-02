﻿#version 330 core

out vec4 FragColor;
in vec3 oColor;
in vec3 oNormal;
in vec3 oPos;

uniform vec3 lightColor;

uniform vec3 LightPosition;

void main()
{
        vec3 norm = normalize(oNormal);
        vec3 lightDir = normalize(LightPosition- oPos);  
        float diff = max(dot(norm, lightDir), 0.0);
        vec3 diffuse = diff * lightColor;
        vec3 result = (diffuse+0.5) * oColor;
        FragColor = vec4(result, 1.0);
}