using Highway.Data;
using System.Collections.Generic;
using System;

namespace Festify.DAL.Speakers
{
    public class SpeakerService
    {
        private readonly IRepository _repository;

        public SpeakerService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Speaker> GetAllSpeakers()
        {
            return _repository.Find(new AllSpeakers());
        }

        public Speaker AddSpeaker(string userName)
        {
            var speaker = _repository.Find(new SpeakerByUserName(userName));
            if (speaker == null)
            {
                speaker = _repository.Context.Add(Speaker.Create(userName));
            }
            return speaker;
        }

        public Speaker GetSpeakerByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
