using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Entity;
using LuizMario.Domain.Core.Repository;
using LuizMario.Dto.Filter;
using LuizMario.Dto.Pagination;

namespace LuizMario.Domain.Core.Service
{
    public class FinancialMovimentService : BaseService
    {
        private readonly FinancialMovimentRepository _repository;
        private readonly CategoryService _categoryService;
        private readonly PersonService _personService;

        public FinancialMovimentService(
            FinancialMovimentRepository repository, 
            CategoryService categoryService, 
            PersonService personService, 
            INotification notification
        ) : base(notification)
        {
            this._repository = repository;
            this._categoryService = categoryService;
            this._personService = personService;
        }

        public ResponsePaginationDto<FinancialMoviment> Search(FinancialMovimentFilterDto filter)
        {
            return new ResponsePaginationDto<FinancialMoviment>(_repository.CalculeteTotalElements(filter), _repository.Find(filter));
        }

        public FinancialMoviment FindById(int id)
        {
            return _repository.FindById(id);
        }

        public FinancialMoviment Save(FinancialMoviment financialMoviment)
        {
            if (IsValidData(financialMoviment)) 
            { 
                _repository.Save(financialMoviment);            
            }

            return financialMoviment;
        }     

        public void Update(FinancialMoviment financialMoviment)
        {
            if (IsValidData(financialMoviment))
            {
                _repository.Update(financialMoviment);
            }
        }
        public void Delete(FinancialMoviment finacialMoviment)
        {
            _repository.Delete(finacialMoviment);
        }

        private bool IsValidData(FinancialMoviment financialMoviment)
        {
            if (financialMoviment.IsInvalidType())
            {
                AddError("Tipo de operação inválido.");
            }

            if (financialMoviment.Value == 0)
            {
                AddError("Valor deve ser maior que zero.");
            }

            var category = _categoryService.FindById(financialMoviment.CategoryId);
            financialMoviment.Category = category;

            if (category == null)
            {
                AddError("Categoria não foi encontrada.");
            }

            var person = _personService.FindById(financialMoviment.PersonId);
            financialMoviment.Person = person;

            if (person == null)
            {
                AddError("Pessoa não foi encontrada.");
            }
            else if (person.IsInactive())
            {
                AddError("Pessoa inativa.");
            }

            return IsValid();
        }
    }
}
