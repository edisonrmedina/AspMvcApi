USE dbSaleSystem;

CREATE TABLE rol (
    id_rol INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
    fecha_registro DATETIME DEFAULT GETDATE()
);

CREATE TABLE menu (
    id_menu INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
    icono VARCHAR(50),
    url VARCHAR(50)
);

CREATE TABLE menu_rol (
    id_menu_rol INT PRIMARY KEY IDENTITY(1,1),
    id_menu INT REFERENCES menu(id_menu),
    id_rol INT REFERENCES rol(id_rol),
    url VARCHAR(50)
);

CREATE TABLE usuario (
    id_usuario INT PRIMARY KEY IDENTITY(1,1),
    nombre_completo VARCHAR(100),
    correo VARCHAR(50),
    id_rol INT,
    clave VARCHAR(50),
    es_activo BIT,
    fecha_registro DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (id_rol) REFERENCES rol(id_rol)
);


CREATE TABLE categoria (
    id_categoria INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50),
	es_activo BIT,
    descripcion VARCHAR(255)
);

CREATE TABLE producto (
    id_producto INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100),
    descripcion VARCHAR(255),
    precio DECIMAL(10, 2),
    id_categoria INT REFERENCES categoria(id_categoria),
    stock INT,
    fecha_registro DATETIME DEFAULT GETDATE()
);

CREATE TABLE numero_documento (
    id_formato INT PRIMARY KEY IDENTITY(1,1),
    fecha_registro DATETIME DEFAULT GETDATE(),
    numero_documento INT
);

CREATE TABLE factura (
    id_factura INT PRIMARY KEY IDENTITY(1,1),
    id_usuario INT REFERENCES usuario(id_usuario),
    fecha_emision DATETIME DEFAULT GETDATE(),
    total DECIMAL(10, 2),
    numero_formato VARCHAR(50)
);

CREATE TABLE detalle_factura (
    id_detalle_factura INT PRIMARY KEY IDENTITY(1,1),
    id_factura INT REFERENCES factura(id_factura),
    id_producto INT REFERENCES producto(id_producto),
    cantidad INT,
    precio_unitario DECIMAL(10, 2),
    subtotal DECIMAL(10, 2)
);

-- Eliminar la columna id_usuario
ALTER TABLE factura
DROP COLUMN id_usuario;

ALTER TABLE factura
DROP CONSTRAINT FK_factura_usuario;
