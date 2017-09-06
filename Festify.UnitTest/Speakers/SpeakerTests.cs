using FluentAssertions;
using System.Linq;
using Xunit;

namespace Festify.UnitTest.Speakers
{
    public class SpeakerTests
    {
        [Fact]
        public void InitiallyNoSpeakers()
        {
            var context = SpeakerTestContext.GivenSpeakerService();
            var speakers = context.WhenGetAllSpeakers();
            speakers.Count().Should().Be(0);
        }

        [Fact]
        public void WhenAddSpeaker_ThenOneSpeaker()
        {
            var context = SpeakerTestContext.GivenSpeakerService();
            context.WhenAddSpeaker(userName: "michaellperry");
            var speakers = context.WhenGetAllSpeakers();
            speakers.Count().Should().Be(1);
        }

        [Fact]
        public void WhenAddSpeakerTwice_ThenStillOneSpeaker()
        {
            var context = SpeakerTestContext.GivenSpeakerService();
            context.WhenAddSpeaker(userName: "michaellperry");
            context.WhenAddSpeaker(userName: "michaellperry");
            var speakers = context.WhenGetAllSpeakers();
            speakers.Count().Should().Be(1);
        }
    }
}
