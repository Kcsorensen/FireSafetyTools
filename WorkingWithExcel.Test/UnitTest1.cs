using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkingWithExcel.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Excel.Application xlApp = new Excel.Application();

            //if (xlApp == null)
            //{
            //    throw new NullReferenceException("Excel is not properly installed!");
            //}

            //Excel.Workbook xlWorkBook;
            //Excel.Worksheet xlWorkSheet;
            //object misValue = System.Reflection.Missing.Value;

            //xlWorkBook = xlApp.Workbooks.Add(misValue);
            //xlWorkSheet = (Excel.Worksheet) xlWorkBook.Worksheets.get_Item(1);

            //xlWorkSheet.Cells[1, 1] = "ID";
            //xlWorkSheet.Cells[1, 2] = "Name";
            //xlWorkSheet.Cells[2, 1] = "1";
            //xlWorkSheet.Cells[2, 2] = "One";
            //xlWorkSheet.Cells[3, 1] = "2";
            //xlWorkSheet.Cells[3, 2] = "Two";

            //xlWorkBook.SaveAs("d:\\csharp-Excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //xlWorkBook.Close(true, misValue, misValue);
            //xlApp.Quit();

            //Assert.IsTrue(File.Exists("d:\\csharp-Excel.xls"));
        }
    }
}
