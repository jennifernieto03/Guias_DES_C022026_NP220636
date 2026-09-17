using Microsoft.AspNetCore.Mvc;
using Registro_Personas_Naturales_NP220636.Controllers;
using Registro_Personas_Naturales_NP220636.Models;
using System;
using Xunit;

namespace PersonasAPI.Tests
{
    public class PersonasControllerTest
    {
        [Fact]
        public async Task PostPersona_AgregarPersona_CuandoDatosSonValidos()
        {
            // Arrange
            var context = Setup.GetDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona
            {
                PrimerNombre = "Juan",
                PrimerApellido = "Pérez",
                DUI = "12345678-9",
                FechaNacimiento = new DateTime(1990, 5, 20)
            };

            // Act
            var result = await controller.PostPersona(nuevaPersona);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var persona = Assert.IsType<Persona>(createdResult.Value);
            Assert.Equal("Juan", persona.PrimerNombre);
        }

        [Fact]
        public async Task PostPersona_RetornaBadRequest_CuandoPrimerNombreEsNulo()
        {
            // Arrange
            var context = Setup.GetDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona
            {
                PrimerNombre = null, // Inválido
                PrimerApellido = "Pérez",
                DUI = "12345678-9",
                FechaNacimiento = new DateTime(1990, 5, 20)
            };

            // Act
            var result = await controller.PostPersona(nuevaPersona);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostPersona_RetornaBadRequest_CuandoDUIEsInvalido()
        {
            // Arrange
            var context = Setup.GetDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona
            {
                PrimerNombre = "Juan",
                PrimerApellido = "Pérez",
                DUI = "1234567-89", // Formato incorrecto
                FechaNacimiento = new DateTime(1990, 5, 20)
            };

            // Act
            var result = await controller.PostPersona(nuevaPersona);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostPersona_RetornaBadRequest_CuandoNombreExcede100Caracteres()
        {
            // Arrange
            var context = Setup.GetDatabaseContext();
            var controller = new PersonasController(context);
            var nuevaPersona = new Persona
            {
                PrimerNombre = new string('A', 101), // Excede 100 caracteres
                PrimerApellido = "Pérez",
                DUI = "12345678-9",
                FechaNacimiento = new DateTime(1990, 5, 20)
            };

            // Act
            var result = await controller.PostPersona(nuevaPersona);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
