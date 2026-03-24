-- Seed data for PitLaneShop database
-- Run this script after migrations have been applied.

-- ============================================
-- VeiculosModelo
-- ============================================
DECLARE @ModeloHB20s  UNIQUEIDENTIFIER = NEWID();
DECLARE @ModeloS10    UNIQUEIDENTIFIER = NEWID();
DECLARE @ModeloOnix  UNIQUEIDENTIFIER = NEWID();

INSERT INTO VeiculosModelo (Id, Marca, Modelo, Categoria, DataCriacao)
VALUES
    (@ModeloHB20s, 'Hyundai',  'HB20s', 1, GETUTCDATE()),  
    (@ModeloS10,   'Chevrolet',   'S10',   5, GETUTCDATE()),  
    (@ModeloOnix, 'Chevrolet','Onix', 1, GETUTCDATE()); 

-- ============================================
-- Carros
-- ============================================
INSERT INTO Carros (Id, Placa, Quilometragem, Status, Imagem, VeiculoModeloId, DataCriacao)
VALUES
    (NEWID(), 'ABC-1234', '1500',  0, 'https://www.localiza.com/brasil-site/geral/Frota/HB2C.png', @ModeloHB20s, GETUTCDATE()),
    (NEWID(), 'DEF-5678', '300',   0, 'https://www.localiza.com/brasil-site/geral/Frota/S10X.png', @ModeloS10,   GETUTCDATE()),
    (NEWID(), 'GHI-9012', '8500',  0, 'https://www.localiza.com/brasil-site/geral/Frota/ONIT.png', @ModeloOnix,  GETUTCDATE()),
    (NEWID(), 'JKL-3456', '12000', 0, 'https://www.localiza.com/brasil-site/geral/Frota/HB2C.png', @ModeloHB20s, GETUTCDATE()),
    (NEWID(), 'MNO-7890', '4200',  0, 'https://www.localiza.com/brasil-site/geral/Frota/S10X.png', @ModeloS10,   GETUTCDATE()),
    (NEWID(), 'PQR-1122', '950',   0, 'https://www.localiza.com/brasil-site/geral/Frota/ONIT.png', @ModeloOnix,  GETUTCDATE()),
    (NEWID(), 'STU-3344', '27000', 1, 'https://www.localiza.com/brasil-site/geral/Frota/HB2C.png', @ModeloHB20s, GETUTCDATE()),
    (NEWID(), 'VWX-5566', '18500', 1, 'https://www.localiza.com/brasil-site/geral/Frota/S10X.png', @ModeloS10,   GETUTCDATE()),
    (NEWID(), 'YZA-7788', '6100',  0, 'https://www.localiza.com/brasil-site/geral/Frota/ONIT.png', @ModeloOnix,  GETUTCDATE());  

-- ============================================
-- TarifasDiarias
-- ============================================
INSERT INTO TarifasDiarias (Id, ValorDiaria, ValorMulta, EhValorDiariaVigente, DataInicioVigencia, DataFimVigencia, VeiculoModeloId, DataCriacao)
VALUES
    (NEWID(), 150.00, 75.00,  1, '2026-01-01', NULL, @ModeloHB20s, GETUTCDATE()),
    (NEWID(), 170.00, 85.00,  1, '2026-01-01', NULL, @ModeloS10,   GETUTCDATE()),
    (NEWID(), 220.00, 110.00, 1, '2026-01-01', NULL, @ModeloOnix, GETUTCDATE());
