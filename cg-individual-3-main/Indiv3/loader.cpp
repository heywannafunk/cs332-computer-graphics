#pragma once

#include <gl/glew.h>
#include <SFML/OpenGL.hpp>
#include <SFML/Window.hpp>
#include <SFML/Graphics.hpp>

#include <iostream>
#include <vector>
#include <fstream>
#include <sstream>
#include <string>

struct Vertex
{
	GLfloat x;
	GLfloat y;
	GLfloat z;
};

struct Tex
{
	GLfloat x;
	GLfloat y;
};

struct Normal
{
	GLfloat x;
	GLfloat y;
	GLfloat z;
};

struct Index
{
	GLushort x;
	GLushort y;
	GLushort z;
};

struct Face
{
	Index vert[3];
};

Index slashStringToInd(std::string s)
{
	std::vector<std::string> split;
	std::stringstream ss(s);
	std::string str;
	while (std::getline(ss, str, '/'))
	{
		split.push_back(str);
	}
	Index i = { std::stoi(split[0]), std::stoi(split[1]), std::stoi(split[2]) };
	return i;
}

Index faceToInd(Face s)
{
	std::vector<Index> v;
	Index i = { s.vert[0].x, s.vert[1].x, s.vert[2].x };
	return i;
}

std::vector<float> InitializeVBO(std::string filename, int& count)
{
	std::vector<Vertex> v;
	std::vector<Tex> t;
	std::vector<Normal> n;
	std::vector<Face> s;

	std::ifstream file(filename);
	std::string line;

	//���������� �������
	while (std::getline(file, line))
	{
		if (line[0] == 'v' || line[0] == 'f')
		{
			std::vector<std::string> split;
			std::stringstream ss(line);
			std::string str;
			while (std::getline(ss, str, ' '))
			{
				split.push_back(str);
			}
			//���������� ������
			if (split[0] == "v")
			{
				Vertex vert = { std::stof(split[1]), std::stof(split[2]), std::stof(split[3]) };
				v.push_back(vert);
			}
			//���������� ����������
			else if (split[0] == "vt")
			{
				Tex tex = { std::stof(split[1]), std::stof(split[2]) };
				t.push_back(tex);
			}
			//������ �������� �������
			else if (split[0] == "vn")
			{
				Normal norm = { std::stof(split[1]), std::stof(split[2]), std::stof(split[3]) };
				n.push_back(norm);
			}
			//�������
			else if (split[0] == "f")
			{
				Face face =
				{
					slashStringToInd(split[1]),
					slashStringToInd(split[2]),
					slashStringToInd(split[3]),
				};
				s.push_back(face);
			}
		}
	}

	std::vector<float> pos_tex;
	for (int i = 0; i < s.size(); ++i)
	{
		pos_tex.push_back(v[s[i].vert[0].x - 1].x);
		pos_tex.push_back(v[s[i].vert[0].x - 1].y);
		pos_tex.push_back(v[s[i].vert[0].x - 1].z);
		pos_tex.push_back(t[s[i].vert[0].y - 1].x);
		pos_tex.push_back(1 - t[s[i].vert[0].y - 1].y);
		pos_tex.push_back(n[s[i].vert[0].z - 1].x);
		pos_tex.push_back(n[s[i].vert[0].z - 1].y);
		pos_tex.push_back(n[s[i].vert[0].z - 1].z);

		pos_tex.push_back(v[s[i].vert[1].x - 1].x);
		pos_tex.push_back(v[s[i].vert[1].x - 1].y);
		pos_tex.push_back(v[s[i].vert[1].x - 1].z);
		pos_tex.push_back(t[s[i].vert[1].y - 1].x);
		pos_tex.push_back(1 - t[s[i].vert[1].y - 1].y);
		pos_tex.push_back(n[s[i].vert[1].z - 1].x);
		pos_tex.push_back(n[s[i].vert[1].z - 1].y);
		pos_tex.push_back(n[s[i].vert[1].z - 1].z);

		pos_tex.push_back(v[s[i].vert[2].x - 1].x);
		pos_tex.push_back(v[s[i].vert[2].x - 1].y);
		pos_tex.push_back(v[s[i].vert[2].x - 1].z);
		pos_tex.push_back(t[s[i].vert[2].y - 1].x);
		pos_tex.push_back(1 - t[s[i].vert[2].y - 1].y);
		pos_tex.push_back(n[s[i].vert[2].z - 1].x);
		pos_tex.push_back(n[s[i].vert[2].z - 1].y);
		pos_tex.push_back(n[s[i].vert[2].z - 1].z);
	}
	count = s.size() * 3;
	return pos_tex;
}