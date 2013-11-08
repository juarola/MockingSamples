using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocking
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new Model();
            model.Name = "heps";

            var service = new Service();

            service.MethodToTest(model);

        }
    }

    public class Model
    {
        public string Name { get; set; }
    }

    public class DTO
    {
        public string Name { get; set; }
    }

    public interface IService
    {
        void MethodToTest(Model model);
    }

    public class Service : IService
    {
        private IRepository _repository;

        public Service()
        {
            this._repository = new Repository();
        }

        public Service(IRepository repository)
        {
            this._repository = repository;
        }

        public void MethodToTest(Model model)
        {
            var dto = new DTO();
            dto.Name = model.Name;

            _repository.Save(dto);
        }
    }

    public interface IRepository
    {
        void Save(DTO dto);
    }

    public class Repository : IRepository
    {
        public void Save(DTO dto)
        {
            // do stuff
        }
    }
}
