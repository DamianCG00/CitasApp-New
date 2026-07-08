# Documentación Arquitectónica C4 - CitasApp

### C4 Nivel 1 - Contexto
**Para quién es:** Para cualquier persona, incluyendo clientes, profesores o equipo no técnico.
**Qué pregunta responde:** ¿Qué es el sistema CitasApp y quién interactúa con él a gran escala?

```mermaid
graph TD
    Usuario[Usuario / Navegador Web] -->|Agenda y gestiona| CitasApp[CitasApp: Sistema de Gestión Médica]
    CitasApp -->|Envía notificaciones de confirmación| Paciente[Paciente / Médico]
```

---

### C4 Nivel 2 - Contenedores
**Para quién es:** Para el equipo técnico, desarrolladores de software y arquitectos.
**Qué pregunta responde:** ¿Cuáles son las piezas principales (aplicación Web, Core de negocio, Infraestructura) y cómo se comunican entre ellas?

```mermaid
graph TD
    Usuario[Usuario / Navegador] -->|Peticiones HTTP| Web[CitasApp.Web: Aplicación MVC]
    Web -->|Llamadas a servicios| Core[CitasApp.Application / Domain: Core de Negocio]
    Core -->|Lectura / Escritura| Infra[CitasApp.Infrastructure: Acceso a Datos]
    Infra -->|Persistencia| DB[(Base de Datos: JSON, CSV, SQLite)]
```

---

### C4 Nivel 3 - Componentes
**Para quién es:** Para los programadores que van a tocar y modificar el código directamente.
**Qué pregunta responde:** ¿Cómo están estructuradas las capas internas (Domain, Application, Infrastructure) y dónde están aplicados los patrones GoF (Factory, Decorator) dentro de la pieza principal?

```mermaid
graph TD
    %% Capa Web
    subgraph CitasApp.Web
        Controllers[Controladores MVC] --> Program[Program.cs: Inyección de Dependencias]
    end
    
    %% Capa Application
    subgraph CitasApp.Application
        Program --> CS[CitaService]
        Program --> MS[MedicoService]
        Program --> PS[PacienteService]
    end
    
    %% Capa Domain
    subgraph CitasApp.Domain
        CS --> Int[Interfaces: IRepository]
        MS --> Int
        PS --> Int
    end
    
    %% Capa Infrastructure (Con Patrones GoF)
    subgraph CitasApp.Infrastructure
        Int --> Log[LoggingPacienteRepository: Decorator]
        Int --> Fact[RepositoryFactory: Factory Pattern]
        
        Fact --> ReposJSON[Repositorios JSON]
        Fact --> ReposCSV[Repositorios CSV]
        Fact --> ReposSQL[Repositorios SQLite]
        
        Log --> ReposJSON
    end
```

---

### Declaración de Uso de IA
Para la elaboración de esta documentación, utilicé asistencia de inteligencia artificial (Gemini) exclusivamente como herramienta de apoyo para generar y estructurar la sintaxis de los diagramas Mermaid. El diseño, la definición de las capas arquitectónicas y la implementación de los patrones GoF documentados están basados al 100% en el código fuente de mi autoría en el proyecto CitasApp.