using Highway.Data;
using System.Linq;

namespace Festify.DAL.Speakers
{
    public class SpeakerByUserName : Scalar<Speaker>
    {
        public SpeakerByUserName(string userName)
        {
            ContextQuery = c => c.AsQueryable<Speaker>()
                .Where(x => x.UserName == userName)
                .SingleOrDefault();
        }
    }
}
