using CarAPI.Entities;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Day2_unitTest
{
    public class OwnerRepoTest
    {
        Mock<FactoryContext> factoryContxetMock;
        OwnerRepository ownerRepository;
        public OwnerRepoTest()
        {
            factoryContxetMock = new();
            factoryContxetMock.Setup(c => c.Owners).Returns(Mock.Of<DbSet<Owner>>());
            ownerRepository = new(factoryContxetMock.Object);
        }
        [Fact]
        [Trait("Author", "Mohammed Samy")]
        [Trait("priority", "2")]
        public void testOwner_AddFunctionTest_Sucess() {

            //Arrange
            Owner owner = new() { Id = 10, Name = "Hamada" };
            //Act
            bool result = ownerRepository.AddOwner(owner);
            //Assert
            Assert.True(result);

        }
        [Fact]
        [Trait("Author", "Mohammed Samy")]
        [Trait("priority", "2")]
        public void TestOwner_GetAllOwners_Sucess() {

            List<Owner> owners = new() { 
            new Owner { Id = 10,Name="Hamada"},
            new Owner { Id = 20,Name="Hamoda"},
            new Owner { Id = 30,Name="Hamo"},
            };
            factoryContxetMock.Setup(o => o.Owners).ReturnsDbSet(owners);
            List<Owner> result = ownerRepository.GetAllOwners();
            Assert.NotNull(result);
        }

    }
}
