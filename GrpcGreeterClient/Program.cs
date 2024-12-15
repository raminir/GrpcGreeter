using Grpc.Net.Client;
using GrpcGreeterClient;


// ایجاد کانال برای اتصال به سرور gRPC
using var channel = GrpcChannel.ForAddress("https://localhost:7255");

// ایجاد کلاینت برای سرویس PersonService
var client = new PersonService.PersonServiceClient(channel);

// 1. ایجاد یک شخص جدید
var newPerson = new Person
{
    FirstName = "Ali",
    LastName = "Mohammadi",
    NationalCode = "1234567890",
    BirthDate = "1990-01-01"
};

var createResponse = await client.CreatePersonAsync(newPerson);
Console.WriteLine(createResponse.Message);  // نمایش پیام ایجاد شخص

// 2. دریافت اطلاعات یک شخص
var getResponse = await client.GetPersonAsync(new PersonRequest { NationalCode = "1234567890" });
Console.WriteLine($"Person found: {getResponse.FirstName} {getResponse.LastName}");

// 3. آپدیت اطلاعات یک شخص
var updatedPerson = new Person
{
    FirstName = "Ali",
    LastName = "Mohammadi Updated",  // تغییر در نام خانوادگی
    NationalCode = "1234567890",
    BirthDate = "1990-01-01"
};

var updateResponse = await client.UpdatePersonAsync(updatedPerson);
Console.WriteLine(updateResponse.Message);  // نمایش پیام آپدیت

// 4. حذف شخص
var deleteResponse = await client.DeletePersonAsync(new PersonRequest { NationalCode = "1234567890" });
Console.WriteLine(deleteResponse.Message);  // نمایش پیام حذف شخص