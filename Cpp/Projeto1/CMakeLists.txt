cmake_minimum_required(VERSION 3.1)

#Definindo o nome do projeto
project(Projeto "CXX")

message(STATUS "Gerando um CONSTRUTOR para o projeto...")

add_executable(${PROJECT_NAME} src/main.cpp src/Dado.cpp)
