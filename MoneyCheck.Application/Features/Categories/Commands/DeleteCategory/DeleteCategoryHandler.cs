using MediatR;
using MoneyCheck.Application.Contracts.Persistance;
using MoneyCheck.Application.Exceptions;
using MoneyCheck.Application.Mapping;
using MoneyCheck.Application.Models.Localization;

namespace MoneyCheck.Application.Features.Categories.Commands.DeleteCategory
{
  public class DeleteCategoryHandler(ICategoryRepository categoryRepository) : IRequestHandler<DeleteCategory, CategoryDto>
  {
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<CategoryDto> Handle(DeleteCategory request, CancellationToken cancellationToken)
    {
      var categoryToDelete = await _categoryRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundException(LocaleErrorParam.Category, "Category", request.Id);

      await _categoryRepository.DeleteAsync(categoryToDelete);

      return EntityMapper.CategoryToDto(categoryToDelete);
    }
  }
}