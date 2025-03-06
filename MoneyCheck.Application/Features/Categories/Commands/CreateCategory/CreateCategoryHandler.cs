using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Domain.Enteties;

namespace MoneyCheck.Application.Features.Categories.Commands.CreateCategory
{
  public class CreateCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategory, CategoryDto>
  {
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<CategoryDto> Handle(CreateCategory request, CancellationToken cancellationToken)
    {
      var validator = new CategoryCommandValidator(_categoryRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      var category = new Category
      {
        Id = 0,
        BudgetId = request.BudgetId,
        CategoryName = request.CategoryName,
      };
      category = await _categoryRepository.AddAsync(category);

      return EntityMapper.CategoryToDto(category);
    }
  }
}