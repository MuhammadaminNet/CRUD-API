using FluentAssertions;
using Force.DeepCloner;
using src.Data.IRepositories;
using src.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yanabitta.Test.Unit.Services
{
    public partial class UserServiceTest
    {
        [Fact]
        public async ValueTask ShouldCreateUser()
        {
            // given
            var randomUser = CreateRandomUser();
            var inputUser = randomUser;
            var expectedUser = inputUser.DeepClone();

            // when
            user = await userService.CreateAsync(inputUser);

            // then
            user.Should().NotBeNull();

            user.Name.Should().BeEquivalentTo(expectedUser.Name);
        }
    }
}
