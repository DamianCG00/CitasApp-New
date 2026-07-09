# Documentación Arquitectónica - CitasApp
Este diagrama refleja la arquitectura interna del sistema, estructurada en capas (Domain, Application, Infrastructure) y la aplicación de los patrones de diseño GoF (Factory, Decorator).
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