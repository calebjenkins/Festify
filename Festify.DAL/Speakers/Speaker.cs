using Highway.Data;

namespace Festify.DAL.Speakers
{
    public class Speaker : IIdentifiable<int>
    {
        public Speaker(string userName)
        {
            UserName = userName;
        }

        int IIdentifiable<int>.Id { get => SpeakerId; set => SpeakerId = value; }

        public int SpeakerId { get; private set; }
        public string UserName { get; private set; }
    }
}
