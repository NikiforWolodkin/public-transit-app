using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly IMongoCollection<RouteTimetable> _timetablesCollection;

        public TimetableRepository(IOptions<TimetableDbSettings> timetableDbSettings)
        {
            var mongoClient = new MongoClient(timetableDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(timetableDbSettings.Value.DatabaseName);

            _timetablesCollection = mongoDatabase.GetCollection<RouteTimetable>(
                timetableDbSettings.Value.BooksCollectionName);
        }

        void ITimetableRepository.Add(RouteTimetable timetable)
        {
            _timetablesCollection.InsertOne(timetable);
        }

        async Task<List<RouteTimetable>> ITimetableRepository.GetAllAsync()
        {
            return await _timetablesCollection.Find(_ => true).ToListAsync();
        }

        async Task<RouteTimetable> ITimetableRepository.GetByRouteIdAsync(Guid id)
        {
            return await _timetablesCollection.Find(table => table.RouteId == id).FirstAsync();
        }

        void ITimetableRepository.Remove(RouteTimetable timetable)
        {
            _timetablesCollection.DeleteOneAsync(table => table.Id == timetable.Id);
        }

        async Task ITimetableRepository.UpdateAsync(RouteTimetable timetable)
        {
            await _timetablesCollection.ReplaceOneAsync(table => table.Id == timetable.Id, timetable);
        }
    }
}
