using Festify.DAL.Speakers;
using System.Collections.Generic;

namespace Festify.Representations
{
    public class SpeakerRepresentation
    {
        private SpeakerRepresentation()
        {
        }

        public string userName { get; set; }
        public Dictionary<string, LinkReference> _links { get; set; }

        public static SpeakerRepresentation FromEntity(Speaker speaker)
        {
            return new SpeakerRepresentation
            {
                userName = speaker.UserName
            };
        }
    }
}
