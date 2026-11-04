CREATE DATABASE IF NOT EXISTS bsa_db;
USE bsa_db;

DROP TABLE IF EXISTS Produks;
DROP TABLE IF EXISTS Kategoris;

CREATE TABLE Kategoris (
  Id INT NOT NULL AUTO_INCREMENT,
  Nama VARCHAR(100) NOT NULL,
  WaktuUpdate DATETIME(6) NOT NULL,
  PRIMARY KEY (Id)
);

CREATE TABLE Produks (
  Id INT NOT NULL AUTO_INCREMENT,
  Nama VARCHAR(100) NOT NULL,
  IdKategori INT NOT NULL,
  WaktuUpdate DATETIME(6) NOT NULL,
  PRIMARY KEY (Id),
  CONSTRAINT FK_Produks_Kategoris FOREIGN KEY (IdKategori)
    REFERENCES Kategoris (Id) ON DELETE CASCADE
);

INSERT INTO Kategoris (Nama, WaktuUpdate) VALUES
('Elektronik', NOW()),
('Pakaian', NOW()),
('Makanan', NOW());

INSERT INTO Produks (Nama, IdKategori, WaktuUpdate) VALUES
('Laptop', 1, NOW()),
('Smartphone', 1, NOW()),
('Headphone', 1, NOW()),
('Kaos Polos', 2, NOW()),
('Celana Jeans', 2, NOW()),
('Keripik Kentang', 3, NOW()),
('Cokelat Batang', 3, NOW());