-- Seed data for PitLaneShop database
-- Run this script after migrations have been applied.

-- ============================================
-- Produtos
-- ============================================
DECLARE
-- OLEO_LUBRIFICANTE (0)
@ProdOleo1  UNIQUEIDENTIFIER = NEWID(),
@ProdOleo2  UNIQUEIDENTIFIER = NEWID(),
-- PNEU (1)
@ProdPneu1  UNIQUEIDENTIFIER = NEWID(),
@ProdPneu2  UNIQUEIDENTIFIER = NEWID(),
-- RODA (2)
@ProdRoda1  UNIQUEIDENTIFIER = NEWID(),
@ProdRoda2  UNIQUEIDENTIFIER = NEWID(),
-- FREIO (3)
@ProdFreio1 UNIQUEIDENTIFIER = NEWID(),
@ProdFreio2 UNIQUEIDENTIFIER = NEWID(),
-- SUSPENSAO (4)
@ProdSusp1  UNIQUEIDENTIFIER = NEWID(),
@ProdSusp2  UNIQUEIDENTIFIER = NEWID(),
-- BATERIA (5)
@ProdBat1   UNIQUEIDENTIFIER = NEWID(),
@ProdBat2   UNIQUEIDENTIFIER = NEWID(),
-- FILTROS (6)
@ProdFiltro1 UNIQUEIDENTIFIER = NEWID(),
@ProdFiltro2 UNIQUEIDENTIFIER = NEWID(),
-- ESCAPAMENTO (7)
@ProdEsc1   UNIQUEIDENTIFIER = NEWID(),
@ProdEsc2   UNIQUEIDENTIFIER = NEWID(),
-- PERFORMANCE_TUNING (8)
@ProdPerf1  UNIQUEIDENTIFIER = NEWID(),
@ProdPerf2  UNIQUEIDENTIFIER = NEWID();

