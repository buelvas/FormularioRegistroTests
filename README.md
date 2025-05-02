# FormularioRegistroTests

Este repositorio contiene un conjunto de pruebas automatizadas para validar un formulario de registro utilizando **Selenium** y **NUnit**. Las pruebas incluyen varios escenarios de validación, como el registro exitoso, validaciones de campos vacíos, validación de correo electrónico y celular, contraseñas débiles, y más.

## Descripción

El formulario de registro está implementado en **HTML** y **JavaScript** y las pruebas automatizadas se ejecutan utilizando el navegador **Chrome** a través de **Selenium WebDriver**. Las pruebas se aseguran de que el formulario valide correctamente los campos ingresados por el usuario y muestre los mensajes de éxito o error correspondientes.

## Tecnologías Utilizadas

- **Selenium WebDriver** (para automatización de pruebas de la interfaz de usuario)
- **NUnit** (framework para pruebas unitarias)
- **ChromeDriver** (para interactuar con el navegador Chrome)
- **.NET 8** (plataforma de desarrollo)
- **Visual Studio** (IDE para el desarrollo)

## Requisitos

- **Visual Studio 2022** o superior.
- **.NET 8.0** o superior.
- **Google Chrome** instalado en tu máquina (necesario para el **ChromeDriver**). Debes tener en cuenta que la versión de tú **Google Chrome** debe coincidir con la versión de **ChromeDriver**.

## Instalación y Ejecución

### 1. Clonar el Repositorio

Para comenzar, clona este repositorio en tu máquina local:

```bash
git clone https://github.com/buelvas/FormularioRegistroTests.git
```

### 2. Instalar Dependencias
En primer lugar, debes instalar el SDK de .NET 8: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

Asegúrate de tener **Visual Studio** instalado. Luego, abre el proyecto en **Visual Studio**.

Para instalar los paquetes necesarios, abre la **Consola del Administrador de Paquetes NuGet** y ejecuta:

```bash
dotnet restore
```

Esto instalará todas las dependencias del proyecto.

### 3. Ejecutar las Pruebas

#### Opción 1: Ejecutar desde Visual Studio

Abre el proyecto en Visual Studio.

Haz clic en "Ejecutar todas las pruebas" en el menú de Test Explorer.

Las pruebas se ejecutarán y los resultados se mostrarán en la consola de salida de Visual Studio.

#### Opción 2: Ejecutar desde la Consola

Si prefieres ejecutar las pruebas desde la línea de comandos:

Navega a la carpeta del proyecto.

Ejecuta el siguiente comando:

```bash
dotnet test
```

## Contribuciones

Si deseas contribuir a este proyecto, por favor realiza un fork, crea tu rama de características y abre un pull request.
