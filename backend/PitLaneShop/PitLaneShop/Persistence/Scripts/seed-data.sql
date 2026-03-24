-- Seed data for PitLaneShop database
-- Run this script after migrations have been applied.

-- ============================================
-- Produtos
-- ============================================
DECLARE @ProdOleo1      UNIQUEIDENTIFIER = NEWID();
DECLARE @ProdPneu1      UNIQUEIDENTIFIER = NEWID();
DECLARE @ProdFreio1     UNIQUEIDENTIFIER = NEWID();
DECLARE @ProdFiltro1    UNIQUEIDENTIFIER = NEWID();
DECLARE @ProdLuz1       UNIQUEIDENTIFIER = NEWID();
DECLARE @ProdAcess1     UNIQUEIDENTIFIER = NEWID();

INSERT INTO Produtos (Id, Nome, Imagem, Descricao, Preco, QuantidadeEstoque, Categoria, DataCriacao)
VALUES
    (@ProdOleo1,   'Oleo Motor 5W30 Sintetico',     'https://http2.mlstatic.com/D_NQ_NP_2X_667823-MLA99394411168_112025-F.webp', 'Oleo sintetico para motores modernos, 1L',                89.90,  50, 0,  GETUTCDATE()),
    (@ProdPneu1,   'Pneu 205/55 R16',               'https://http2.mlstatic.com/D_NQ_NP_2X_974231-MLU77331934845_062024-F.webp', 'Pneu radial para veiculos de passeio',                   420.00, 30, 1,  GETUTCDATE()),
    (@ProdFreio1,  'Pastilha de Freio Dianteira',    'https://http2.mlstatic.com/D_NQ_NP_2X_804686-MLB101084546832_122025-F.webp', 'Jogo de pastilhas de freio ceramicas',                   185.00, 40, 2,  GETUTCDATE()),
    (@ProdFiltro1, 'Filtro de Ar Esportivo',         'https://http2.mlstatic.com/D_NQ_NP_2X_815414-MLB91197673410_092025-F-filtro-de-ar-esportivo-duplo-fluxo-universal-ronco-potencia.webp', 'Filtro de ar de alta vazao lavavel',                     250.00, 20, 5,  GETUTCDATE());
    --(@ProdLuz1,    'Kit Lampada LED H7',             'https://via.placeholder.com/200', 'Par de lampadas LED 6000K super brancas',                159.90, 35, 10, GETUTCDATE()),
    --(@ProdAcess1,  'Tapete Borracha Universal',      'https://via.placeholder.com/200', 'Jogo de tapetes de borracha com borda alta',              79.90, 60, 8,  GETUTCDATE());

-- ============================================
-- CodigosPromocionais
-- ============================================
DECLARE @Promo1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Promo2 UNIQUEIDENTIFIER = NEWID();

INSERT INTO CodigosPromocionais (Id, Codigo, Desconto, EhValido, DataCriacao)
VALUES
    (@Promo1, 'PITLANE10', 10.00, 1, GETUTCDATE()),
    (@Promo2, 'TURBO20',   20.00, 1, GETUTCDATE());

-- ============================================
-- Clientes
-- ============================================
DECLARE @Cliente1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Cliente2 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Clientes (Id, Nome, Email, Telefone, DataCriacao)
VALUES
    (@Cliente1, 'Carlos Silva',  'carlos.silva@email.com',  '(11) 99999-0001', GETUTCDATE()),
    (@Cliente2, 'Ana Oliveira',  'ana.oliveira@email.com',  '(21) 98888-0002', GETUTCDATE());
