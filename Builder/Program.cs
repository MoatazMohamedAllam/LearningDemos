using System;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            Report report;

            ReportDirector reportDirector = new ReportDirector();

            PDFReport pDFReport = new PDFReport();

            ExcelReport excelReport = new ExcelReport();

            report = reportDirector.MakeReport(pDFReport);
            report.DisplayReport();

            Console.WriteLine("-------------------");

            report = reportDirector.MakeReport(excelReport);
            report.DisplayReport();

        }
    }
}
