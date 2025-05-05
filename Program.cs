using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Threading;

namespace RegistroAutomatizado
{
    public class FormularioRegistroTests
    {
        IWebDriver driver;
        string htmlFilePath = "";

        [SetUp]
        public void SetUp()
        {
            try
            {
                driver = new ChromeDriver();
                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                htmlFilePath = Path.Combine(currentDirectory, "index.html");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar el entorno de pruebas: {ex.Message}");
                throw;
            }
        }

        private void IrAFormulario()
        {
            try
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl($"file:///{htmlFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al navegar al formulario: {ex.Message}");
                throw;
            }
        }

        private void LlenarFormulario(string nombre, string correo, string celular, string contrasena)
        {
            try
            {
                driver.FindElement(By.Id("nombre")).SendKeys(nombre);
                driver.FindElement(By.Id("correo")).SendKeys(correo);
                driver.FindElement(By.Id("celular")).SendKeys(celular);
                driver.FindElement(By.Id("contraseña")).SendKeys(contrasena);
                driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al llenar el formulario: {ex.Message}");
                throw;
            }
        }

        // CP1: Registro con datos válidos
        [Test]
        [Order(1)]
        public void TestRegistroExitoso()
        {
            try
            {
                Console.WriteLine("Ejecutando CP1: Registro con datos válidos");

                IrAFormulario();
                LlenarFormulario("Juan Perez", "juan@email.com", "3211234567", "Segura2024$");

                Thread.Sleep(2000);
                var mensaje = driver.FindElement(By.Id("mensaje")).Text;

                Console.WriteLine($"Mensaje recibido: {mensaje}");
                Assert.AreEqual("¡Registro Exitoso!", mensaje);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la prueba CP1: {ex.Message}");
                throw;
            }
        }

        // CP2: Registro con correo y celular inválido
        [Test]
        [Order(3)]
        public void TestCorreoYCelularInvalidos()
        {
            try
            {
                Console.WriteLine("Ejecutando CP2: Registro con correo y celular inválido");

                IrAFormulario();
                LlenarFormulario("Ana", "anaemail.com", "12345", "Segura2024$");

                Thread.Sleep(2000);
                var mensajeError = driver.FindElement(By.Id("mensajeError")).Text;

                Console.WriteLine($"Mensaje de error recibido: {mensajeError}");
                Assert.AreEqual("Correo electrónico inválido.", mensajeError);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la prueba CP2: {ex.Message}");
                throw;
            }
        }

        // CP3: Registro con contraseña débil
        [Test]
        [Order(2)]
        public void TestContraseñaDebil()
        {
            try
            {
                Console.WriteLine("Ejecutando CP3: Registro con contraseña débil");

                IrAFormulario();
                LlenarFormulario("Luis", "luis@email.com", "3211234567", "123");

                Thread.Sleep(2000);
                var mensajeError = driver.FindElement(By.Id("mensajeError")).Text;
                Console.WriteLine($"Mensaje de error recibido: {mensajeError}");
                Assert.AreEqual("La contraseña debe tener al menos 8 caracteres, incluyendo letras y números.", mensajeError);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la prueba CP3: {ex.Message}");
                throw;
            }
        }

        // CP4: Registro con campos vacíos
        [Test]
        [Order(5)]
        public void TestCamposVacios()
        {
            try
            {
                Console.WriteLine("Ejecutando CP4: Registro con campos vacíos");

                IrAFormulario();
                LlenarFormulario("", "", "", "");

                Thread.Sleep(2000);
                var mensajeError = driver.FindElement(By.Id("mensajeError")).Text;
                Console.WriteLine($"Mensaje de error recibido: {mensajeError}");
                Assert.AreEqual("Todos los campos son obligatorios o tienen un formato incorrecto.", mensajeError);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la prueba CP4: {ex.Message}");
                throw;
            }
        }

        // CP5: Registro con contraseñas especiales y nombres no alfabéticos
        [Test]
        [Order(4)]
        public void TestContraseñasYNombresEspeciales()
        {
            try
            {
                Console.WriteLine("Ejecutando CP5: Registro con contraseñas especiales y nombres no alfabéticos");

                IrAFormulario();
                LlenarFormulario("José-123", "jose@email.com", "3211234567", "Contrasena$123");

                Thread.Sleep(2000);
                var mensaje = driver.FindElement(By.Id("mensaje")).Text;
                Console.WriteLine($"Mensaje recibido: {mensaje}");
                Assert.AreEqual("¡Registro Exitoso!", mensaje);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la prueba CP5: {ex.Message}");
                throw;
            }
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                driver.Quit();
                Console.WriteLine("Cerrando el navegador.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cerrar el navegador: {ex.Message}");
            }
        }
    }
}
