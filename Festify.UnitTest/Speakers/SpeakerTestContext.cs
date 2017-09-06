using Festify.DAL.Speakers;
using Highway.Data;
using Highway.Data.Contexts;
using System.Collections.Generic;

namespace Festify.UnitTest.Speakers
{
    public class SpeakerTestContext
    {
        private readonly InMemoryDataContext _context;
        private readonly Repository _repository;
        private readonly SpeakerService _speakerService;

        public SpeakerTestContext()
        {
            _context = new InMemoryDataContext();
            _repository = new Repository(_context);
            _speakerService = new SpeakerService(_repository);
        }

        public IEnumerable<Speaker> WhenGetAllSpeakers()
        {
            return _repository.Find(new AllSpeakers());
        }

        public void WhenAddSpeaker(string userName)
        {
            _speakerService.AddSpeaker(userName);
            _context.Commit();
        }

        public static SpeakerTestContext GivenSpeakerRepository()
        {
            return new SpeakerTestContext();
        }
    }
}
