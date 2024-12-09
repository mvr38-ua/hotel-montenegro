-- Insertar datos en la tabla Categoria
INSERT INTO Categoria (nombre) VALUES
(N'Economy'),
(N'Standard'),
(N'Deluxe');

-- Insertar datos en la tabla Temporada
INSERT INTO Temporada (nombre, fecha_ini, fecha_fin, tarifa) VALUES
(N'Temporada Alta', '2024-06-01', '2024-08-31', 1.5),
(N'Temporada Baja', '2024-09-01', '2024-11-30', 1.0);

-- Insertar datos en la tabla Habitacion
INSERT INTO Habitacion (numero, precio_base, capacidad, bloqueada, adaptada, categoria_id) VALUES
(101, 50.00, 2, 0, 1, 1),
(102, 75.00, 3, 0, 0, 2),
(103, 100.00, 4, 1, 1, 3);

-- Insertar datos en la tabla TipoUsuario
INSERT INTO TipoUsuario (tipo, descripcion) VALUES
(N'Cliente', N'Usuario regular que realiza reservas'),
(N'Administrador', N'Usuario con permisos para gestionar el sistema');

-- Insertar datos en la tabla DireccionUsuario
INSERT INTO DireccionUsuario (direccion, cod_postal, municipio, provincia, pais) VALUES
(N'Calle Falsa 123', N'28013', N'Madrid', N'Madrid', N'España'),
(N'Av. Siempre Viva 742', N'41001', N'Sevilla', N'Sevilla', N'España');

-- Insertar datos en la tabla ContraseñaUsuario
INSERT INTO ContraseñaUsuario (contraseña, ultima_modi) VALUES
(N'password123', '2024-01-01'),
(N'adminpass456', '2024-01-15');

-- Insertar datos en la tabla Usuario
INSERT INTO Usuario (email, nombre, apellidos, dni, movil, telefono, genero, fecha_nac, fecha_alta, activo, fecha_baja, tipo_usuario_id, direccion_id, contraseña_id) VALUES
(N'cliente1@ejemplo.com', N'Juan', N'Pérez', N'12345678A', 600123456, 911234567, N'Masculino', '1990-05-15', '2024-01-01', 1, NULL, 1, 1, 1),
(N'admin@ejemplo.com', N'Ana', N'García', N'87654321B', 600654321, 912345678, N'Femenino', '1985-03-10', '2024-01-01', 1, NULL, 2, 2, 2);

-- Insertar datos en la tabla Servicio
INSERT INTO Servicio (nombre, descripcion, precio_servicio) VALUES
(N'Desayuno', N'Desayuno continental', 10.00),
(N'WiFi Premium', N'Internet de alta velocidad', 5.00);

-- Insertar datos en la tabla Reserva
INSERT INTO Reserva (fecha_inicio, fecha_final, precio_total, usuario_id, habitacion_id) VALUES
('2024-06-15', '2024-06-20', 375.00, 1, 2),
('2024-07-01', '2024-07-10', 750.00, 1, 3);

-- Insertar datos en la tabla Reserva_Servicio
INSERT INTO Reserva_Servicio (reserva_id, servicio_id) VALUES
(1, 1),
(1, 2),
(2, 1);
