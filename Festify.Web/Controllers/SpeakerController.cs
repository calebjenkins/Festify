using Festify.DAL;
using Festify.DAL.Speakers;
using Festify.Representations;
using Highway.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Festify.Web.Controllers
{
    public static class SpeakerControllerHelper
    {
        public static string GetSpeaker(this UrlHelper url, string userName)
        {
            return url.Link("GetSpeaker", new { userName = userName });
        }
    }

    [RoutePrefix("api/speakers")]
    public class SpeakerController : ApiController
    {
        private readonly SpeakerService _speakerService;
        private readonly IUnitOfWork _unitOfWork;

        public SpeakerController()
        {
            string connectionString = "DefaultConnection";
            IMappingConfiguration mapping = new FestifyMappingConfiguration();
            IDataContext context = new DataContext(connectionString, mapping);
            IRepository repository = new Repository(context);
            _unitOfWork = context;
            _speakerService = new SpeakerService(repository);
        }

        [Route(Name = "GetSpeakers")]
        public IHttpActionResult GetSpeakers()
        {
            var speakers = _speakerService.GetAllSpeakers();
            return Ok(speakers.Select(CreateRepresentation));
        }

        [Route("{userName}", Name = "GetSpeaker")]
        public IHttpActionResult GetSpeaker(string userName)
        {
            var speaker = _speakerService.GetSpeakerByUserName(userName);
            if (speaker == null)
                return NotFound();
            else
                return Ok(CreateRepresentation(speaker));
        }

        [Route(Name = "CreateSpeaker")]
        public IHttpActionResult CreateSpeaker(SpeakerRepresentation representation)
        {
            var speaker = _speakerService.AddSpeaker(representation.userName);
            var created = CreateRepresentation(speaker);
            _unitOfWork.Commit();
            return Created(
                created._links["self"].href,
                created);
        }

        private SpeakerRepresentation CreateRepresentation(Speaker speaker)
        {
            var representation = SpeakerRepresentation.FromEntity(speaker);
            representation._links = new Dictionary<string, LinkReference>
            {
                ["self"] = new LinkReference
                {
                    href = Url.GetSpeaker(speaker.UserName)
                }
            };

            return representation;
        }
    }
}
