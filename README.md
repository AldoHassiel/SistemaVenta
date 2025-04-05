# AlyDev - Contribución y Desarrollo

Este repositorio contiene el código fuente de **AlyDev**, un sistema de punto de venta desarrollado con **Windows Forms (.NET Framework)** y **SQL Server Express 2022**.

Este archivo de `README.md` está destinado para los desarrolladores que están trabajando en la rama `develop`, donde se encuentran las últimas características y cambios en desarrollo.

---
## 📌 Requisitos de desarrollo

- Visual Studio Community 2022
- .NET Framework 4.7.2
- SQL Server Express 2022
- Git

---

## 🚀 Pasos para contribuir

### 1. Clonar el proyecto

```bash
git clone https://github.com/AldoHassiel/SistemaVenta.git
cd SistemaVenta
```

### 3. Cambiar a la rama `develop`
La rama `develop` es donde se agregan todas las nuevas características y mejoras. Asegúrate de estar trabajando en esta rama para contribuir con tus cambios:
```bash
git checkout develop
```

## 4. Actualizar tu rama local develop
Antes de empezar a trabajar en tu nueva característica o corrección, asegúrate de tener la última versión de la rama `develop`. Puedes hacerlo con:
```bash
git pull origin develop
```

## 5. Crear una rama para tu función
Es una buena práctica crear una nueva rama para cada nueva característica o corrección que desarrolles. Esto mantiene el flujo de trabajo organizado y facilita la gestión de los cambios.
```bash
git checkout -b nombre-de-tu-rama-o-funcionalidad
```
Asegúrate de que tu nueva rama esté basada en `develop` para que esté actualizada con los últimos cambios.

## 6. Desarrolla tu código
Haz los cambios que necesites en el proyecto. Asegúrate de seguir las mejores prácticas de desarrollo, como:
- Mantener un estilo de código consistente con el proyecto.
- Comentar tu código cuando sea necesario, especialmente si las funcionalidades son complejas.

## 7. Realizar commits frecuentes
Es recomendable hacer commits frecuentes y con mensajes claros que describan lo que has hecho. Esto facilitará la colaboración con otros desarrolladores y la revisión del código.
```bash
git add .
git commit -m "Descripción clara de lo que hiciste"
```

## 8. Empujar tu rama a GitHub
Cuando hayas hecho tus cambios y estés listo para compartirlos, empuja tu rama al repositorio remoto:
```bash
git push origin nombre-de-tu-rama
```

## 9. Crear un Pull Request (PR)
Una vez que tu rama esté en GitHub, diregete a este repositorio y haz clic en Compare & pull request. Aquí puedes revisar los cambios que has hecho. Luego, crea un Pull Request hacia la rama `develop`.
- Asegúrate de que el título del PR sea claro y explique los cambios realizados.
- En el cuerpo del PR, incluye una descripción detallada de los cambios que has hecho, incluyendo cualquier detalle importante sobre la implementación.

## 10. Revisión del código
Un miembro del equipo o responsable del repositorio revisará tu Pull Request. Asegúrate de estar disponible para hacer cambios si es necesario. Si hay comentarios o sugerencias, puedes hacer nuevos commits en tu rama.

## 11. Fusionar el Pull Request
Una vez que se apruebe el Pull Request, se fusionará en la rama `develop`. Si eres el encargado de realizar el merge, puedes hacerlo a través de la interfaz de GitHub. Haz clic en **Merge pull request** y luego en **Confirm merge**.

---
## 📝 Notas Importantes
- Asegúrate de mantener tu rama develop siempre actualizada con los últimos cambios de la rama main para evitar conflictos de fusión.
- Si encuentras un bug o un problema en el código, abre un issue para que pueda ser resuelto.
- Si tu Pull Request es muy grande o cambia muchas funcionalidades, intenta dividirlo en múltiples PRs más pequeños para facilitar la revisión.
---
