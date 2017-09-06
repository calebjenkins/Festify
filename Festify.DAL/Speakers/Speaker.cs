using Highway.Data;

namespace Festify.DAL.Speakers
{
    public class Speaker : IIdentifiable<int>
    {
        private Speaker() { }

        int IIdentifiable<int>.Id { get => SpeakerId; set => SpeakerId = value; }

        public int SpeakerId { get; private set; }
        public string UserName { get; private set; }

        public static Speaker Create(string userName)
        {
            return new Speaker
            {
                UserName = userName
            };
        }
    }
}
