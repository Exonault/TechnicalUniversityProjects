// KG-5.cpp : Defines the entry point for the console application.
//

//#define GLEW_STATIC
#include <GL\glew.h>

#include <SDL.h>
#include <SDL_opengl.h>
#include <stdio.h>
#include <gl\GLU.h>
#include <glm/glm.hpp>

#include "Shader.h"
#define STB_IMAGE_IMPLEMENTATION
#include "stb_image.h"

bool init();
bool initGL();
void render();
GLuint CreateCube(GLuint&);
GLuint CreateShaderProg();
void DrawCube(GLuint id);
void close();
bool LoadTexture(const char*, GLuint&);
GLuint CreatePyramid();
void DrawPyramid(GLuint);

void HandleKeyUp(const SDL_KeyboardEvent& key);

//The window we'll be rendering to
SDL_Window* gWindow = NULL;

//OpenGL context
SDL_GLContext gContext;

//GLuint gShaderProgID;
GLuint gVAOCube, gVBO, gVAOPyramid;
Shader shaderTexture, shaderSolid;
GLuint textureId;
GLuint textureId2;
GLuint textureId3;
//GLint alphaLocation;

float deltaTime;

glm::vec3 translation = glm::vec3(0.0f);


int main(int argc, char* args[])
{
	init();

	SDL_Event e;
	//While application is running
	bool quit = false;
	GLuint currentFrame = SDL_GetTicks();
	GLuint lastFrame = currentFrame;
	while (!quit)
	{
		currentFrame = SDL_GetTicks();
		deltaTime = currentFrame - lastFrame;
		deltaTime /= 1000.0f;

		lastFrame = currentFrame;

		//Handle events on queue
		while (SDL_PollEvent(&e) != 0)
		{
			//User requests quit
			if (e.type == SDL_QUIT)
			{
				quit = true;
			}
			switch (e.type)
			{
			case SDL_QUIT:
				quit = true;
				break;
			case SDL_KEYUP:
				if (e.key.keysym.sym == SDLK_ESCAPE)
				{
					quit = true;
				}
				else {
					HandleKeyUp(e.key);
				}
				break;
			}
		}

		//Render quad
		render();

		//Update screen
		SDL_GL_SwapWindow(gWindow);
	}

	close();

	return 0;
}

void HandleKeyUp(const SDL_KeyboardEvent& key)
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
	//Initialization flag
	bool success = true;

	//Initialize SDL
	if (SDL_Init(SDL_INIT_VIDEO) < 0)
	{
		printf("SDL could not initialize! SDL Error: %s\n", SDL_GetError());
		success = false;
	}
	else
	{
		//Use OpenGL 3.3
		SDL_GL_SetAttribute(SDL_GL_CONTEXT_PROFILE_MASK, SDL_GL_CONTEXT_PROFILE_CORE);
		SDL_GL_SetAttribute(SDL_GL_CONTEXT_MAJOR_VERSION, 3);
		SDL_GL_SetAttribute(SDL_GL_CONTEXT_MINOR_VERSION, 3);


		//Create window
		gWindow = SDL_CreateWindow("SDL Tutorial", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 640, 480,
			SDL_WINDOW_OPENGL | SDL_WINDOW_SHOWN /*| SDL_WINDOW_FULLSCREEN*/);
		if (gWindow == NULL)
		{
			printf("Window could not be created! SDL Error: %s\n", SDL_GetError());
			success = false;
		}
		else
		{
			//Create context
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

bool LoadTexture(const char* filename, GLuint& texID)
{
	glGenTextures(1, &texID);
	glBindTexture(GL_TEXTURE_2D, texID);
	// set the texture wrapping/filtering options (on the currently bound texture object)
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT); //these are the default values for warping
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

	// read the texture
	GLint width, height, channels;
	stbi_set_flip_vertically_on_load(true); //flip the image vertically while loading
	unsigned char* img_data = stbi_load(filename, &width, &height, &channels, 0); //read the image data

	if (img_data)
	{   //3 channels - rgb, 4 channels - RGBA
		GLenum format;
		switch (channels)
		{
		case 4:
			format = GL_RGBA;
			break;
		default:
			format = GL_RGB;
			break;
		}
		glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, img_data);
		glGenerateMipmap(GL_TEXTURE_2D);
	}
	else
	{
		printf("Failed to load texture %s \n", filename);
		return false;
	}
	stbi_image_free(img_data);

	return true;
}

