USE tienda;

CREATE TABLE establecimientos (
  id INT IDENTITY(1,1) PRIMARY KEY,
  nombre VARCHAR(255),
  direccion VARCHAR(255),
  telefono VARCHAR(255),
  usuario_id NVARCHAR(450) REFERENCES AspNetUsers (Id)
);

CREATE TABLE productos (
  id INT IDENTITY(1,1) PRIMARY KEY,
  establecimineto_id INT REFERENCES establecimientos (Id),
  nombre VARCHAR(255),
  precio DECIMAL(10,2),
  stock INT
);

CREATE TABLE ventas (
  id INT IDENTITY(1,1) PRIMARY KEY,
  tienda_id INT REFERENCES establecimientos (id),
  producto_id INT REFERENCES productos (id),
  usuario_id NVARCHAR(450) REFERENCES AspNetUsers (Id),
  cantidad INT,
  fecha DATETIME
);

CREATE TABLE productos_ventas (
  producto_id INT REFERENCES productos (id),
  venta_id INT REFERENCES ventas (id)
);