using Grpc.Core;
using GrpcGreeter.Protos;

namespace GrpcGreeter.Services
{
    public class PersonService : Protos.PersonService.PersonServiceBase
    {
        // داده‌های نمونه برای ذخیره اطلاعات
        private static List<Person> _personDb = new List<Person>();

        // پیاده‌سازی متد CreatePerson
        public override Task<PersonResponse> CreatePerson(Person request, ServerCallContext context)
        {
            _personDb.Add(request);
            return Task.FromResult(new PersonResponse
            {
                Message = "Person created successfully",
                Success = true
            });
        }

        // پیاده‌سازی متد GetPerson
        public override Task<Person> GetPerson(PersonRequest request, ServerCallContext context)
        {
            var person = _personDb.FirstOrDefault(p => p.NationalCode == request.NationalCode);
            if (person == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));

            return Task.FromResult(person);
        }

        // پیاده‌سازی متد UpdatePerson
        public override Task<PersonResponse> UpdatePerson(Person request, ServerCallContext context)
        {
            var person = _personDb.FirstOrDefault(p => p.NationalCode == request.NationalCode);
            if (person == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));

            // بروزرسانی اطلاعات
            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.BirthDate = request.BirthDate;

            return Task.FromResult(new PersonResponse
            {
                Message = "Person updated successfully",
                Success = true
            });
        }

        // پیاده‌سازی متد DeletePerson
        public override Task<PersonResponse> DeletePerson(PersonRequest request, ServerCallContext context)
        {
            var person = _personDb.FirstOrDefault(p => p.NationalCode == request.NationalCode);
            if (person == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Person not found"));

            _personDb.Remove(person);
            return Task.FromResult(new PersonResponse
            {
                Message = "Person deleted successfully",
                Success = true
            });
        }
    }
}