bool initGL()
{
	bool success = true;
	GLenum error = GL_NO_ERROR;

	glewInit();

	error = glGetError();
	if (error != GL_NO_ERROR)
	{
		success = false;
		printf("Error initializing OpenGL! %s\n", gluErrorString(error));
	}

	LoadTexture("./Textures/wall.jpg", textureId);

	LoadTexture("./Textures/concrete.jpg", textureId2);

	LoadTexture("./Textures/OpenGL_logo.png", textureId3);

	glClearColor(0, 1, 0, 1);
	//gShaderProgID = CreateShaderProg();
	shaderTexture.Load("./Shaders/vertex.vert", "./Shaders/fragment.frag");

	shaderSolid.Load("./Shaders/vertex.vert", "./Shaders/solid.frag");

	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, textureId);

	shaderTexture.setInt("diffuse", 0);
	shaderTexture.setInt("diffuse2", 1);
	shaderTexture.setInt("alphaMask", 2);

	gVAOCube = CreateCube(gVBO);
	gVAOPyramid = CreatePyramid();



	//alphaLocation = glGetUniformLocation(gShaderProgID, "alpha");

	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	//glEnable(GL_CULL_FACE);
	//glCullFace(GL_BACK);

	glEnable(GL_DEPTH_TEST);
	glDepthFunc(GL_LEQUAL);

	return success;
}



void close()
{
	//delete GL programs, buffers and objects
	glDeleteProgram(shaderTexture.ID);
	glDeleteVertexArrays(1, &gVAOCube);
	glDeleteBuffers(1, &gVBO);

	//Delete OGL context
	SDL_GL_DeleteContext(gContext);
	//Destroy window
	SDL_DestroyWindow(gWindow);
	gWindow = NULL;

	//Quit SDL subsystems
	SDL_Quit();
}

void render()
{
	//Clear color buffer
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glm::mat4 projection = glm::perspective(glm::radians(45.0), 4.0 / 3.0, 0.1, 100.0);

	glm::mat4 view = glm::lookAt(glm::vec3(0, 3, 10), glm::vec3(0, 0, 0), glm::vec3(0, 1, 0));

	glm::mat4 model = glm::mat4(1.0f);

	model = glm::translate(model, translation);
	//model = glm::rotate(model, glm::radians(30.0f), glm::vec3(0.0f, 0.0f, 1.0f));
	//model = glm::scale(model, glm::vec3(2.0f, 2.0f, 2.0f));

	shaderTexture.use();

	shaderTexture.setMat4("projection", projection);
	shaderTexture.setMat4("view", view);
	shaderTexture.setMat4("model", model);

	DrawCube(gVAOCube);

	glm::mat4 cubeModel = model;

	shaderSolid.use();

	shaderSolid.setMat4("projection", projection);
	shaderSolid.setMat4("view", view);

	static float angle = 0.0f;
	angle += 90.0f * deltaTime;
	
	model = glm::rotate(model, glm::radians(angle), glm::vec3(0.0f, 0.0f, 1.0f));

	model = glm::translate(model, glm::vec3(0.0f, 1.2f, 0.0f));

	shaderSolid.setMat4("model", model);
	
	DrawPyramid(gVAOPyramid);

	static float angle2 = 0.0f;
	angle2 += 180.0f * deltaTime;

	model = glm::rotate(cubeModel, glm::radians(angle2),
		glm::vec3(0.0f, 1.0f, 0.0f));

	model = glm::translate(model, glm::vec3(4.0f, 0.0f, 0.0f));

	shaderSolid.setMat4("model", model);

	DrawPyramid(gVAOPyramid);
	
	glm::mat4 pyramidModel = model;

	static float angle3 = 0.0f;
	angle3 = 90.0f * deltaTime;

	model = glm::rotate(pyramidModel, glm::radians(angle2),
		glm::vec3(1.0f, 0.0f, 0.0f));

	model = glm::translate(model, glm::vec3(0.0f, 2.0f, 0.0f));

	model = glm::scale(model, glm::vec3(0.5f, 0.5f, 0.5f));

	shaderSolid.setMat4("model", model);

	DrawCube(gVAOCube);

	shaderTexture.use();

	model = glm::rotate(pyramidModel, glm::radians(angle2), glm::vec3(0.0f, 0.0f, 1.0f));

	model = glm::translate(model, glm::vec3(1.0f, 0.0f, 0.0f));

	model = glm::scale(model, glm::vec3(0.7f, 0.7f, 0.7f));

	shaderTexture.setMat4("model", model);

	DrawCube(gVAOCube); 

}

