using Festify.DAL.Speakers;
using Highway.Data;
using Highway.Data.Contexts;
using System.Collections.Generic;

namespace Festify.UnitTest.Speakers
{
    public class SpeakerTestContext
    {
        private readonly SpeakerService _speakerService;
        private readonly IUnitOfWork _unitOfWork;

        public SpeakerTestContext()
        {
            var context = new InMemoryDataContext();
            var repository = new Repository(context);
            _speakerService = new SpeakerService(repository);
            _unitOfWork = context;
        }

        public IEnumerable<Speaker> WhenGetAllSpeakers()
        {
            return _speakerService.GetAllSpeakers();
        }

        public void WhenAddSpeaker(string userName)
        {
            _speakerService.AddSpeaker(userName);
            _unitOfWork.Commit();
        }

        public static SpeakerTestContext GivenSpeakerService()
        {
            return new SpeakerTestContext();
        }
    }
}
