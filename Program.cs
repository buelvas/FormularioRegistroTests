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
            driver = new ChromeDriver();
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            htmlFilePath = Path.Combine(currentDirectory, "index.html");
            Console.WriteLine("Iniciando las pruebas automatizadas...");
        }

        private void IrAFormulario()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl($"file:///{htmlFilePath}");
        }

        private void LlenarFormulario(string nombre, string correo, string celular, string contrasena)
        {
            driver.FindElement(By.Id("nombre")).SendKeys(nombre);
            driver.FindElement(By.Id("correo")).SendKeys(correo);
            driver.FindElement(By.Id("celular")).SendKeys(celular);
            driver.FindElement(By.Id("contraseña")).SendKeys(contrasena);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        // CP1: Registro con datos válidos
        [Test]
        public void TestRegistroExitoso()
        {
            Console.WriteLine("Ejecutando CP1: Registro con datos válidos");

            IrAFormulario();
            LlenarFormulario("Juan Perez", "juan@email.com", "3211234567", "Segura2024$");

            Thread.Sleep(2000);
            var mensaje = driver.FindElement(By.Id("mensaje")).Text;

            Console.WriteLine($"Mensaje recibido: {mensaje}");
            Assert.AreEqual("¡Registro Exitoso!", mensaje);
        }

        // CP2: Registro con correo y celular inválido
        [Test]
        public void TestCorreoYCelularInvalidos()
        {
            Console.WriteLine("Ejecutando CP2: Registro con correo y celular inválido");

            IrAFormulario();
            LlenarFormulario("Ana", "anaemail.com", "12345", "Segura2024$");

            Thread.Sleep(2000);
            var mensajeError = driver.FindElement(By.Id("mensajeError")).Text;

            Console.WriteLine($"Mensaje de error recibido: {mensajeError}");
            Assert.AreEqual("Correo electrónico inválido.", mensajeError);
        }

        // CP3: Registro con contraseña débil
        [Test]
        public void TestContraseñaDebil()
        {
            Console.WriteLine("Ejecutando CP3: Registro con contraseña débil");

            IrAFormulario();
            LlenarFormulario("Luis", "luis@email.com", "3211234567", "123");

            Thread.Sleep(2000);
            var mensajeError = driver.FindElement(By.Id("mensajeError")).Text;

            Console.WriteLine($"Mensaje de error recibido: {mensajeError}");
            Assert.AreEqual("La contraseña debe tener al menos 8 caracteres, incluyendo letras y números.", mensajeError);
        }

        // CP4: Registro con campos vacíos
        [Test]
        public void TestCamposVacios()
        {
            Console.WriteLine("Ejecutando CP4: Registro con campos vacíos");

            IrAFormulario();
            LlenarFormulario("", "", "", "");

            Thread.Sleep(2000);
            var mensajeError = driver.FindElement(By.Id("mensajeError")).Text;

            Console.WriteLine($"Mensaje de error recibido: {mensajeError}");
            Assert.AreEqual("Todos los campos son obligatorios o tienen un formato incorrecto.", mensajeError);
        }

        // CP5: Registro con contraseñas especiales y nombres no alfabéticos
        [Test]
        public void TestContraseñasYNombresEspeciales()
        {
            Console.WriteLine("Ejecutando CP5: Registro con contraseñas especiales y nombres no alfabéticos");

            IrAFormulario();
            LlenarFormulario("José-123", "jose@email.com", "3211234567", "Contrasena$123");

            Thread.Sleep(2000);
            var mensaje = driver.FindElement(By.Id("mensaje")).Text;

            Console.WriteLine($"Mensaje recibido: {mensaje}");
            Assert.AreEqual("¡Registro Exitoso!", mensaje);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
