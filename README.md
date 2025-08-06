# Sistema de Gestión Académica Inteligente

## Propósito

Este repositorio contiene un sistema de gestión académica inteligente cuyo objetivo es facilitar el seguimiento y mejora del rendimiento estudiantil. El sistema permite gestionar notas, calcular promedios y proporcionar feedback personalizado a los estudiantes, además de ofrecer soporte y recomendaciones de refuerzo en las materias donde se detecten oportunidades de mejora.

## Tecnologías Utilizadas

- **.NET 8**: Framework principal para el desarrollo de la solución.
- **ASP.NET Core**: Para la construcción de APIs RESTful.
- **Swagger/OpenAPI**: Documentación y pruebas interactivas de la API.
- **Arquitectura por Capas (Clean Architecture)**: Separación clara de responsabilidades en los siguientes proyectos:
  - **Domain**: Entidades y lógica de negocio central.
  - **Application/UseCases**: Casos de uso y lógica de aplicación.
  - **Infrastructure/Persistence**: Acceso a datos y persistencia.
  - **Presentation/API**: Exposición de servicios mediante controladores HTTP.

## Arquitectura

El sistema está estructurado siguiendo principios de Clean Architecture, lo que facilita la escalabilidad, mantenibilidad y testeo. Cada capa tiene una responsabilidad bien definida:

- **Domain**: Define las entidades principales como `Student`, `Grade`, etc.
- **Application/UseCases**: Implementa la lógica para calcular promedios, generar feedback y gestionar el soporte académico.
- **Infrastructure/Persistence**: Implementa la persistencia de datos, por ejemplo, mediante Entity Framework Core.
- **Presentation/API**: Expone los servicios a través de endpoints REST, permitiendo la integración con clientes web, móviles u otros sistemas.

## Funcionalidades Principales

- Registro y gestión de estudiantes y sus calificaciones.
- Cálculo automático de promedios académicos.
- Generación de feedback personalizado según el desempeño.
- Sugerencias de refuerzo académico en áreas de bajo rendimiento.
- API documentada y testeable mediante Swagger.

## Cómo empezar

1. Clona el repositorio.
2. Restaura los paquetes NuGet.
3. Configura la cadena de conexión a la base de datos en el proyecto de infraestructura.
4. Ejecuta el proyecto `campusuno.API` para iniciar la API y acceder a la documentación Swagger.

## Licencia

Este proyecto está licenciado bajo los términos de la licencia MIT. Consulta el archivo `LICENSE` para más detalles.

© 2025 milodevv. Todos los derechos reservados.

---

Este sistema está diseñado para ser la base de una plataforma educativa moderna, adaptable y centrada en el estudiante.
