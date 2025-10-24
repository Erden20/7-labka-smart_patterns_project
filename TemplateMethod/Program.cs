using System;

namespace TemplateMethod
{
    abstract class DataProcessor
    {
       
        public void ProcessData()
        {
            LoadData();
            Process();
            SaveData();
        }

        protected abstract void LoadData();
        protected abstract void Process();
        protected abstract void SaveData();
    }

    class ExcelProcessor : DataProcessor
    {
        protected override void LoadData() => Console.WriteLine("Загрузка данных из Excel...");
        protected override void Process() => Console.WriteLine("Обработка данных Excel...");
        protected override void SaveData() => Console.WriteLine("Сохранение отчёта Excel...");
    }

    class CSVProcessor : DataProcessor
    {
        protected override void LoadData() => Console.WriteLine("Загрузка данных из CSV...");
        protected override void Process() => Console.WriteLine("Обработка данных CSV...");
        protected override void SaveData() => Console.WriteLine("Сохранение отчёта CSV...");
    }

    class Program
    {
        static void Main()
        {
            DataProcessor excel = new ExcelProcessor();
            excel.ProcessData();

            Console.WriteLine();

            DataProcessor csv = new CSVProcessor();
            csv.ProcessData();
        }
    }
}
