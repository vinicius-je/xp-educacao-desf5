using PitLaneShop.Model.Entities;
using PitLaneShop.Model.Repositories;
using PitLaneShop.Services.Features.RealizarAluguel.Dtos;
using PitLaneShop.Services.Features.RealizarAluguel.Interfaces;

namespace PitLaneShop.Services.Features.RealizarAluguel.Implementation;

public class RealizarAluguelService : IRealizarAluguelService
{
    private readonly ICarroRepository _carroRepository;
    private readonly ITarifaDiariaRepository _tarifaDiariaRepository;
    private readonly IAluguelRepository _aluguelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RealizarAluguelService(
        ICarroRepository carroRepository,
        ITarifaDiariaRepository tarifaDiariaRepository,
        IAluguelRepository aluguelRepository,
        IUnitOfWork unitOfWork)
    {
        _carroRepository = carroRepository;
        _tarifaDiariaRepository = tarifaDiariaRepository;
        _aluguelRepository = aluguelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RealizarAluguelResponseDto> ExecutarAsync(
        RealizarAluguelRequestDto request,
        CancellationToken cancellationToken = default)
    {
        // RN01 — Verificar disponibilidade
        var carroDisponivel = await _carroRepository
            .GetFirstDisponivelByModeloAsync(request.VeiculoModeloId, cancellationToken)
            ?? throw new InvalidOperationException(
                "Não há veículos disponíveis para o modelo selecionado.");

        // RN02 — Calcular valor total
        var tarifaVigente = await _tarifaDiariaRepository
            .GetVigenteByModeloAsync(request.VeiculoModeloId, cancellationToken)
            ?? throw new InvalidOperationException(
                "Não há tarifa diária vigente para o modelo selecionado.");

        var diarias = request.DataDevolucao.DayNumber - request.DataRetirada.DayNumber;
        if (diarias <= 0)
            throw new InvalidOperationException(
                "A data de devolução deve ser posterior à data de retirada.");

        // RN03 — Registrar aluguel
        var aluguel = new Aluguel(request.DataRetirada, request.DataDevolucao, diarias, carroDisponivel.Id, request.ClienteId, tarifaVigente.ValorDiaria);
        await _aluguelRepository.AddAsync(aluguel, cancellationToken);

        // RN04 — Atualizar status do veículo
        carroDisponivel.AlugarCarro();
        await _carroRepository.UpdateAsync(carroDisponivel, cancellationToken);

        // Persiste RN03 + RN04 atomicamente
        await _unitOfWork.SaveAsync(cancellationToken);

        return new RealizarAluguelResponseDto
        {
            Id = aluguel.Id,
            DataRetirada = aluguel.DataRetirada,
            DataDevolucaoPrevista = aluguel.DataDevolucaoPrevista,
            Diarias = aluguel.Diarias,
            ValorTotal = aluguel.ValorTotal,
            CarroId = aluguel.CarroId,
            CarroPlaca = carroDisponivel.Placa,
            ClienteId = aluguel.ClienteId,
        };
    }
}