GLuint CreateCube(GLuint& VBO)
{
	float vertices[] = {
		//coordinates      //color			 //tex cord
		-0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
		0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f,
		0.5f,  0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f,
		-0.5f, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0, 1.0f,
	};

	GLuint indices[] = {
		0, 1, 2,
		0, 2, 3
	};

	GLuint VAO;
	GLuint EBO;

	glGenBuffers(1, &VBO);
	glGenBuffers(1, &EBO);
	glGenVertexArrays(1, &VAO);

	glBindVertexArray(VAO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);


	//coordinates attribute
	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);

	//color attribute
	glVertexAttribPointer(1, 3, GL_FLOAT, GL_TRUE, 8 * sizeof(float), (void*)(3 * sizeof(float)));
	glEnableVertexAttribArray(1);

	//texture attribute
	glVertexAttribPointer(2, 2, GL_FLOAT, GL_FALSE, 8 * sizeof(float), (void*)(6 * sizeof(float)));
	glEnableVertexAttribArray(2);

	// note that this is allowed, the call to glVertexAttribPointer registered VBO as the vertex attribute's bound vertex buffer object so afterwards we can safely unbind
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	// You can unbind the VAO afterwards so other VAO calls won't accidentally modify this VAO, but this rarely happens. Modifying other
	// VAOs requires a call to glBindVertexArray anyways so we generally don't unbind VAOs (nor VBOs) when it's not directly necessary.
	glBindVertexArray(0);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);

	return VAO;
}

void DrawCube(GLuint vaoID)
{
	glBindVertexArray(vaoID);

	GLuint time = SDL_GetTicks();
	GLfloat alpha = (sin(time / 1000.0f) + 1) * 0.5f;
	//float alpha = 1.0f;
	//glUniform1f(alphaLocation, alpha);
	shaderTexture.setFloat("alpha", alpha);

	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, textureId);

	glActiveTexture(GL_TEXTURE1);
	glBindTexture(GL_TEXTURE_2D, textureId2);

	glActiveTexture(GL_TEXTURE2);
	glBindTexture(GL_TEXTURE_2D, textureId3);


	glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
	//glDrawArrays(GL_TRIANGLES, 0, 6);
	glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, (void*)0);
	glBindVertexArray(0);
}

GLuint CreatePyramid()
{
	float vertices[] = {
		-0.5f, -0.5f, 0.5f,
		0.5f, -0.5f, 0.5f,
		0.5f, -0.5f, -0.5f,
		-0.5f, -0.5f, -0.5f,
		0.0f, 0.5f, 0.0f
	};

	int indices[] = {
		0, 1 ,4,
		1, 2, 4,
		2, 3, 4,
		3, 0, 4
	};

	GLuint VBO;
	GLuint VAO;
	GLuint EBO;

	glGenBuffers(1, &VBO);
	glGenBuffers(1, &EBO);
	glGenVertexArrays(1, &VAO);

	glBindVertexArray(VAO);
	glBindBuffer(GL_ARRAY_BUFFER, VBO);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, EBO);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, GL_STATIC_DRAW);

	glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 3 * sizeof(float), (void*)0);
	glEnableVertexAttribArray(0);
	
	glBindBuffer(GL_ARRAY_BUFFER, 0);
	
	glBindVertexArray(0);

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);

	
	return VAO;
}

void DrawPyramid(GLuint vaoID)
{
	glBindVertexArray(vaoID);

	glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
	glDrawElements(GL_TRIANGLES, 12, GL_UNSIGNED_INT, (void*)0);
	glBindVertexArray(0);
}
