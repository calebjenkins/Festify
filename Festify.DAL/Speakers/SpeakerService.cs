using Highway.Data;

namespace Festify.DAL.Speakers
{
    public class SpeakerService
    {
        private readonly IRepository _repository;

        public SpeakerService(IRepository repository)
        {
            _repository = repository;
        }

        public Speaker AddSpeaker(string userName)
        {
            var speaker = _repository.Find(new SpeakerByUserName(userName));
            if (speaker == null)
            {
                speaker = _repository.Context.Add(new Speaker(userName));
            }
            return speaker;
        }
    }
}
