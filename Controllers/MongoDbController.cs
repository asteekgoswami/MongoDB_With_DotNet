using DotNet_With_MongoDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotNet_With_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoDbController : ControllerBase
    {
        private readonly IMongoCollection<Student> studentCollection;
        private readonly IMongoCollection<AIModel> aiModelCollection;

        public MongoDbController(IOptions<MongoDb> mongodb)
        {
            IMongoClient client = new MongoClient(mongodb.Value.ConnectionString);
            var database = client.GetDatabase(mongodb.Value.Database);
            studentCollection = database.GetCollection<Student>(mongodb.Value.Student);
            aiModelCollection = database.GetCollection<AIModel>("AIModel");
        }

        [HttpGet]
        public async Task<IActionResult> Hey()
        {
            return Ok("Hello From API");
        }

        [HttpGet("GetStudent")]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await studentCollection.Find(_ => true).ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            student.Id = ObjectId.GenerateNewId().ToString();
            await studentCollection.InsertOneAsync(student);
            return Ok(student);
        }

        [HttpPost("AddAiModel")]
        public async Task<IActionResult> AddModel(AIModel model)
        {
            model.Id = ObjectId.GenerateNewId().ToString();
            await aiModelCollection.InsertOneAsync(model);
            return Ok(model);
        }


        [HttpGet("GetAiModels")]
        public async Task<IActionResult> GetAIModel()
        {
            return Ok(await aiModelCollection.Find(_=>true).ToListAsync());
        }

    }
}
