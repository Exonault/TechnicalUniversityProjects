// CG-6.cpp : This file contains the 'main' function. Program execution begins and ends there.


#include <iostream>
#include <GL/glew.h>
#include <SDL.h>
#include <SDL_opengl.h>
#include <gl/GLU.h>
#include <glm/glm.hpp>
#include "Shader.h"

#define STD_IMAGE_IMPLEMENTATION
#include "stb_image.h"

bool init();
bool initGL();
void render();
GLuint createPyramid();
void drawPyramid(GLuint);
void close();
void handleKeyUp(const SDL_KeyboardEvent& key);


SDL_Window* gWindow = NULL;
SDL_GLContext gContext;
GLuint gVertexArrayObjectPyramid;

Shader shaderSolid;

glm::vec3 translation = glm::vec3(0.0f);

int main(int argc, char* args[])
{
	init();
	SDL_Event event;
	bool quit = false;
	while (!quit)
	{
		while (SDL_PollEvent(&event) != 0)
		{
			if (event.type == SDL_QUIT)
			{
				quit = true;
			}

			switch (event.type)
			{
			case SDL_QUIT:
				quit = true;
				break;
			case SDL_KEYUP:
				if (event.key.keysym.sym == SDLK_ESCAPE)
				{
					quit = true;
				}
				else
				{
					handleKeyUp(event.key);
				}
				break;
			}

		}

		render();

		SDL_GL_SwapWindow(gWindow);
	}

	close();

	return 0;
}


void handleKeyUp(const SDL_KeyboardEvent& key)
{
	switch (key.keysym.sym)
	{
	case SDLK_LEFT:
		translation.x -= 0.5f;
		break;

	case SDLK_RIGHT:
		translation.x += 0.5f;
		break;

	case SDLK_UP:
		translation.y += 0.5f;
		break;

	case SDLK_DOWN:
		translation.y -= 0.5f;
		break;
	}
}

bool init()
{
	bool success = true;

	if (SDL_Init(SDL_INIT_VIDEO) < 0)
	{
		printf("SDL could not initialize! SDL Error: %s\n", SDL_GetError());
		success = false;
	}
	else
	{
		SDL_GL_SetAttribute(SDL_GL_CONTEXT_PROFILE_MASK, SDL_GL_CONTEXT_PROFILE_CORE);
		SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3);
		SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 3);

		gWindow = SDL_CreateWindow("Pyramid", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 640, 480, SDL_WINDOW_OPENGL | SDL_WINDOW_SHOWN);

		if (gWindow == NULL)
		{
			printf("Window could not be created! SDL Error: %s\n", SDL_GetError());
			success = false;
		}
		else
		{
			gContext = SDL_GL_CreateContext(gWindow);
			if (gContext == NULL)
			{
				printf("OpenGL context could not be created! SDL Error: %s\n", SDL_GetError());
				success = false;
			}
			else
			{
				//Use Vsync
				if (SDL_GL_SetSwapInterval(1) < 0)
				{
					printf("Warning: Unable to set VSync! SDL Error: %s\n", SDL_GetError());
				}

				//Initialize OpenGL
				if (!initGL())
				{
					printf("Unable to initialize OpenGL!\n");
					success = false;
				}
			}
		}

	}

	return success;
}


bool initGL()
{
	bool success = true;
	GLenum error = GL_NO_ERROR;

	glewInit();

	error = glGetError();

	if (error != GL_NO_ERROR)
	{
		printf("Error initializing OpenGL! %s\n", gluErrorString(error));
		success = false;
	}

	glClearColor(0, 0, 0, 1);

	shaderSolid.Load("./Shaders/vertex.vert", "./Shaders/solid.frag");

	gVertexArrayObjectPyramid = createPyramid();

	glEnable(GL_CULL_FACE);
	glCullFace(GL_BACK);


	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	return success;
}

void close()
{
	glDeleteProgram(shaderSolid.ID);

	glDeleteVertexArrays(1, &gVertexArrayObjectPyramid);

	SDL_GL_DeleteContext(gContext);

	SDL_DestroyWindow(gWindow);
	gWindow = NULL;

	SDL_Quit();
}

void render()
{
	glClear(GL_COLOR_BUFFER_BIT);

	glm::mat4 projection = glm::perspective(glm::radians(45.0), 4.0 / 3.0, 0.1, 100.0);
	glm::mat4 view = glm::lookAt(glm::vec3(0, 0, 6), glm::vec3(0, 0, 0), glm::vec3(0, 1, 0));
	glm::mat4 model = glm::mat4(1.0f);

	model = glm::translate(model, translation);
	model = glm::rotate(model, glm::radians(45.0f), glm::vec3(0.0f, 1.0f, 0.0f));

	shaderSolid.use();

	shaderSolid.setMat4("projection", projection);
	shaderSolid.setMat4("view", view);
	shaderSolid.setMat4("model", model);

	drawPyramid(gVertexArrayObjectPyramid);

}

GLuint createPyramid()
{
	float vertices[] =
	{
		-0.5f, -0.5f, 0.5f,
		0.5f, -0.5f, 0.5f,
		0.5f, -0.5f, -0.5f,
		-0.5f, -0.5f - 0.5f,
		0.0f, 0.5f, 0.0f
	};

	GLuint indices[] =
	{
		0, 1, 4,
		1, 2, 4,
		2, 3, 4,
		3, 0, 4
	};

	GLuint vertexArrayObject;
	GLuint vertexBufferObject;
	GLuint elementBufferObject;

	glGenBuffers(1, &vertexBufferObject);
	glGenBuffers(1, &elementBufferObject);
	glGenVertexArrays(1, &vertexArrayObject);

	glBindVertexArray(vertexArrayObject);
	glBindBuffer(GL_ARRAY_BUFFER, vertexBufferObject);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, elementBufferObject);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);

	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);

	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glBindVertexArray(0);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);

	return vertexArrayObject;
}

void drawPyramid(GLuint vertexArrayObjectId)
{
	glBindVertexArray(vertexArrayObjectId);

	glDrawElements(GL_TRIANGLES, 12, GL_UNSIGNED_INT, (void*)0);
	glBindVertexArray(0);
}