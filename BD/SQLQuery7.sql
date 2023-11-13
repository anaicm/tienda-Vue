USE tienda;

CREATE TABLE usuarios (
  id INT PRIMARY KEY,
  nombre VARCHAR(255),
  apellido VARCHAR(255),
  email VARCHAR(255),
  contraseña VARCHAR(255)
);

CREATE TABLE tiendas (
  id INT PRIMARY KEY,
  nombre VARCHAR(255),
  dirección VARCHAR(255),
  teléfono VARCHAR(255),
  usuario_id INT REFERENCES usuarios (id)
);

CREATE TABLE productos (
  id INT PRIMARY KEY,
  nombre VARCHAR(255),
  precio DECIMAL(10,2),
  stock INT
);

CREATE TABLE ventas (
  id INT PRIMARY KEY,
  tienda_id INT REFERENCES tiendas (id),
  producto_id INT REFERENCES productos (id),
  cliente_id INT,
  cantidad INT,
  fecha DATETIME
);
CREATE TABLE clientes (
  id INT PRIMARY KEY,
  nombre VARCHAR(255),
  apellido VARCHAR(255),
  email VARCHAR(255),
  contraseña VARCHAR(255),
  fecha_nacimiento DATE
);
