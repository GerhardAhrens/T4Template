//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Lifeprojects.de">
//     Class: Program
//     Copyright © Lifeprojects.de 2023
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>12.02.2023 09:18:06</date>
//
// <summary>
// Konsolen Applikation
// </summary>
//-----------------------------------------------------------------------

namespace T4Template
{
    using System;
    using System.Data;

    public class Program
    {
        private readonly static string[] SqlServerTypes = { "bigint", "binary", "bit", "char", "date", "datetime", "datetime2", "datetimeoffset", "decimal", "filestream", "float", "geography", "geometry", "hierarchyid", "image", "int", "money", "nchar", "ntext", "numeric", "nvarchar", "real", "rowversion", "smalldatetime", "smallint", "smallmoney", "sql_variant", "text", "time", "timestamp", "tinyint", "uniqueidentifier", "varbinary", "varchar", "xml" };
        private readonly static string[] CSharpTypes = { "long", "byte[]", "bool", "char", "DateTime", "DateTime", "DateTime", "DateTimeOffset", "decimal", "byte[]", "double", "Microsoft.SqlServer.Types.SqlGeography", "Microsoft.SqlServer.Types.SqlGeometry", "Microsoft.SqlServer.Types.SqlHierarchyId", "byte[]", "int", "decimal", "string", "string", "decimal", "string", "Single", "byte[]", "DateTime", "short", "decimal", "object", "string", "TimeSpan", "byte[]", "byte", "Guid", "byte[]", "string", "string" };


        private static void Main(string[] args)
        {
            string importFile = @"e:\PTA2890\_Development\Sourcen\T4TemplateExamples\T4Template\T4Template\InputFile\DemoModel.csv";
            var bb = Convert.ToInt32("");
            var aa = ImportCSV(importFile, new string[] {";" });
        }

        private static DataTable ImportCSV(string fullPath, string[] sepString)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(fullPath))
            {
                //stream uses using statement because it implements iDisposable
                string firstLine = sr.ReadLine();
                var headers = firstLine.Split(sepString, StringSplitOptions.None);
                foreach (var header in headers)
                {
                    dt.Columns.Add(header);
                }

                int columnInterval = headers.Count();
                string newLine = sr.ReadLine();
                while (newLine != null)
                {
                    var fields = newLine.Split(sepString, StringSplitOptions.None);
                    var currentLength = fields.Count();
                    if (currentLength < columnInterval)
                    {
                        while (currentLength < columnInterval)
                        {
                            newLine += sr.ReadLine();
                            currentLength = newLine.Split(sepString, StringSplitOptions.None).Count();
                        }
                        fields = newLine.Split(sepString, StringSplitOptions.None);
                    }

                    if (currentLength > columnInterval)
                    {
                        newLine = sr.ReadLine();
                        continue;
                    }

                    dt.Rows.Add(fields);
                    newLine = sr.ReadLine();
                }
                sr.Close();
            }

            return dt;
        }

        private static string ConvertSqlServerFormatToCSharp(string typeName)
        {
            var index = Array.IndexOf(SqlServerTypes, typeName);
            return index > -1 ? CSharpTypes[index] : "object";
        }

        private static string ConvertCSharpFormatToSqlServer(string typeName)
        {
            var index = Array.IndexOf(CSharpTypes, typeName);
            return index > -1 ? SqlServerTypes[index] : "varchar(50)";
        }
    }
}
