
namespace Shared.Application.Validation;

public interface IValidator<T>
{
    Task ValidateAsync(T entity);
}