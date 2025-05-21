# 🚚 Proyecto de Integración - Empresa de Transporte de Carga

Este repositorio contiene el desarrollo de dos aplicaciones ASP.NET Core correspondientes a los sistemas de una empresa de transporte de carga: el **Sistema de Mantención de Camiones** y el **Sistema de Transporte**. Ambas aplicaciones utilizan una base de datos MySQL compartida y han sido desarrolladas con Visual Studio 2022 y Entity Framework Core 8 bajo la metodología **Database First**.

## 📌 Descripción del Problema

La empresa posee una flota de camiones para realizar viajes entre distintas ciudades. Para su gestión, requiere de dos sistemas:

1. **Sistema de Mantención de Camiones**: Permite registrar y administrar el mantenimiento preventivo de los camiones, actualizar estados (Disponible, EnViaje, Malo, EnReparación) y el kilometraje.
2. **Sistema de Transporte**: Registra y administra los viajes realizados, asociados a un camión y cliente, con control de estado (Iniciado/Terminado) y selección de camiones disponibles.

## 🧰 Tecnologías Utilizadas

- Visual Studio 2022 Community
- ASP.NET Core
- Entity Framework Core 8
- MySQL Workbench
- MySQL Server
- Bootstrap (para interfaz de usuario)
- C#
  
### Sistema de Mantención de Camiones
- Listar camiones
- Modificar estado del camión
- Actualizar kilometraje
- Registrar mantenciones
- Listar mantenciones de un camión

### Sistema de Transporte
- Registrar nuevo viaje (con camiones disponibles)
- Listar viajes
- Cambiar estado del viaje (Iniciado/Terminado)
