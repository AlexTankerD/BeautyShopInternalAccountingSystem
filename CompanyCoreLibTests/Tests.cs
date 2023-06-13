using CompanyCoreLib;

namespace CompanyCoreLibTests
{
    [TestClass()]
    public class AnalyticsTest
    {
        private Analytics classAnalytics = new Analytics();

        #region Easy Test Methods
        [TestMethod()]
        public void PopularMonths_Easy_01()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_01.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_01.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_02()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_02.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_02.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_03()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_03.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_03.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_04()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_04.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_04.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_05()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_05.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_05.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_06()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_06.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_06.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_07()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_07.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_07.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_08()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_08.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_08.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_09()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_09.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_09.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_10()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_10.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_10.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_11()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_11.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_11.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_12()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_12.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_12.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_13()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_13.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_13.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_14()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_14.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_14.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_15()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_15.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_15.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_16()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_16.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_16.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_17()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_17.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_17.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_18()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_18.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_18.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_19()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_19.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_19.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Easy_20()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Easy_20.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Easy_20.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }
        #endregion

        #region Hard Test Methods
        [TestMethod()]
        public void PopularMonths_Hard_01()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Hard_01.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Hard_01.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Hard_02()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Hard_02.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Hard_02.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Hard_03()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Hard_03.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Hard_03.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Hard_04()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Hard_04.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Hard_04.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Hard_05()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Hard_05.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Hard_05.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Hard_06()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Hard_06.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Hard_06.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Hard_07()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Hard_07.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Hard_07.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Hard_08()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Hard_08.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Hard_08.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Hard_09()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Hard_09.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Hard_09.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }

        [TestMethod()]
        public void PopularMonths_Hard_10()
        {
            // Arrange
            List<DateTime> listDates = ReadTestingData("InputData_Hard_10.txt");
            List<DateTime> expected = ReadTestingData("OutputData_Hard_10.txt");

            // Act
            List<DateTime> actual = classAnalytics.PopularMonths(listDates);

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(expected, actual));
        }
        #endregion

        private List<DateTime> ReadTestingData(string fileName)
        {
            string path = "..\\..\\..\\..\\TestingData\\" + fileName;
            string[] data = File.ReadAllLines(path);
            List<DateTime> listData = data.Select(x => DateTime.Parse(x)).ToList();
            return listData;
        }
    }
}