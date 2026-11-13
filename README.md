# Biblioteca App – Backend

### Introducción
Backend tipo servicio web (API) del proyecto **Biblioteca App**, con las siguientes funcionalidades principales:
- Consulta de información de libros, autores y editoriales
- Inicio de sesión y registro de usuarios
- Gestión de favoritos (libros, autores o editoriales, requiere autenticación)
- Documentación en Notion: [Notion-BibliotecaCloudApp](https://app.notion.com/p/Biblioteca-Cloud-App-3d3fe676179480e78900de0c08b0e7d0?source=copy_link) 

### Contexto
Proyecto académico desarrollado en el curso **Desarrollo de Aplicaciones en la Nube**, con énfasis en la **integración y entrega continua (CI/CD)**. <br/>
- Trabajé en equipo de 3 integrantes
- Me enfoqué en realizar el backend
- Desarrollé el frontend 1 en su totalidad
- Colaboré en el desarrollo del frontend 2
  
Repositorio del frontend 1: [frontend1-bibliotecaApp](https://github.com/diequisper/biblioteca-cloud-app-frontend.git) <br/>
Repositorio del frontend 2: [frontend2-bibliotecaApp](https://github.com/XianMina/BookStore.git)

### Características Técnicas
- **Contenedores**: Docker para el backend y la base de datos(sql server)
  Disponible en [Docker Hub – diegeauq/daan_proyecto](https://hub.docker.com/repository/docker/diegeauq/daan_proyecto/general)  
- **CI/CD**: Automatización de integración continua con **Azure DevOps**  
  <img width="151" height="92" alt="image" src="https://github.com/user-attachments/assets/fed902c4-7805-44b2-8178-1be683e13f87" />
- **Framework**: .NET 8  
- **Seguridad**: Contraseñas protegidas mediante **PBKDF2**
- **Persistencia**: SQL Server
- **Gestión de sesiones**: Cookies HTTP-only para el almacenamiento del token.
- **Autenticación**: JWT
- **Autorización**: Restricción de acceso a endpoints de favoritos para usuarios autenticados. 

### Cómo Ejecutar en Docker
  #### Requisitos
  - Docker Desktop instalado y en ejecución.
  - Certificado HTTPS de desarrollo de ASP.NET Core.
  #### Pasos
  1. Descargar el archivo `docker-compose.yml` disponible en este repositorio.
  2. Configurar las variables de entorno. La plantilla está disponible en el archivo `.env.example`. Completar los valores                  correspondientes y guardar una copia con el nombre `.env` en la misma carpeta que `docker-compose.yml`.
  3. Generar el certificado HTTPS de desarrollo de ASP.NET Core. Este certificado con extensión ```.pfx``` debe crearse en la ruta ```%USERPROFILE%\.aspnet\https``` con el comando:
  ```powershell
  dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\aspnetapp.pfx" -p "TuContraseñaCertificado"
  ```
  4. Definir la variable de entorno ```HTTPS_CERT_PASSWORD``` con la contraseña usada en el paso 3
  5. Abrir PowerShell en la carpeta donde se encuentran los archivos `docker-compose.yml` y `.env`.
  6. Levantar el contenedor con ```docker compose up``` Docker descargará automáticamente las imágenes necesarias desde Docker Hub y        levantará los servicios.

  Una vez que las imágenes (db y backend) esten ejecutandose en el contenedor, el frontend 1 puede clonarse y ejecutarse con ```ng serve --ssl``` y este se comunicará con ellos.
  
  **Importante!** El frontend está configurado para utilizar HTTPS, por lo que debe ejecutarse con el parámetro `--ssl`. Si el navegador muestra una advertencia de seguridad relacionada con el certificado de desarrollo, será necesario configurar la confianza del certificado antes de continuar. En la pagina de Notion para este proyecto existe un tutorial para enclarecer esto.
