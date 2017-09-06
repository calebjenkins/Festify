using Highway.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
