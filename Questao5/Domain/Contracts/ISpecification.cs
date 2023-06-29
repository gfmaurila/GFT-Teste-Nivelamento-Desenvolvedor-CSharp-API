namespace Questao5.Domain.Contracts;
public interface ISpecification<T>
{
    bool IsSatisfied(T item);
    string ErrorMessage { get; }
}
