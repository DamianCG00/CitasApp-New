# Arquitectura de Software - Actividad #26 - Práctica .NET: Implementación de API REST, Calculadora y Patrones GoF

## 👨‍💻 Información del Estudiante

* **Nombre:** Damian Coba G.
* **Grupo:** 3B
* **Cuatrimestre:** 3er Cuatrimestre
* **Carrera:** TSU en Desarrollo e Innovación de Software
* **Profesor:** Jorge Javier Pedrozo Romero

---

# 🏥 Sistema de Gestión de Citas Médicas (API REST & MVC)

Este proyecto es una **aplicación web y API REST desarrollada en .NET 8** que permite administrar información relacionada con pacientes, médicos y citas médicas. 

Además del sistema base MVC, el proyecto destaca por la construcción de una **API REST independiente** con arquitectura orientada a servicios, implementando endpoints de cálculos matemáticos y aplicando **Patrones de Diseño (GoF)** para optimizar el rendimiento y manejo de datos mediante archivos JSON.

---

## 📌 Características Principales

* **Arquitectura Desacoplada:** Separación entre el proyecto MVC tradicional y una API REST pura.
* **Gestión de Datos Centralizada:** Implementación del **Patrón Singleton** (`DataService`) para optimizar la lectura/escritura de archivos JSON en memoria.
* **API de Entidades:** Endpoints (GET, POST, PUT, DELETE) completos para Pacientes, Médicos y Citas.
* **API de Servicios (Calculadora):** Endpoints personalizados para cálculos matemáticos (suma, resta, multiplicación, división), cálculo de IMC y cálculo de edad.
* **Control de Versiones Profesional:** Desarrollo basado en ramas independientes (`main`, `Api`, `Api-Calculadora`, `GOF-Patrones`) integradas en GitHub.

---

## 🩺 Cómo funciona el sistema API

1. **Peticiones HTTP:** El usuario o aplicación cliente realiza peticiones web a los endpoints de la API (ej. `GET /api/pacientes`).
2. **DataService (Singleton):** El servicio intercepta la petición, accede a los datos cargados en memoria (provenientes de los JSON) y los devuelve de forma instantánea sin saturar el disco.
3. **Controladores Ligeros:** Los controladores delegan la lógica de negocio al servicio, manteniendo el código limpio y mantenible.
4. **Calculadora Inteligente:** El controlador `CalculadoraController` recibe parámetros por URL (`[FromQuery]`) y devuelve los resultados matemáticos y lógicos en formato JSON.

---

## 🗺️ Documentación de Arquitectura (Modelo C4)

La arquitectura completa de este sistema, estructurada en capas (Domain, Application, Infrastructure, Web) y destacando la implementación de los patrones GoF, ha sido documentada visualmente mediante código.

[🔗 Ver Documentación C4 (Contexto, Contenedores y Componentes)](doc/c4-citasapp.md)

---

## 📸 Capturas de Pantalla

| Endpoint Pacientes (JSON) | Endpoint Calculadora IMC |

| <img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/4b48141e-0a49-444a-b5f7-ff8618407f98" />

---

## 📁 Estructura del Proyecto (Rama GOF-Patrones)

```text
CitasApp-New/
├── CitasApp.Api/
│   ├── Controllers/
│   │   ├── CalculadoraController.cs
│   │   ├── CitasController.cs
│   │   ├── MedicosController.cs
│   │   └── PacientesController.cs
│   ├── data/
│   │   ├── citas.json
│   │   ├── medicos.json
│   │   └── pacientes.json
│   ├── Models/
│   │   └── Models.cs
│   ├── Services/
│   │   └── DataService.cs  <-- (Implementación Singleton)
│   ├── Program.cs
│   └── CitasApp.Api.csproj
├── CitasApp.Web/ (Proyecto MVC Base)
├── README.md
└── .gitignore