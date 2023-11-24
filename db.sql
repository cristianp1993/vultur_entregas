-- Creacion de la tabla Usuario
CREATE TABLE Usuario (
    UsuarioID INT PRIMARY KEY IDENTITY,
    NombreUsuario NVARCHAR(50),
    Contrasena NVARCHAR(50),
    Email NVARCHAR(100),
    Rol NVARCHAR(50)
);

-- Creacion de la tabla Cliente
CREATE TABLE Cliente (
    ClienteID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Direccion NVARCHAR(255),
    Telefono NVARCHAR(20),
    Email NVARCHAR(100)
);

-- Creacion de la tabla Emisor
CREATE TABLE Emisor (
    EmisorID INT PRIMARY KEY IDENTITY,
    ClienteID INT,
    FOREIGN KEY (ClienteID) REFERENCES Cliente(ClienteID)
);

-- Creacion de la tabla Receptor
CREATE TABLE Receptor (
    ReceptorID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Direccion NVARCHAR(255),
    Telefono NVARCHAR(20)
);

-- Creacion de la tabla Envio
CREATE TABLE Envio (
    EnvioID INT PRIMARY KEY IDENTITY,
    EmisorID INT,
    ReceptorID INT,
    DireccionOrigen NVARCHAR(255),
    DireccionDestino NVARCHAR(255),
    TelefonoContacto NVARCHAR(20),
    DescripcionPaquete NVARCHAR(255),
    PesoPaquete DECIMAL(10, 2),
    ValorEnvio DECIMAL(10, 2),
    FechaEnvio DATETIME,
    FOREIGN KEY (EmisorID) REFERENCES Emisor(EmisorID),
    FOREIGN KEY (ReceptorID) REFERENCES Receptor(ReceptorID)
);

-- Creacion de la tabla Trazabilidad
CREATE TABLE Trazabilidad (
    TrazabilidadID INT PRIMARY KEY IDENTITY,
    EnvioID INT,
    FechaHora DATETIME,
    Ubicacion NVARCHAR(255),
    Estado NVARCHAR(50),
    DetallesAdicionales NVARCHAR(255),
    FOREIGN KEY (EnvioID) REFERENCES Envio(EnvioID)
);

-- Creacion de la tabla Ciudad (si es necesaria)
CREATE TABLE Ciudad (
    CiudadID INT PRIMARY KEY IDENTITY,
    NombreCiudad NVARCHAR(100)
);

-- Creacion de la tabla Conexion (si es necesaria)
CREATE TABLE Conexion (
    ConexionID INT PRIMARY KEY IDENTITY,
    EnvioID INT,
    CiudadOrigenID INT,
    CiudadDestinoID INT,
    FechaHoraSalida DATETIME,
    FechaHoraLlegada DATETIME,
    FOREIGN KEY (EnvioID) REFERENCES Envio(EnvioID),
    FOREIGN KEY (CiudadOrigenID) REFERENCES Ciudad(CiudadID),
    FOREIGN KEY (CiudadDestinoID) REFERENCES Ciudad(CiudadID)
);


--Ciudades principales
INSERT INTO Ciudad (NombreCiudad) VALUES ('Bogotá');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Medellín');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Cali');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Barranquilla');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Cartagena');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Cúcuta');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Bucaramanga');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Pereira');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Santa Marta');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Ibagué');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Manizales');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Villavicencio');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Neiva');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Armenia');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Pasto');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Montería');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Valledupar');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Popayán');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Sincelejo');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Floridablanca');
INSERT INTO Ciudad (NombreCiudad) VALUES ('Palmira');

--creacion del primer usuario administrador
INSERT INTO Usuario (NombreUsuario, Contrasena, Email, Rol) 
VALUES ('vultur', '123456', 'cristian.piedrahita88@gmail.com', 'administrador');