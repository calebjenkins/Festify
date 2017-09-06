using Highway.Data;

namespace Festify.DAL.Speakers
{
    public class AllSpeakers : Query<Speaker>
    {
        public AllSpeakers()
        {
            ContextQuery = c => c.AsQueryable<Speaker>();
        }
    }
}
