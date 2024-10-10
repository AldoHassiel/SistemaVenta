# Sistema de punto de venta

## Requisitos

1. **Instalar Git:**
   - Descarga e instala Git desde [git-scm.com](https://git-scm.com/downloads/win) dandole siguiente a todo.
  
2. **Configurar tu identidad:**
   - Abre la terminal "Git Bash" y configura tu nombre de usuario y correo electrónico (con el que te registraste en GitHub):
     ```bash
     git config --global user.name "Tu Nombre"
     git config --global user.email "tu-email@example.com"
     ```

3. **Clonar el repositorio:**
   - Copia la URL del repositorio desde el boton verde "Code".
   - Desde Git Bash dirigete al directorio donde quisieras tener el proyecto:
   ```bash
   cd Documents/"
   ```
   - Clona el repositorio usando git clone:
   ```bash
   git clone URL
   git clone https://github.com/AldoHassiel/SistemaVenta.git
   ```

4. Tener ganas de chambear.

## Pasos a realizar antes de empezar cada video

**IMPORTANTE:** El proyecto no tiene que estar abiero en Visual Studio.

1. **Traerse los utlimos cambios del proyecto:**
   ```bash
   git pull origin main
   ```
   
2. **Mover la carpeta "BackupSQL" a "C:\"**
   ```bash
   mv BackupSQL/ /c/
   ```

3. **Restaurar la base de datos dentro de SQL.**

## Pasos a realizar despues de un video

**IMPORTANTE:** El proyecto no tiene que estar abiero en Visual Studio.

1. **Cerrar completamente Visual Studio.**

2. **Crear una copia de seguridad de la base de datos en C:\BackupSQL**

3. **Mover la carpeta C:\BackupSQL a la ruta del proyecto.**
   
4. **Añadir los archivos modificados al area de preparación:**
   ```bash
   git add .
   ```

5. **Hacer un commit de todos los cambios:**
   ```bash
   git commit -m "Video N"
   ```

6. **Subir los cambios a GitHub:**
   ```bash
   git push -u origin main
   ```
