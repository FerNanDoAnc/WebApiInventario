# 🚀 Backend API - Inventario (ASP.NET Core 9)

Este proyecto es una API REST construida con .NET 9 que permite gestionar productos y categorías utilizando procedimientos almacenados en MySQL.

## 🔧 Requisitos

- [.NET SDK 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- MySQL corriendo en Docker (puerto 3307 o configurado)
- Visual Studio 2022 o superior (opcional)

## ⚙️ Configuración

### `appsettings.json`

```json
{
  "ConnectionStrings": {
    "MySqlConnection": "Server=localhost;Port=3307;Database=inventario_db;Uid=admin;Pwd=contrasenia;"
  },
  "AllowedHosts": "*"
}
