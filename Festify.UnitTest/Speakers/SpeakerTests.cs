using Festify.DAL.Speakers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Festify.UnitTest.Speakers
{
    public class SpeakerTests
    {
        [Fact]
        public void InitiallyNoSpeakers()
        {
            var context = SpeakerTestContext.GivenSpeakerRepository();
            var speakers = context.WhenGetAllSpeakers();
            speakers.Count().Should().Be(0);
        }
    }
}
