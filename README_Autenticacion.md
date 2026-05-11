# Configuración de Autenticación JWT

## Resumen
Se ha configurado un sistema de autenticación JWT completo para la API SIGEVET en .NET 8, incluyendo:

- Generación y validación de tokens JWT
- Sistema de refresh tokens para renovación segura
- Hashing seguro de contraseñas con salt
- Protección de intentos fallidos de login
- Endpoints de login y refresh

## Configuración

### 1. Claves JWT
En `appsettings.json`, configura las claves JWT:
```json
"Jwt": {
  "Key": "TuClaveSecretaMuyLargaYSeguraAquiDeAlMenos32Caracteres",
  "Issuer": "https://localhost:5001",
  "Audience": "https://localhost:5001"
}
```

**IMPORTANTE:** Cambia la `Key` por una clave secreta segura y única para tu aplicación.

### 2. Base de Datos
Se agregó una migración para incluir el campo `salt` en la tabla `CuentasUsuarios`. Asegúrate de aplicar las migraciones:
```bash
dotnet ef database update
```

## Endpoints de Autenticación

### Login
**POST** `/api/CuentasUsuarios/login`

**Request Body:**
```json
{
  "usuario": "admin",
  "contrasenia": "password123"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "abc123...",
  "expiration": "2024-05-10T15:30:00Z",
  "usuario": "admin",
  "rol": "Administrador"
}
```

### Refresh Token
**POST** `/api/CuentasUsuarios/refresh`

**Request Body:**
```json
{
  "refreshToken": "abc123..."
}
```

**Response:** Mismo formato que login.

## Seguridad Implementada

1. **Hashing de Contraseñas:** PBKDF2 con salt único por usuario
2. **Tokens JWT:** Expiran en 15 minutos
3. **Refresh Tokens:** Expiran en 7 días, revocables
4. **Bloqueo de Cuentas:** Después de 5 intentos fallidos, bloqueo por 30 minutos
5. **Validación de Tokens:** Middleware automático en cada request

## Uso en el Frontend

Para acceder a endpoints protegidos, incluye el token JWT en el header `Authorization`:

```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

Cuando el token expire, usa el refresh token para obtener uno nuevo sin requerir login nuevamente.

## Próximos Pasos

1. Agrega `[Authorize]` a los controladores/endpoints que requieran autenticación
2. Implementa roles y políticas si necesitas control de acceso granular
3. Considera implementar logout para revocar refresh tokens
4. Configura HTTPS en producción