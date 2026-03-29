using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Features.Pedido.Dtos;
using PitLaneShop.Services.Features.Pedido.Interfaces;
using PedidoEntity = PitLaneShop.Model.Entities.Pedido;

namespace PitLaneShop.Services.Features.Pedido.Implementation
{
    public class PedidoItemBuilder : IPedidoItemBuilder
    {
        private readonly IProdutoRepository _produtoRepository;

        public PedidoItemBuilder(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task AdicionarItensNoPedidoAsync(CreatePedidoDto dto, PedidoEntity pedido, CancellationToken cancellationToken)
        {
            foreach (var itemDto in dto.Itens)
            {
                var produto = await _produtoRepository.GetByIdAsync(itemDto.ProdutoId, cancellationToken)
                    ?? throw new InvalidOperationException($"Produto '{itemDto.ProdutoId}' não encontrado.");

                if (produto.QuantidadeEstoque < itemDto.Quantidade)
                    throw new InvalidOperationException(
                        $"Estoque insuficiente para '{produto.Nome}'. Disponível: {produto.QuantidadeEstoque}, solicitado: {itemDto.Quantidade}.");

                produto.DecrementarEstoque(itemDto.Quantidade);

                pedido.AdicionarItem(new ItemPedido(produto.Preco, itemDto.Quantidade, produto.Nome, pedido.Id, produto.Id));
            }
        }
    }
}