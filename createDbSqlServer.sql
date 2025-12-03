CREATE DATABASE PokemonDB;

SELECT name, database_id, create database_id
FROM sys.databases 
WHERE name = 'PokemonDB';

USE PokemonDB;

-- Tabla Habilidad
CREATE TABLE Habilidad (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500),
    Beneficiosa BIT NOT NULL,
    Oculta BIT NOT NULL,
    Unica BIT NOT NULL
);

-- Tabla Pokeball
CREATE TABLE Pokeball (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Ratio FLOAT NOT NULL,
    Precio FLOAT NOT NULL,
    Color NVARCHAR(50),
    Efecto NVARCHAR(500)
);

-- Tabla Habitat
CREATE TABLE Habitat (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Region NVARCHAR(100),
    Clima NVARCHAR(100),
    Temperatura INT,
    Descripcion NVARCHAR(500)
);

-- Tabla Objeto
CREATE TABLE Objeto (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500),
    Precio FLOAT NOT NULL,
    Unico BIT NOT NULL,
    Efecto NVARCHAR(500)
);

-- Tabla Pokemon (con relaciones a las demás tablas)
CREATE TABLE Pokemon (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Region NVARCHAR(100),
    Nombre NVARCHAR(100) NOT NULL,
    Peso FLOAT NOT NULL,
    Shiny BIT NOT NULL,
    Tipo NVARCHAR(50),
    Habilidad INT,
    Pokeball INT,
    Habitat INT,
    Objeto INT,
    FOREIGN KEY (Habilidad) REFERENCES Habilidad(Id),
    FOREIGN KEY (Pokeball) REFERENCES Pokeball(Id),
    FOREIGN KEY (Habitat) REFERENCES Habitat(Id),
    FOREIGN KEY (Objeto) REFERENCES Objeto(Id)
);

-- Datos de ejemplo para Habilidad
INSERT INTO Habilidad (Nombre, Descripcion, Beneficiosa, Oculta, Unica) VALUES
('Intimidación', 'Reduce el ataque del oponente al entrar en combate', 1, 0, 0),
('Absorbe Agua', 'Recupera PS cuando es alcanzado por movimientos de tipo Agua', 1, 0, 0),
('Clorofila', 'Duplica la velocidad bajo el sol intenso', 1, 1, 0);

-- Datos de ejemplo para Pokeball
INSERT INTO Pokeball (Nombre, Ratio, Precio, Color, Efecto) VALUES
('Poké Ball', 1.0, 200, 'Rojo y Blanco', 'Pokéball estándar'),
('Super Ball', 1.5, 600, 'Azul y Blanco', 'Mayor ratio de captura que la Poké Ball'),
('Ultra Ball', 2.0, 1200, 'Negro y Amarillo', 'Muy efectiva para capturar Pokémon');

-- Datos de ejemplo para Habitat
INSERT INTO Habitat (Nombre, Region, Clima, Temperatura, Descripcion) VALUES
('Bosque Viridian', 'Kanto', 'Templado', 20, 'Bosque denso con gran biodiversidad'),
('Monte Moon', 'Kanto', 'Frío', 10, 'Montaña con muchas cuevas y fósiles'),
('Playa Celeste', 'Johto', 'Cálido', 28, 'Costa con aguas cristalinas');

-- Datos de ejemplo para Objeto
INSERT INTO Objeto (Nombre, Descripcion, Precio, Unico, Efecto) VALUES
('Poción', 'Objeto que restaura unos pocos PS', 300, 0, 'Cura 20 PS'),
('Baya Zreza', 'Cura parálisis', 0, 0, 'Elimina estado de parálisis'),
('Piedra Fuego', 'Evoluciona ciertos Pokémon', 2100, 0, 'Piedra evolutiva de tipo fuego');

-- Datos de ejemplo para Pokemon
INSERT INTO Pokemon (Region, Nombre, Peso, Shiny, Tipo, Habilidad, Pokeball, Habitat, Objeto) VALUES
('Kanto', 'Pikachu', 6.0, 0, 'Eléctrico', 1, 1, 1, 1),
('Kanto', 'Charizard', 90.5, 1, 'Fuego/Volador', 2, 3, 2, 3),
('Johto', 'Totodile', 9.5, 0, 'Agua', 3, 2, 3, 2);


-- DROPTABLES


-- Primero eliminamos la tabla dependiente
DROP TABLE IF EXISTS Pokemon;

-- Luego eliminamos las tablas referenciadas
DROP TABLE IF EXISTS Habilidad;
DROP TABLE IF EXISTS Pokeball;
DROP TABLE IF EXISTS Habitat;
DROP TABLE IF EXISTS Objeto;