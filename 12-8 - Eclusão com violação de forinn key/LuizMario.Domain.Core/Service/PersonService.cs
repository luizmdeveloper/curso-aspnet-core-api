using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Services;
using LuizMario.Domain.Core.Entity;
using LuizMario.Domain.Core.Repository;
using LuizMario.Dto.Filter;
using LuizMario.Dto.Pagination;
using System;

namespace LuizMario.Domain.Core.Service
{
    public class PersonService : BaseService
    {
        private readonly PersonRepository _repository;

        public PersonService(PersonRepository repository, INotification notification) : base(notification)
        {
            this._repository = repository;
        }

        public ResponsePaginationDto<Person> Search(PersonFilterDto filter)
        {
            return new ResponsePaginationDto<Person>(_repository.CalculeteTotalElements(filter), _repository.Find(filter));
        }

        public Person FindById(int id)
        {
            return _repository.FindById(id);
        }

        public void Save(Person person)
        {
            person.Created = DateTime.Now;
            _repository.Save(person);
        }

        public void Update(Person person)
        {
            _repository.Update(person);
        }

        public void Delete(Person person)
        {
            try
            {
                _repository.Delete(person);
            }
            catch (Exception ex) when(ex.Message.Contains("fk_person"))
            {
                AddError("Pessoa não pode ser excluída devido ter movimentação bancária.");
            }
        }

        public void Actived(Person person)
        {
            person.Actived();
            Update(person);
        }

        public void Inactive(Person person) 
        {
            person.Inactive();
            Update(person);
        }
    }
}
