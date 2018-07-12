namespace SimpleRegistration.Core.Contracts.Base
{
    public interface IRequest { }
    public interface IRequest<out TResponse> { }
}
