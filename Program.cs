using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // CP1: Registro con datos válidos
        [Test]
        public void TestRegistroExitoso()
        {
            Console.WriteLine("Ejecutando CP1: Registro con datos válidos");

            driver.Navigate().GoToUrl($"file:///{htmlFilePath}");

            driver.FindElement(By.Id("nombre")).SendKeys("Juan Pérez");
            driver.FindElement(By.Id("correo")).SendKeys("juan@email.com");
            driver.FindElement(By.Id("celular")).SendKeys("1234567890");
            driver.FindElement(By.Id("contraseña")).SendKeys("ContraseñaSegura123");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(2000);
            var mensaje = driver.FindElement(By.Id("mensaje")).Text;

            Console.WriteLine($"Mensaje recibido: {mensaje}");
            Assert.AreEqual("¡Registro Exitoso!", mensaje);

            Console.WriteLine("Prueba exitosa: Registro completado correctamente");
        }

        // CP2: Registro con correo y celular inválido
        [Test]
        public void TestCorreoYCelularInvalidos()
        {
            Console.WriteLine("Ejecutando CP2: Registro con correo y celular inválido");

            driver.Navigate().GoToUrl($"file:///{htmlFilePath}");

            driver.FindElement(By.Id("nombre")).SendKeys("Ana");
            driver.FindElement(By.Id("correo")).SendKeys("anaemail.com");
            driver.FindElement(By.Id("celular")).SendKeys("12345");
            driver.FindElement(By.Id("contraseña")).SendKeys("ContraseñaSegura123");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(2000);
            var mensajeError = driver.FindElement(By.Id("mensajeError")).Text;

            Console.WriteLine($"Mensaje de error recibido: {mensajeError}");

            Assert.AreEqual("Correo electrónico inválido.", mensajeError);
            Console.WriteLine("Prueba exitosa: Error de correo inválido mostrado correctamente");
        }

        // CP3: Registro con contraseña débil
        [Test]
        public void TestContraseñaDebil()
        {
            Console.WriteLine("Ejecutando CP3: Registro con contraseña débil");

            driver.Navigate().GoToUrl($"file:///{htmlFilePath}");

            driver.FindElement(By.Id("nombre")).SendKeys("Luis");
            driver.FindElement(By.Id("correo")).SendKeys("luis@email.com");
            driver.FindElement(By.Id("celular")).SendKeys("1234567890");
            driver.FindElement(By.Id("contraseña")).SendKeys("123");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(2000);
            var mensajeError = driver.FindElement(By.Id("mensajeError")).Text;

            Console.WriteLine($"Mensaje de error recibido: {mensajeError}");

            Assert.AreEqual("La contraseña debe tener al menos 8 caracteres.", mensajeError);
            Console.WriteLine("Prueba exitosa: Error de contraseña débil mostrado correctamente");
        }

        // CP4: Registro con campos vacíos
        [Test]
        public void TestCamposVacios()
        {
            Console.WriteLine("Ejecutando CP4: Registro con campos vacíos");

            driver.Navigate().GoToUrl($"file:///{htmlFilePath}");

            driver.FindElement(By.Id("nombre")).Clear();
            driver.FindElement(By.Id("correo")).Clear();
            driver.FindElement(By.Id("celular")).Clear();
            driver.FindElement(By.Id("contraseña")).Clear();
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(2000);
            var mensajeError = driver.FindElement(By.Id("mensajeError")).Text;

            Console.WriteLine($"Mensaje de error recibido: {mensajeError}");

            Assert.AreEqual("Todos los campos son obligatorios o tienen un formato incorrecto.", mensajeError);

            Console.WriteLine("Prueba exitosa: Error mostrado correctamente");
        }

        // CP5: Registro con contraseñas especiales y nombres no alfabéticos
        [Test]
        public void TestContraseñasYNombresEspeciales()
        {
            Console.WriteLine("Ejecutando CP5: Registro con contraseñas especiales y nombres no alfabéticos");

            driver.Navigate().GoToUrl($"file:///{htmlFilePath}");

            driver.FindElement(By.Id("nombre")).SendKeys("José-123");
            driver.FindElement(By.Id("correo")).SendKeys("jose@email.com");
            driver.FindElement(By.Id("celular")).SendKeys("1234567890");
            driver.FindElement(By.Id("contraseña")).SendKeys("Contraseña$123");
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(2000);
            var mensaje = driver.FindElement(By.Id("mensaje")).Text;

            Console.WriteLine($"Mensaje recibido: {mensaje}");

            Assert.AreEqual("¡Registro Exitoso!", mensaje);
            Console.WriteLine("Prueba exitosa: Campos especiales aceptados correctamente");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
