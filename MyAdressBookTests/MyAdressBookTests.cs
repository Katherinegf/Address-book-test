using AddressBook.Services;
using MyAddressBook;
using MyAddressBook.Readers;

namespace MyAdressBookTests
{
    [TestClass]
    public class MyAdressBookTests
    {
        [TestMethod]
        public void TestEmptyListing()
        {
            var testReader = new TestConsoleReader();
            MenuServices menuService = new(testReader, true);

            testReader.EnqueueInput("2");
            //testReader.EnqueueInput("3");

            menuService.WelcomeMenu();

            var output = testReader.InfoOutputLines.ToArray();

            Assert.AreEqual(1, output.Length);
            Assert.AreEqual(output[0], "Adressboken är tom");
        }        

    }
}