using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.Categories.Commands.UpdateCategory
{
  public class UpdateCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<UpdateCategory, CategoryDto>
  {
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<CategoryDto> Handle(UpdateCategory request, CancellationToken cancellationToken)
    {
      var categoryToUpdate = await _categoryRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundException(LocaleErrorParam.Category, "Category", request.Id);

      var validator = new CategoryCommandValidator(_categoryRepository);
      var validationResult = await validator.ValidateAsync(request, cancellationToken);

      if (validationResult.Errors.Count > 0)
        throw new ValidationException(validationResult);

      categoryToUpdate.CategoryName = request.CategoryName;

      await _categoryRepository.UpdateAsync(categoryToUpdate);

      return EntityMapper.CategoryToDto(categoryToUpdate);
    }
  }
}