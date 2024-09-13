using Spire.Doc;

public static class ConvertPDF
{
    public static void Convert(string fileName)
    {
        try
        {
            var document = new Document();
            document.LoadFromFile(fileName);

            string filePdf = Path.ChangeExtension(fileName, ".pdf");
            document.SaveToFile(filePdf, FileFormat.PDF);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"""
                                Erro ao converter para PDF.
                                Message: {exception.Message} 
                                StackTrace: {exception.StackTrace}
                            """);
        }
    }
}