# APILIBROS

API minimal para gestionar libros (proyecto .NET 6 / C# 10).
## Requisitos previos
- .NET SDK 6 (instalar desde Microsoft)
- Git
- Opcional: Visual Studio 2022 (compatible con .NET 6) o VS Code

## Clonar el repositorio

``` git clone https://github.com/armazapol/APILIBROS.git ```


## Restaurar dependencias

Navegar a la carpeta del proyecto y ejecutar:
``` dotnet restore ```

## Modificar cadena de conexión

Editar el archivo `appsettings.json` para configurar la cadena de conexión a la base de datos Postgres:
``` 
"ConnectionStrings": {
	"PostgreSqlConnection": "Server=127.0.0.1;Port=5432;Database=ExamenTadconDB;User Id=tu_usuario;Password=tu_contraseña"
  }
```

## Ejecutar la aplicación
``` dotnet run ```

La API estará disponible en `https://localhost:7147`.
