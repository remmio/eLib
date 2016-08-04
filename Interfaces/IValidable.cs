//using FluentValidation.Results;

namespace eLib.Interfaces
{
    public  interface IValidable
    {
        //Task<List<ValidationFailure>> Validate();
        void ValidateAndThrow();
    }
}
