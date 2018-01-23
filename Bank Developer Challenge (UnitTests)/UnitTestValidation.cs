using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantBank_Developer_Test;

namespace UnitTests
{
    [TestClass]
    public class UnitTestValidation
    {
        [TestMethod]
        public void Test_Validation_1_PinLengthCheck_For_True()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            bool expectedResult = true;

            // Act
            bool actualResult = ValidationHelper.PinLengthCheck(36350);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_1_PinLengthCheck_For_False()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "0000", "6519", "6433", "3635");
            bool expectedResult = false;

            // Act
            bool actualResult = ValidationHelper.PinLengthCheck(3645);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_2_ConsecutiveNumberCheck_For_True()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "0000", "6519", "6433", "3635");
            bool expectedResult = true;

            // Act
            bool actualResult = ValidationHelper.ConsecutiveNumberCheck(2000);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_2_ConsecutiveNumberCheck_For_False()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "0000", "6519", "6433", "3635");
            bool expectedResult = false;

            // Act
            bool actualResult = ValidationHelper.ConsecutiveNumberCheck(2200);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_3_SequenceCheck_For_True()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "0000", "6519", "6433", "3635");
            bool expectedResult = true;

            // Act
            bool actualResult = ValidationHelper.SequenceCheck(1234);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_3_SequenceCheck_For_False()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "0000", "6519", "6433", "3635");
            bool expectedResult = false;

            // Act
            bool actualResult = ValidationHelper.SequenceCheck(1544);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_4_PastPinCheck_For_True()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "0000", "6519", "6433", "3635");
            bool expectedResult = true;

            // Act
            bool actualResult = ValidationHelper.PastPinCheck(customer, 3635);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_4_PastPinCheck_For_False()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "0000", "6519", "6433", "6345");
            bool expectedResult = false;

            // Act
            bool actualResult = ValidationHelper.PastPinCheck(customer, 3635);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_5_SortCodeContainCheck_For_True()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "188221", "0000", "6519", "6433", "3635");
            bool expectedResult = true;

            // Act
            bool actualResult = ValidationHelper.SortCodeContainCheck(customer, 8822);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_5_SortCodeContainCheck_For_False()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "188221", "0000", "6519", "6433", "3635");
            bool expectedResult = false;

            // Act
            bool actualResult = ValidationHelper.SortCodeContainCheck(customer, 8821);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_6_AccountNumberContainCheck_For_True()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "18822132", "6519", "6433", "3635");
            bool expectedResult = true;

            // Act
            bool actualResult = ValidationHelper.AccountNumberContainCheck(customer, 8822);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_6_AccountNumberContainCheck_For_False()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "18822132", "6519", "6433", "3635");
            bool expectedResult = false;

            // Act
            bool actualResult = ValidationHelper.AccountNumberContainCheck(customer, 8821);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_7_BeginWithOneCheck_For_True()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "18822132", "6519", "6433", "3635");
            bool expectedResult = true;

            // Act
            bool actualResult = ValidationHelper.BeginWithOneCheck(1001);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Test_Validation_7_BeginWithOneCheck_For_False()
        {
            // Assemble
            ValidationHelper ValidationHelper = new ValidationHelper();
            Customer customer = new Customer("test", "test", "test", "0000", "18822132", "6519", "6433", "3635");
            bool expectedResult = false;

            // Act
            bool actualResult = ValidationHelper.BeginWithOneCheck(2001);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


    }
}
