using Bogus;
using Bogus.DataSets;
using System;
using Xunit;
using TestProject1.Models;

namespace TestProject1
{
    public class Calc
    {
        private int x;
        private int y;

        public Calc(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int add()
        {
            return x + y;
        }

        public int mult()
        {
            return y * x;
        }

        public int div()
        {
            try
            {
                return x / y;
            }
            catch (DivideByZeroException)
            {
                throw new DivideByZeroException("0 division");
            }
        }

        public int minus()
        {
            return x - y;
        }

    }
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Faker<UserAddress> addressBogus = new Faker<UserAddress>();

            addressBogus.RuleFor(x => x.Description, y => y.Address.FullAddress());
            addressBogus.RuleFor(x => x.Number, y => y.Address.CountryCode());

            var personBogus = new Faker<Models.Person>();

            personBogus.RuleFor(x => x.Name, y => y.Person.FirstName);
            personBogus.RuleFor(x => x.Surname, y => y.Person.LastName);
            personBogus.RuleFor(x => x.Birthday, y => y.Person.DateOfBirth);
            personBogus.RuleFor(x => x.Address, y => addressBogus.Generate());

            var hundredPpl = personBogus.Generate(100);
        }

        [Theory]
        [InlineData(3, 5, 8)]
        public void add_two_numbers(int x, int y, int res)
        {
            var calc = new Calc(x, y);

            Assert.Equal(calc.add(), res);
        }

        [Theory]
        [InlineData(3, 6, -3)]
        public void minus_two_numbers(int x, int y, int res)
        {
            var calc = new Calc(x, y);

            Assert.Equal(calc.minus(), res);

        }

        [Theory]
        [InlineData(5, 0, int.MaxValue)]
        public void divide_two_numbers(int x, int y, int res)
        {
            var calc = new Calc(x, y);
            Assert.Throws<DivideByZeroException>(() => calc.div());
        }
    }
}