INSERT INTO Produtos (Id, Nome, Imagem, Descricao, Preco, QuantidadeEstoque, Categoria, DataCriacao)
VALUES
    -- OLEO_LUBRIFICANTE = 0
    (@ProdOleo1,   'Oleo Motor 5W30 Sintetico',       'https://http2.mlstatic.com/D_NQ_NP_2X_667823-MLA99394411168_112025-F.webp',                                                                               'Oleo sintetico para motores modernos, 1L',                  89.90,  50, 0, GETUTCDATE()),
    (@ProdOleo2,   'Oleo Motor 10W40 Semissintetico',  'https://http2.mlstatic.com/D_NQ_NP_2X_785492-MLA99928541155_112025-F.webp',                                                                              'Oleo semissintetico multigrau, 4L',                        159.90,  35, 0, GETUTCDATE()),

    -- PNEU = 1
    (@ProdPneu1,   'Pneu 205/55 R16',                  'https://http2.mlstatic.com/D_NQ_NP_2X_974231-MLU77331934845_062024-F.webp',                                                                              'Pneu radial para veiculos de passeio',                     420.00,  30, 1, GETUTCDATE()),
    (@ProdPneu2,   'Pneu 225/45 R17 Run Flat',         'https://http2.mlstatic.com/D_NQ_NP_2X_841197-MLA99636742984_122025-F.webp',                                                                              'Pneu run flat para veiculos de alto desempenho',           680.00,  15, 1, GETUTCDATE()),

    -- RODA = 2
    (@ProdRoda1,   'Pastilha de Freio Dianteira',       'https://http2.mlstatic.com/D_NQ_NP_2X_804686-MLB101084546832_122025-F.webp',                                                                             'Jogo de pastilhas de freio ceramicas',                     185.00,  40, 2, GETUTCDATE()),
    (@ProdRoda2,   'Roda Liga Leve Aro 17 5x100',       'https://http2.mlstatic.com/D_NQ_NP_2X_995737-MLA99866306719_112025-F.webp',                                                                              'Roda de liga leve estilo esportivo, unitaria',             750.00,  12, 2, GETUTCDATE()),

    -- FREIO = 3
    (@ProdFreio1,  'Disco de Freio Ventilado Dianteiro','https://http2.mlstatic.com/D_NQ_NP_2X_613161-MLB107402248018_032026-F.webp',                                                                              'Par de discos ventilados com tratamento anticorrosao',     380.00,  20, 3, GETUTCDATE()),
    (@ProdFreio2,  'Fluido de Freio DOT 4',             'https://http2.mlstatic.com/D_NQ_NP_2X_949165-MLA104258316295_012026-F.webp',                                                                              'Fluido de freio de alta performance, 500ml',                32.90,  60, 3, GETUTCDATE()),

    -- SUSPENSAO = 4
    (@ProdSusp1,   'Amortecedor Dianteiro Esportivo',   'https://http2.mlstatic.com/D_NQ_NP_2X_909294-MLB75397571798_042024-F.webp',                                                                              'Amortecedor a gas de alta performance, unitario',          420.00,  18, 4, GETUTCDATE()),
    (@ProdSusp2,   'Kit Molas Esportivas Rebaixadas',   'https://http2.mlstatic.com/D_NQ_NP_2X_890009-MLA80171983621_102024-F.webp',                                                                              'Jogo de molas com rebaixamento de 40mm',                   890.00,   8, 4, GETUTCDATE()),

    -- BATERIA = 5
    (@ProdBat1,    'Bateria Automotiva 60Ah',           'https://http2.mlstatic.com/D_NQ_NP_2X_712631-MLB100193943603_122025-F.webp',                                                                              'Bateria selada livre de manutencao, 60Ah',                 459.90,  22, 5, GETUTCDATE()),
 
    -- FILTROS = 6
    (@ProdFiltro1, 'Filtro de Ar Esportivo',            'https://http2.mlstatic.com/D_NQ_NP_2X_815414-MLB91197673410_092025-F-filtro-de-ar-esportivo-duplo-fluxo-universal-ronco-potencia.webp',                  'Filtro de ar de alta vazao lavavel',                       250.00,  20, 6, GETUTCDATE()),
    (@ProdFiltro2, 'Filtro de Combustivel Universal',   'https://http2.mlstatic.com/D_NQ_NP_2X_609671-MLB89549267722_082025-F-filtro-combustivel-acrilico-anodizado-cnc-moto-universal.webp',                                                                              'Filtro inline de combustivel para carros injetados',        38.90,  45, 6, GETUTCDATE()),

    -- ESCAPAMENTO = 7
    (@ProdEsc1,    'Ponteira de Escapamento Esportiva', 'https://http2.mlstatic.com/D_NQ_NP_2X_960379-MLB42334652096_062020-F.webp',                                                                              'Ponteira inox dupla saida, universal 51mm',                210.00,  25, 7, GETUTCDATE()),
    (@ProdEsc2,    'Silencioso Esportivo Inox',         'https://http2.mlstatic.com/D_NQ_NP_2X_896421-MLB107649605027_022026-F.webp',                                                                              'Silencioso de troca direta em aco inoxidavel',             890.00,   7, 7, GETUTCDATE()),

    -- PERFORMANCE_TUNING = 8
    (@ProdPerf1,   'Modulo de Potencia Plug and Play',  'https://http2.mlstatic.com/D_NQ_NP_2X_755583-MLB107450236105_022026-F-modulo-chip-potncia-eco-gas-pedal-shiftpower-plug-and-play.webp',                                                                              'Modulo para ganho de potencia sem remapear a ECU',         650.00,  14, 8, GETUTCDATE()),
    (@ProdPerf2,   'Kit Turbo Universal 0.5 Bar',       'https://http2.mlstatic.com/D_NQ_NP_2X_723688-MLB85113205222_052025-F-kit-turbo-ap-mono-carburado-zr-apl240-42-48-1kg.webp',                                                                              'Kit turbo compacto para motores aspirados 1.0 a 1.6',    2490.00,   5, 8, GETUTCDATE());
-- ============================================
-- CodigosPromocionais
-- ============================================
DECLARE @Promo1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Promo2 UNIQUEIDENTIFIER = NEWID();

INSERT INTO CodigosPromocionais (Id, Codigo, Desconto, EhValido, DataCriacao)
VALUES
    (@Promo1, 'PITLANE10', 10.00, 1, GETUTCDATE()),
    (@Promo2, 'TURBO20',   20.00, 1, GETUTCDATE());
