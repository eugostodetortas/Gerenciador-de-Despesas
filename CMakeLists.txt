cmake_minimum_required(VERSION 3.15)
project(ExpenseTracker LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Encontra as dependências instaladas via vcpkg
find_package(nlohmann_json 3.2.0 REQUIRED)
find_package(libxlsxwriter REQUIRED)

# Adiciona o diretório 'src' para que os includes funcionem sem prefixo
include_directories(src)

# Adiciona o executável principal
add_executable(ExpenseTracker src/main.cpp src/Despesa.cpp src/GerenciadorDeDespesas.cpp)

# Vincula as bibliotecas ao executável
target_link_libraries(ExpenseTracker PRIVATE
    nlohmann_json::nlohmann_json
    xlsxwriter
)