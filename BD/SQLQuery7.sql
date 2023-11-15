USE tienda;


CREATE TABLE establecimientos (
  id INT PRIMARY KEY,
  nombre VARCHAR(255),
  direccion VARCHAR(255),
  telefono VARCHAR(255),
  usuario_id NVARCHAR(450) REFERENCES AspNetUsers (Id)
);

CREATE TABLE productos (
  id INT PRIMARY KEY,
  nombre VARCHAR(255),
  precio DECIMAL(10,2),
  stock INT
);

CREATE TABLE ventas (
  id INT PRIMARY KEY,
  tienda_id INT REFERENCES establecimientos (id),
  producto_id INT REFERENCES productos (id),
  usuario_id NVARCHAR(450) REFERENCES AspNetUsers (Id),
  cantidad INT,
  fecha DATETIME
);