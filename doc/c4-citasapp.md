# Documentación Arquitectónica C4 - CitasApp

## C4 Nivel 1 - Contexto
**Para quién es:** Para todos (clientes, profesores, equipo técnico y no técnico).
**Qué pregunta responde:** ¿Qué es el sistema y quién lo usa en términos simples?

```mermaid
graph TD
    Usuario[Usuario / Navegador Web] -->|Agenda y gestiona| CitasApp[CitasApp: Sistema de Gestión Médica]
    CitasApp -->|Envía notificaciones de confirmación| Paciente[Paciente / Médico]