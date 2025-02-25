using MediatR;
using SharedKernel;

namespace Application.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
