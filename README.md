# Arquitectura de Software - Actividad #32 - Práctica .NET: Detectar code smells y refactorizar

## 👨‍💻 Información del Estudiante
* **Nombre:** Damian Coba G.
* **Grupo:** 3B
* **Cuatrimestre:** 3er Cuatrimestre
* **Carrera:** TSU en Desarrollo e Innovación de Software
* **Profesor:** Jorge Javier Pedrozo Romero

---

# 🏥 Sistema de Gestión de Citas Médicas (Refactorización y Arquitectura Limpia)

Este proyecto es una **aplicación web ASP.NET Core MVC (.NET 10)** diseñada para administrar información relacionada con pacientes, médicos y citas médicas. 

El proyecto destaca por la implementación de una arquitectura limpia en capas (`Controllers → Interfaces → Repositories → Models`) y la aplicación práctica de técnicas de refactorización orientadas a solucionar *Code Smells* (como código duplicado y métodos largos), persistiendo los datos de manera eficiente mediante archivos JSON.

---

## 📌 Características Principales
* **Arquitectura en Capas:** Separación estricta de responsabilidades entre Modelos, Repositorios, Controladores y Vistas.
* **Inversión de Dependencias (DI):** Los controladores reciben las interfaces (ej. `ICitaRepository`) por inyección desde el contenedor de servicios en `Program.cs`.
* **Refactorización Estructural:** Implementación de técnicas como *Extract Class* (clase base genérica `JsonFileStore<T>`) y *Extract Method* para mantener el código limpio y mantenible.
* **Control de Versiones Profesional:** Seguimiento de ramas específicas y documentación de deuda técnica mediante registros ADR.

---

## 🩺 Cómo funciona el flujo MVC
1. **Petición del Usuario:** El cliente solicita una vista (ej. `GET /Cita`).
2. **Controlador Orquestador:** El `CitaController` recibe la petición y delega la responsabilidad de obtener los datos a la capa de abstracción.
3. **Repositorios de Datos:** Las clases como `JsonCitaRepository` (heredando de la clase genérica de acceso a archivos) leen la información local desde los archivos `.json` en disco.
4. **Mapeo y Vista:** Los datos DTO son mapeados a modelos de dominio utilizando métodos extraídos de responsabilidad única, y son enviados a las vistas Razor (HTML/Bootstrap) para su renderizado final.

---

## 🗺️ Documentación de Arquitectura (Deuda Técnica)
Las decisiones sobre la infraestructura del proyecto, incluyendo el registro e identificación de deudas técnicas deliberadas y sus respectivas propuestas de refactorización, se encuentran documentadas en nuestro registro de decisiones.

[🔗 Ver Documentación ADR (Deuda Técnica)](ADR.md)

---

## 📸 Capturas de Pantalla

| Vista de Citas Médicas | Vista de Pacientes |
| :---: | :---: |
| <img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/4b48141e-0a49-444a-b5f7-ff8618407f98" /> | <img width="1920" height="1080" alt="image" src="ruta/a/tu/imagen2.jpg" /> |

*(Nota: Reemplaza las rutas del atributo `src` con los links de las imágenes reales de tu aplicación corriendo).*

---

## 📁 Estructura del Proyecto

```text
CitaApp_limpio/
├── CitaApp.Web/
│   ├── Controllers/
│   │   ├── CitaController.cs
│   │   ├── HomeController.cs
│   │   ├── MedicoController.cs
│   │   └── PacienteController.cs
│   ├── data/
│   │   ├── citas.json
│   │   ├── medicos.json
│   │   └── pacientes.json
│   ├── Interfaces/
│   │   ├── ICitaRepository.cs
│   │   ├── IMedicoRepository.cs
│   │   └── IPacienteRepository.cs
│   ├── Models/
│   │   ├── Cita.cs
│   │   ├── Medico.cs
│   │   └── Paciente.cs
│   ├── Repositories/
│   │   ├── JsonCitaRepository.cs
│   │   ├── JsonFileStore.cs        
│   │   ├── JsonMedicoRepository.cs
│   │   └── JsonPacienteRepository.cs
│   ├── Views/
│   ├── Program.cs
│   └── CitaApp.Web.csproj
├── ADR.md
├── README.md
└── .gitignore