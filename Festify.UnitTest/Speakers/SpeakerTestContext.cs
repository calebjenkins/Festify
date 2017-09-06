using Festify.DAL.Speakers;
using Highway.Data;
using Highway.Data.Contexts;
using System;
using System.Collections.Generic;

namespace Festify.UnitTest.Speakers
{
    public class SpeakerTestContext
    {
        private readonly InMemoryDataContext _context;
        private readonly Repository _repository;

        public SpeakerTestContext()
        {
            _context = new InMemoryDataContext();
            _repository = new Repository(_context);
        }

        public IEnumerable<Speaker> WhenGetAllSpeakers()
        {
            return _repository.Find(new AllSpeakers());
        }

        public static SpeakerTestContext GivenSpeakerRepository()
        {
            return new SpeakerTestContext();
        }
    }
}
