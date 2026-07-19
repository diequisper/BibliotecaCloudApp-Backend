# Biblioteca App – Backend

### Introducción
Backend tipo servicio web (API) del proyecto **Biblioteca App**, con las siguientes funcionalidades principales:
- Consulta de información de libros, autores y editoriales
- Inicio de sesión y registro de usuarios
- Gestión de favoritos (libros, autores o editoriales, requiere autenticación)

### Contexto
Proyecto académico desarrollado en el curso **Desarrollo de Aplicaciones en la Nube**, con énfasis en la **integración y entrega continua (CI/CD)**. <br/>
- Trabajé en equipo de 3 integrantes
- Me enfoqué en realizar el backend
- Implementé algunas funciones del frontend
- Repositorio del frontend: [frontend-bibliotecaApp](https://github.com/XianMina/BookStore.git)

### Características Técnicas
- **Contenedores**: Backend y base de datos dockerizados  
  Disponible en [Docker Hub – diegeauq/daan_proyecto](https://hub.docker.com/repository/docker/diegeauq/daan_proyecto/general)  
- **CI/CD**: Automatización de integración continua con **Azure DevOps**  
  <img width="196" height="140" alt="image" src="https://github.com/user-attachments/assets/27640d3e-4791-4a04-9083-a2104ef58d49" />
- **Framework**: .NET Core 8.0  
- **Seguridad**: Contraseñas protegidas mediante **PBKDF2**  

### Cómo Ejecutar
1. Crear una carpeta con cualquier nombre y en cualquier lugar
2. Dentro, crear un archivo docker-compose.yml con la siguiente estructura
```yaml
version: '3.8'
services: 
  web: 
    image: diegeauq/daan_proyecto:api 
    container_name: daan_api 
    ports: 
      - "5000:44374" 
    depends_on: 
      - db 
    environment: 
      ASPNETCORE_URLS: "http://+:44374" 
      ConnectionStrings__DefaultConnection: "Server=db;Database=dbbteca;User Id=sa;Password=CoolyPassy0!" 
  db: 
    image: diegeauq/daan_proyecto:db 
    container_name: daan_db 
    environment: 
      SA_PASSWORD: "CoolyPassy0!" 
      ACCEPT_EULA: "Y" 
    ports: 
      - "1433:1433"
```
3. Abrir powershell en la ruta de la carpeta creada y jalar las imagenes desde DockerHub
```powershell
docker pull diegeauq/daan_proyecto:api
```
```powershell
docker pull diegeauq/daan_proyecto:db
```
4. Ejecutar el docker-compose.yml
```powershell
docker-compose up
```
